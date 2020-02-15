using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MVCForum.Utilities
{
    public class HttpContentResult<T>
    {
        public bool Result { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string SysMessage { get; set; }
        public int TotalItem { get; set; }
        public T Data { get; set; }

    }
    public class HttpContentResultPaged<T>
    {
        [JsonProperty(PropertyName = "result")]
        public bool Result { get; set; }
        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "sysMessage")]
        public string SysMessage { get; set; }
        [JsonProperty(PropertyName = "totalItem")]
        public int TotalItem { get; set; }
        [JsonProperty(PropertyName = "totalPage")]
        public int TotalPage { get; set; }
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }

    }



    public class HttpResponse
    {
        public bool Success { get; set; }

        public string SysMessage { get; set; }

        public string Message { get; set; }

        public string StatusCode { get; set; }

        public object Data { get; set; }

        public object RequestBody { get; set; }

    }
    

}