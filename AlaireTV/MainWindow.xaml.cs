using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Text.Json;

namespace AlaireTV
{
    public partial class MainWindow : Window
    {
        private MediaPlayerManager _playerManager;
        private List<string> _contentList;
        private List<VideoItem> _scheduledVideos;
        private DispatcherTimer _playbackTimer;
        private DispatcherTimer _commercialTimer;
        private int _currentVideoIndex;
        private string _dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "playout_data.json");

        public MainWindow()
        {
            InitializeComponent();
            _playerManager = new MediaPlayerManager();
            _contentList = new List<string>();
            _scheduledVideos = LoadData() ?? new List<VideoItem>();
            ScheduleDataGrid.ItemsSource = _scheduledVideos;
            VolumeSlider.Value = 50; // Volumen predeterminado

            _playbackTimer = new DispatcherTimer();
            _playbackTimer.Interval = TimeSpan.FromSeconds(1);
            _playbackTimer.Tick += PlaybackTimer_Tick;

            _commercialTimer = new DispatcherTimer();
            _commercialTimer.Interval = TimeSpan.FromMinutes(CommercialIntervalSlider.Value);
            _commercialTimer.Tick += CommercialTimer_Tick;

            _playerManager.EndReached += PlayerManager_EndReached;
        }

        private void AddContentButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = OpenFileDialogToSelectVideo();
            if (!string.IsNullOrEmpty(filePath))
            {
                _contentList.Add(filePath);
                ContentListBox.Items.Add(System.IO.Path.GetFileName(filePath));
            }
        }

        private string OpenFileDialogToSelectVideo()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".mp4"; // Default file extension
            dlg.Filter = "Video files (.mp4)|*.mp4"; // Filter files by extension

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                return dlg.FileName;
            }
            return null;
        }

        private void StartPlaybackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContentListBox.SelectedItem != null)
            {
                string selectedPath = _contentList[ContentListBox.SelectedIndex];
                _playerManager.Play(selectedPath);
                UpdateNowPlayingText(selectedPath);
                _playbackTimer.Start();
            }
        }

        private void SchedulePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContentListBox.SelectedItem != null)
            {
                string selectedPath = _contentList[ContentListBox.SelectedIndex];
                var newItem = new VideoItem()
                {
                    Title = System.IO.Path.GetFileName(selectedPath),
                    VideoPath = selectedPath,
                    Date = DateTime.Now,
                    Duration = TimeSpan.FromMilliseconds(_playerManager.GetDuration()).ToString(@"hh\:mm\:ss")
                };
                _scheduledVideos.Add(newItem);
                ScheduleDataGrid.Items.Refresh();
                SaveData();
            }
        }

        private void StartRecordingButton_Click(object sender, RoutedEventArgs e)
        {
            string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "recording.mp4");
            _playerManager.StartRecording(outputPath);
            NotificationText.Text = "Grabación iniciada.";
        }

        private void StopRecordingButton_Click(object sender, RoutedEventArgs e)
        {
            _playerManager.StopRecording();
            NotificationText.Text = "Grabación detenida.";
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            _playerManager.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _playerManager.Stop();
            NowPlayingText.Text = "Reproducción detenida.";
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _playerManager.SetVolume((int)VolumeSlider.Value);
        }

        private void StartCommercialsButton_Click(object sender, RoutedEventArgs e)
        {
            _commercialTimer.Interval = TimeSpan.FromMinutes(CommercialIntervalSlider.Value);
            _commercialTimer.Start();
            NotificationText.Text = $"Comerciales iniciados con un intervalo de {CommercialIntervalSlider.Value} minutos.";
        }

        private void AddBannerButton_Click(object sender, RoutedEventArgs e)
        {
            string bannerPath = OpenFileDialogToSelectVideo();
            if (!string.IsNullOrEmpty(bannerPath))
            {
                _playerManager.AddBanner(bannerPath, 10, 10);
                NotificationText.Text = "Banner añadido.";
            }
        }

        private void ActivateTransitionButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationText.Text = "Transición activada.";
        }

        private void UpdateNowPlayingText(string path)
        {
            NowPlayingText.Text = $"Reproduciendo: {System.IO.Path.GetFileName(path)}";
        }

        private void ConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementación para abrir la ventana de configuración
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementación para generar y mostrar reportes
        }

        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            ProgressBar.Value = _playerManager.GetCurrentTime();
        }

        private void CommercialTimer_Tick(object sender, EventArgs e)
        {
            // Lógica para insertar comerciales
            NotificationText.Text = "Insertando comerciales...";
        }

        private void PlayerManager_EndReached(object sender, EventArgs e)
        {
            // Lógica para reproducir el siguiente video
            _currentVideoIndex++;
            if (_currentVideoIndex < _contentList.Count)
            {
                _playerManager.Play(_contentList[_currentVideoIndex]);
                UpdateNowPlayingText(_contentList[_currentVideoIndex]);
            }
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(_scheduledVideos);
            File.WriteAllText(_dataFilePath, json);
        }

        private List<VideoItem> LoadData()
        {
            if (File.Exists(_dataFilePath))
            {
                string json = File.ReadAllText(_dataFilePath);
                return JsonSerializer.Deserialize<List<VideoItem>>(json);
            }
            return null;
        }
    }
}