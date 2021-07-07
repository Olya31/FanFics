namespace DAL.Models
{
    public sealed class Chapter
    {
        public int Id { get; set; }

        public int CompositionId { get; set; }

        public Composition Composition { get; set; }

        public string Text { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }
    }
}
