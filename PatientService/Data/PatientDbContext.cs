using Microsoft.EntityFrameworkCore;
using PatientService.Models;

namespace PatientService.Data;

public class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.Email)
            .IsUnique();

        modelBuilder.Entity<Patient>()
            .Property(p => p.CreatedDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
