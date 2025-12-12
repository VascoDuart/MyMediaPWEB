using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMedia.Data.Models;

namespace MyMedia.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configuração de Chave Estrangeira explícita, se necessário
            builder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany() // Relação com ApplicationUser
                .HasForeignKey(p => p.FornecedorId);
        }*/
    }
}
