using Microsoft.EntityFrameworkCore;
using QianQian_Novel.Entity.Entity;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace QianQian_Novel.PostgreSQL
{
    public class QianContext: DbContext
    {
        public QianContext([NotNull] DbContextOptions options) : base(options) { }

        public DbSet<BaseUser> BaseUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
