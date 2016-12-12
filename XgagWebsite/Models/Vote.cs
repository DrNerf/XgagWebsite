using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XgagWebsite.Enums;

namespace XgagWebsite.Models
{
    public class Vote
    {
        public int VoteId { get; set; }

        public virtual Post Post { get; set; }
        
        public virtual ApplicationUser Voter { get; set; }

        public VoteType Type { get; set; }
    }
}