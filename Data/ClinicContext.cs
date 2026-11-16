using Microsoft.EntityFrameworkCore;
using ClinicSupplyTracker.Models;

namespace ClinicSupplyTracker.Data;

public class ClinicContext : DbContext
{
    public ClinicContext(DbContextOptions<ClinicContext> options)
        : base(options)
    {
    }

    public DbSet<SupplyRequest> SupplyRequests { get; set; }
}