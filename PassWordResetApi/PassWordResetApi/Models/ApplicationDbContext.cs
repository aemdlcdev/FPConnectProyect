using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PassWordResetApi.domain;

namespace PassWordResetApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Profesores> Profesores { get; set; }
    }
}

