namespace TestExam;

public static class ServiceGuard
{
    public static void Required(string? value, string propertyName)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        throw new ErrorException(
            StatusCodes.Status400BadRequest,
            "Error de validacion",
            $"La propiedad '{propertyName}' es obligatoria.");
    }

    public static void Positive(int value, string propertyName)
    {
        if (value > 0)
        {
            return;
        }

        throw new ErrorException(
            StatusCodes.Status400BadRequest,
            "Error de validacion",
            $"La propiedad '{propertyName}' debe ser mayor a 0.");
    }

    public static void Positive(decimal value, string propertyName)
    {
        if (value > 0)
        {
            return;
        }

        throw new ErrorException(
            StatusCodes.Status400BadRequest,
            "Error de validacion",
            $"La propiedad '{propertyName}' debe ser mayor a 0.");
    }
}

