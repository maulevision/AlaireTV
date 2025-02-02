namespace AlaireTV
{
    public class ScheduledContent
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; } // Nueva propiedad
        public DateTime EndTime { get; set; }   // Nueva propiedad
        public string FilePath { get; set; }    // Añadida si aún se usa
    }
}