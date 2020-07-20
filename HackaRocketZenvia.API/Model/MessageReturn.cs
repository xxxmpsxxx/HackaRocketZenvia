using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Model.Messages
{
    public class MessageReturn
    {
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string direction { get; set; }
        public string channel { get; set; }
        public ContentReturn[] contents { get; set; }
    }

    public class ContentReturn
    {
        public string type { get; set; }
        public string text { get; set; }
        public string payload { get; set; }
    }
}
