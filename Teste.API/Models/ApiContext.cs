using Microsoft.EntityFrameworkCore;

namespace Teste.API.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Carteira> Carteiras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var usuarioBuilder = modelBuilder.Entity<Usuario>();
            usuarioBuilder.HasKey(c => c.Id);
            usuarioBuilder.HasOne(c => c.Carteira).WithOne(c => c.Usuario).HasForeignKey<Carteira>(c => c.IdUsuario);
            usuarioBuilder.Property(c => c.Nome).HasMaxLength(100);
            usuarioBuilder.Property(c => c.Nome).IsRequired(true);

            var carteiraBuilder = modelBuilder.Entity<Carteira>();
            carteiraBuilder.HasKey(c => c.Id);
            carteiraBuilder.HasOne(c => c.Usuario).WithOne(c => c.Carteira).HasForeignKey<Usuario>(c => c.IdCarteira);
            carteiraBuilder.Property(c => c.ValorInvestido).HasColumnType("decimal(18,2)");
            carteiraBuilder.Property(c => c.ValorInvestido).IsRequired(true);
        }
    }
}
