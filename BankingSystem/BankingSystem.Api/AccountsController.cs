using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankingSystem.Api
{
    public class AccountsController : Controller
    {
        private IAccountManager _accountManager { get; set; }

        public AccountsController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public List<Account> GetAccounts()
        {
            return _accountManager.GetAccounts();
        }

        public Account GetAccount(int accountNumber)
        {
            return _accountManager.GetAccount(accountNumber);
        }
    }
}
