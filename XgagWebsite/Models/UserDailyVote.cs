using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XgagWebsite.Enums;

namespace XgagWebsite.Models
{
    public class UserDailyVote
    {
        [Key]
        public int UserDailyVoteId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime VoteDate { get; set; }

        public VoteType VoteType { get; set; }
    }
}