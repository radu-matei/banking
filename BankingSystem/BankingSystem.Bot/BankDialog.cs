using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BankingSystem.Bot
{
    [Serializable]
    public class BankDialog : IDialog<object>
    {
        private const string helpMessage = "Hello, this is your bank bot!";
        private bool userWelcomed;

        private string accountNumber;
        private string userName;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (!context.UserData.TryGetValue(ConstantKeys.UserNameKey, out userName))
            {
                PromptDialog.Text(context, this.ResumeAfterUserNamePrompt, "Before get started, please tell me your name?");
                return;
            }

            if (!this.userWelcomed)
            {
                this.userWelcomed = true;
                await context.PostAsync($"Welcome back {userName}! {helpMessage}");

                context.Wait(this.MessageReceivedAsync);
                return;
            }

            if (message.Text.Equals("balance"))
            {
                var bankService = new BankService();
                await context.PostAsync($"{userName}, your balance is {await bankService.GetAccountBalance(this.accountNumber)}!");
            }

            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterAccountNumberPrompt(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                accountNumber = await result;

                await context.PostAsync($"Your account number is {accountNumber}");

                context.UserData.SetValue(ConstantKeys.AccountNumberKey, accountNumber);
            }
            catch (TooManyAttemptsException)
            {
            }

            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterUserNamePrompt(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                userName = await result;
                this.userWelcomed = true;

                await context.PostAsync($"Welcome {userName}! {helpMessage}");

                context.UserData.SetValue(ConstantKeys.UserNameKey, userName);

                if (!context.UserData.TryGetValue(ConstantKeys.AccountNumberKey, out accountNumber))
                {
                    PromptDialog.Text(context, this.ResumeAfterAccountNumberPrompt, $"{userName}, what is your bank account?");
                    return;
                }
            }
            catch (TooManyAttemptsException)
            {
            }

            context.Wait(this.MessageReceivedAsync);
        }
    }
}
