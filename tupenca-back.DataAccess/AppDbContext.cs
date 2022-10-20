using Microsoft.EntityFrameworkCore;
using tupenca_back.Model;

namespace tupenca_back.DataAccess
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Campeonato>? Campeonatos { get; set; }

        public DbSet<Equipo>? Equipos { get; set; }

        public DbSet<Deporte>? Deportes { get; set; }

        public DbSet<Evento>? Eventos { get; set; }

        public DbSet<Empresa>? Empresa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>()
                .HasOne(evento => evento.EquipoLocal)
                .WithMany()
                .HasForeignKey(evento => evento.EquipoLocalId);
            modelBuilder.Entity<Evento>()
                .HasOne(evento => evento.EquipoVisitante)
                .WithMany()
                .HasForeignKey(evento => evento.EquipoVisitanteId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}
