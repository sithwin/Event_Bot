using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Bot.Models
{
    public enum ShirtSizeOptions
    {
        [Terms("xs", "s", "m", "l", "xl")]
        ExtraSmall,
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    public enum FreeBieOptions
    {
        WaterBottle,
        Towels,
        Sunglass,
        Hat
    }

    [Serializable]
    public class EventRegistration
    {
        [Pattern(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        public string Name { get; set; }
        public ShirtSizeOptions? ShirtSize { get; set; }
        public List<FreeBieOptions> Freebies { get; set; }

        [Prompt("Please enter collection date (dd/mm/yyyy)")]
        [Pattern(@"^\d{1,2}/\d{1,2}/\d{4}$")]
        public string CollectionDate { get; set; }

        public static IForm<EventRegistration> BuildForm()
        {
            return new FormBuilder<EventRegistration>()
                .Message("Welcome to the event registration bot!")
                .Build()
                ;
        }
    }
}