using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankingSystem.Api
{
    public class TransactionsController : Controller
    {
        private ITransactionManager _transactionManager { get; set; }

        public TransactionsController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        [HttpGet]
        public void ExecuteTransaction([FromQuery]int sourceAccountNumber, [FromQuery]int destinationAccountNumber, [FromQuery]decimal amount)
        {
            _transactionManager.ExcuteTransaction(sourceAccountNumber, destinationAccountNumber, amount);
        }
    }
}
