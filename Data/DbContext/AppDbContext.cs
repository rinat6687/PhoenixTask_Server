using Microsoft.EntityFrameworkCore;
using PhoenixTaskApp.Data.Modals;


namespace PhoenixTaskApp.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual  DbSet<OperationType> OperationTypes { get; set; }
        public virtual DbSet<AccountTransactions> AccountTransactions { get; set; }


    }
}
