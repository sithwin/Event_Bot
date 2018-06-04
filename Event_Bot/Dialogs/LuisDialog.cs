using Event_Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Event_Bot.Dialogs
{
    [LuisModel("04fb35e8-a944-4afc-b852-c808a038a1c0", "ff46453060b149868c763c341bb4d645")]
    [Serializable]
    public class LuisDialog: LuisDialog<EventRegistration>
    {
        private readonly BuildFormDelegate<EventRegistration> RegistrationForm;

        public LuisDialog(BuildFormDelegate<EventRegistration> registrationForm)
        {
            this.RegistrationForm = registrationForm;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), CallBack);
        }

        [LuisIntent("Registration")]
        public async Task Registration(IDialogContext context, LuisResult result)
        {
            var registartionForm = new FormDialog<EventRegistration>(new EventRegistration(), 
                this.RegistrationForm, FormOptions.PromptInStart);
            context.Call<EventRegistration>(registartionForm, CallBack);
        }

        [LuisIntent("QueryFreebies")]
        public async Task QueryFreebies(IDialogContext context, LuisResult result)
        {
            foreach(var entity in result.Entities.Where(Entity => Entity.Type == "Freebies"))
            {
                var value = entity.Entity.ToLower();
                if (value == "water" || value == "towels" || value == "sunglass" || value == "hat")
                {
                    await context.PostAsync("Yes, we provide you!");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("I'm sorry we don't provide that.");
                    context.Wait(MessageReceived);
                    return;
                }
            }
            await context.PostAsync("I'm sorry we don't provide that.");
        }

        private async Task CallBack(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}