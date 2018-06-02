﻿using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Bot.Models
{
    public enum ShirtSizeOptions
    {
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
        SunGlass,
        Hat
    }

    public class EventRegistration
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public ShirtSizeOptions? ShirtSize { get; set; }
        public List<FreeBieOptions> FreeBies { get; set; }
        public DateTime? CollectionDate { get; set; }

        public static IForm<EventRegistration> BuildForm()
        {
            return new FormBuilder<EventRegistration>()
                .Message("Welcome to the event registration bot!")
                .Build();
        }
    }
}