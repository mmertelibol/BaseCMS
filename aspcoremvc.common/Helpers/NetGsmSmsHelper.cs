using Common.Dto;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Common.Helpers
{
    public class NetGsmSmsHelper
    {
        public static void SendSms(NetGsmSmsRequest smsRequest)
        {

            var conf = HttpHelper.GetConfig<string>("AppConfig:ActiveConnection");

            //Test ortaminda ise tüm smsler asagidaki numaraya gonderiliyor
            if (conf.ToLower() == "test")
            {
                smsRequest.Phone = "05322459801";
            }

            string phoneNumber = smsRequest.Phone;

            if (!string.IsNullOrEmpty(smsRequest.Phone) && !string.IsNullOrWhiteSpace(smsRequest.Phone))
            {
                phoneNumber = smsRequest.Phone.Replace(" ", "");
                if (phoneNumber.Length == 10 && phoneNumber.ToCharArray()[0].ToString() != "0")
                {
                    phoneNumber = "0" + phoneNumber;
                }
            }

            string parameters =
                $"?usercode={smsRequest.UserCode}&password={smsRequest.Password}&gsmno={phoneNumber}&message={smsRequest.Message}&msgheader={smsRequest.MsgHeader}&dil={smsRequest.LangCode}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.netgsm.com.tr/sms/send/get/");
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, parameters);

            client.SendAsync(request)
                .ContinueWith(responseTask => { Console.WriteLine("Response: {0}", responseTask.Result); });
        }

        public static int GetSMSCredit()
        {
            int credit = 0;
            string parameters =
                $"?usercode={HttpHelper.GetConfig<string>("NetGsmSmsSettings:UserCode")}&password={HttpHelper.GetConfig<string>("NetGsmSmsSettings:Password")}&tip=1";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.netgsm.com.tr/balance/list/get/");

            var response = client.GetAsync(parameters).Result;

            if (response.IsSuccessStatusCode)
            {
                var creditResponse = response.Content.ReadAsStringAsync().Result.ToString();

                if (creditResponse != "100")
                {
                    string[] creditResponseArray = creditResponse.Split("|");
                    Int32.TryParse(creditResponseArray[0], out credit);
                    return credit;
                }
            }
            return -1;
        }
    }
}
