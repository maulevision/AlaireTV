using System;
using System.Collections.Generic;

namespace AlaireTV
{
    public class ScheduleManager
    {
        // Lista para almacenar las programaciones
        private List<ScheduleItem> schedules;

        public ScheduleManager()
        {
            schedules = new List<ScheduleItem>();
        }

        // Método para agregar una nueva programación
        public void AddSchedule(string title, DateTime date, string videoPath)
        {
            var schedule = new ScheduleItem
            {
                Title = title,
                Date = date,
                VideoPath = videoPath
            };

            schedules.Add(schedule);
        }

        // Método para eliminar una programación existente
        public void RemoveSchedule(ScheduleItem item)
        {
            if (schedules.Contains(item))
            {
                schedules.Remove(item);
            }
        }

        // Método para obtener todas las programaciones
        public List<ScheduleItem> GetSchedules()
        {
            return schedules;
        }
    }

    // Clase para representar un elemento de programación
    public class ScheduleItem
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string VideoPath { get; set; }

        public override string ToString()
        {
            return $"{Date:G} - {Title}";
        }
    }
}
