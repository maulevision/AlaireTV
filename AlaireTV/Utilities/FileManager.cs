using System;
using System.IO; // Agregado para usar la clase File
using Microsoft.Win32; // Agregado para usar OpenFileDialog

namespace AlaireTV.Utilities
{
    public static class FileManager
    {
        public static string OpenFileDialog(string filter = "Video files (*.mp4)|*.mp4")
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter,
                DefaultExt = ".mp4"
            };

            bool? result = dialog.ShowDialog();
            return result == true ? dialog.FileName : null;
        }

        public static void SaveSettings(string filePath, object settings)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, json);
        }

        public static T LoadSettings<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            return default;
        }
    }
}