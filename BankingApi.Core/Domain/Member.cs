using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Core.Domain
{
    public class Member : BaseEntity
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public int InstitutionId { get; set; }

        public Account[] Accounts { get; set; }
    }
}
