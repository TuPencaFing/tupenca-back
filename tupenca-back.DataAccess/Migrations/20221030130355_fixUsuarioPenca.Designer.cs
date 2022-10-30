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
    [Migration("20221030130355_fixUsuarioPenca")]
    partial class fixUsuarioPenca
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FechaCreacion = new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            RUT = "214873040018",
                            Razonsocial = "McDonald's S.A."
                        },
                        new
                        {
                            Id = 2,
                            FechaCreacion = new DateTime(2022, 11, 21, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            RUT = "304001821487",
                            Razonsocial = "BMW Ibérica S.A."
                        },
                        new
                        {
                            Id = 3,
                            FechaCreacion = new DateTime(2022, 11, 21, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            RUT = "821473040018",
                            Razonsocial = "Air Europa Líneas Aéreas S.A."
                        },
                        new
                        {
                            Id = 4,
                            FechaCreacion = new DateTime(2022, 11, 21, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            RUT = "040001821487",
                            Razonsocial = "Punto FA S.L."
                        });
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

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampeonatoId");

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

                    b.Property<int>("score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PencaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuariosPencas");
                });

            modelBuilder.Entity("tupenca_back.Model.Administrador", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.HasDiscriminator().HasValue("Administrador");

                    b.HasData(
                        new
                        {
                            Id = 10,
                            Email = "mati98bor@gmail.com",
                            HashedPassword = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 },
                            PasswordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 },
                            UserName = "Matibor"
                        },
                        new
                        {
                            Id = 11,
                            Email = "user@example.com",
                            HashedPassword = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 },
                            PasswordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 },
                            UserName = "Administrador123"
                        });
                });

            modelBuilder.Entity("tupenca_back.Model.Funcionario", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasIndex("EmpresaId");

                    b.HasDiscriminator().HasValue("Funcionario");

                    b.HasData(
                        new
                        {
                            Id = 8,
                            Email = "mati98bor@gmail.com",
                            HashedPassword = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 },
                            PasswordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 },
                            UserName = "Matibor",
                            EmpresaId = 1
                        },
                        new
                        {
                            Id = 9,
                            Email = "user@example.com",
                            HashedPassword = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 },
                            PasswordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 },
                            UserName = "funcionario123",
                            EmpresaId = 2
                        });
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

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("PlanId");

                    b.HasDiscriminator().HasValue("PencaEmpresa");
                });

            modelBuilder.Entity("tupenca_back.Model.Usuario", b =>
                {
                    b.HasBaseType("tupenca_back.Model.Persona");

                    b.HasDiscriminator().HasValue("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 49,
                            Email = "mati98bor@gmail.com",
                            HashedPassword = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 },
                            PasswordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 },
                            UserName = "Matibor"
                        },
                        new
                        {
                            Id = 50,
                            Email = "user123@example.com",
                            HashedPassword = new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 },
                            PasswordSalt = new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 },
                            UserName = "user123"
                        });
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

                    b.Navigation("Campeonato");
                });

            modelBuilder.Entity("tupenca_back.Model.Prediccion", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", null)
                        .WithMany("Predicciones")
                        .HasForeignKey("PencaId");
                });

            modelBuilder.Entity("tupenca_back.Model.Premio", b =>
                {
                    b.HasOne("tupenca_back.Model.Penca", null)
                        .WithMany("Premios")
                        .HasForeignKey("PencaId");
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

                    b.HasOne("tupenca_back.Model.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

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
