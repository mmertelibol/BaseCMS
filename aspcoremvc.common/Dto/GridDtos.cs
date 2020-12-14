using System;
using System.Collections.Generic;

namespace Common.Dto.DataTablesGrid
{
    public class DataTableAjaxPostModel
    {
        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public List<Column> columns { get; set; }

        public Search search { get; set; }

        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }

        public string name { get; set; }

        public bool searchable { get; set; }

        public bool orderable { get; set; }

        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }

        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }

        public string dir { get; set; }
    }

    public class DataTableAjaxResponse
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public dynamic data { get; set; }
    }

    public class GridRowBase
    {
        public string DT_RowId { get; set; }

        public string DT_RowClass { get; set; }
    }

    public class GridRowBaseKeyed : IKeyedObject
    {
        public int Id { get; set; }

        [IgnoreExcel]
        public string DT_RowId { get; set; }

        [IgnoreExcel]
        public string DT_RowClass { get; set; }

        [IgnoreExcel]
        public bool IsMultipleSearch { get; set; }
    }

}