using DAL.Models;
using System;
using System.Collections.Generic;

namespace FanFics.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        public int CompositionId { get; set; }

        public int RatingCounter { get; set; }

        public List<Rating> ListOfComments { get; set; }

        public string Comments { get; set; }

        public string UserId { get; set; }

        public DateTime ThisDateTime { get; set; }

        public Rating ToRating(
            string comment, 
            int compositionId,
            int rating,
            string userId) => new Rating
        {
            Id = this.Id,
            CompositionId = compositionId,
            RatingCounter = rating,
            Comment = comment,
            UserId = userId,
            ThisDateTime = DateTime.Now
        };

    }
}
