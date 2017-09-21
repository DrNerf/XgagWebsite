using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using XgagWebsite.AjaxResponses;
using XgagWebsite.Enums;
using XgagWebsite.Helpers;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public class ChitChatController : BaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateChitChat(ChitChat model)
        {
            var response = new GenericOperationResponse<ChitChat>();
            try
            {
                var chitChat = DbContext.ChitChats.Create();
                chitChat.Content = model.Content;
                chitChat.DangerType = model.DangerType;
                chitChat.DateTimeCreated = DateTime.Now;
                var dbChitChat = DbContext.ChitChats.Add(chitChat);
                await DbContext.SaveChangesAsync();
                response.Response = dbChitChat;
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                throw;
            }

            return JsonResult(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Vote(int chitChatId, VoteType voteType)
        {
            var voter = GetCurrentUser();
            var chitChat = DbContext.ChitChats.FirstOrDefault(p => p.ChitChatId == chitChatId);
            if (!chitChat.Votes.Any(v => v.Voter.Id == voter.Id))
            {
                var vote = DbContext.ChitChatVotes.Create();
                vote.ChitChat = chitChat;
                vote.VoteType = voteType;
                vote.Voter = voter;
                chitChat.Votes.Add(vote);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Unvote(int chitChatId)
        {
            var vote = DbContext.ChitChatVotes
                .Where(v => v.ChitChat.ChitChatId == chitChatId)
                .FirstOrDefault(v => v.Voter.UserName == User.Identity.Name);

            if (vote != null)
            {
                DbContext.ChitChatVotes.Remove(vote);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize(Roles = RolesHelper.AdminRole)]
        [HttpPost]
        public async Task<ActionResult> Remove(int chitChatId)
        {
            var result = new GenericOperationResponse();
            var chitChat = DbContext.ChitChats.FirstOrDefault(cc => cc.ChitChatId == chitChatId);
            if (chitChat != null)
            {
                chitChat.Content = ConfigurationHelper.Instance.CensorshipText;
                await DbContext.SaveChangesAsync();
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Record not found in DB.";
            }

            return JsonResult(result);
        }
    }
}