using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Common.Clients.Models
{
    public class WebApiResponse<T>
    {
        public WebApiResponse()
        {

        }
        public WebApiResponse(bool isSuccess, string resultMessage)
        {
            IsSuccess = isSuccess;
            ResultMessage = resultMessage;
        }
        public WebApiResponse(bool isSuccess, string resultMessage,T resultData)
        {
            IsSuccess = isSuccess;
            ResultMessage = resultMessage;
            ResulData = resultData;
        }
        public bool IsSuccess { get; set; }
        public string ResultMessage { get; set; }
        public IEnumerable<string> ValiationErrors { get; set; }
        public T ResulData { get; set; }
    }
}
