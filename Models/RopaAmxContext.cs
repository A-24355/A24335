using Microsoft.EntityFrameworkCore;

namespace RopaAmx.Models;

public partial class RopaAmxContext : DbContext
{
    public RopaAmxContext(DbContextOptions<RopaAmxContext> options) : base(options) { }

    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(e =>
        {
            e.Property(x => x.Nombre).HasMaxLength(50).IsUnicode(false);
            e.Property(x => x.Slug).HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(e =>
        {
            e.Property(x => x.Id).HasDefaultValueSql("(newid())");
            e.Property(x => x.Nombre).HasMaxLength(100).IsUnicode(false);
            e.Property(x => x.Descripcion).HasMaxLength(250).IsUnicode(false);
            e.Property(x => x.Tipo).HasMaxLength(30).IsUnicode(false);
            e.Property(x => x.ImagenUrl).HasMaxLength(250).IsUnicode(false);
            e.Property(x => x.Precio).HasColumnType("decimal(10, 2)");
            e.Property(x => x.PrecioRebaja).HasColumnType("decimal(10, 2)");
            e.Property(x => x.FechaRegistro).HasColumnType("datetime").HasDefaultValueSql("(getdate())");

            e.HasOne(x => x.Categoria).WithMany(c => c.Productos)
             .HasForeignKey(x => x.IdCategoria)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_Productos_Categorias");
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.Property(x => x.Id).HasDefaultValueSql("(newid())");
            e.Property(x => x.NombreCompleto).HasMaxLength(120).IsUnicode(false);
            e.Property(x => x.CorreoElectronico).HasMaxLength(150).IsUnicode(false);
            e.Property(x => x.Password).HasMaxLength(250).IsUnicode(false);
            e.Property(x => x.Activo).HasDefaultValue(true);
            e.Property(x => x.FechaRegistro).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        });
    }
}
