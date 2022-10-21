using Microsoft.EntityFrameworkCore;
using tupenca_back.Model;

namespace tupenca_back.DataAccess
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Persona>? Personas { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Administrador>? Administradores { get; set; }
        public DbSet<Funcionario>? Funcionarios { get; set; }


        public DbSet<Campeonato>? Campeonatos { get; set; }

    }

}
