namespace TestExam.Domain;

/// <summary>
/// Representa una dirección física básica.
/// </summary>
public class Direccion
{
    /// <summary>
    /// Obtiene o establece el identificador de la dirección.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre de la calle.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece la altura o numeración de la dirección.
    /// </summary>
    public int Altura { get; set; }
}
