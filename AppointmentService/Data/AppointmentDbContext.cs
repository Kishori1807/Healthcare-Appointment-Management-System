using Microsoft.EntityFrameworkCore;
using AppointmentService.Models;

namespace AppointmentService.Data;

public class AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasIndex(a => new { a.PatientId, a.DoctorId, a.AppointmentDate });

        modelBuilder.Entity<Appointment>()
            .Property(a => a.CreatedDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
