using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MauiApp1.Model
{
    public class Repository : IRepository
    {
        private string usersFilePath;
        private string chatsFilePath;
        private List<User> allUsers;
        private List<Chat> allChats;

        public Repository(string usersFilePath, string chatsFilePath)
        {
            this.usersFilePath = usersFilePath;
            this.chatsFilePath = chatsFilePath;
            LoadUsersAndChats();
        }

        private void SortChatMessages(Chat chat)
        {
            List<Message> sortedMessages = chat.GetAllMessages().OrderBy(message => message.GetTimestamp()).ToList();
            chat.SetMessageList(sortedMessages);
        }

        private void LoadUsersAndChats()
        {
            allUsers = Utils.ReadUsersFromXml(usersFilePath);
            allChats = Utils.ReadChatsFromXml(chatsFilePath);
            foreach (Chat c in allChats)
            {
                SortChatMessages(c);
            }
        }

        private void SaveChats()
        {
            Utils.WriteChatsToXml(allChats, chatsFilePath);
        }

        public List<Chat> GetChatsByUser(int userId)
        {
            List<Chat> chats = new List<Chat>();
            foreach (Chat chat in allChats)
            {
                if (chat.SenderId == userId)
                {
                    chats.Add(chat);
                }
            }

            return chats;
        }

        public User? GetUser(int userId)
        {
            return allUsers.Find(user => user.UserId == userId);
        }

        public Chat? GetChat(int chatId)
        {
            return allChats.Find(chat => chat.ChatId == chatId);
        }

        public void AddMessageToChat(int chatId, Message message)
        {
            Chat? chat = GetChat(chatId);
            if (chat == null)
            {
                return;
            }

            int oppositeChatId = (chatId % 2 == 0 ? chatId - 1 : chatId + 1);
            Chat? oppositeChat = GetChat(oppositeChatId);
            if (oppositeChat == null)
            {
                return;
            }

            int lastId = 0;
            foreach (Message m in chat.GetAllMessages())
            {
                int mId = m.GetMessageId();
                if (mId > lastId)
                {
                    lastId = mId;
                }
            }

            message.SetMessageId(lastId + 1);
            message.SetStatus("Sent");

            Message message2;

            if (message is TextMessage)
            {
                message2 = new TextMessage(lastId + 1, oppositeChatId, message.GetSenderId(), message.GetTimestamp(), "New", message.GetMessage());
            }
            else if (message is FileMessage)
            {
                message2 = new FileMessage(lastId + 1, oppositeChatId, message.GetSenderId(), message.GetTimestamp(), "New", message.GetMessage());
            }
            else if (message is VoiceMessage)
            {
                message2 = new VoiceMessage(lastId + 1, oppositeChatId, message.GetSenderId(), message.GetTimestamp(), "New", message.GetMessage());
            }
            else if (message is VideoMessage)
            {
                message2 = new VideoMessage(lastId + 1, oppositeChatId, message.GetSenderId(), message.GetTimestamp(), "New", message.GetMessage());
            }
            else
            {
                message2 = new PhotoMessage(lastId + 1, oppositeChatId, message.GetSenderId(), message.GetTimestamp(), "New", message.GetMessage());
            }

            chat.AddMessage(message);
            oppositeChat.AddMessage(message2);
            SortChatMessages(chat);
            SortChatMessages(oppositeChat);

            SaveChats();
        }
    }
}
