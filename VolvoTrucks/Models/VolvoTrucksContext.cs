using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VolvoTrucks.Models
{
    public partial class VolvoTrucksContext : DbContext
    {
        public VolvoTrucksContext()
        {
        }

        public VolvoTrucksContext(DbContextOptions<VolvoTrucksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TruckModels> TruckModels { get; set; }
        public virtual DbSet<Trucks> Trucks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=VolvoTrucks;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<TruckModels>(entity =>
            {
                entity.HasKey(e => e.ModelId);
            });

            modelBuilder.Entity<Trucks>(entity =>
            {
                entity.Property(e => e.Chassis).HasMaxLength(50);

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Trucks)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trucks_TruckModels");
            });
        }
    }
}
