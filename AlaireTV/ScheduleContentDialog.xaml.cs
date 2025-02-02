using System;
using System.Windows;

namespace AlaireTV
{
    public partial class ScheduleContentDialog : Window
    {
        public ScheduledContent ScheduledContent { get; set; }

        public ScheduleContentDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Validar y agregar el contenido
            ScheduledContent = new ScheduledContent
            {
                FilePath = _titleTextBox.Text,
                StartTime = _startDatePicker.SelectedDate ?? DateTime.Now,
                EndTime = _startDatePicker.SelectedDate.Value.AddMinutes(30)
            };
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}