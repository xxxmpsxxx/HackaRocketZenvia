using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackaRocketZenvia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WhatsappController : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> SendMessage()
        //{
        //    var x = new Clients.WhatsappClient("qwXNkAVQQ6WUAni_BrTOkN0BFg-zOkc3AjiZ");
        //    await x.SendMessage(new Model.Message()
        //    {
        //        from = "few-jumper",
        //        to = "5517991119839",
        //        contents = new Model.Content[] { new Model.Content() { text = "Olá Marcus, tudo bem?", type = "text" } }
        //    });

        //    return Ok();
        //}


        //localhost/Whatsapp/WebHook
        [HttpPost("WebHook")]
        public async Task<IActionResult> Webhook([FromBody] Model.WebHookResponse message)
        {
            if (ModelState.IsValid)
            {

                if (message != null)
                {
                    //salvar o message.message no banco...
                    
                    //Quando recebe a informacao de status da msg enviada
                    if (message.type == "MESSAGE_STATUS")
                    {

                    }
                    
                    //Quando recebe realmente a resposta do cliente
                    if (message.type == "MESSAGE")
                    {
                        //Fazendo uma resposta de teste...
                        var x = new Clients.WhatsappClient("qwXNkAVQQ6WUAni_BrTOkN0BFg-zOkc3AjiZ");

                        var numeroRandom = new Random().Next(1, 9999);

                        var msg = new Model.MessageRequest()
                        {
                            from = "few-jumper",
                            to = "5517991815083", //"5517991119839",
                            contents = new Model.ContentRequest[] 
                            { 
                                new Model.ContentRequest() 
                                { 
                                    text = $"Feedback do webhook pela API propria - {numeroRandom}", 
                                    type = "text", 
                                    payload = "" 
                                } 
                            }
                        };
                        await x.SendMessage(msg);
                    }                                                            
                }

                return Ok("");
            }

            return NotFound();
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> Send([FromBody] Model.MessageRequest message)
        {
            if (ModelState.IsValid)
            {
                if (message != null)
                {
                    var x = new Clients.WhatsappClient("qwXNkAVQQ6WUAni_BrTOkN0BFg-zOkc3AjiZ");
                    await x.SendMessage(message);
                }

                return Ok("");
            }

            return NotFound();
        }
    }
}
