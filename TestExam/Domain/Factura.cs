namespace TestExam.Domain;

/// <summary>
/// Representa una factura con su cliente, materias primas y etapas asociadas.
/// </summary>
public class Factura
{
    /// <summary>
    /// Obtiene o establece el identificador de la factura.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del cliente asociado.
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Obtiene o establece el cliente asociado a la factura.
    /// </summary>
    public Cliente? Cliente { get; set; }

    /// <summary>
    /// Obtiene o establece la colección de materias primas de la factura.
    /// </summary>
    public List<MateriaPrima> MateriasPrimas { get; set; } = [];

    /// <summary>
    /// Obtiene o establece la colección de etapas de la factura.
    /// </summary>
    public List<Etapas> Etapas { get; set; } = [];
}
