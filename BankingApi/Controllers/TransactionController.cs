using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.Services.Contracts;
using BankingApi.Services.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST: api/<UpdateController>
        [HttpPost]
        public async Task<IActionResult> Update(int memberId, int accountId, decimal amount)
        {
            var transactionData = new TransactionData() { Amount = amount, FromAccountId = accountId, ToAccountId = accountId, FromMemberId = memberId, ToMemberId = memberId, Command = amount > 0 ? Core.Transaction.DEPOSIT : Core.Transaction.WITHDRAW };
            var transactionResult = await _transactionService.ProcessTransaction(transactionData);

            return Ok(transactionResult);
        }

        // POST: api/<TransactionController>
        [HttpPost]
        public async Task<IActionResult> Transfer(int fromMemberId, int accountId, int toMemberId, int toAccountId, decimal amount)
        {
            var transactionData = new TransactionData() { Amount = amount, FromAccountId = accountId, ToAccountId = accountId, FromMemberId = fromMemberId, ToMemberId = toMemberId, Command = Core.Transaction.TRANSFER };
            var transactionResult = await _transactionService.ProcessTransaction(transactionData);

            return Ok(transactionResult);
        }
    }
}
