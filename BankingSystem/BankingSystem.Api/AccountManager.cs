using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSystem.Api
{
    public class AccountManager : IAccountManager
    {
        private static List<Account> _accountList { get; set; }

        public AccountManager()
        {
            _accountList = new List<Account>()
            {
                new Account("John", 46, 1000),
                new Account("Jane", 93, 2000)
            };
        }

        public void AddAccount(Account account)
        {
            _accountList.Add(account);
        }

        public Account GetAccount(int accountNumber)
        {
            return _accountList.FirstOrDefault(a => a.Number == accountNumber);
        }

        public List<Account> GetAccounts()
        {
            return _accountList;
        }

        public bool Contains(Account account)
        {
            return _accountList.Contains(account);
        }
    }
}
