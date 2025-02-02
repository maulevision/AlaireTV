using System;
using System.Collections.Generic;

namespace AlaireTV
{
    public partial class ScheduleManager
    {
        private List<ScheduledProgram> schedules = new List<ScheduledProgram>();

        public void AddSchedule(string title, DateTime startDate, TimeSpan duration, string videoPath)
        {
            schedules.Add(new ScheduledProgram { Title = title, StartDate = startDate, Duration = duration, FilePath = videoPath });
        }

        public void RemoveSchedule(ScheduledProgram program)
        {
            schedules.Remove(program);
        }

        public List<ScheduledProgram> GetSchedules()
        {
            return schedules;
        }
    }
}