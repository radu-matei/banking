using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankingSystem.Bot
{
    public class BankService
    {
        public async Task<decimal> GetAccountBalance(string accountNumber)
        {
            using(HttpClient client = new HttpClient())
            {
                int number = Convert.ToInt32(accountNumber);
                client.BaseAddress = new Uri($"http://localhost:5000/api/Accounts/GetAccountBalance?accountNumber={number}");

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                return await response.Content.ReadAsAsync<decimal>();
            }
        }
    }
}