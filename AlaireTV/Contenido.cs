public class Contenido
{
    public string Titulo { get; set; }
    public string Ruta { get; set; }
    public TimeSpan Duracion { get; set; }
    public DateTime HoraInicio { get; set; }

    // Constructor para inicializar un nuevo contenido
    public Contenido(string titulo, string ruta, TimeSpan duracion, DateTime horaInicio)
    {
        Titulo = titulo;
        Ruta = ruta;
        Duracion = duracion;
        HoraInicio = horaInicio;
    }
}
