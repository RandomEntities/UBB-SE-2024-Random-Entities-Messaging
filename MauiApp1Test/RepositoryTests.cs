using MauiApp1.Model;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test
{
    public class RepositoryTests
    {
        Repository repo;

        [SetUp]
        public void Setup()
        {
            string testChatFilePath = "test_chat_data.xml";

            List<Chat> expectedChats = new List<Chat>
            {
                new Chat(1, 1, 2),
                new Chat(2, 2, 3)
            };
            expectedChats[0].AddMessage(new TextMessage(1, 1, 2, DateTime.Now, "Delivered", "Hi Alice!"));
            expectedChats[0].AddMessage(new TextMessage(2, 1, 2, DateTime.Now, "Read", "How are you?"));
            expectedChats[1].AddMessage(new TextMessage(3, 2, 3, DateTime.Now, "Delivered", "Hey Bob!"));
            expectedChats[1].AddMessage(new TextMessage(4, 2, 3, DateTime.Now, "Read", "I'm good, thanks!"));

            Utils.WriteChatsToXml(expectedChats, testChatFilePath);

            string testUserFilePath = "test_user_data.xml";

            List<User> expectedUsers = new List<User>
            {
                new User(1, "John Doe", "/path/to/profile1.jpg"),
                new User(2, "Alice Smith", "/path/to/profile2.jpg"),
                new User(3, "Bob Johnson", "/path/to/profile3.jpg")
            };
            Utils.WriteUserToXml(expectedUsers[0], testUserFilePath);
            Utils.WriteUserToXmlAppending(expectedUsers[1], testUserFilePath);
            Utils.WriteUserToXmlAppending(expectedUsers[2], testUserFilePath);

            repo = new Repository(testUserFilePath, testChatFilePath);
        }

        [Test]
        public void SortChatMessages_ChatMessagesBeSorted()
        {
            Chat auxChat = new Chat(1, 1, 2);
            TextMessage msg1 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            TextMessage msg2 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hi");
            auxChat.AddMessage(msg1);
            auxChat.AddMessage(msg2);
            repo.SortChatMessages(auxChat);
            List<Message> messagesAux = auxChat.GetAllMessages();
            Assert.That(messagesAux[0].Equals(msg1));
            Assert.That(messagesAux[1].Equals(msg2));
        }

        [Test]
        public void AddMessageToChat_NewChatsForExistingChat_MessageGetsAddedToChat()
        {
            TextMessage msg1 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            TextMessage msg2 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hi");
            repo.AddMessageToChat(1, msg1);
            repo.AddMessageToChat(1, msg2);

            Chat? chat = repo.GetChat(1);
            if (chat != null)
            {
                List<Message> messagesAux = chat.GetAllMessages();

                Assert.That(messagesAux[messagesAux.Count() - 2], Is.EqualTo(msg1));
                Assert.That(messagesAux[messagesAux.Count() - 1], Is.EqualTo(msg2));
            }
        }

        [Test]
        public void GetChatsByUser_ChatExists_ReturnsListOfChats()
        {
            List<Chat> auxChats = repo.GetChatsByUser(1);
            Assert.That(auxChats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetChatsByUser_ChatDoesNotExist_ReturnsEmptyList()
        {
            List<Chat> auxChats = repo.GetChatsByUser(6);
            Assert.That(auxChats.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetUser_ExistentUser_ReturnsUser()
        {
            User auxUser = repo.GetUser(1);

            Assert.NotNull(auxUser);
        }

        [Test]
        public void GetUser_NonExistentUser_ReturnsNull()
        {
            User auxUser = repo.GetUser(6);

            Assert.IsNull(auxUser);
        }

        [Test]
        public void GetChat_ExistentChat_ReturnsChat()
        {
            Chat auxChat = repo.GetChat(6);

            Assert.IsNull(auxChat);
        }

        [Test]
        public void GetChat_NonExistent_ReturnsNull()
        {
            Chat auxChat = repo.GetChat(6);

            Assert.IsNull(auxChat);
        }
    }
}
