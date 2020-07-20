using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackaRocketZenvia.API.Model.WebHook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HackaRocketZenvia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WhatsappController : ControllerBase
    {
        [HttpPost("WebHookStatus")]
        public IActionResult WebhookMessageStatus([FromBody] Model.WebHook.StatusWebHook message)
        {
            if (ModelState.IsValid)
            {
                if (message != null)
                {
                    if (message.type == "MESSAGE_STATUS")
                    {
                        //Gravando LOG do status da mensagem no banco                        
                    }
                }
            }

            return Ok();
        }

        //localhost/Whatsapp/WebHook
        [HttpPost("WebHookMessage")]
        public IActionResult WebhookMessage([FromBody] Model.WebHook.MessageWebHook message)
        {
            if (ModelState.IsValid)
            {

                if (message != null)
                {
                    //Quando recebe realmente a resposta do cliente
                    if (message.type == "MESSAGE")
                    {
                        //Fazendo uma resposta de teste...
                        var x = new Clients.WhatsappClient("qwXNkAVQQ6WUAni_BrTOkN0BFg-zOkc3AjiZ");

                        var numeroRandom = new Random().Next(1, 9999);

                        var msg = new Model.Messages.MessageSend()
                        {
                            from = "few-jumper",
                            to = "5511989259128", //"5547999167022", //"5517991815083", //"5517991119839",
                            contents = new Model.Messages.ContentSend[]
                            {
                                new Model.Messages.ContentSend()
                                {
                                    text = $"Feedback do webhook pela API propria - {numeroRandom}",
                                    type = "text"
                                }
                            }
                        };
                        x.SendMessage(msg);
                    }
                }

                return Ok();
            }

            return NotFound();
        }

        [HttpPost("SendMessage")]
        public IActionResult Send([FromBody] Model.Messages.MessageSend message)
        {
            if (ModelState.IsValid)
            {
                if (message != null)
                {
                    var x = new Clients.WhatsappClient("qwXNkAVQQ6WUAni_BrTOkN0BFg-zOkc3AjiZ");
                    x.SendMessage(message);
                }

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("RegisterClient")]
        public Model.System.Cliente RegisterClient(Model.System.Cliente data)
        {
            return new Model.System.Cliente();
        }

        [HttpGet("GetProdutosByName")]
        public IEnumerable<Model.System.Produto> GetProdutosByName(string name)
        {
            var lst = new List<Model.System.Produto>();
            lst.Add(new Model.System.Produto()
            {
                categoria = "animal",
                celular_vendedor = "(99)999999999",
                imagem = "",
                nome = "Frango resfriado desossado",
                valor = "R$15,00"
            });

            lst.Add(new Model.System.Produto()
            {
                categoria = "animal",
                celular_vendedor = "(88)888888888",
                imagem = "",
                nome = "Frango assado com farofa",
                valor = "R$24,00"
            });
            
            return lst;
        }

        [HttpGet("ReturnSell")]
        public Model.System.Venda_Result ReturnSell(string codigo)
        {
            var result = new Model.System.Venda_Result()
            {
                produto = "Frango resfriado desossado",
                pxlink = "https://zenclassificados.com.br/pix/aviario-agua-verde/15.0",
                whatsapp = "https://api.whatsapp.com/send?phone=5547988061107&text=Ola%20Avi%C3%A1rio%20%C3%A1gua%20verde%20acabei%20de%20comprar%20um%20frango%20congelado,%20paguei%2015%20reais%20via%20PIX,%20quero%20combinar%20para%20buscar",
                qrcode = "https://internationalbarcodes.net/wp-content/uploads/2017/04/QR%20code%20example.jpg"
            };

            return result;
        }
    }
}
