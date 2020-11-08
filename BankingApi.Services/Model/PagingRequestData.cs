using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Model
{
    public class PagingRequestData
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool CountOnly { get; set; }
    }
}
