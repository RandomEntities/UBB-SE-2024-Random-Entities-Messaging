using MauiApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test
{
    public class ChatTests
    {
        Chat chat;

        [SetUp]
        public void Setup()
        {
            chat = new Chat(0, 0, 1);
        }

        [Test]
        public void ChatAddMessage_AddMessageToEmptyChat_MessageAddedToChat()
        {
            TextMessage msg = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            chat.AddMessage(msg);
            Assert.That(chat.MessageList[0], Is.EqualTo(msg));
        }
    }
}
