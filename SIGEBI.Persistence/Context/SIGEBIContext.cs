using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities.circulation;
using SIGEBI.Application.DTOs;

namespace SIGEBI.Persistence.Context
{
    public class SIGEBIContext : DbContext
    {
        public SIGEBIContext(DbContextOptions<SIGEBIContext> options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<ReservationHistory> ReservationHistory { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) // Aqui tuve que agregarlo ya que mi base de datos tiene los nombres de tablas en plural...
        //{
        //    modelBuilder.Entity<Reservation>().ToTable("Reservations");
        //    modelBuilder.Entity<ReservationStatus>().ToTable("ReservationStatuses");

        //}

    }

}
