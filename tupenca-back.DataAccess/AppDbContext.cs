using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using tupenca_back.Model;

namespace tupenca_back.DataAccess
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Administrador>? Administradores { get; set; }
        public DbSet<Campeonato>? Campeonatos { get; set; }
        public DbSet<Deporte>? Deportes { get; set; }
        public DbSet<Empresa>? Empresas { get; set; }
        public DbSet<Equipo>? Equipos { get; set; }
        public DbSet<Evento>? Eventos { get; set; }
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<Penca>? Pencas { get; set; }
        public DbSet<PencaCompartida>? PencaCompartidas { get; set; }
        public DbSet<PencaEmpresa>? PencaEmpresas { get; set; }
        public DbSet<Persona>? Personas { get; set; }
        public DbSet<Plan>? Planes { get; set; }
        public DbSet<Prediccion>? Predicciones { get; set; }
        public DbSet<Premio>? Premios { get; set; }
        public DbSet<Resultado>? Resultados { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<UsuarioPenca>? UsuariosPencas { get; set; }
        public DbSet<UserInviteToken>? UserInviteTokens { get; set; }
        public DbSet<Puntaje>? Puntajes { get; set; }
        public DbSet<Foro>? Foros { get; set; }
        public DbSet<PuntajeUsuarioPenca>? PuntajeUsuarioPencas { get; set; }
        public DbSet<LookAndFeel>? LookAndFeels { get; set; }
        public DbSet<UsuarioPremio>? UsuarioPremios { get; set; }


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

            modelBuilder.Entity<Funcionario>()
                .HasOne(funcionario => funcionario.Empresa)
                .WithMany(e => e.Funcionarios)
                .HasForeignKey(funcionario => funcionario.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioPenca>(entity =>
            {
                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuariosPencas)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);


                entity.HasOne(d => d.Penca)
                    .WithMany(p => p.UsuariosPencas)
                    .HasForeignKey(d => d.PencaId);


            });


        }

    }

}
