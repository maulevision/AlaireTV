using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Controls;
using LibVLCSharp.WPF;

namespace AlaireTV
{
        public class VideoTransition
    {
        private VideoView _videoView;

        public VideoTransition(VideoView videoView)
        {
            _videoView = videoView;
        }

        public async Task FadeToBlack()
        {
            double opacity = 1.0;
            while (opacity > 0)
            {
                _videoView.Opacity = opacity;
                opacity -= 0.05;
                await Task.Delay(50);
            }
            _videoView.Opacity = 0;

            await FadeIn();
        }

        public async Task FadeIn()
        {
            double opacity = 0;
            while (opacity < 1.0)
            {
                _videoView.Opacity = opacity;
                opacity += 0.05;
                await Task.Delay(50);
            }
            _videoView.Opacity = 1.0;
        }
    }
}
