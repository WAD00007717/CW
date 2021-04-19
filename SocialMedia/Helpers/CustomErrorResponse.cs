using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApi.Helpers
{
    public class CustomErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public int Status { get; set; }


        public CustomErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            Status = 500;
            StackTrace = ex.ToString();
        }
    }
}
