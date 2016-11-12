using System;
using System.Collections.Generic;

namespace BankingSystem.Api
{
    public class TransactionManager : ITransactionManager
    {
        private IAccountManager _accountManager { get; set; }

        public TransactionManager(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public bool ExcuteTransaction(int sourceAccountNumber, int destinationAccountNumber, decimal amount)
        {
            var sourceAccount = _accountManager.GetAccount(sourceAccountNumber);
            var destinationAccount = _accountManager.GetAccount(destinationAccountNumber);

            if (sourceAccount == null || 
                destinationAccount == null || 
                amount < 0)
                return false;

            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            return true;
        }
    }
}
