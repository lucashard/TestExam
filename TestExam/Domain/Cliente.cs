namespace TestExam.Domain;

/// <summary>
/// Representa un cliente con sus datos personales.
/// </summary>
public class Cliente
{
    /// <summary>
    /// Obtiene o establece el identificador del cliente.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del cliente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el apellido del cliente.
    /// </summary>
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el documento identificatorio del cliente.
    /// </summary>
    public string Documento { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el correo electrónico del cliente.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el teléfono del cliente.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el identificador de la dirección asociada.
    /// </summary>
    public int DireccionId { get; set; }

    /// <summary>
    /// Obtiene o establece la dirección del cliente.
    /// </summary>
    public Direccion? Direccion { get; set; }
}
