public class Programacion
{
    public string Titulo { get; set; }
    public DateTime HoraInicio { get; set; }
    public TimeSpan Duracion { get; set; }
    public string TipoContenido { get; set; }
    public string RutaContenido { get; set; }

    public Programacion(string titulo, DateTime horaInicio, TimeSpan duracion, string tipoContenido, string rutaContenido)
    {
        Titulo = titulo;
        HoraInicio = horaInicio;
        Duracion = duracion;
        TipoContenido = tipoContenido;
        RutaContenido = rutaContenido;
    }
}
