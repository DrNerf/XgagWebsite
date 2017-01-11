using System;
using System.Collections.Generic;
using XgagWebsite.Enums;
using XgagWebsite.Models;

namespace XgagWebsite.ViewModels
{
    public class PeopleListViewModel
    {
        public VoteType PageType { get; set; }

        public DateTime? LastRankingDateTime { get; set; }

        public List<PersonRank> LastRanking { get; set; }
    }
}