using System;
using System.Windows;

namespace AlaireTV
{
    public partial class ScheduleContentDialog : Window
    {
        public ScheduleContentDialog()
        {
            InitializeComponent();
        }

        // Definir la propiedad ScheduledContent (si no está definida en otro lugar)
        public ScheduledContent ScheduledContent { get; set; }

        // Método OkButton_Click
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Lógica del botón Ok
        }

        // Método CancelButton_Click
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Lógica del botón Cancelar
        }
    }
}