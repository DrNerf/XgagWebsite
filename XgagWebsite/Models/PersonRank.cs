using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XgagWebsite.Enums;

namespace XgagWebsite.Models
{
    public class PersonRank
    {
        [Key]
        public int PersonRankId { get; set; }

        public int Rank { get; set; }

        public virtual Person Person { get; set; }

        public VoteType RankType { get; set; }

        public int Score { get; set; }
    }
}