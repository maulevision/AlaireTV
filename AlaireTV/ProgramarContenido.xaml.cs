using System;
using System.Windows;
using System.Windows.Controls;

namespace AlaireTV
{
    public partial class ProgramarContenido : Window
    {
        public ProgramarContenido()
        {
            InitializeComponent();
        }

        // Evento para el botón "Guardar"
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            // Validar los datos antes de guardarlos
            if (string.IsNullOrWhiteSpace(TituloTextBox.Text))
            {
                MessageBox.Show("El título no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (HoraInicioDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una hora de inicio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(DuracionTextBox.Text))
            {
                MessageBox.Show("La duración no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TipoContenidoComboBox.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de contenido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(RutaContenidoTextBox.Text))
            {
                MessageBox.Show("Debe proporcionar una ruta para el contenido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtener los datos del formulario
            string titulo = TituloTextBox.Text;
            string horaInicio = HoraInicioDatePicker.SelectedDate?.ToString("yyyy-MM-dd HH:mm") ?? "Sin seleccionar";
            string duracion = DuracionTextBox.Text;
            string tipoContenido = (TipoContenidoComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string rutaContenido = RutaContenidoTextBox.Text;

            // Mostrar los datos guardados (puedes reemplazarlo con lógica de guardado real)
            MessageBox.Show($"Datos guardados:\n\nTítulo: {titulo}\nHora de inicio: {horaInicio}\nDuración: {duracion}\nTipo: {tipoContenido}\nRuta: {rutaContenido}",
                            "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Evento para el botón "Cancelar"
        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // Cierra la ventana sin guardar
            this.Close();
        }

        // Evento para el botón "Buscar"
        private void BuscarRuta_Click(object sender, RoutedEventArgs e)
        {
            // Abrir un cuadro de diálogo para seleccionar un archivo
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Archivos de medios (*.mp4;*.mp3;*.avi;*.mkv)|*.mp4;*.mp3;*.avi;*.mkv|Todos los archivos (*.*)|*.*",
                Title = "Seleccionar archivo de contenido"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                RutaContenidoTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
