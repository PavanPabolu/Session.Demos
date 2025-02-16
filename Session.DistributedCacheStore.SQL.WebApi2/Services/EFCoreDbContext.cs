using Microsoft.EntityFrameworkCore;

namespace Session.DistributedCacheStore.SQL.WebApi2.Services
{
    public class EFCoreDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {
        }
    }
}
