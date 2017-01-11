using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XgagWebsite.Models
{
    public class RankingHistoryItem
    {
        [Key]
        public int PersonRankingHistoryId { get; set; }

        public DateTime RankingDateTime { get; set; }
    }
}