using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Implementations;
using BankingApi.Services.Model;
using BankingApi.Services.Validations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApi.BankingApi.Services.Tests
{
    public class TransactionServiceTests
    {
        private Mock<IJsonRepository<Member>> _memberRepository;
        private Mock<IJsonRepository<Account>> _accountRepository;
        private IMemberService _memberService;
        private ITransactionService _transactionService;
        private IAccountService _accountService;
        private MemberValidator _memberValidator;

        [SetUp]
        public void SetUp()
        {
            _memberRepository = new Mock<IJsonRepository<Member>>();
            _accountRepository = new Mock<IJsonRepository<Account>>();
            _memberValidator = new MemberValidator();

            _memberService = new MemberService(_memberRepository.Object, _memberValidator);
            _accountService = new AccountService(_accountRepository.Object, _memberRepository.Object);
            _transactionService = new TransactionService(_accountService, _memberService);
        }

        [Test]
        public void Ensure_Deposit_Transaction_Works()
        {
            var allmembers = new List<Member>();
            allmembers.Add(new Member() { Id = 1, GivenName = "John", SurName = "Doe", InstitutionId = 1, Accounts = (new List<Account>() { new Account() { Id = 1, Balance = 10 } }).ToArray() });
            allmembers.Add(new Member() { Id = 2, GivenName = "Jane", SurName = "Doe", InstitutionId = 2, Accounts = (new List<Account>() { new Account() { Id = 1, Balance = 25 } }).ToArray() });
            allmembers.Add(new Member() { Id = 3, GivenName = "Dan", SurName = "Rather", InstitutionId = 1, Accounts = (new List<Account>() { new Account() { Id = 1, Balance = 0 } }).ToArray() });

            var transactionData = new TransactionData() { Amount = 20, AccountId = 1, MemberId = 1, InstitutionId = 1, Command = Core.Transaction.DEPOSIT };

            _memberRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => allmembers.FirstOrDefault(x => x.Id == id));
            _memberRepository.Setup(x => x.Update(It.IsAny<Member>())).Callback<Member>(member => { { if (member.Id > 0) { allmembers.Remove(allmembers.FirstOrDefault(x => x.Id == member.Id)); allmembers.Add(member); } } });

            var originalAccountState = _accountService.GetAccountById(transactionData.AccountId, transactionData.MemberId);
            var isSuccess = _transactionService.ProcessTransaction(transactionData).GetAwaiter().GetResult();
            var resultAccountState = _accountService.GetAccountById(transactionData.AccountId, transactionData.MemberId);

            Assert.AreEqual(isSuccess, true);
            Assert.AreEqual(originalAccountState.Object.Balance + transactionData.Amount, resultAccountState.Object.Balance);
        }

        [Test]
        public void Ensure_Withdraw_Transaction_Works()
        {
            var transactionData = new TransactionData() { Amount = 20, AccountId = 1, MemberId = 1,  InstitutionId = 1, Command = Core.Transaction.WITHDRAW };

            var originalAccountState = _accountService.GetAccountById(transactionData.AccountId, transactionData.MemberId);
            var isSuccess = _transactionService.ProcessTransaction(transactionData).GetAwaiter().GetResult();
            var resultAccountState = _accountService.GetAccountById(transactionData.AccountId, transactionData.MemberId);

            Assert.AreEqual(isSuccess, true);
            Assert.AreEqual(originalAccountState.Object.Balance - transactionData.Amount, resultAccountState.Object.Balance);
        }

        [Test]
        public void Ensure_Transfer_Transaction_Works()
        {
            var transactionData = new TransactionData() { Amount = 20, FromAccountId = 1, ToAccountId = 1, FromMemberId = 1, ToMemberId = 2, InstitutionId = 1, Command = Core.Transaction.TRANSFER };

            var originalFromAccountState = _accountService.GetAccountById(transactionData.FromAccountId, transactionData.FromMemberId);
            var originalToAccountState = _accountService.GetAccountById(transactionData.ToAccountId, transactionData.ToMemberId);

            var isSuccess = _transactionService.ProcessTransaction(transactionData).GetAwaiter().GetResult();

            var resultFromAccountState = _accountService.GetAccountById(transactionData.FromAccountId, transactionData.FromMemberId);
            var resultToAccountState = _accountService.GetAccountById(transactionData.ToAccountId, transactionData.ToMemberId);


            Assert.AreEqual(isSuccess, true);
            Assert.AreEqual(originalFromAccountState.Object.Balance - transactionData.Amount, resultFromAccountState.Object.Balance);
            Assert.AreEqual(originalToAccountState.Object.Balance + transactionData.Amount, resultToAccountState.Object.Balance);
        }
    }
}
