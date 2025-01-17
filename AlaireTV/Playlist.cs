using System.Collections.Generic;

namespace AlaireTV
{
    public class Playlist
    {
        public string Nombre { get; set; }
        public List<Contenido> Contenidos { get; set; }

        public Playlist(string nombre)
        {
            Nombre = nombre;
            Contenidos = new List<Contenido>();
        }
    }
}
