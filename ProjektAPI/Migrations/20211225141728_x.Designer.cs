﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektAPI.Models;

namespace ProjektAPI.Migrations
{
    [DbContext(typeof(APIDatabaseContext))]
    [Migration("20211225141728_x")]
    partial class x
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjektAPI.Models.FilmModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Cena")
                        .HasColumnType("real");

                    b.Property<string>("CzasTrwania")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gatunek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OgraniczeniaWiek")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Filmy");
                });

            modelBuilder.Entity("ProjektAPI.Models.KlientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataUrodzenia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("KodPocztowy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Miasto")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumerTelefonu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ulica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UzytkownikId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UzytkownikId")
                        .IsUnique();

                    b.ToTable("Klienci");
                });

            modelBuilder.Entity("ProjektAPI.Models.SalaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IloscMiejsc")
                        .HasColumnType("int");

                    b.Property<int>("IloscRzedow")
                        .HasColumnType("int");

                    b.Property<string>("NazwaSali")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SaleKinowe");
                });

            modelBuilder.Entity("ProjektAPI.Models.UzytkownikModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RodzajUzytkownika")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Login");
                });

            modelBuilder.Entity("ProjektAPI.Models.KlientModel", b =>
                {
                    b.HasOne("ProjektAPI.Models.UzytkownikModel", "Uzytkownik")
                        .WithOne("Klient")
                        .HasForeignKey("ProjektAPI.Models.KlientModel", "UzytkownikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("ProjektAPI.Models.UzytkownikModel", b =>
                {
                    b.Navigation("Klient");
                });
#pragma warning restore 612, 618
        }
    }
}
