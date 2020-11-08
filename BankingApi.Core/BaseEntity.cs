using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Core
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string DbTableName { get; set; }
    }
}
