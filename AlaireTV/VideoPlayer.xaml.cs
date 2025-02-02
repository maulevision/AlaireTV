using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlaireTV
{
    public partial class VideoPlayer : Window
    {
        public VideoPlayer()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }

        // Método para cargar un video
        public void LoadVideo(string filePath)
        {
            myMediaElement.Source = new Uri(filePath);
            myMediaElement.Play();
        }
    }
}