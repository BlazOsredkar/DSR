// Naloga 5: Podatkovni kontekst za Entity Framework Core
// Ta razred poveže naše modele s SQL Server bazo podatkov
// Code First pristop - iz razredov generiramo tabele

using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabele v bazi - vsak DbSet je ena tabela
        public DbSet<Uporabnik> Uporabniki { get; set; }
        public DbSet<Avto> Avti { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Naloga 5: Nastavitev relacij med tabelami
            // Rezervacija -> Uporabnik (en uporabnik ima več rezervacij)
            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Uporabnik)
                .WithMany(u => u.Rezervacije)
                .HasForeignKey(r => r.UporabnikId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rezervacija -> Avto (en avto ima več rezervacij)
            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Avto)
                .WithMany(a => a.Rezervacije)
                .HasForeignKey(r => r.AvtoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed podatki za testiranje (Naloga 1 - statični prikaz)
            modelBuilder.Entity<Avto>().HasData(
                new Avto { Id = 1, Znamka = "Volkswagen", Model = "Golf", Barva = "Siva", RegistrskaStevilka = "LJ AB-123", Letnik = 2020, SteviloSedezev = 5, CenaNaDan = 45.99, ProstorninaMotorja = 1.6, DatumZadnjegaServisa = new DateTime(2024, 6, 15) },
                new Avto { Id = 2, Znamka = "BMW", Model = "X5", Barva = "Bela", RegistrskaStevilka = "LJ CD-456", Letnik = 2022, SteviloSedezev = 7, CenaNaDan = 120.00, ProstorninaMotorja = 3.0, DatumZadnjegaServisa = new DateTime(2024, 11, 20) },
                new Avto { Id = 3, Znamka = "Toyota", Model = "Yaris", Barva = "Rdeča", RegistrskaStevilka = "LJ EF-789", Letnik = 2021, SteviloSedezev = 5, CenaNaDan = 35.50, ProstorninaMotorja = 1.0, DatumZadnjegaServisa = new DateTime(2025, 1, 10) }
            );

            modelBuilder.Entity<Uporabnik>().HasData(
                new Uporabnik { Id = 1, Ime = "Janez", Priimek = "Novak", Naslov = "Slovenska cesta 1", PostnaStevilka = "1000", Posta = "Ljubljana", Drzava = "Slovenija", ENaslov = "janez.novak@email.com", Emso = "0101990500123", DatumRojstva = new DateTime(1990, 1, 1), Starost = 35, SkupniZnesek = 250.00 },
                new Uporabnik { Id = 2, Ime = "Maja", Priimek = "Kovač", Naslov = "Mariborska ulica 5", PostnaStevilka = "2000", Posta = "Maribor", Drzava = "Slovenija", ENaslov = "maja.kovac@email.com", Emso = "1505995500456", DatumRojstva = new DateTime(1995, 5, 15), Starost = 30, SkupniZnesek = 120.50 }
            );

            modelBuilder.Entity<Rezervacija>().HasData(
                new Rezervacija { Id = 1, DatumOd = new DateTime(2025, 7, 1), DatumDo = new DateTime(2025, 7, 5), Opombe = "Brez opomb", Status = "Zaključena", SteviloDni = 4, SkupnaCena = 183.96, UporabnikId = 1, AvtoId = 1 },
                new Rezervacija { Id = 2, DatumOd = new DateTime(2025, 8, 10), DatumDo = new DateTime(2025, 8, 15), Opombe = "Potreben otroški sedež", Status = "Aktivna", SteviloDni = 5, SkupnaCena = 177.50, UporabnikId = 2, AvtoId = 3 }
            );
        }
    }
}
