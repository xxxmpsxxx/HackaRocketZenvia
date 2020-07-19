using HackaRocketZenvia.API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Clients
{
    public class WhatsappClient : BaseClient
    {
        private string token;
        private const string URL_BASE = @"https://api.zenvia.com/v1/channels/whatsapp/messages";

        public WhatsappClient(string token) : base()
        {
            this.token = token;
        }

        public async Task<WebHookResponse> SendMessage(MessageRequest message)
        {
            var header = new Dictionary<string, object>()
            {
                { "X-API-TOKEN", this.token}
            };

            var body = message;

            var response = await PostAsync($"{URL_BASE}", headers: header, body: body);

            await EnsureSuccessStatusCodeAsync(response);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WebHookResponse>(json);
        }
    }
}
