using System;
using System.ComponentModel.DataAnnotations;

namespace Teste.API.Models
{
    public class Carteira
    {
        public Carteira(Guid idUsuario, decimal valorInvestido)
        {
            IdUsuario = idUsuario;
            ValorInvestido = valorInvestido;
        }

        public Carteira(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public decimal ValorInvestido { get; set; }


        /// FK
        public Guid IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
