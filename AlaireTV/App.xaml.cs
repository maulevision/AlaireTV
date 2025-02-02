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

            // Inicializar la aplicaci�n
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            // Aqu� puedes agregar l�gica de inicializaci�n, como cargar configuraciones o verificar dependencias
            Console.WriteLine("Aplicaci�n inicializada correctamente.");
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Manejar excepciones no controladas
            MessageBox.Show($"Se produjo un error no controlado: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true; // Evitar que la aplicaci�n se cierre
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // L�gica adicional al cerrar la aplicaci�n
            Console.WriteLine("Aplicaci�n cerrada correctamente.");
            base.OnExit(e);
        }
    }
}