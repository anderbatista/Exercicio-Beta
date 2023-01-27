using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Models;

namespace ProjetoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subcategoria>()
                .HasOne(subcategoria => subcategoria.Categoria)
                .WithMany(categoria => categoria.Subcategorias)
                .HasForeignKey(subcategoria => subcategoria.CategoriaId);
            
            builder.Entity<Produto>()
                .HasOne(produto => produto.Subcategoria)
                .WithMany(Subcategoria => Subcategoria.Produtos)
                .HasForeignKey(produto => produto.SubcategoriaId);
            
            builder.Entity<Produto>()
                .HasOne(produto => produto.CentroDeDistribuicao)
                .WithMany(centro => centro.Produtos)
                .HasForeignKey(produto => produto.CentroId);
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CentroDistribuicao> CentrosD { get; set; }
    }
}
