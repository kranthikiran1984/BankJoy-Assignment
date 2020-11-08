using BankingApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public class MemberCreatedEvent
    {
        public MemberCreatedEvent(Member member)
        {
            Member = member;
        }

        public Member Member { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedBy { get; set; }

    }
}
