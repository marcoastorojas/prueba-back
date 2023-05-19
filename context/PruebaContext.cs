using Microsoft.EntityFrameworkCore;
using prueba_backend.Models;
namespace prueba_backend;

public class PruebaContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Perfil> perfiles { get; set; }
    public PruebaContext(DbContextOptions<PruebaContext> options) : base(options) { }
}