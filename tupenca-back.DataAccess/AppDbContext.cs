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


            #region DeporteSeed
            modelBuilder.Entity<Deporte>().HasData(
                new Deporte { Id = 1, Nombre = "Futbol" },
                new Deporte { Id = 2, Nombre = "Tennis" });
            #endregion

            #region EquipoSeed
            modelBuilder.Entity<Equipo>().HasData(
                //grupo A
                new Equipo { Id = 1, Nombre = "Qatar" },
                new Equipo { Id = 2, Nombre = "Ecuador" },
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

            //DateForTennis
            DateTime date1T = new DateTime(2023, 01, 02, 07, 00, 00);
            DateTime date2T = new DateTime(2023, 01, 02, 08, 00, 00);

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
                new Evento { Id = 12, EquipoLocalId = 8, EquipoVisitanteId = 5, FechaInicial = date39 },
                new Evento { Id = 13, EquipoLocalId = 2, EquipoVisitanteId = 7, FechaInicial = date1T, IsEmpateValid = false, IsPuntajeEquipoValid = false },
                new Evento { Id = 14, EquipoLocalId = 7, EquipoVisitanteId = 2, FechaInicial = date2T, IsEmpateValid = false, IsPuntajeEquipoValid = false });
            #endregion

            #region PlanSeed
            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, CantPencas = 1, CantUser = 50, LookAndFeel = 1, PercentageCost = 10 , Cost = 100 },
                new Plan { Id = 2, CantPencas = 5, CantUser = 100, LookAndFeel = 2, PercentageCost = 10, Cost = 200 },
                new Plan { Id = 3, CantPencas = 10, CantUser = 500, LookAndFeel = 2, PercentageCost = 10, Cost = 500 });
            #endregion

            #region EmpresaSeed
            modelBuilder.Entity<Empresa>().HasData(
                new Empresa { Id = 1, Razonsocial = "McDonald's S.A.", RUT = "214873040018",FechaCreacion = date1 , PlanId=1},
                new Empresa { Id = 2, Razonsocial = "BMW Ibérica S.A.", RUT = "304001821487", FechaCreacion = date2 , PlanId=2},
                new Empresa { Id = 3, Razonsocial = "Air Europa Líneas Aéreas S.A.", RUT = "821473040018", FechaCreacion = date3, PlanId=1},
                new Empresa { Id = 4, Razonsocial = "Punto FA S.L.", RUT = "040001821487", FechaCreacion = date4 , PlanId = 3 });
            #endregion
            //password is: string
            var passwordHash = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92};
            var passwordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 };
            #region UsuarioSeed
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 49, Email = "mati98bor@gmail.com", UserName = "Matibor", HashedPassword = passwordHash, PasswordSalt = passwordSalt },
                new Usuario { Id = 50, Email = "user123@example.com", UserName = "user123", HashedPassword = passwordHash, PasswordSalt = passwordSalt });
            #endregion

            #region FuncionarioSeed
            modelBuilder.Entity<Funcionario>().HasData(
                new Funcionario { Id = 8, Email = "mati98bor@gmail.com", UserName = "Matibor", HashedPassword = passwordHash, PasswordSalt = passwordSalt, EmpresaId = 1 },
                new Funcionario { Id = 9, Email = "user@example.com", UserName = "funcionario123", HashedPassword = passwordHash, PasswordSalt = passwordSalt, EmpresaId = 2 });
            #endregion

            #region AdminSeed
            modelBuilder.Entity<Administrador>().HasData(
                new Administrador { Id = 10, Email = "mati98bor@gmail.com", UserName = "Matibor", HashedPassword = passwordHash, PasswordSalt = passwordSalt },
                new Administrador { Id = 11, Email = "user@example.com", UserName = "Administrador123", HashedPassword = passwordHash, PasswordSalt = passwordSalt });
            #endregion
        }

    }

}
