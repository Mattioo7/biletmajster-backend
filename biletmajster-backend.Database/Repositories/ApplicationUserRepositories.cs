using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biletmajster_backend.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories
{
    public class ApplicationUserRepositories :BaseRepository<ApplicationUser>, IApplicationUserRepositories
    {
        public ApplicationUserRepositories(ApplicationDbContext dbContext) : base(dbContext) { }
        protected override DbSet<ApplicationUser> DbSet => mDbContext.Users;
    }
}
