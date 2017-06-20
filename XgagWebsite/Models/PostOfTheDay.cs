using System;
using System.ComponentModel.DataAnnotations;

namespace XgagWebsite.Models
{
    public class PostOfTheDay
    {
        [Key]
        public int PostOfTheDayId { get; set; }

        public DateTime Date { get; set; }

        public virtual Post Post { get; set; }
    }
}