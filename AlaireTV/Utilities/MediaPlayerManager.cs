using LibVLCSharp.Shared;
using System;

namespace AlaireTV.Utilities
{
    public class MediaPlayerManager : IDisposable
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        public MediaPlayerManager()
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
        }

        public void Play(string filePath)
        {
            var media = new Media(_libVLC, new Uri(filePath));
            _mediaPlayer.Media = media;
            _mediaPlayer.Play();
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
        }

        public void SetVolume(int volume)
        {
            _mediaPlayer.Volume = volume;
        }

        public void Dispose()
        {
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }
    }
}