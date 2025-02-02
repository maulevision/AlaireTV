using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AlaireTV
{
    public partial class ScheduleWindow : Window
    {
        private List<ScheduledProgram> ProgramList = new List<ScheduledProgram>();
        private ScheduleManager scheduleManager;

        public ScheduleWindow(ScheduleManager manager)
        {
            InitializeComponent();
            scheduleManager = manager;
            LoadPrograms();
        }

        private void LoadPrograms()
        {
            ProgramListBox.ItemsSource = null; // Limpiar la fuente de datos antes de actualizarla
            ProgramList = scheduleManager.GetSchedules(); // Obtener los programas programados desde ScheduleManager
            ProgramListBox.ItemsSource = ProgramList; // Asignar la lista a la fuente de datos del ListBox
        }

        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            DateTime? selectedDate = StartDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(title) || !selectedDate.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            scheduleManager.AddSchedule(title, selectedDate.Value, TimeSpan.FromMinutes(30), ""); // Ruta del video vacía por ahora
            LoadPrograms(); // Actualizar la lista de programas programados
            ClearFields();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramListBox.SelectedItem != null)
            {
                ScheduledProgram selectedProgram = (ScheduledProgram)ProgramListBox.SelectedItem;
                scheduleManager.RemoveSchedule(selectedProgram);
                LoadPrograms(); // Actualizar la lista de programas programados
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un programa para eliminar.");
            }
        }

        private void RecurrentScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para programar contenido de manera recurrente
            MessageBox.Show("Funcionalidad de programación recurrente aún no implementada.");
        }

        private void ClearFields()
        {
            TitleTextBox.Text = string.Empty;
            StartDatePicker.SelectedDate = null;
        }
    }
}