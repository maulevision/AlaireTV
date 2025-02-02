using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Timers; // Aseguramos el uso de System.Timers.Timer
using Microsoft.Win32; // Para abrir cuadros de diálogo de archivos
using MediaToolkit;
using System.Windows.Media;
// Biblioteca para reproducción y grabación de video (necesaria implementación real)

namespace AlaireTV
{
    public partial class MainWindow : Window
    {
        private List<ScheduledContent> _scheduledContentList;
        private PlaylistManager _playlistManager;
        private Timer _commercialTimer;
        private int _commercialIntervalMinutes = 15; // Intervalo de comerciales en minutos
        private Timer _backgroundPlaybackTimer; // Reproducción en segundo plano
        private MediaPlayer _mediaPlayer; // Controlador para la reproducción de video
        private string _recordingOutputPath = @"C:\Path\To\Recordings"; // Ruta donde se guardarán las grabaciones

        public MainWindow()
        {
            InitializeComponent();
            _scheduledContentList = new List<ScheduledContent>();
            _playlistManager = new PlaylistManager(@"C:\Path\To\Videos"); // Ruta donde están los videos
            _mediaPlayer = new MediaPlayer(); // Inicialización del reproductor multimedia
            EnsureDirectoriesExist();
            SetupCommercialTimer();
            SetupBackgroundPlaybackTimer();
        }

        // Verifica que las carpetas necesarias existan
        private void EnsureDirectoriesExist()
        {
            Directory.CreateDirectory(_playlistManager.BasePath);
            Directory.CreateDirectory(_recordingOutputPath);
        }

        // Manejo de errores con una función genérica
        private void HandleError(Action action, string errorMessage)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errorMessage}\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Configuración del temporizador para comerciales
        private void SetupCommercialTimer()
        {
            _commercialTimer = new Timer(_commercialIntervalMinutes * 60 * 1000); // Convertir minutos a milisegundos
            _commercialTimer.Elapsed += CommercialTimer_Elapsed;
            _commercialTimer.Start();
        }

        // Lógica para activar comerciales automáticamente
        private void CommercialTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Console.WriteLine("¡Es hora de un comercial!");
                PlayCommercials();
            });
        }

        // Lógica para reproducir comerciales
        private void PlayCommercials()
        {
            var commercialPlaylist = _playlistManager.GetCommercialsPlaylist();
            foreach (var commercial in commercialPlaylist)
            {
                PlayVideo(commercial);
                Console.WriteLine($"Reproduciendo comercial: {commercial}");
            }
        }

        // Función para agregar contenido a la parrilla de programación
        private void AddContentButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Archivos de video|*.mp4;*.avi;*.mkv",
                Multiselect = true
            };

            if (dialog.ShowDialog() == true)
            {
                foreach (var file in dialog.FileNames)
                {
                    var scheduledContent = new ScheduledContent { FilePath = file };
                    _scheduledContentList.Add(scheduledContent);
                    ContentListBox.Items.Add(file);
                }

                UpdateScheduleGrid();
            }
        }

        // Actualiza la parrilla de programación en el DataGrid
        private void UpdateScheduleGrid()
        {
            ScheduleDataGrid.ItemsSource = null; // Asegurarnos de que ScheduleDataGrid esté definido en XAML
            ScheduleDataGrid.ItemsSource = _scheduledContentList;
        }

        // Función para iniciar la reproducción de la lista de reproducción aleatoria
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPlaylist = _playlistManager.GetRandomPlaylist(60); // Duración de 60 minutos
            foreach (var video in selectedPlaylist)
            {
                PlayVideo(video);
                Console.WriteLine($"Reproduciendo: {video}");
            }
        }

        // Función para grabar la transmisión
        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            HandleError(() =>
            {
                Console.WriteLine("Iniciando grabación...");
                StartRecording();
            }, "Error al iniciar la grabación.");
        }

        // Inicia la grabación
        private void StartRecording()
        {
            string outputFile = Path.Combine(_recordingOutputPath, $"Recording_{DateTime.Now:yyyyMMdd_HHmmss}.mp4");
            Console.WriteLine($"Guardando grabación en: {outputFile}");
            // Aquí implementaríamos la lógica para usar FFmpeg o similar para capturar la transmisión
        }

        // Función para mostrar notificaciones de contenido
        private void ShowNotification(string message)
        {
            MessageBox.Show(message, "Notificación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Función para manejar la reproducción en segundo plano
        private void SetupBackgroundPlaybackTimer()
        {
            _backgroundPlaybackTimer = new Timer(10000); // Reproducir cada 10 segundos
            _backgroundPlaybackTimer.Elapsed += BackgroundPlaybackTimer_Elapsed;
            _backgroundPlaybackTimer.Start();
        }

        private void BackgroundPlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Console.WriteLine("Reproducción en segundo plano...");
                PlayInBackground();
            });
        }

        private void PlayInBackground()
        {
            var selectedPlaylist = _playlistManager.GetRandomPlaylist(60); // Duración de 60 minutos
            foreach (var video in selectedPlaylist)
            {
                PlayVideo(video);
                Console.WriteLine($"Reproduciendo en segundo plano: {video}");
            }
        }

        // Función para reproducir un video
        private void PlayVideo(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Archivo no encontrado: {filePath}");
                return;
            }

            Console.WriteLine($"Reproduciendo video: {filePath}");
            // Aquí se integraría el MediaElement u otra biblioteca para reproducción real
        }

        // Gestión de la cola de reproducción
        private void ManageQueueButton_Click(object sender, RoutedEventArgs e)
        {
            HandleError(() =>
            {
                Console.WriteLine("Gestión de la cola de reproducción en progreso...");
                // Aquí implementaríamos un cuadro de diálogo o interfaz para mover/eliminar elementos
            }, "Error al gestionar la cola de reproducción.");
        }
    }

    public class ScheduledContent
    {
        public string FilePath { get; set; }
        public DateTime? ScheduledTime { get; set; }
    }

    public class PlaylistManager
    {
        public string BasePath { get; }

        public PlaylistManager(string basePath)
        {
            BasePath = basePath;
        }

        public List<string> GetCommercialsPlaylist()
        {
            return Directory.GetFiles(BasePath, "*commercial*.mp4").ToList();
        }

        public List<string> GetRandomPlaylist(int durationMinutes)
        {
            var files = Directory.GetFiles(BasePath, "*.mp4").ToList();
            return files.OrderBy(_ => Guid.NewGuid()).Take(durationMinutes).ToList();
        }
}