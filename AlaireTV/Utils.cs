using System;

namespace AlaireTV
{
    public class Utils
    {
        public void HandleError(Action action, string errorMessage)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{errorMessage}\nDetalles: {ex.Message}");
            }
        }
    }
}
