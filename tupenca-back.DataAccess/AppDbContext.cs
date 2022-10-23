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

        public DbSet<Persona>? Personas { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Administrador>? Administradores { get; set; }
        public DbSet<Funcionario>? Funcionarios { get; set; }


        public DbSet<Campeonato>? Campeonatos { get; set; }

        public DbSet<Equipo>? Equipos { get; set; }

        public DbSet<Deporte>? Deportes { get; set; }

        public DbSet<Evento>? Eventos { get; set; }

        public DbSet<Empresa>? Empresas { get; set; }

        public DbSet<Resultado>? Resultados { get; set; }

        public DbSet<Prediccion>? Predicciones { get; set; }

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


            #region DeporteSeed
            modelBuilder.Entity<Deporte>().HasData(
                new Deporte { Id = 1, Nombre = "Futbol"});
            #endregion

            #region EquipoSeed
            modelBuilder.Entity<Equipo>().HasData(
                //grupo A
                new Equipo { Id = 1, Nombre = "Qatar" },
                new Equipo { Id = 2, Nombre = "Ecuador"},
                new Equipo { Id = 3, Nombre = "Senegal" },
                new Equipo { Id = 4, Nombre = "Holanda" },
                //grupo H
                new Equipo { Id = 5, Nombre = "Portugal" },
                new Equipo { Id = 6, Nombre = "Ghana" },
                new Equipo { Id = 7, Nombre = "Uruguay" },
                new Equipo { Id = 8, Nombre = "Corea del Sur" });
            #endregion
            
            //primera jornada
            DateTime date1 = new DateTime(2022, 11, 20, 13, 00, 00);

            DateTime date2 = new DateTime(2022, 11, 21, 07, 00, 00);
            DateTime date3 = new DateTime(2022, 11, 21, 10, 00, 00);
            DateTime date4 = new DateTime(2022, 11, 21, 16, 00, 00);

            DateTime date5 = new DateTime(2022, 11, 22, 07, 00, 00);
            DateTime date6 = new DateTime(2022, 11, 22, 10, 00, 00);
            DateTime date7 = new DateTime(2022, 11, 22, 13, 00, 00);
            DateTime date8 = new DateTime(2022, 11, 22, 16, 00, 00);

            DateTime date9 = new DateTime(2022, 11, 23, 07, 00, 00);
            DateTime date10 = new DateTime(2022, 11, 23, 10, 00, 00);
            DateTime date11 = new DateTime(2022, 11, 23, 13, 00, 00);
            DateTime date12 = new DateTime(2022, 11, 23, 16, 00, 00);

            DateTime date13 = new DateTime(2022, 11, 24, 07, 00, 00);
            DateTime date14 = new DateTime(2022, 11, 24, 10, 00, 00);
            DateTime date15 = new DateTime(2022, 11, 24, 13, 00, 00);
            DateTime date16 = new DateTime(2022, 11, 24, 16, 00, 00);

            //segunda jornada
            DateTime date17 = new DateTime(2022, 11, 25, 07, 00, 00);
            DateTime date18 = new DateTime(2022, 11, 25, 10, 00, 00);
            DateTime date19 = new DateTime(2022, 11, 25, 13, 00, 00);
            DateTime date20 = new DateTime(2022, 11, 25, 16, 00, 00);

            DateTime date21 = new DateTime(2022, 11, 26, 07, 00, 00);
            DateTime date22 = new DateTime(2022, 11, 26, 10, 00, 00);
            DateTime date23 = new DateTime(2022, 11, 26, 13, 00, 00);
            DateTime date24 = new DateTime(2022, 11, 26, 16, 00, 00);

            DateTime date25 = new DateTime(2022, 11, 27, 07, 00, 00);
            DateTime date26 = new DateTime(2022, 11, 27, 10, 00, 00);
            DateTime date27 = new DateTime(2022, 11, 27, 13, 00, 00);
            DateTime date28 = new DateTime(2022, 11, 27, 16, 00, 00);

            DateTime date29 = new DateTime(2022, 11, 28, 07, 00, 00);
            DateTime date30 = new DateTime(2022, 11, 28, 10, 00, 00);
            DateTime date31 = new DateTime(2022, 11, 28, 13, 00, 00);
            DateTime date32 = new DateTime(2022, 11, 28, 16, 00, 00);

            //tercera jornada
            DateTime date33 = new DateTime(2022, 11, 29, 12, 00, 00);
            DateTime date34 = new DateTime(2022, 11, 29, 16, 00, 00);

            DateTime date35 = new DateTime(2022, 11, 30, 12, 00, 00);
            DateTime date36 = new DateTime(2022, 11, 30, 16, 00, 00);

            DateTime date37 = new DateTime(2022, 12, 01, 12, 00, 00);
            DateTime date38 = new DateTime(2022, 12, 01, 16, 00, 00);

            DateTime date39 = new DateTime(2022, 12, 02, 12, 00, 00);
            DateTime date40 = new DateTime(2022, 12, 02, 16, 00, 00);

            #region EventoSeed
            modelBuilder.Entity<Evento>().HasData(
                new Evento { Id = 1, EquipoLocalId = 1, EquipoVisitanteId = 2, FechaInicial = date1 },
                new Evento { Id = 2, EquipoLocalId = 3, EquipoVisitanteId = 4, FechaInicial = date2 },
                new Evento { Id = 3, EquipoLocalId = 7, EquipoVisitanteId = 8, FechaInicial = date14 },
                new Evento { Id = 4, EquipoLocalId = 5, EquipoVisitanteId = 6, FechaInicial = date15 },
                new Evento { Id = 5, EquipoLocalId = 1, EquipoVisitanteId = 3, FechaInicial = date18 },
                new Evento { Id = 6, EquipoLocalId = 4, EquipoVisitanteId = 2, FechaInicial = date19 },
                new Evento { Id = 7, EquipoLocalId = 8, EquipoVisitanteId = 6, FechaInicial = date30 },
                new Evento { Id = 8, EquipoLocalId = 5, EquipoVisitanteId = 7, FechaInicial = date32 },
                new Evento { Id = 9, EquipoLocalId = 2, EquipoVisitanteId = 3, FechaInicial = date33 },
                new Evento { Id = 10, EquipoLocalId = 4, EquipoVisitanteId = 1, FechaInicial = date33 },
                new Evento { Id = 11, EquipoLocalId = 6, EquipoVisitanteId = 7, FechaInicial = date39 },
                new Evento { Id = 12, EquipoLocalId = 8, EquipoVisitanteId = 5, FechaInicial = date39 });
            #endregion

        }

    }

}
