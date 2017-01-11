using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XgagWebsite.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<PersonVote> Votes { get; set; }
        
        public int DownExperience { get; set; }

        public int UpExperience { get; set; }

        public Person()
        {
            Votes = new List<PersonVote>();
        }
    }
}