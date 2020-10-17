using System;

namespace Teste.API.Models
{
    public class Usuario
    {
        public Usuario(string nome, Guid? idCarteira = null)
        {
            Nome = nome;
            IdCarteira = idCarteira;
        }

        public Usuario(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }


        /// FK
        public virtual Carteira Carteira { get; set; }

        public Guid? IdCarteira { get; set; }
    }
}
