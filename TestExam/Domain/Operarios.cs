namespace TestExam.Domain;

/// <summary>
/// Representa un operario.
/// </summary>
public class Operarios
{
    /// <summary>
    /// Obtiene o establece el identificador del operario.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del operario.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;
}
