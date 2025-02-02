using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlaireTV
{
        public class StreamingManager
    {
        public string StreamUrl { get; set; }
        public bool IsStreaming { get; set; }

        public void StartStreaming(string url)
        {
            StreamUrl = url;
            IsStreaming = true;
            Console.WriteLine($"Started streaming to {StreamUrl}");
        }

        public void StopStreaming()
        {
            IsStreaming = false;
            Console.WriteLine("Stopped streaming.");
        }
    }
}