namespace DAL.Models
{
    public sealed class Rating
    {
        public int Id { get; set; }

        public int CompositionId { get; set; }

        public Composition Composition { get; set; }

        public int RatingCounter { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
