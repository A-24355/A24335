using System.ComponentModel.DataAnnotations;

namespace RopaAmx.Models;

public partial class Producto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 999999)]
    public decimal Precio { get; set; }

    // Si tiene valor, el producto aparece en REBAJAS
    public decimal? PrecioRebaja { get; set; }

    // Camisas, Sudaderas, Pantalones, Accesorios
    [StringLength(30)]
    public string? Tipo { get; set; }

    public int Existencia { get; set; }

    public bool EsMasVendido { get; set; }

    [StringLength(250)]
    public string? ImagenUrl { get; set; }

    [Display(Name = "Categoría")]
    public int IdCategoria { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Categoria? Categoria { get; set; }
}
