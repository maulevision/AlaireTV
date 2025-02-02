using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlaireTV
{
    public class EventAutomation
    {
        public void ScheduleEvent(DateTime eventTime, string eventName)
        {
            Console.WriteLine($"Scheduled event: {eventName} at {eventTime}");
        }

        public void TriggerEvent(string eventName)
        {
            Console.WriteLine($"Triggered event: {eventName}");
        }
    }
}