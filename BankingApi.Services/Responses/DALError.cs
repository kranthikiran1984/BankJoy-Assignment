using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Responses
{
    public class DALError
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
