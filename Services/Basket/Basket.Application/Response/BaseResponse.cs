using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Response
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }

        public BaseResponse(T data, bool success, string message, string error)
        {
            Data = data;
            Success = success;
            Message = message;
            Error = error;
        }
    }
}
