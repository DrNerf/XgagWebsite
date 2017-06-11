using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using XgagWebsite.Helpers;
using XgagWebsite.Models;
using Z.EntityFramework.Extensions;

namespace XgagWebsite.Services
{
    public class ChatService
    {
        private const double m_DBUpdateInterval = 10000;

        private static ChatService m_Current;
        private static int m_PageSize = ConfigurationHelper.Instance.ChatPageSize;

        public static ChatService Current
        {
            get
            {
                if (m_Current == null)
                {
                    m_Current = new ChatService();
                }

                return m_Current;
            }
        }

        private List<ChatMessage> m_Messages;

        private ChatService()
        {
            m_Messages = new List<ChatMessage>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                m_Messages.InsertRange(
                    0, 
                    db.ChatMessages
                    .OrderBy(m => m.DateTime)
                    .TakeLast(m_PageSize)
                    .ToList()
                    .Select(m => m.Clone()));
            }

            var dbTimer = new Timer(m_DBUpdateInterval);
            dbTimer.AutoReset = true;
            dbTimer.Elapsed += DbTimerElapsed;
            dbTimer.Start();
        }

        public void WriteMessage(ChatMessage message)
        {
            m_Messages.Add(message);
        }

        public IEnumerable<ChatMessage> GetMessages()
        {
            return m_Messages.OrderBy(m => m.DateTime).TakeLast(m_PageSize);
        }

        private void DbTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateDB();
        }

        private async void UpdateDB()
        {
            if (m_Messages.Any())
            {
                var newMessages = m_Messages.Where(m => m.ChatMessageId == 0).ToList();
                if (newMessages.Any())
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        var dbMessages = new List<ChatMessage>();
                        foreach (var message in newMessages)
                        {
                            var dbmessage = db.ChatMessages.Create();
                            dbmessage.DateTime = message.DateTime;
                            dbmessage.Message = message.Message;
                            dbmessage.OwnerId = message.OwnerId;
                            dbMessages.Add(db.ChatMessages.Add(dbmessage));
                        }
                        //var dbMessages = db.ChatMessages.AddRange(newMessages);
                        await db.BulkSaveChangesAsync();
                        foreach (var message in newMessages)
                        {
                            m_Messages.Remove(message);
                        }

                        m_Messages.AddRange(dbMessages.Select(m => m.Clone()));
                    }
                } 
            }
        }
    }
}