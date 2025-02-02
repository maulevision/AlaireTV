using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlaireTV
{
    public class AdManager
    {
        public void AddAd(string adContent)
        {
            Console.WriteLine($"Added Ad: {adContent}");
        }

        public void DisplayAds()
        {
            Console.WriteLine("Displaying Ads...");
        }
    }
}