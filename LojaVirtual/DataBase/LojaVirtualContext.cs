using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.DataBase
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes{ get; set; }
        public DbSet<NewsLetterEmail> NewsLetterEmails { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
    }
}
