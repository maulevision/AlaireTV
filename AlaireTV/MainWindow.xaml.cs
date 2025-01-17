using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;

namespace AlaireTV
{
    public partial class MainWindow : Window
    {
        private List<string> _playlist = new List<string>(); // Lista de reproducción
        private bool _isLooping = false; // Control para el bucle de reproducción
        private List<ScheduledContent> _scheduledContent = new List<ScheduledContent>(); // Contenido programado
        private string _scheduleFilePath = "scheduledContent.json"; // Ruta para guardar la programación

        public MainWindow()
        {
            InitializeComponent();
            LoadScheduledContent(); // Cargar programación al inicio
        }

        // Cargar videos desde un archivo
        private void LoadVideos_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de Video|*.mp4;*.avi;*.mov;*.mkv|Todos los Archivos|*.*",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    _playlist.Add(file);
                    PlaylistManualListBox.Items.Add(file);
                }
            }
        }

        // Eliminar el video seleccionado
        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistManualListBox.SelectedItem != null)
            {
                _playlist.Remove(PlaylistManualListBox.SelectedItem.ToString());
                PlaylistManualListBox.Items.Remove(PlaylistManualListBox.SelectedItem);
            }
        }

        // Limpiar la lista de reproducción
        private void ClearPlaylist_Click(object sender, RoutedEventArgs e)
        {
            _playlist.Clear();
            PlaylistManualListBox.Items.Clear();
        }

        // Reproducir el siguiente video de la lista
        private void PlayNextVideo()
        {
            if (_playlist.Count > 0)
            {
                string videoPath = _playlist[0]; // Tomar el primer video
                _playlist.RemoveAt(0); // Eliminarlo de la lista
                PlaylistMediaElement.Source = new Uri(videoPath);
                PlaylistMediaElement.Play();
            }
        }

        // Función de reproducción finalizada (reproducir el siguiente si está en bucle)
        private void PlaylistMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_isLooping)
            {
                PlayNextVideo();
            }
        }

        // Activar/desactivar el bucle
        private void LoopCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _isLooping = true;
        }

        private void LoopCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _isLooping = false;
        }

        // Abrir ventana para programación
        private void OpenScheduleWindow_Click(object sender, RoutedEventArgs e)
        {
            var scheduleDialog = new ScheduleContentDialog();
            if (scheduleDialog.ShowDialog() == true)
            {
                ScheduledContent content = scheduleDialog.ScheduledContent;
                _scheduledContent.Add(content);
                SaveScheduledContent();
                UpdateScheduleListBox();
            }
        }

        // Guardar la programación en un archivo JSON
        private void SaveScheduledContent()
        {
            var json = JsonSerializer.Serialize(_scheduledContent);
            File.WriteAllText(_scheduleFilePath, json);
        }

        // Cargar la programación desde un archivo JSON
        private void LoadScheduledContent()
        {
            try
            {
                if (File.Exists(_scheduleFilePath))
                {
                    var json = File.ReadAllText(_scheduleFilePath);
                    _scheduledContent = JsonSerializer.Deserialize<List<ScheduledContent>>(json) ?? new List<ScheduledContent>();
                    UpdateScheduleListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la programación: {ex.Message}");
            }
        }

        // Actualizar la lista de programación
        private void UpdateScheduleListBox()
        {
            ScheduleListBox.Items.Clear();
            foreach (var content in _scheduledContent)
            {
                ScheduleListBox.Items.Add($"{content.StartTime} - {content.EndTime}: {content.FilePath}");
            }
        }
    }

    // Clase para representar el contenido programado
    public class ScheduledContent
    {
        public string FilePath { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    // Ejemplo de una ventana de programación (necesitarás implementarla, este es solo un ejemplo)
    public class ScheduleContentDialog : Window
    {
        public ScheduledContent ScheduledContent { get; private set; }

        public ScheduleContentDialog()
        {
            // Esta ventana debe permitir que el usuario ingrese una programación
            // y luego crear un objeto ScheduledContent con esos valores.
        }
    }
}
