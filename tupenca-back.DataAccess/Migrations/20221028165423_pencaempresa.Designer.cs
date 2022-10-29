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
    [Migration("20221028165423_pencaempresa")]
    partial class pencaempresa
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

            modelBuilder.Entity("tupenca_back.Model.Campeonato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DeporteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("ImagenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Deportes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Futbol"
                        });
                });

            modelBuilder.Entity("tupenca_back.Model.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("RUT")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Razonsocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("tupenca_back.Model.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Qatar"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Ecuador"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Senegal"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Holanda"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Portugal"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Ghana"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Uruguay"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Corea del Sur"
                        });
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

                    b.HasKey("Id");

                    b.HasIndex("EquipoLocalId");

                    b.HasIndex("EquipoVisitanteId");

                    b.ToTable("Eventos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EquipoLocalId = 1,
                            EquipoVisitanteId = 2,
                            FechaInicial = new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            EquipoLocalId = 3,
                            EquipoVisitanteId = 4,
                            FechaInicial = new DateTime(2022, 11, 21, 7, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            EquipoLocalId = 7,
                            EquipoVisitanteId = 8,
                            FechaInicial = new DateTime(2022, 11, 24, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            EquipoLocalId = 5,
                            EquipoVisitanteId = 6,
                            FechaInicial = new DateTime(2022, 11, 24, 13, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            EquipoLocalId = 1,
                            EquipoVisitanteId = 3,
                            FechaInicial = new DateTime(2022, 11, 25, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            EquipoLocalId = 4,
                            EquipoVisitanteId = 2,
                            FechaInicial = new DateTime(2022, 11, 25, 13, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            EquipoLocalId = 8,
                            EquipoVisitanteId = 6,
                            FechaInicial = new DateTime(2022, 11, 28, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            EquipoLocalId = 5,
                            EquipoVisitanteId = 7,
                            FechaInicial = new DateTime(2022, 11, 28, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            EquipoLocalId = 2,
                            EquipoVisitanteId = 3,
                            FechaInicial = new DateTime(2022, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            EquipoLocalId = 4,
                            EquipoVisitanteId = 1,
                            FechaInicial = new DateTime(2022, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 11,
                            EquipoLocalId = 6,
                            EquipoVisitanteId = 7,
                            FechaInicial = new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 12,
                            EquipoLocalId = 8,
                            EquipoVisitanteId = 5,
                            FechaInicial = new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        });
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

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampeonatoId");

                    b.HasIndex("EmpresaId");

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

            modelBuilder.Entity("tupenca_back.Model.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantUser")
                        .HasColumnType("int");

                    b.Property<int>("LookAndFeel")
                        .HasColumnType("int");

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

                    b.Property<int?>("PencaId")
                        .HasColumnType("int");

                    b.Property<int?>("PuntajeEquipoLocal")
                        .HasColumnType("int");

                    b.Property<int?>("PuntajeEquipoVisitante")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("prediccion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PencaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Predicciones");
                });

            modelBuilder.Entity("tupenca_back.Model.Premio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PencaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Percentage")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PencaId");

                    b.ToTable("Premios");
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

            modelBuilder.Entity("tupenca_back.Model.Administrador", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("tupenca_back.Model.Funcionario", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.Property<int?>("EmpresaId")
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

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.HasIndex("PlanId");

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

            modelBuilder.Entity("tupenca_back.Model.Campeonato", b =>
                {
                    b.HasOne("tupenca_back.Model.Deporte", "Deporte")
                        .WithMany()
                        .HasForeignKey("DeporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deporte");
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

                    b.HasOne("tupenca_back.Model.Empresa", "Empresa")
                        .WithMany("Pencas")
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Campeonato");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("tupenca_back.Model.Prediccion", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", null)
                        .WithMany("Predicciones")
                        .HasForeignKey("PencaId");

                    b.HasOne("tupenca_back.Model.Usuario", "Usuario")
                        .WithMany("Predicciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("tupenca_back.Model.Premio", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", null)
                        .WithMany("Premios")
                        .HasForeignKey("PencaId");
                });

            modelBuilder.Entity("tupenca_back.Model.Funcionario", b =>
                {
                    b.HasOne("tupenca_back.Model.Empresa", "Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("tupenca_back.Model.PencaEmpresa", b =>
                {
                    b.HasOne("tupenca_back.Model.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
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

            modelBuilder.Entity("tupenca_back.Model.Penca", b =>
                {
                    b.Navigation("Predicciones");

                    b.Navigation("Premios");
                });

            modelBuilder.Entity("tupenca_back.Model.Usuario", b =>
                {
                    b.Navigation("Predicciones");
                });
#pragma warning restore 612, 618
        }
    }
}