namespace RopaAmx.Models;

public partial class Usuario
{
    public Guid Id { get; set; }
    public string? NombreCompleto { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Password { get; set; }
    public bool? Activo { get; set; }
    public DateTime? FechaRegistro { get; set; }
}
