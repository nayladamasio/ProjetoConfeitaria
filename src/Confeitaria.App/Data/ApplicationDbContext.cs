using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Confeitaria.App.ViewModels;

namespace Confeitaria.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Confeitaria.App.ViewModels.FaleConoscoViewModel> FaleConoscoViewModel { get; set; }
    }
}
