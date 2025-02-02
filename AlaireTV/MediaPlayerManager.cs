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

    public bool IsPlaying()
    {
        return _mediaPlayer.IsPlaying;
    }

    public void SetVolume(int volume)
    {
        _mediaPlayer.Volume = volume;
    }

    public long GetDuration()
    {
        return _mediaPlayer.Media?.Duration ?? 0;
    }

    public long GetCurrentTime()
    {
        return _mediaPlayer.Time;
    }

    public void SetCurrentTime(long time)
    {
        _mediaPlayer.Time = time;
    }

    public void StartRecording(string outputPath)
    {
        _mediaPlayer.AddOption($":sout=#file{{dst={outputPath}}}");
        _mediaPlayer.Record = true;
        _isRecording = true;
    }

    public void StopRecording()
    {
        _mediaPlayer.Record = false;
        _isRecording = false;
    }

    public bool IsRecording()
    {
        return _isRecording;
    }

    public void AddBanner(string bannerPath, int x, int y)
    {
        _mediaPlayer.AddOption($":sub-file={bannerPath}");
        _mediaPlayer.AddOption($":sub-filter=logo:logo-x={x}:logo-y={y}");
    }

    public void Dispose()
    {
        _mediaPlayer.Dispose();
        _libVLC.Dispose();
    }
}