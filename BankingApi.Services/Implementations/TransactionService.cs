using BankingApi.Core;
using BankingApi.Core.Domain;
using BankingApi.Services.AccountRules;
using BankingApi.Services.Contracts;
using BankingApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;
        private readonly IMemberService _memberService;

        public TransactionService(IAccountService accountService, IMemberService memberService)
        {
            _accountService = accountService;
            _memberService = memberService;
        }

        public async Task<bool> ProcessTransaction(TransactionData trasanctionData)
        {
            switch (trasanctionData.Command)
            {
                case Transaction.WITHDRAW:
                    return await Withdraw(trasanctionData);
                    
                case Transaction.DEPOSIT:
                    return await Deposit(trasanctionData);

                case Transaction.TRANSFER:
                    return await Deposit(trasanctionData);
                default:
                    break;
            }

            return true;
        }

        private async Task<bool> Withdraw(TransactionData trasanctionData)
        {
            var member = _memberService.GetMemberById(trasanctionData.MemberId)?.Object;

            if(member != null && member.Accounts != null && member.Accounts.Any())
            {
                var account = member.Accounts.FirstOrDefault(x => x.Id == trasanctionData.AccountId);

                if (account != null)
                {
                    var withDrawRules = new List<IAccountRule>();
                    withDrawRules.Add(new HasEnoughBalance());

                    var withDrawRulesResult = true;

                    foreach(var rule in withDrawRules)
                    {
                        withDrawRulesResult = withDrawRulesResult && rule.PassRule(account, trasanctionData);
                    }

                    if (withDrawRulesResult)
                    {
                        account.Balance -= trasanctionData.Amount;
                        await _memberService.UpdateMember(member);
                        return true;
                    }
                }
            }

            return false;
        }

        private async Task<bool> Deposit(TransactionData trasanctionData)
        {
            var member = _memberService.GetMemberById(trasanctionData.MemberId).Object;

            if (member != null && member.Accounts != null && member.Accounts.Any())
            {
                var account = member.Accounts.FirstOrDefault(x => x.Id == trasanctionData.AccountId);

                if (account != null)
                {
                    //No rules for now. But the design allows to add rules without changing core functionality.
                    var withDrawRules = new List<IAccountRule>();
                    var withDrawRulesResult = true;

                    foreach (var rule in withDrawRules)
                    {
                        withDrawRulesResult = withDrawRulesResult && rule.PassRule(account, trasanctionData);
                    }

                    if (withDrawRulesResult)
                    {
                        account.Balance += trasanctionData.Amount;
                        await _memberService.UpdateMember(member);
                        return true;
                    }
                }
            }

            return false;
        }

        private async Task<bool> Transfer(TransactionData trasanctionData)
        {
            trasanctionData.MemberId = trasanctionData.FromMemberId;
            trasanctionData.AccountId = trasanctionData.FromAccountId;

            if(await Withdraw(trasanctionData))
            {
                trasanctionData.MemberId = trasanctionData.ToMemberId;
                trasanctionData.AccountId = trasanctionData.ToAccountId;

                if(! await Deposit(trasanctionData))
                {
                    //Deposit it back to origin account. 
                    //State pattern is best suited instead of if else. Due to time constraint, just basic IMoneyTrasnferStateController class has been created and not implemented.
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
