using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Responses
{
    public class BasicResponse<T> : BasicResponse
    {
        public BasicResponse():base()
        {

        }

        public T Object { get; set; }
    }
}
