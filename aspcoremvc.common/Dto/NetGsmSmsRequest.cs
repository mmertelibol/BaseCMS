using System.Collections.Generic;

namespace Common.Dto
{
    public class NetGsmSmsRequest
    {
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Sms Başlığı
        /// </summary>
        public string MsgHeader { get; set; } 
        public string LangCode { get; set; } 
    }
}
