using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using XgagWebsite.AjaxResponses;
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
    }
}