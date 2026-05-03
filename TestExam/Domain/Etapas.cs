namespace TestExam.Domain;

/// <summary>
/// Representa una etapa de un proceso.
/// </summary>
public class Etapas
{
    /// <summary>
    /// Obtiene o establece el identificador de la etapa.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre de la etapa.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece los operarios asignados con su duración individual en la etapa.
    /// </summary>
    public List<OperarioEtapa> OperariosConDuracion { get; set; } = [];
}
