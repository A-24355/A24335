using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopaAmx.Models;

namespace RopaAmx.Controllers;

public class CategoriaController : Controller
{
    private readonly RopaAmxContext _context;

    public CategoriaController(RopaAmxContext context) => _context = context;

    // /Categoria/hombre  |  /Categoria/mujer  |  /Categoria/ninos
    public async Task<IActionResult> Ver(string slug)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Slug == slug);

        if (categoria == null) return NotFound();

        return View(categoria);
    }
}
