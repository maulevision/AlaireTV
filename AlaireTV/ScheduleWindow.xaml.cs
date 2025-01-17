using System;
using System.Windows;
using System.Collections.Generic;

namespace AlaireTV
{
    public partial class ScheduleWindow : Window
    {
        private List<ScheduledProgram> ProgramList = new List<ScheduledProgram>();  // Lista para almacenar los programas

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

        // Asegúrate de que el método LoadPrograms se llame correctamente durante la inicialización de la UI.
    }
}
