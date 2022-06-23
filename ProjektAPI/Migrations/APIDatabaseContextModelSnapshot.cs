﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektAPI.Database;

#nullable disable

namespace ProjektAPI.Migrations
{
    [DbContext(typeof(APIDatabaseContext))]
    partial class APIDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProjektAPI.Models.EmisjaModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FilmId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Godzina")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SalaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SalaId");

                    b.ToTable("Emisja");
                });

            modelBuilder.Entity("ProjektAPI.Models.FilmModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Cena")
                        .HasColumnType("real");

                    b.Property<int>("CzasTrwania")
                        .HasColumnType("int");

                    b.Property<string>("Gatunek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("OgraniczeniaWiek")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Filmy");
                });

            modelBuilder.Entity("ProjektAPI.Models.KlientModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataUrodzenia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("KodPocztowy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Miasto")
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

                    b.Property<Guid>("UzytkownikId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UzytkownikId")
                        .IsUnique();

                    b.ToTable("Klienci");
                });

            modelBuilder.Entity("ProjektAPI.Models.RezerwacjaModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmisjaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KlientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Miejsce")
                        .HasColumnType("int");

                    b.Property<int>("Rzad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmisjaId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("KlientId");

                    b.ToTable("Rezerwacja");
                });

            modelBuilder.Entity("ProjektAPI.Models.SalaModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IloscMiejsc")
                        .HasColumnType("int");

                    b.Property<int>("IloscRzedow")
                        .HasColumnType("int");

                    b.Property<string>("NazwaSali")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("NazwaSali")
                        .IsUnique();

                    b.ToTable("SaleKinowe");
                });

            modelBuilder.Entity("ProjektAPI.Models.UzytkownikModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Login");
                });

            modelBuilder.Entity("ProjektAPI.Models.ZajeteMiejsca", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmisjaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Miejsce")
                        .HasColumnType("int");

                    b.Property<int>("Rzad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("ZajeteMiejsca");
                });

            modelBuilder.Entity("ProjektAPI.Models.EmisjaModel", b =>
                {
                    b.HasOne("ProjektAPI.Models.FilmModel", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjektAPI.Models.SalaModel", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Sala");
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

            modelBuilder.Entity("ProjektAPI.Models.RezerwacjaModel", b =>
                {
                    b.HasOne("ProjektAPI.Models.EmisjaModel", "Emisja")
                        .WithMany("Rezerwacje")
                        .HasForeignKey("EmisjaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjektAPI.Models.KlientModel", "Klient")
                        .WithMany("Rezerwacje")
                        .HasForeignKey("KlientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emisja");

                    b.Navigation("Klient");
                });

            modelBuilder.Entity("ProjektAPI.Models.EmisjaModel", b =>
                {
                    b.Navigation("Rezerwacje");
                });

            modelBuilder.Entity("ProjektAPI.Models.KlientModel", b =>
                {
                    b.Navigation("Rezerwacje");
                });

            modelBuilder.Entity("ProjektAPI.Models.UzytkownikModel", b =>
                {
                    b.Navigation("Klient");
                });
#pragma warning restore 612, 618
        }
    }
}
