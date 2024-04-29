using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.ViewModel;

namespace MauiApp1.Model
{
    public class Service : IService
    {
        private IRepository repo;

        public Service(Repository repo)
        {
            this.repo = repo;
        }

        private List<Chat> GetChatsSortedByLastMessageTimeStamp(int userId)
        {
            return repo.GetChatsByUser(userId).OrderByDescending(chat => chat.GetLastMessage().GetTimestamp()).ToList();
        }

        private List<Chat> FilterChatsByName(int userId, string name)
        {
            List<Chat> chats = GetChatsSortedByLastMessageTimeStamp(userId);
            if (name.Length == 0)
            {
                return chats;
            }

            name = name.ToLower();

            List<Chat> matchingChats = new List<Chat>();
            foreach (Chat chat in chats)
            {
                User? u = repo.GetUser(chat.ReceiverId);
                if (u == null)
                {
                    continue;
                }

                string userName = u.Name.ToLower();
                if (userName.Contains(name))
                {
                    matchingChats.Add(chat);
                }
            }

            return matchingChats;
        }

        public List<ContactLastMessage> GetContactLastMessages(int userId, string name)
        {
            List<ContactLastMessage> result = new List<ContactLastMessage>();

            List<Chat> chats = this.FilterChatsByName(userId, name);
            foreach (Chat chat in chats)
            {
                User? u = repo.GetUser(chat.ReceiverId);
                if (u == null)
                {
                    continue;
                }

                Message m = chat.GetLastMessage();
                string msg = m.GetMessage();
                if (m.GetSenderId() == userId)
                {
                    msg = "You: " + msg;
                }
                if (msg.Length > 20)
                {
                    msg = msg.Substring(0, 17) + "...";
                }

                DateTime dt = m.GetTimestamp();
                string time = Utils.ToStringWithLeadingZero(dt.Day) + "." + Utils.ToStringWithLeadingZero(dt.Month) + "\n";
                time = time + Utils.ToStringWithLeadingZero(dt.Hour) + ":" + Utils.ToStringWithLeadingZero(dt.Minute);

                ContactLastMessage clm = new ContactLastMessage(u.Name, u.ProfilePhotoPath, msg, time, m.GetStatus(), chat.ChatId);
                result.Add(clm);
            }

            return result;
        }

        public string GetContactName(int chatId)
        {
            Chat? chat = repo.GetChat(chatId);
            if (chat == null)
            {
                return string.Empty;
            }

            User? contact = repo.GetUser(chat.ReceiverId);
            if (contact == null)
            {
                return string.Empty;
            }

            return contact.Name;
        }

        public string GetContactProfilePhotoPath(int chatId)
        {
            Chat? chat = repo.GetChat(chatId);
            if (chat == null)
            {
                return string.Empty;
            }

            User? contact = repo.GetUser(chat.ReceiverId);
            if (contact == null)
            {
                return string.Empty;
            }

            return contact.ProfilePhotoPath;
        }

        public List<MessageModel> GetChatMessages(int chatId)
        {
            List<MessageModel> result = new List<MessageModel>();

            Chat? chat = repo.GetChat(chatId);
            if (chat == null)
            {
                return result;
            }

            List<Message> messages = chat.GetAllMessages();
            foreach (Message m in messages)
            {
                if (m is Message)
                {
                    bool incoming = (m.GetSenderId() == chat.ReceiverId);
                    MessageModel model = new MessageModel("text", incoming, m.GetMessage());
                    result.Add(model);
                }
            }

            return result;
        }

        public void AddTextMessageToChat(int chatId, int senderId, string text)
        {
            Message message = new TextMessage(0, chatId, senderId, DateTime.Now, string.Empty, text);
            repo.AddMessageToChat(chatId, message);
        }
    }
}
