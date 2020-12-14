using Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection;
using System.Diagnostics;
using Common.Helpers;

namespace Data
{
    public static class DataHelper
    {
        public static void ExecuteRemoteFile(string sql)
        {
            const string fileName = "c:\\share\\batch.sql";

            LogHelper.LogCustom(sql, true, fileName);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "sqlcmd",
                    Arguments = $"-S 192.168.10.167 -d carwizztest -U sa -P Password1 -i {fileName}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }

        public static void ExecuteSqlSplitLines(char splitChar, string sql, List<SqlParameter> parameters = null, bool isSqlQuery = true)
        {
            var strs = sql.Split(splitChar);
            var sb = new StringBuilder();
            var x = 0;

            foreach (var str in strs)
            {
                x++;
                sb.Append(str);

                if (x == 5 * 1000)
                {
                    SimpleRetry(3, () => ExecuteSql<int>(sb.ToString(), parameters, isSqlQuery));
                    //ExecuteSQL<int>(sb.ToString(), parameters, isSqlQuery);
                    sb = new StringBuilder();
                    x = 0;
                }
            }

            if (sb.Length > 0)
                SimpleRetry(3, () => ExecuteSql<int>(sb.ToString(), parameters, isSqlQuery));
        }

        public static void SimpleRetry<T>(int retryCount, Func<T> function)
        {
            var x = 0;

            while (x < retryCount)
            {
                try
                {
                    function();
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(3000);
                }

                x++;
            }
        }

        public static void ExecuteSql(string sql, List<SqlParameter> parameters = null, bool isSqlQuery = true)
        {
            var result = ExecuteSql<int>(sql, parameters, isSqlQuery);
        }

        public static List<T> ExecuteSql<T>(string sql, List<SqlParameter> parameters = null, bool isSqlQuery = true) 
            where T : new()
        {
            return ExecuteNewSqlWithConnStr<T>(sql, parameters, isSqlQuery, null);
        }

        public static void ExecuteNewSqlWithConnStr(string sql, List<SqlParameter> parameters = null, bool isSqlQuery = true, string consString = null)
        {
            var result = ExecuteNewSqlWithConnStr<int>(sql, parameters, isSqlQuery, consString, true);
        }

        public static List<T> ExecuteNewSqlWithConnStr<T>(string sql, List<SqlParameter> parameters = null, bool isSqlQuery = true, string connString = null, bool skipResult = false) 
            where T : new()
        {
            if (connString == null)
                connString = HttpHelper.ConnStr;

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 600 * 15;
                    cmd.CommandType = isSqlQuery ? CommandType.Text : CommandType.StoredProcedure;

                    if (parameters != null)
                        foreach (var parameter in parameters)
                        {
                            parameter.Value = parameter.Value ?? DBNull.Value;
                            cmd.Parameters.Add(parameter);
                        }

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        if (!dataReader.HasRows || skipResult)
                            return null;

                        var test = DataReaderMapToList<T>(dataReader);

                        return test;
                    }
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, List<SqlParameter> parameters = null, bool isSqlQuery = true)
        {
            var dt = new DataTable();
            var connString = HttpHelper.ConnStr;

            using (var conn = new SqlConnection(connString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    command.CommandTimeout = 60 * 15;
                    command.CommandType = isSqlQuery ? CommandType.Text : CommandType.StoredProcedure;

                    if (parameters != null)
                        foreach (var param in parameters)
                            command.Parameters.Add(param);

                    var adp = new SqlDataAdapter(command);

                    try
                    {
                        adp.Fill(dt);
                    }
                    catch
                    {
                        //Ignore
                    }

                    return dt;
                }
            }
        }

        public static DataTable BulkInsertDto<TSource>(IEnumerable<TSource> source, string tableName)
        {
            var types = new List<Type> { typeof(short), typeof(int), typeof(decimal), typeof(bool), typeof(DateTime),
                typeof(short?), typeof(int?), typeof(decimal?), typeof(bool?), typeof(DateTime?),typeof(string) };

            var dt = ExecuteDataTable("SELECT TOP 0 * FROM " + tableName, null, true);

            var props = typeof(TSource).GetProperties().Where(p => types.Contains(p.PropertyType));

            if (source != null)
            {
                source.ToList().ForEach(
                    i =>
                    {
                        var dr = dt.NewRow();

                        foreach (var propertyInfo in props)
                            dr[propertyInfo.Name] = propertyInfo.GetValue(i, null) ?? DBNull.Value;

                        dt.Rows.Add(dr);
                    }
                );
            }

            DataHelper.BulkCopy(dt, tableName);

            return dt;
        }

        private static void BulkCopy(DataTable dtNewLogs, string tableName)
        {
            //string connString = ConfigurationManager.ConnectionStrings[ActiveModulHelper.ConnectionStringConfigName].ConnectionString;
            var connString = HttpHelper.ConnStr;

            using (var con = new SqlConnection(connString))
            {
                using (var sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.BulkCopyTimeout = 1000 * 90;
                    sqlBulkCopy.DestinationTableName = tableName;

                    con.Open();
                    sqlBulkCopy.WriteToServer(dtNewLogs);
                    con.Close();
                }
            }
        }

        private static List<T> DataReaderMapToList<T>(DbDataReader dr)
        {
            var list = new List<T>();
            var obj1 = Activator.CreateInstance<T>();
            var props = obj1.GetType().GetProperties();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var obj = Activator.CreateInstance<T>();

                    if (props.Any())
                        foreach (var prop in props)
                        {
                            if (!Equals(dr[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, dr[prop.Name], null);
                            }
                        }
                    else
                        obj = (T)dr[0];

                    list.Add(obj);
                }

                return list;
            }

            return new List<T>();
        }

        public static List<T> BindList<T>(DataTable dt)
        {
            var props = typeof(T).GetProperties();
            var list = new List<T>();
            var rowindex = props.FirstOrDefault(p => p.Name == "RowIndex");
            var index = 0;

            foreach (DataRow dr in dt.Rows)
            {
                var ob = Activator.CreateInstance<T>();

                if (rowindex != null)
                    rowindex.SetValue(ob, index);

                foreach (var propinfo in props)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (propinfo.Name == dc.ColumnName)
                        {
                            var type = propinfo.PropertyType;
                            var value = GetValue(dr[dc.ColumnName], type);

                            propinfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                list.Add(ob);
                index++;
            }

            return list;
        }

        private static object GetValue(object ob, Type targetType)
        {
            if (targetType == null)
            {
                return null;
            }
            else if (targetType == typeof(string))
            {
                return ob + "";
            }
            else if (targetType == typeof(int))
            {
                int i = 0;
                int.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(short))
            {
                short i = 0;
                short.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(long))
            {
                long i = 0;
                long.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ushort))
            {
                ushort i = 0;
                ushort.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(uint))
            {
                uint i = 0;
                uint.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ulong))
            {
                ulong i = 0;
                ulong.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(double))
            {
                double i = 0;
                double.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(DateTime))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(bool))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(decimal))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(float))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(byte))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(sbyte))
            {
                // do the parsing here...
            }

            return ob;
        }
    }
}