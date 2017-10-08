using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business
{
    public class Response<T> : Response
    {
        public T Data { get; set; }

        public Response(int responseCode, string message = null, T data = default(T))
            : base(responseCode, message)
        {
            Data = data;
        }
    }

    public class Response
    {
        public int ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public Response(int responseCode, string message = null)
        {
            ResponseCode = responseCode;
            ResponseMessage = message;
        }
    }
}
