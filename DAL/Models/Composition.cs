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

        public DateTime DateUpDate { get; set; }

        public List<Tags> Tags { get; set; } = new List<Tags>();

        public List<Chapter> Chapters { get; set; } = new List<Chapter>();

        public List<Rating> Rating { get; set; } = new List<Rating>();

        public string Author { get; set; }
    }
}
