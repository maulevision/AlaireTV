using System.Windows;

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
            ProgramListBox.ItemsSource = null;
            ProgramList = scheduleManager.GetSchedules();
            ProgramListBox.ItemsSource = ProgramList;
        }

        // Elimina RecurrentScheduleButton_Click de este archivo si existe
    }
}