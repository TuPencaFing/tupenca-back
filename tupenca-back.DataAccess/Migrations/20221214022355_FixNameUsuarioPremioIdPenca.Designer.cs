﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tupenca_back.DataAccess;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221214022355_FixNameUsuarioPremioIdPenca")]
    partial class FixNameUsuarioPremioIdPenca
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CampeonatoEvento", b =>
                {
                    b.Property<int>("CampeonatosId")
                        .HasColumnType("int");

                    b.Property<int>("EventosId")
                        .HasColumnType("int");

                    b.HasKey("CampeonatosId", "EventosId");

                    b.HasIndex("EventosId");

                    b.ToTable("CampeonatoEvento");
                });

            modelBuilder.Entity("EmpresaUsuario", b =>
                {
                    b.Property<int>("EmpresasId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("EmpresasId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("EmpresaUsuario");
                });

            modelBuilder.Entity("PencaPremio", b =>
                {
                    b.Property<int>("PencasId")
                        .HasColumnType("int");

                    b.Property<int>("PremiosId")
                        .HasColumnType("int");

                    b.HasKey("PencasId", "PremiosId");

                    b.HasIndex("PremiosId");

                    b.ToTable("PencaPremio");
                });

            modelBuilder.Entity("tupenca_back.Model.Campeonato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DeporteId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PremiosEntregados")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeporteId");

                    b.ToTable("Campeonatos");
                });

            modelBuilder.Entity("tupenca_back.Model.Deporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Deportes");
                });

            modelBuilder.Entity("tupenca_back.Model.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Habilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<string>("RUT")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Razonsocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("tupenca_back.Model.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("tupenca_back.Model.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EquipoLocalId")
                        .HasColumnType("int");

                    b.Property<int>("EquipoVisitanteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInicial")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmpateValid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPuntajeEquipoValid")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EquipoLocalId");

                    b.HasIndex("EquipoVisitanteId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("tupenca_back.Model.Foro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Creacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PencaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Foros");
                });

            modelBuilder.Entity("tupenca_back.Model.LookAndFeel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Generalbackground")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Generaltext")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Navbar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Textnavbar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LookAndFeels");
                });

            modelBuilder.Entity("tupenca_back.Model.NotificationUserDeviceId", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("deviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NotificationUserDeviceIds");
                });

            modelBuilder.Entity("tupenca_back.Model.Penca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CampeonatoId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PremiosEntregados")
                        .HasColumnType("bit");

                    b.Property<int>("PuntajeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampeonatoId");

                    b.HasIndex("PuntajeId");

                    b.ToTable("Pencas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Penca");
                });

            modelBuilder.Entity("tupenca_back.Model.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("HashedPassword")
                        .HasMaxLength(64)
                        .HasColumnType("varbinary(64)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasMaxLength(128)
                        .HasColumnType("varbinary(128)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Personas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Persona");
                });

            modelBuilder.Entity("tupenca_back.Model.PersonaResetPassword", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PersonaId")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.HasIndex("PersonaId");

                    b.ToTable("PersonaResetPassword");
                });

            modelBuilder.Entity("tupenca_back.Model.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantPencas")
                        .HasColumnType("int");

                    b.Property<int>("CantUser")
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int>("LookAndFeel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PercentageCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("tupenca_back.Model.Prediccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("PencaId")
                        .HasColumnType("int");

                    b.Property<int?>("PuntajeEquipoLocal")
                        .HasColumnType("int");

                    b.Property<int?>("PuntajeEquipoVisitante")
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("prediccion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Predicciones");
                });

            modelBuilder.Entity("tupenca_back.Model.Premio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Percentage")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Premios");
                });

            modelBuilder.Entity("tupenca_back.Model.Puntaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Resultado")
                        .HasColumnType("int");

                    b.Property<int>("ResultadoExacto")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Puntajes");
                });

            modelBuilder.Entity("tupenca_back.Model.PuntajeUsuarioPenca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PencaId")
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PuntajeUsuarioPencas");
                });

            modelBuilder.Entity("tupenca_back.Model.Resultado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int?>("PuntajeEquipoLocal")
                        .HasColumnType("int");

                    b.Property<int?>("PuntajeEquipoVisitante")
                        .HasColumnType("int");

                    b.Property<int>("resultado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Resultados");
                });

            modelBuilder.Entity("tupenca_back.Model.UserInviteToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PencaId")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.ToTable("UserInviteTokens");
                });

            modelBuilder.Entity("tupenca_back.Model.UsuarioPenca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PencaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("habilitado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PencaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuariosPencas");
                });

            modelBuilder.Entity("tupenca_back.Model.UsuarioPremio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Banco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CuentaBancaria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("PencaId")
                        .HasColumnType("int");

                    b.Property<bool>("PendientePago")
                        .HasColumnType("bit");

                    b.Property<decimal>("Premio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Reclamado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PencaId");

                    b.ToTable("UsuarioPremios");
                });

            modelBuilder.Entity("tupenca_back.Model.Administrador", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("tupenca_back.Model.Funcionario", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasIndex("EmpresaId");

                    b.HasDiscriminator().HasValue("Funcionario");
                });

            modelBuilder.Entity("tupenca_back.Model.PencaCompartida", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Penca");

                    b.Property<decimal>("Commission")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostEntry")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Pozo")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("PencaCompartida");
                });

            modelBuilder.Entity("tupenca_back.Model.PencaEmpresa", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Penca");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasIndex("EmpresaId");

                    b.HasDiscriminator().HasValue("PencaEmpresa");
                });

            modelBuilder.Entity("tupenca_back.Model.Usuario", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.HasDiscriminator().HasValue("Usuario");
                });

            modelBuilder.Entity("CampeonatoEvento", b =>
                {
                    b.HasOne("tupenca_back.Model.Campeonato", null)
                        .WithMany()
                        .HasForeignKey("CampeonatosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tupenca_back.Model.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmpresaUsuario", b =>
                {
                    b.HasOne("tupenca_back.Model.Empresa", null)
                        .WithMany()
                        .HasForeignKey("EmpresasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tupenca_back.Model.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PencaPremio", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", null)
                        .WithMany()
                        .HasForeignKey("PencasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tupenca_back.Model.Premio", null)
                        .WithMany()
                        .HasForeignKey("PremiosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tupenca_back.Model.Campeonato", b =>
                {
                    b.HasOne("tupenca_back.Model.Deporte", "Deporte")
                        .WithMany()
                        .HasForeignKey("DeporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deporte");
                });

            modelBuilder.Entity("tupenca_back.Model.Empresa", b =>
                {
                    b.HasOne("tupenca_back.Model.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("tupenca_back.Model.Evento", b =>
                {
                    b.HasOne("tupenca_back.Model.Equipo", "EquipoLocal")
                        .WithMany()
                        .HasForeignKey("EquipoLocalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tupenca_back.Model.Equipo", "EquipoVisitante")
                        .WithMany()
                        .HasForeignKey("EquipoVisitanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EquipoLocal");

                    b.Navigation("EquipoVisitante");
                });

            modelBuilder.Entity("tupenca_back.Model.Penca", b =>
                {
                    b.HasOne("tupenca_back.Model.Campeonato", "Campeonato")
                        .WithMany("Pencas")
                        .HasForeignKey("CampeonatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tupenca_back.Model.Puntaje", "Puntaje")
                        .WithMany()
                        .HasForeignKey("PuntajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campeonato");

                    b.Navigation("Puntaje");
                });

            modelBuilder.Entity("tupenca_back.Model.PersonaResetPassword", b =>
                {
                    b.HasOne("tupenca_back.Model.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("tupenca_back.Model.Prediccion", b =>
                {
                    b.HasOne("tupenca_back.Model.Evento", null)
                        .WithMany("Predicciones")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tupenca_back.Model.PuntajeUsuarioPenca", b =>
                {
                    b.HasOne("tupenca_back.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("tupenca_back.Model.UsuarioPenca", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", "Penca")
                        .WithMany("UsuariosPencas")
                        .HasForeignKey("PencaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tupenca_back.Model.Usuario", "Usuario")
                        .WithMany("UsuariosPencas")
                        .HasForeignKey("UsuarioId")
                        .IsRequired();

                    b.Navigation("Penca");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("tupenca_back.Model.UsuarioPremio", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", "Penca")
                        .WithMany()
                        .HasForeignKey("PencaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Penca");
                });

            modelBuilder.Entity("tupenca_back.Model.Funcionario", b =>
                {
                    b.HasOne("tupenca_back.Model.Empresa", "Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("tupenca_back.Model.PencaEmpresa", b =>
                {
                    b.HasOne("tupenca_back.Model.Empresa", "Empresa")
                        .WithMany("Pencas")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("tupenca_back.Model.Campeonato", b =>
                {
                    b.Navigation("Pencas");
                });

            modelBuilder.Entity("tupenca_back.Model.Empresa", b =>
                {
                    b.Navigation("Funcionarios");

                    b.Navigation("Pencas");
                });

            modelBuilder.Entity("tupenca_back.Model.Evento", b =>
                {
                    b.Navigation("Predicciones");
                });

            modelBuilder.Entity("tupenca_back.Model.Penca", b =>
                {
                    b.Navigation("UsuariosPencas");
                });

            modelBuilder.Entity("tupenca_back.Model.Usuario", b =>
                {
                    b.Navigation("UsuariosPencas");
                });
#pragma warning restore 612, 618
        }
    }
}
