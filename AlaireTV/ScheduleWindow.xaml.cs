using System;
using System.Windows;
using System.Collections.Generic;

namespace AlaireTV
{
    public partial class ScheduleWindow : Window
    {
        private List<ScheduledProgram> ProgramList = new List<ScheduledProgram>();  // Lista para almacenar los programas
        private ScheduleManager scheduleManager = new ScheduleManager();  // Instancia de ScheduleManager

        public ScheduleWindow()
        {
            InitializeComponent();
            LoadPrograms(); // Llamar al método para cargar los programas al iniciar la ventana
        }

        private void LoadPrograms()
        {
            // Ejemplo de carga de programas. Se puede sustituir por la carga desde una base de datos o archivo.
            ProgramList.Add(new ScheduledProgram { Title = "Programa 1", StartDate = DateTime.Now });
            ProgramList.Add(new ScheduledProgram { Title = "Programa 2", StartDate = DateTime.Now.AddDays(1) });

            ProgramListBox.ItemsSource = ProgramList; // Asegúrate de tener un ListBox llamado ProgramListBox en tu XAML
        }

        private void SelectVideo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Archivos de Video|*.mp4;*.avi;*.mkv",
                Title = "Seleccionar Video"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                VideoPathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            DateTime? selectedDate = StartDatePicker.SelectedDate;
            string videoPath = VideoPathTextBox.Text;

            if (string.IsNullOrWhiteSpace(title) || !selectedDate.HasValue || string.IsNullOrWhiteSpace(videoPath))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            scheduleManager.AddSchedule(title, selectedDate.Value, videoPath);
            LoadSchedules();
            ClearFields();
        }

        private void LoadSchedules()
        {
            // Cargar las programaciones desde ScheduleManager
            ProgramListBox.Items.Clear();
            foreach (var schedule in scheduleManager.GetSchedules())
            {
                ProgramListBox.Items.Add(schedule.ToString());
            }
        }

        private void ClearFields()
        {
            TitleTextBox.Text = string.Empty;
            StartDatePicker.SelectedDate = null;
            VideoPathTextBox.Text = string.Empty;
        }
    }

    // Clase para representar la programación
    public class ScheduledProgram
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}
