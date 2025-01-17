using System;
using System.Collections.Generic;
using System.IO;

namespace AlaireTV
{
    public class PlaylistManager
    {
        private string _videoDirectory;

        public PlaylistManager(string videoDirectory)
        {
            _videoDirectory = videoDirectory;
        }

        public List<string> GetRandomPlaylist(int durationMinutes)
        {
            // Lógica para obtener una lista de videos aleatorios basada en la duración deseada
            List<string> videos = new List<string>(Directory.GetFiles(_videoDirectory, "*.mp4"));
            Random rnd = new Random();
            List<string> selectedVideos = new List<string>();

            for (int i = 0; i < durationMinutes / 5; i++) // Supongamos que cada video dura 5 minutos
            {
                selectedVideos.Add(videos[rnd.Next(videos.Count)]);
            }

            return selectedVideos;
        }
    }
}
