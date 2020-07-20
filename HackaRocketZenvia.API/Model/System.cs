using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Model.System
{
    public class Cliente
    {
        public string celular { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }
        public string receber_newsletter { get; set; }
    }

    public class Vendedor
    {
        public string celular { get; set; }
        public string nome { get; set; }
        public string ramo { get; set; }
        public string cep { get; set; }
        public string chave_pix { get; set; }
        public string full_time { get; set; }
        public string tipo_classificado { get; set; }
    }

    public class Produto
    {
        public string celular_vendedor { get; set; }
        public string nome { get; set; }
        public string categoria { get; set; }
        public string valor { get; set; }
        public string imagem { get; set; }
    }

    public class Produtos_Vendedor
    {
        public Vendedor vendedor { get; set; }
        public IEnumerable<Produto> produtos { get; set; }
    }

    public class Venda_Result
    {
        public string pxlink { get; set; }
        public string whatsapp { get; set; }
        public string produto { get; set; }
        public string qrcode { get; set; }
    }

}
