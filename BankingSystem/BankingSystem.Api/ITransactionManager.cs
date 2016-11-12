namespace BankingSystem.Api
{
    public interface ITransactionManager
    {
        bool ExcuteTransaction(int sourceAccountNumber, int destinationAccountNumber, decimal amount);
    }
}
