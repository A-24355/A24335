using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RopaAmx.Models;

namespace RopaAmx.Controllers;

public class ProductosController : Controller
{
    private readonly RopaAmxContext _context;

    public ProductosController(RopaAmxContext context) => _context = context;

    // Botón "Añadir productos"
    public async Task<IActionResult> Crear(bool masVendido = false, bool rebaja = false)
    {
        await CargarListas();
        return View(new Producto { EsMasVendido = masVendido, Existencia = 1 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Producto producto)
    {
        ModelState.Remove(nameof(Producto.Categoria));

        if (!ModelState.IsValid)
        {
            await CargarListas();
            return View(producto);
        }

        producto.Id = Guid.NewGuid();
        producto.FechaRegistro = DateTime.Now;
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Eliminar(Guid id)
{
    var producto = await _context.Productos.FindAsync(id);

    if (producto != null)
    {
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Index", "Home");
}

    private async Task CargarListas()
    {
        ViewBag.Categorias = new SelectList(
            await _context.Categorias.OrderBy(c => c.Id).ToListAsync(), "Id", "Nombre");
        ViewBag.Tipos = new SelectList(new[] { "Camisas", "Sudaderas", "Pantalones", "Accesorios" });
    }
}
