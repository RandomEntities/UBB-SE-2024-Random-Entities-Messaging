using MauiApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test
{
    public class FakeRepository : IRepository
    {
        Chat chat;

        public FakeRepository()
        {
            chat = new Chat(0, 1, 0);
            TextMessage msg = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            chat.AddMessage(msg);
        }

        public void AddMessageToChat(int chatId, Message message)
        {
            if (chatId == 0) chat.AddMessage(message);
        }

        public Chat? GetChat(int chatId)
        {
            if (chatId == 0) return chat;
            return null;
        }

        public List<Chat> GetChatsByUser(int userId)
        {
            List<Chat> chats = new List<Chat>();

            if (userId == 0)
                chats.Add(chat);

            return chats;
        }

        public User? GetUser(int userId)
        {
            if (userId == 0) return new User(0, "DefaultName", "david.png");
            return null;
        }

        public void SortChatMessages(Chat chat)
        {
            throw new NotImplementedException();
        }
    }
}
