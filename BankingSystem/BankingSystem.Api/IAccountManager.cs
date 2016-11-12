using System.Collections.Generic;

namespace BankingSystem.Api
{
    public interface IAccountManager
    {
        List<Account> GetAccounts();
        Account GetAccount(int accountNumber);
        void AddAccount(Account account);
        bool Contains(Account account);
    }
}
