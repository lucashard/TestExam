namespace TestExam;

public class ErrorException : Exception
{
    public ErrorException(int statusCode, string mensaje, string descripcion) : base(descripcion)
    {
        StatusCode = statusCode;
        Mensaje = mensaje;
        Descripcion = descripcion;
    }

    public int StatusCode { get; }

    public string Mensaje { get; }

    public string Descripcion { get; }
}

