using System;

namespace AlaireTV
{
    /// <summary>
    /// Representa un programa en la parrilla de programación.
    /// </summary>
    public class ProgramSchedule
    {
        /// <summary>
        /// Título del programa.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Hora de inicio del programa.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Ruta del archivo de video asociado al programa.
        /// </summary>
        public string VideoPath { get; set; }

        /// <summary>
        /// Duración del programa en minutos.
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Hora de finalización calculada del programa.
        /// </summary>
        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);

        /// <summary>
        /// Indica si el programa es un evento en vivo.
        /// </summary>
        public bool IsLiveEvent { get; set; }

        /// <summary>
        /// Constructor de la clase ProgramSchedule.
        /// </summary>
        /// <param name="title">Título del programa.</param>
        /// <param name="startTime">Hora de inicio del programa.</param>
        /// <param name="videoPath">Ruta del archivo de video.</param>
        /// <param name="durationMinutes">Duración del programa en minutos.</param>
        /// <param name="isLiveEvent">Indica si el programa es en vivo.</param>
        public ProgramSchedule(string title, DateTime startTime, string videoPath, int durationMinutes, bool isLiveEvent = false)
        {
            Title = title;
            StartTime = startTime;
            VideoPath = videoPath;
            DurationMinutes = durationMinutes;
            IsLiveEvent = isLiveEvent;
        }

        /// <summary>
        /// Verifica si este programa se solapa con otro.
        /// </summary>
        /// <param name="other">Otro programa a verificar.</param>
        /// <returns>True si hay solapamiento, de lo contrario False.</returns>
        public bool OverlapsWith(ProgramSchedule other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }

        /// <summary>
        /// Obtiene una representación en texto del programa.
        /// </summary>
        /// <returns>Una cadena que describe el programa.</returns>
        public override string ToString()
        {
            return $"{Title} - Inicio: {StartTime:HH:mm}, Fin: {EndTime:HH:mm}, En vivo: {IsLiveEvent}";
        }
    }
}
