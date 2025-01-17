using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlaireTV
{
    public class RandomPlaylistSchedule
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> VideoPaths { get; set; }
    }
}