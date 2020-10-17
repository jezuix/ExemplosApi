using System;

namespace Teste.API.RequestModel
{
    public class DeletaCarteiraRequest
    {
        public Guid IdCarteira { get; set; }
        public bool TemCerteza { get; set; } = false;
    }
}
