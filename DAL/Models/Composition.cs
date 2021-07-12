using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public sealed  class Composition
    {
        public int Id { get; set; }

        public string TitleComposition { get; set; }

        public string ShortDescription { get; set; }

        public DateTime DateAdded { get; set; }

        public string Fandom { get; set; }

        public DateTime DateUpdate { get; set; }

        public List<Tags> Tags { get; set; } = new List<Tags>();

        public List<Chapter> Chapters { get; set; } = new List<Chapter>();

        public List<Rating> Rating { get; set; } = new List<Rating>();

        public List<Favorite> Favorite { get; set; } = new List<Favorite>();

        public string Author { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
