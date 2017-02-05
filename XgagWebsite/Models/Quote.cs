using System;
using System.ComponentModel.DataAnnotations;

namespace XgagWebsite.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }

        public string Text { get; set; }

        public virtual Person Author { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string AnonymousAuthor { get; set; }
    }
}