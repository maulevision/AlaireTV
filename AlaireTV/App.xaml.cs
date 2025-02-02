using System;
using System.Windows;
using System.Windows.Threading;

namespace AlaireTV
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Manejar excepciones no controladas
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            // Inicializar la aplicación
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            // Aquí puedes agregar lógica de inicialización, como cargar configuraciones o verificar dependencias
            Console.WriteLine("Aplicación inicializada correctamente.");
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Manejar excepciones no controladas
            MessageBox.Show($"Se produjo un error no controlado: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true; // Evitar que la aplicación se cierre
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Lógica adicional al cerrar la aplicación
            Console.WriteLine("Aplicación cerrada correctamente.");
            base.OnExit(e);
        }
    }
}