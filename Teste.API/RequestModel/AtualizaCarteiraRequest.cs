using System;

namespace Teste.API.RequestModel
{
    public class AtualizaCarteiraRequest
    {
        public Guid IdCarteira { get; set; }
        public decimal ValorInvestido { get; set; }
    }
}
