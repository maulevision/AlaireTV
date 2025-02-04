using System;
using LibVLCSharp.Shared;

public class MediaPlayerManager : IDisposable
{
    private LibVLC _libVLC;
    private MediaPlayer _mediaPlayer;
    private bool _isRecording;

    public event EventHandler EndReached;

    public MediaPlayerManager()
    {
        Core.Initialize();
        _libVLC = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVLC);
        _mediaPlayer.EndReached += (sender, e) => EndReached?.Invoke(this, EventArgs.Empty);
    }

    // ... (métodos Play, Pause, Stop, etc. se mantienen igual)

    public void StartRecording(string outputPath)
    {
        // Crea un nuevo Media con opciones de grabación
        using (var media = new Media(_libVLC, new Uri("rtsp://tu_stream_url")))
        {
            media.AddOption($":sout=#file{{dst={outputPath}}}");
            _mediaPlayer.Play(media);
            _isRecording = true;
        }
    }

    public void StopRecording()
    {
        _mediaPlayer.Stop();
        _isRecording = false;
    }

    public void AddBanner(string bannerPath, int x, int y)
    {
        // Configura el banner en el Media actual
        if (_mediaPlayer.Media != null)
        {
            _mediaPlayer.Media.AddOption($":sub-file={bannerPath}");
            _mediaPlayer.Media.AddOption($":sub-filter=logo:logo-x={x}:logo-y={y}");
        }
    }

    // ... (el resto del código se mantiene igual)
}