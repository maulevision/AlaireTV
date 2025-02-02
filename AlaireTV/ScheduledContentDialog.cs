using System.Windows;

namespace AlaireTV
{
    public partial class ScheduleContentDialog : Window
    {
        public ScheduledContent ContentSchedule { get; set; }

        // Constructor único
        public ScheduleContentDialog()
        {
            InitializeComponent();
            ContentSchedule = new ScheduledContent();
            this.DataContext = ContentSchedule;
        }

        // Métodos únicos
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContentSchedule.Title))
            {
                MessageBox.Show("El título no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ContentSchedule.StartDate == default)
            {
                MessageBox.Show("Seleccione una fecha de inicio válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}