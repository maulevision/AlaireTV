using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlaireTV
{
    public class PlaylistManager
    {
        private readonly string _videoDirectory;
        private readonly Random _random;

        public PlaylistManager(string videoDirectory)
        {
            if (string.IsNullOrWhiteSpace(videoDirectory))
                throw new ArgumentException("Video directory path cannot be null or empty.");

            if (!Directory.Exists(videoDirectory))
                throw new DirectoryNotFoundException($"The directory '{videoDirectory}' does not exist.");

            _videoDirectory = videoDirectory;
            _random = new Random();
        }

        /// <summary>
        /// Generates a playlist with videos selected randomly to fit the desired duration.
        /// </summary>
        /// <param name="durationMinutes">Desired total duration in minutes.</param>
        /// <returns>A list of file paths for the selected videos.</returns>
        public List<string> GetRandomPlaylist(int durationMinutes)
        {
            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            List<string> videos = GetAllVideos();
            List<string> selectedVideos = new List<string>();
            int totalDuration = 0;

            while (totalDuration < durationMinutes && videos.Count > 0)
            {
                string selectedVideo = videos[_random.Next(videos.Count)];
                int videoDuration = GetVideoDuration(selectedVideo);

                if (totalDuration + videoDuration <= durationMinutes)
                {
                    selectedVideos.Add(selectedVideo);
                    totalDuration += videoDuration;
                }

                videos.Remove(selectedVideo);
            }

            return selectedVideos;
        }

        /// <summary>
        /// Retrieves all video files from the directory.
        /// </summary>
        /// <returns>A list of file paths for all videos in the directory.</returns>
        public List<string> GetAllVideos()
        {
            return Directory.GetFiles(_videoDirectory, "*.mp4", SearchOption.AllDirectories).ToList();
        }

        /// <summary>
        /// Adds a new video to the playlist directory.
        /// </summary>
        /// <param name="filePath">The full path of the video to add.</param>
        public void AddVideo(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");

            string destinationPath = Path.Combine(_videoDirectory, Path.GetFileName(filePath));

            if (File.Exists(destinationPath))
                throw new InvalidOperationException($"The file '{Path.GetFileName(filePath)}' already exists in the playlist directory.");

            File.Copy(filePath, destinationPath);
        }

        /// <summary>
        /// Deletes a video from the playlist directory.
        /// </summary>
        /// <param name="fileName">The name of the video file to delete.</param>
        public void DeleteVideo(string fileName)
        {
            string filePath = Path.Combine(_videoDirectory, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file '{fileName}' does not exist in the playlist directory.");

            File.Delete(filePath);
        }

        /// <summary>
        /// Shuffles the order of the videos in a playlist.
        /// </summary>
        /// <param name="playlist">The playlist to shuffle.</param>
        public void ShufflePlaylist(List<string> playlist)
        {
            if (playlist == null || playlist.Count == 0)
                throw new ArgumentException("Playlist cannot be null or empty.");

            for (int i = playlist.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (playlist[i], playlist[j]) = (playlist[j], playlist[i]);
            }
        }

        /// <summary>
        /// Calculates the duration of a video in minutes.
        /// </summary>
        /// <param name="filePath">The file path of the video.</param>
        /// <returns>The duration of the video in minutes.</returns>
        private int GetVideoDuration(string filePath)
        {
            // TODO: Implement actual video duration retrieval using a library like MediaToolkit or FFmpeg.
            // For now, return a fixed duration for simulation purposes.
            return 5; // Default duration in minutes for simulation.
        }
    }
}
