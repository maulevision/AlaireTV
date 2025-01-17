using System;
using System.Windows;

namespace AlaireTV
{
    public partial class ScheduleContentDialog : Window
    {
        // Propiedad para almacenar el contenido programado
        public ScheduledContent ScheduledContent { get; set; }

        public ScheduleContentDialog()
        {
            InitializeComponent();
            ScheduledContent = new ScheduledContent(); // Inicializar la propiedad
        }

        // Método OkButton_Click para guardar el contenido programado
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Aquí deberías agregar la lógica para guardar el contenido en la lista o realizar la validación.
            DialogResult = true; // Indicar que el diálogo se completó correctamente
            Close(); // Cerrar el diálogo
        }

        // Método CancelButton_Click para cerrar el diálogo sin guardar cambios
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Indicar que el usuario canceló el diálogo
            Close(); // Cerrar el diálogo
        }
    }

    // Elimina la definición de ScheduledContent si ya existe en otro archivo, ya que ya está definida.
    // Si es necesario, asegúrate de que esta clase esté en el archivo adecuado.

    // Clase ScheduledContent para almacenar la información del contenido programado
    public class ScheduledContent
    {
        public string FilePath { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
