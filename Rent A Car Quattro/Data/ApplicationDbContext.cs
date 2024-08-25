using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rent_A_Car_Quattro.Models;

namespace Rent_A_Car_Quattro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vozilo> Vozilo { get; set; }
        public DbSet<Poslovnica> Poslovnica { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<AktivneRezervacije> AktivneRezervacije { get; set; }
        public DbSet<DostupnaVozila> DostupnaVozila { get; set; }
        public DbSet<Placanje> Placanje { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vozilo>().ToTable("Vozilo");
            modelBuilder.Entity<Poslovnica>().ToTable("Poslovnica");
            modelBuilder.Entity<Rezervacija>().ToTable("Rezervacija");
            modelBuilder.Entity<AktivneRezervacije>().ToTable("AktivneRezervacije");
            modelBuilder.Entity<DostupnaVozila>().ToTable("DostupnaVozila");
            modelBuilder.Entity<Placanje>().ToTable("Placanje");


            base.OnModelCreating(modelBuilder);
        }
    }
}
