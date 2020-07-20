using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Model.WebHook
{   
    public class StatusWebHook
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string type { get; set; }
        public string subscriptionId { get; set; }
        public string channel { get; set; }
        public string messageId { get; set; }
        public int contentIndex { get; set; }
        public Messagestatus messageStatus { get; set; }
    }

    public class Messagestatus
    {
        public DateTime timestamp { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

}
