using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace Common.Dto
{
     
    public class JsonResultDto
    { 
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "data")]
        public object DataObject { get; set; }

        [JsonProperty(PropertyName = "status")]
        public JsonResultStatus Status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }  
    }

    public enum JsonResultStatus
    {
        [JsonProperty(propertyName: "success")]
        Success = 1,
        [JsonProperty(propertyName: "error")]
        Error = 2,
        [JsonProperty(propertyName: "warning")]
        Warning = 3
    }
  
}
