using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Clients
{
    public class ClientResponseException : Exception
    {
        public ClientResponseException() : base()
        {

        }
        public ClientResponseException(string message) : base(message)
        {

        }
        public ClientResponseException(string message, Exception inner) : base(message, inner)
        {

        }
        public ClientResponseException(HttpStatusCode statusCode, string message, HttpContent content) : base(message)
        {
            _StatusCode = statusCode;
            _Content = content;
        }

        private HttpContent _Content { get; set; }
        public HttpContent Content { get { return _Content; } }
        private HttpStatusCode _StatusCode { get; set; }
        public HttpStatusCode StatusCode { get { return _StatusCode; } }
    }
}
