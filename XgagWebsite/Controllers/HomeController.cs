﻿using System;
using System.Collections.Generic;
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
        public ActionResult Index(int? page)
        {
            Task.Factory.StartNew(() => 
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var latestRanking = db.RankingHistory.OrderByDescending(rh => rh.RankingDateTime).FirstOrDefault();
                    if (latestRanking == null || latestRanking.RankingDateTime < DateTime.Now)
                    {
                        RankPeopleLists(db);
                    } 
                }
            });

            var pageSize = ConfigurationHelper.Instance.PageSize;
            var posts = DbContext.Posts
                .OrderByDescending(p => p.DateCreated)
                .Skip(pageSize * ((page ?? 1) - 1))
                .Take(pageSize)
                .ToList();

            return View(posts);
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
                .Select(pr => pr)
                .ToList();

            return View(viewModel);
        }

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
                .Select(pr => pr)
                .ToList();

            return View("ShitList", viewModel);
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
                if (i < ConfigurationHelper.Instance.XPRewardsCount)
                {
                    person.DownExperience +=
                        ConfigurationHelper.Instance.BaseXP - i * ConfigurationHelper.Instance.XPMultiplier;
                }

                var rank = db.PeopleRanking.Create();
                rank.Person = person;
                rank.Rank = i + 1;
                rank.RankType = VoteType.Down;
                rank.Score = person.Votes.Count(v => v.VoteType == VoteType.Down);
                db.PeopleRanking.Add(rank);
            }

            for (int i = 0; i < goodguyRanking.Count(); i++)
            {
                var personId = shitRanking[i];
                var person = db.People.First(p => p.PersonId == personId);
                if (i < ConfigurationHelper.Instance.XPRewardsCount)
                {
                    person.UpExperience +=
                        ConfigurationHelper.Instance.BaseXP - i * ConfigurationHelper.Instance.XPMultiplier;
                }

                var rank = db.PeopleRanking.Create();
                rank.Person = person;
                rank.Rank = i + 1;
                rank.RankType = VoteType.Up;
                rank.Score = person.Votes.Count(v => v.VoteType == VoteType.Up);
                db.PeopleRanking.Add(rank);
            }

            var history = db.RankingHistory.Create();
            history.RankingDateTime = DateTime.Now;
            db.RankingHistory.Add(history);
            db.SaveChanges();
        }
    }
}