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

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            string userName;

            if (!context.UserData.TryGetValue(ConstantKeys.UserNameKey, out userName))
            {
                PromptDialog.Text(context, this.ResumeAfterPrompt, "Before get started, please tell me your name?");
                return;
            }

            if (!this.userWelcomed)
            {
                this.userWelcomed = true;
                await context.PostAsync($"Welcome back {userName}! {helpMessage}");

                context.Wait(this.MessageReceivedAsync);
                return;
            }

            if (message.Text.Equals("test"))
            {
                await context.PostAsync($"{userName}, this is cool!");
            }

            context.Wait(MessageReceivedAsync);
        }


        private async Task ResumeAfterPrompt(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var userName = await result;
                this.userWelcomed = true;

                await context.PostAsync($"Welcome {userName}! {helpMessage}");

                context.UserData.SetValue(ConstantKeys.UserNameKey, userName);
            }
            catch (TooManyAttemptsException)
            {
            }

            context.Wait(this.MessageReceivedAsync);
        }
    }
}
