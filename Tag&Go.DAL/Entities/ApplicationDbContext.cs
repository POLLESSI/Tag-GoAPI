using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag_Go.DAL.Entities
{
    public class ApplicationDbContext : DbContext
    {
    #nullable disable
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<OurEntity> ourEntities { get; set; }
        public object NUsers { get; set; }
    }
}
