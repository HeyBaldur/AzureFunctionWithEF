using AzureFunctionWithEF.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureFunctionWithEF.Common.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<DimAccount> DimAccount { get; set; }
    }
}
