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

            FakeUtils.WriteChatsToXml(expectedChats, testChatFilePath);

            string testUserFilePath = "test_user_data.xml";

            List<User> expectedUsers = new List<User>
            {
                new User(1, "John Doe", "/path/to/profile1.jpg"),
                new User(2, "Alice Smith", "/path/to/profile2.jpg"),
                new User(3, "Bob Johnson", "/path/to/profile3.jpg")
            };
            FakeUtils.WriteUserToXml(expectedUsers[0], testUserFilePath);
            FakeUtils.WriteUserToXmlAppending(expectedUsers[1], testUserFilePath);
            FakeUtils.WriteUserToXmlAppending(expectedUsers[2], testUserFilePath);
            Repository repo = new Repository(testUserFilePath, testChatFilePath);

        }

        [Test]
        //public void SortChatMessages_ChatMessagesBeSorted()
        //{
        //    var chat = new Chat(1, 101, 102);
        //    TextMessage msg1 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
        //    TextMessage msg2 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hi");
        //    chat.AddMessage(msg1);
        //    chat.AddMessage(msg2);
        //}

        public void AddMessageToChat_CheckIfMessageWasAddedToChat()
        {
            TextMessage msg1 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            TextMessage msg2 = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hi");
            repo.AddMessageToChat(1, msg1);
            repo.AddMessageToChat(1, msg2);

            List<Message> messagesAux = repo.GetChat(1).GetAllMessages();


            Assert.That(messagesAux[messagesAux.Count()-2], Is.EqualTo(msg1));
            Assert.That(messagesAux[messagesAux.Count()-1], Is.EqualTo(msg2));

        }
    }
}
