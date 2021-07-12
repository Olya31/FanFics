using System;

namespace DAL.Models
{
    public sealed class Rating
    {
        public int Id { get; set; }

        public int CompositionId { get; set; }

        public Composition Composition { get; set; }

        public int RatingCounter { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime ThisDateTime { get; set; }
    }
}
