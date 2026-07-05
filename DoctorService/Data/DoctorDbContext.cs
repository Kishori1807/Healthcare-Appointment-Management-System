using Microsoft.EntityFrameworkCore;
using DoctorService.Models;

namespace DoctorService.Data;

public class DoctorDbContext(DbContextOptions<DoctorDbContext> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors => Set<Doctor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .HasIndex(d => new { d.FullName, d.Specialization });
    }
}
