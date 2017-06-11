using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.AjaxResponses;
using XgagWebsite.Helpers;
using XgagWebsite.Models;
using XgagWebsite.Services;

namespace XgagWebsite.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        private ChatService m_ChatService = ChatService.Current;

        [HttpPost]
        public ActionResult SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Cannot be empty!", message);
            }

            var currentUser = GetCurrentUser();
            m_ChatService.WriteMessage(new ChatMessage()
            {
                DateTime = DateTime.Now,
                Message = ImagesHelper.IsImageUrl(message) ? ImagesHelper.GetImageTag(message) : message,
                Owner = currentUser,
                OwnerId = currentUser.Id
            });

            return JsonResult(new GenericOperationResponse());
        }

        [HttpGet]
        public ActionResult GetMessages()
        {
            var response = new GenericOperationResponse<IEnumerable<ChatMessageResponse>>();
            try
            {
                var currentUser = GetCurrentUser();
                var responseMessages = m_ChatService.GetMessages()
                    .Select(m => new ChatMessageResponse()
                    {
                        Id = m.ChatMessageId,
                        DateTimeSent = m.DateTime,
                        OwnerAvatar = Url.Content(m.Owner.ProfilePictureUrl),
                        Message = m.Message,
                        OwnerUsername = m.Owner.UserName,
                        IsOwned = m.OwnerId == currentUser.Id
                    }).ToList();

                response.Response = responseMessages;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
            }

            return JsonResult(response);
        }
    }
}