using Microsoft.EntityFrameworkCore;
using Session.DistributedSessionStore.SQL.WebApi2.Models;

namespace Session.DistributedSessionStore.SQL.WebApi2.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<UserSession> UserSessions { get; set; }
}

