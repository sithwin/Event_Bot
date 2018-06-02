using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Event_Bot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
       
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi, I'm Event Bot!");
            context.Wait(MessageReceiveAsync);
        }

        private async Task MessageReceiveAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var userName = string.Empty;
            bool getName = false;

            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }

            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(String.Format("Hi {0}, How can I help you today?", userName));
            }
            context.Wait(MessageReceiveAsync);
        }
    }
}