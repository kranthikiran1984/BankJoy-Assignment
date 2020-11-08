using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApi.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IJsonRepository<Account> _accountRepository;
        private readonly IJsonRepository<Member> _memberRepository;

        public AccountService(IJsonRepository<Account> accountRepository, IJsonRepository<Member> memberRepository)
        {
            _accountRepository = accountRepository;
            _memberRepository = memberRepository;
        }

        public BasicResponse<Account> GetAccountById(int accountId, int memberId)
        {
            var response = new BasicResponse<Account>();

            var member = _memberRepository.GetById(memberId);
            if(member != null && member.Accounts.Any())
            {
                response.Object = member.Accounts.FirstOrDefault(x => x.Id == accountId);
                response.WasSuccessful = response.Object != null;
            }

            return response;
        }
    }
}
