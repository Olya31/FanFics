using System.Collections.Generic;

namespace DAL.Models
{
    public sealed class Tags
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public List<Composition> Composition { get; set; } = new List<Composition>();
    }
}
