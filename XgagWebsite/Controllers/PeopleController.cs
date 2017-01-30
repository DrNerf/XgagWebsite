using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.AjaxResponses;
using XgagWebsite.Enums;
using XgagWebsite.Helpers;

namespace XgagWebsite.Controllers
{
    [Authorize]
    public class PeopleController : BaseController
    {
        public ActionResult Search(string firstName, string lastName, VoteType voteType)
        {
            var results = DbContext.People.Where(p => true);
            if (!string.IsNullOrEmpty(firstName))
            {
                results = results.Where(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                results = results.Where(p => p.LastName.ToLower().Contains(lastName.ToLower()));
            }

            var response = results.ToList().Select(r => new PeopleTableItemResponse()
            {
                FirstName = r.FirstName,
                LastName = r.LastName,
                PersonId = r.PersonId,
                Votes = r.Votes.Count(v => v.VoteType == voteType),
                ImageUrl = r.Image,
                Experience = voteType == VoteType.Down ? r.DownExperience : r.UpExperience
            });

            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Vote(int personId, VoteType voteType)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.DailyVotes.Where(dv => dv.VoteType == voteType)
                .Count(dv => dv.VoteDate == DateTime.Now.Date) 
                >= ConfigurationHelper.Instance.MaxDailyVotes)
            {
                return JsonResult(new GenericOperationResponse<string>
                {
                    IsSuccess = false,
                    Response = "You can't vote anymore today!"
                });
            }

            var person = DbContext.People.First(p => p.PersonId == personId);
            var newVote = DbContext.PeopleVotes.Create();
            newVote.Person = person;
            newVote.VoteType = voteType;
            person.Votes.Add(newVote);
            var dailyVote = DbContext.UsersDailyVotes.Create();
            dailyVote.User = currentUser;
            dailyVote.VoteDate = DateTime.Now.Date;
            dailyVote.VoteType = voteType;
            DbContext.UsersDailyVotes.Add(dailyVote);
            await DbContext.SaveChangesAsync();

            return JsonResult(new GenericOperationResponse<object>());
        }

        public ActionResult GetTopPeople(VoteType voteType)
        {
            var results = DbContext.People
                .OrderByDescending(p => p.Votes.Count(v => v.VoteType == voteType))
                .Where(p => p.Votes.Count(v => v.VoteType == voteType) > 0)
                .Take(ConfigurationHelper.Instance.RankingPeopleCount)
                .Select(p => new PeopleTableItemResponse()
                {
                    FirstName = p.FirstName,
                    ImageUrl = p.Image,
                    LastName = p.LastName,
                    PersonId = p.PersonId,
                    Votes = p.Votes.Count(v => v.VoteType == voteType),
                    Experience = voteType == VoteType.Down ? p.DownExperience : p.UpExperience
                })
                .ToList();

            return JsonResult(results);
        }

        public ActionResult GetTopXPPeople(VoteType voteType)
        {
            var results = DbContext.People
                .OrderByDescending(p => voteType == VoteType.Down ? p.DownExperience : p.UpExperience)
                .Where(p => (voteType == VoteType.Down ? p.DownExperience : p.UpExperience) > 0)
                .Take(ConfigurationHelper.Instance.RankingPeopleCount)
                .Select(p => new PeopleTableItemResponse()
                {
                    FirstName = p.FirstName,
                    ImageUrl = p.Image,
                    LastName = p.LastName,
                    PersonId = p.PersonId,
                    Votes = p.Votes.Count(v => v.VoteType == voteType),
                    Experience = voteType == VoteType.Down ? p.DownExperience : p.UpExperience
                })
                .ToList();

            return JsonResult(results);
        }
    }
}