using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopaAmx.Models;

namespace RopaAmx.Controllers;

public class HomeController : Controller
{
    private readonly RopaAmxContext _context;

    public HomeController(RopaAmxContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        ViewBag.MasVendidos = await _context.Productos
            .Include(p => p.Categoria)
            .Where(p => p.EsMasVendido)
            .OrderBy(p => p.Nombre)
            .ToListAsync();

        ViewBag.Rebajas = await _context.Productos
            .Include(p => p.Categoria)
            .Where(p => p.PrecioRebaja != null)
            .OrderBy(p => p.Nombre)
            .ToListAsync();

        return View();
    }

    public IActionResult Contacto() => View();

    // Buscador del menú y mosaicos (Camisas, Sudaderas...)
    public async Task<IActionResult> Buscar(string? q, string? tipo)
    {
        var query = _context.Productos.Include(p => p.Categoria).AsQueryable();

        if (!string.IsNullOrWhiteSpace(tipo))
            query = query.Where(p => p.Tipo == tipo);

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Nombre.Contains(q) ||
                                     (p.Descripcion != null && p.Descripcion.Contains(q)));

        ViewBag.Titulo = !string.IsNullOrWhiteSpace(tipo) ? tipo : $"Resultados para “{q}”";
        return View(await query.OrderBy(p => p.Nombre).ToListAsync());
    }

    public IActionResult Error() => Content("Ocurrió un error. Intenta de nuevo.");
}
