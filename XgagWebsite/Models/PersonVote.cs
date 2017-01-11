using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XgagWebsite.Enums;

namespace XgagWebsite.Models
{
    public class PersonVote
    {
        [Key]
        public int PersonVoteId { get; set; }

        public virtual Person Person { get; set; }

        public VoteType VoteType { get; set; }
    }
}