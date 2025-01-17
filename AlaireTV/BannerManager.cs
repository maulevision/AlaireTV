using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace AlaireTV
{
    public class BannerManager
    {
        private DispatcherTimer bannerTimer;
        private Queue<string> bannerQueue;

        public event Action<string> OnBannerChange;

        public BannerManager()
        {
            bannerQueue = new Queue<string>();
            bannerTimer = new DispatcherTimer();
            bannerTimer.Interval = TimeSpan.FromSeconds(10); // Cambiar banner cada 10 segundos
            bannerTimer.Tick += BannerTimer_Tick;
        }

        private void BannerTimer_Tick(object sender, EventArgs e)
        {
            if (bannerQueue.Count > 0)
            {
                string nextBanner = bannerQueue.Dequeue();
                OnBannerChange?.Invoke(nextBanner);
                bannerQueue.Enqueue(nextBanner); // Reagregar al final de la cola
            }
        }

        public void AddBanner(string bannerPath)
        {
            if (!string.IsNullOrEmpty(bannerPath))
            {
                bannerQueue.Enqueue(bannerPath);
            }
        }

        public void Start()
        {
            if (!bannerTimer.IsEnabled)
            {
                bannerTimer.Start();
            }
        }

        public void Stop()
        {
            if (bannerTimer.IsEnabled)
            {
                bannerTimer.Stop();
            }
        }
    }
}
