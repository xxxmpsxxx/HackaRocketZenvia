﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Model.WebHook
{   
    public class MessageWebHook
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string type { get; set; }
        public string subscriptionId { get; set; }
        public string channel { get; set; }
        public string direction { get; set; }
        public Message message { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string direction { get; set; }
        public string channel { get; set; }
        public Visitor visitor { get; set; }
        public Content[] contents { get; set; }
    }

    public class Visitor
    {
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public string text { get; set; }
    }
}

