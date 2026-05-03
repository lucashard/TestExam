namespace TestExam.Domain;

/// <summary>
/// Relaciona un operario con la duración que participa dentro de una etapa.
/// </summary>
public class OperarioEtapa
{
    /// <summary>
    /// Obtiene o establece el identificador de la relación.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador de la etapa.
    /// </summary>
    public int EtapaId { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del operario.
    /// </summary>
    public int OperarioId { get; set; }

    /// <summary>
    /// Obtiene o establece el operario asignado.
    /// </summary>
    public Operarios? Operario { get; set; }

    /// <summary>
    /// Obtiene o establece la duración de trabajo del operario en la etapa.
    /// </summary>
    public decimal Duracion { get; set; }
}
