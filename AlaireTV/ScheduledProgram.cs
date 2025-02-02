namespace AlaireTV
{
    public class ScheduledProgram
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }

        // Sobreescribir ToString para una mejor visualización en el ListBox
        public override string ToString()
        {
            return $"{Title} - {StartDate} - Duración: {Duration.TotalMinutes} minutos";
        }
    }
}