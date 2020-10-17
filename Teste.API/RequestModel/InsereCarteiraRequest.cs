using System;

namespace Teste.API.RequestModel
{
    public class InsereCarteiraRequest
    {
        public Guid IdUsuario { get; set; }
        public decimal ValorInvestido { get; set; }
    }
}
