using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace AlaireTV
{
    public partial class SettingsWindow : Window
    {
        private string _recordingFolder;
        private List<User> _users;

        public SettingsWindow()
        {
            InitializeComponent();
            _users = User.LoadUsers("users.json");
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Cargar configuración de calidad de video
            if (Properties.Settings.Default.VideoQuality == "Low")
                LowQualityRadio.IsChecked = true;
            else if (Properties.Settings.Default.VideoQuality == "High")
                HighQualityRadio.IsChecked = true;
            else
                MediumQualityRadio.IsChecked = true;

            // Cargar carpeta de grabación
            _recordingFolder = Properties.Settings.Default.RecordingFolder;
            RecordingFolderTextBox.Text = _recordingFolder;
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _recordingFolder = folderDialog.SelectedPath;
                RecordingFolderTextBox.Text = _recordingFolder;
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var userDialog = new UserDialog();
            if (userDialog.ShowDialog() == true)
            {
                _users.Add(userDialog.NewUser);
                User.SaveUsers(_users, "users.json");
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para eliminar usuario
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Guardar configuración de calidad de video
            if (LowQualityRadio.IsChecked == true)
                Properties.Settings.Default.VideoQuality = "Low";
            else if (HighQualityRadio.IsChecked == true)
                Properties.Settings.Default.VideoQuality = "High";
            else
                Properties.Settings.Default.VideoQuality = "Medium";

            // Guardar carpeta de grabación
            Properties.Settings.Default.RecordingFolder = _recordingFolder;

            // Guardar cambios
            Properties.Settings.Default.Save();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}