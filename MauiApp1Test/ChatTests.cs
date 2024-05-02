using MauiApp1.Model;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test
{
    public class ChatTests
    {
        [Test]
        public void AddMessage_LocallyInstantiatedChat_AddsMessageToList()
        {
            // Arrange
            var chat = new Chat(1, 101, 102);
            TextMessage message = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");

            // Act
            chat.AddMessage(message);

            // Assert
            Assert.That(chat.GetAllMessages()[0], Is.EqualTo(message));
            Assert.That(chat.GetAllMessages().Count, Is.EqualTo(1));
        }

        [Test]
        public void GetLastMessage_LocallyInstantiatedChat_ReturnsLastMessage()
        {
            // Arrange
            var chat = new Chat(1, 101, 102);
            TextMessage firstMessage = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            TextMessage secondMessage = new TextMessage(1, 0, 0, DateTime.Now, "Sent", "Hi");
            chat.AddMessage(firstMessage);
            chat.AddMessage(secondMessage);

            // Act
            var lastMessage = chat.GetLastMessage();

            // Assert
            Assert.That(lastMessage, Is.EqualTo(secondMessage));
        }

        [Test]
        public void SetMessageList_LocallyInstantiatedChat_SetsMessageList()
        {
            // Arrange
            var chat = new Chat(1, 101, 102);
            var newMessageList = new List<Message>
            {
            new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello"),
            new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hi")
            };

            // Act
            chat.SetMessageList(newMessageList);

            // Assert
            Assert.That(newMessageList, Is.EqualTo(chat.GetAllMessages()));
        }

        [Test]
        public void GetAllMessages_LocallyInstantiatedChat_ReturnsAllMessages()
        {
            // Arrange
            var chat = new Chat(1, 101, 102);
            TextMessage firstMessage = new TextMessage(0, 0, 0, DateTime.Now, "Sent", "Hello");
            TextMessage secondMessage = new TextMessage(1, 0, 0, DateTime.Now, "Sent", "Hi");
            chat.AddMessage(firstMessage);
            chat.AddMessage(secondMessage);

            // Act
            var allMessages = chat.GetAllMessages();

            // Assert
            Assert.That(allMessages.Count, Is.EqualTo(2));
            Assert.IsTrue(allMessages.Contains(firstMessage));
            Assert.IsTrue(allMessages.Contains(secondMessage));
        }
    }
}
