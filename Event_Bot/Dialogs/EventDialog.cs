using Event_Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Event_Bot.Dialogs
{
    public class EventDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
            new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, text) =>
            {
                return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuationAsync);
            }),
            new DefaultCase<string, IDialog<string>>((context, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(EventRegistration.BuildForm, FormOptions.PromptInStart), AfterEventContinuationAsync);
            }))
            .Unwrap()
            .PostToUser();

        private static async Task<IDialog<string>> AfterGreetingContinuationAsync(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Hi {name}, How can I help you today?");
        }

        private static async Task<IDialog<string>> AfterEventContinuationAsync(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Thank you for using the event bot {name}!!");
        }
    }
}