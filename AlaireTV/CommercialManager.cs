using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace AlaireTV
{
    public class CommercialManager
    {
        private System.Timers.Timer _commercialTimer;
        private string _basePath;

        public CommercialManager(string basePath)
        {
            _basePath = basePath;
        }

        public void SetupCommercialTimer(Action playCommercialsCallback)
        {
            _commercialTimer = new System.Timers.Timer(15 * 60 * 1000); // Intervalo de 15 minutos
            _commercialTimer.Elapsed += (sender, e) => playCommercialsCallback();
            _commercialTimer.Start();
        }

        public List<string> GetCommercialsPlaylist()
        {
            return Directory.GetFiles(_basePath, "*commercial*.mp4").ToList();
        }
    }
}