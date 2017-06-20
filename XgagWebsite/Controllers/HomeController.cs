using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using XgagWebsite.Enums;
using XgagWebsite.Helpers;
using XgagWebsite.Models;
using XgagWebsite.ViewModels;

namespace XgagWebsite.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public async Task<ActionResult> Index(int? page)
        {
            Task.Factory.StartNew(() => 
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var latestRanking = db.RankingHistory.OrderByDescending(rh => rh.RankingDateTime).FirstOrDefault();
                    if (latestRanking == null || 
                        latestRanking.RankingDateTime.AddDays(ConfigurationHelper.Instance.RankingPersistPeriod) 
                        < DateTime.Now)
                    {
                        RankPeopleLists(db);
                    } 
                }
            });
            var postOfTheDay = DbContext.PostsOfTheDay
                .FirstOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now));
            if (postOfTheDay == null)
            {
                var randomPost = DbContext.Posts.OrderBy(p => Guid.NewGuid()).First();
                var newPostOfTheDay = DbContext.PostsOfTheDay.Create();
                newPostOfTheDay.Date = DateTime.Now.Date;
                newPostOfTheDay.Post = randomPost;
                postOfTheDay = DbContext.PostsOfTheDay.Add(newPostOfTheDay);
                await DbContext.SaveChangesAsync();
            }

            var pageSize = ConfigurationHelper.Instance.PageSize;
            var posts = DbContext.Posts
                .OrderByDescending(p => p.DateCreated)
                .Skip(pageSize * ((page ?? 1) - 1))
                .Take(pageSize)
                .ToList();
            var topStatsCount = ConfigurationHelper.Instance.TopStatsCount;
            var topContributors = DbContext.Users
                .OrderByDescending(u => u.Posts.Count())
                .Take(topStatsCount)
                .ToList();
            var topPosts = DbContext.Posts
                .OrderByDescending(p => p.Votes.Sum(v => (int)v.Type))
                .Take(topStatsCount)
                .ToList();

            var viewModel = new ViewModels.IndexViewModel()
            {
                Posts = posts,
                TopContributors = topContributors,
                TopPosts = topPosts,
                PostOfTheDay = postOfTheDay
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UploadPost(Post post)
        {
            var isSuccess = false;
            var isValid = false;
            string message = string.Empty;

            if (!String.IsNullOrEmpty(post.Title) && post.Image != null)
            {
                isValid = true;
            }
            else
            {
                message = "Invalid data!";
            }

            if (isValid)
            {
                try
                {
                    post.DateCreated = DateTime.Now;
                    DbContext.Posts.Add(post);
                    await DbContext.SaveChangesAsync();
                    isSuccess = true;
                }
                catch (Exception ex) 
                {
                    message = ex.Message;
                }
            }

            return Json(new { IsSuccess = isSuccess, Message = message });
        }

        [Authorize]
        public ActionResult ShitList()
        {
            var voteType = VoteType.Down;
            var viewModel = new PeopleListViewModel();
            viewModel.PageType = voteType;
            viewModel.LastRankingDateTime = DbContext.RankingHistory
                .OrderByDescending(rh => rh.RankingDateTime)
                .FirstOrDefault()?.RankingDateTime;
            viewModel.LastRanking = DbContext.PeopleRanking
                .Where(pr => pr.RankType == voteType)
                .OrderBy(pr => pr.Rank)
                .ToList();
            viewModel.TopPeople = DbContext.People
                .OrderByDescending(p => p.Votes.Count(v => v.VoteType == voteType))
                .Where(p => p.Votes.Count(v => v.VoteType == voteType) > 0)
                .Take(ConfigurationHelper.Instance.RankingPeopleCount)
                .ToList();

            return View(viewModel);
        }

        [Authorize]
        public ActionResult GoodGuyList()
        {
            var voteType = VoteType.Up;
            var viewModel = new PeopleListViewModel();
            viewModel.PageType = voteType;
            viewModel.LastRankingDateTime = DbContext.RankingHistory
                .OrderByDescending(rh => rh.RankingDateTime)
                .FirstOrDefault()?.RankingDateTime;
            viewModel.LastRanking = DbContext.PeopleRanking
                .Where(pr => pr.RankType == voteType)
                .OrderBy(pr => pr.Rank)
                .ToList();
            viewModel.TopPeople = DbContext.People
                .OrderByDescending(p => p.Votes.Count(v => v.VoteType == voteType))
                .Where(p => p.Votes.Count(v => v.VoteType == voteType) > 0)
                .Take(ConfigurationHelper.Instance.RankingPeopleCount)
                .ToList();

            return View("ShitList", viewModel);
        }

        [Authorize]
        public ActionResult ChitChats()
        {
            var chitChats = DbContext.ChitChats
                .OrderByDescending(cc => cc.DateTimeCreated)
                .ToList();

            return View(chitChats);
        }

        [Authorize]
        public ActionResult Chat()
        {
            return View();
        }

        public void RankPeopleLists(ApplicationDbContext db)
        {
            var shitRanking = db.People
                .OrderByDescending(p => p.Votes.Count(v => v.VoteType == VoteType.Down))
                .Take(ConfigurationHelper.Instance.RankingPeopleCount)
                .Select(x => x.PersonId)
                .ToList();

            var goodguyRanking = db.People
                .OrderByDescending(p => p.Votes.Count(v => v.VoteType == VoteType.Up))
                .Take(ConfigurationHelper.Instance.RankingPeopleCount)
                .Select(x => x.PersonId)
                .ToList();

            db.PeopleRanking.RemoveRange(db.PeopleRanking.Select(pr => pr));

            for (int i = 0; i < shitRanking.Count(); i++)
            {
                var personId = shitRanking[i];
                var person = db.People.First(p => p.PersonId == personId);
                var score = person.Votes.Count(v => v.VoteType == VoteType.Down);
                if (score > 0)
                {
                    var xpGain = ConfigurationHelper.Instance.BaseXP - i * ConfigurationHelper.Instance.XPMultiplier;
                    if (i < ConfigurationHelper.Instance.XPRewardsCount)
                    {
                        person.DownExperience += xpGain;
                    }

                    var rank = db.PeopleRanking.Create();
                    rank.Person = person;
                    rank.Rank = i + 1;
                    rank.RankType = VoteType.Down;
                    rank.Score = score;
                    rank.ExperienceGain = xpGain;
                    db.PeopleRanking.Add(rank); 
                }
            }

            for (int i = 0; i < goodguyRanking.Count(); i++)
            {
                var personId = goodguyRanking[i];
                var person = db.People.First(p => p.PersonId == personId);
                var score = person.Votes.Count(v => v.VoteType == VoteType.Up);
                if (score > 0)
                {
                    var xpGain = ConfigurationHelper.Instance.BaseXP - i * ConfigurationHelper.Instance.XPMultiplier;
                    if (i < ConfigurationHelper.Instance.XPRewardsCount)
                    {
                        person.UpExperience += xpGain;
                    }

                    var rank = db.PeopleRanking.Create();
                    rank.Person = person;
                    rank.Rank = i + 1;
                    rank.RankType = VoteType.Up;
                    rank.Score = score;
                    rank.ExperienceGain = xpGain;
                    db.PeopleRanking.Add(rank); 
                }
            }

            var history = db.RankingHistory.Create();
            history.RankingDateTime = DateTime.Now;
            db.RankingHistory.Add(history);
            db.PeopleVotes.RemoveRange(db.PeopleVotes.Select(pv => pv));
            db.UsersDailyVotes.RemoveRange(db.UsersDailyVotes.Select(dv => dv));
            db.SaveChanges();
        }
    }
}