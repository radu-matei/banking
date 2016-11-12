namespace BankingSystem.Api
{
    public class Account
    {
        public int Number { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }

        public Account()
        {
        }

        public Account(string owner, int number, decimal balance)
        {
            Owner = owner;
            Number = number;
            Balance = balance;
        }
    }
}
