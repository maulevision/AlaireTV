using System.Windows;

namespace AlaireTV
{
    public partial class ScheduleContentDialog : Window
    {
        public ScheduledContent ContentSchedule { get; set; } = new ScheduledContent();

        public ScheduleContentDialog()
        {
            InitializeComponent();
            DataContext = ContentSchedule;
        }

        // Elimina estos métodos si no están en el XAML
    }
}