using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMedia.Data.Models;
using System;
using System.Linq;

namespace MyMedia.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<ItemEncomenda> ItensEncomenda { get; set; }
        public DbSet<ModoDisponibilidade> ModosDisponibilidade { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Produto>()
                .Property(p => p.PrecoBase)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Produto>()
                .Property(p => p.PrecoFinal)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Encomenda>()
                .Property(e => e.ValorTotal)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<ItemEncomenda>()
                .Property(ie => ie.PrecoVendaUnitario)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<ItemEncomenda>()
                .HasOne(ie => ie.Produto)
                .WithMany() 
                .HasForeignKey(ie => ie.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.FornecedorId)
                .IsRequired();
        }
    }
}