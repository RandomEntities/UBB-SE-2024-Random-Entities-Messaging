using MauiApp1.Model;
using MauiApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test
{
    public class ServiceTests
    {
        private Service service;
        private FakeRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new FakeRepository();
            service = new Service(repo);
        }

        [Test]
        public void GetChatsSortedByLastMessageTimeStamp_WhenUserIdValid_ReturnsListOfChats()
        {
            int userId = 0;

            List<Chat> result = service.GetChatsSortedByLastMessageTimeStamp(userId);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetChatsSortedByLastMessageTimeStamp_WhenUserIdInvalid_ReturnsEmptyList()
        {
            int userId = 2;

            List<Chat> result = service.GetChatsSortedByLastMessageTimeStamp(userId);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterChatsByName_WhenUserIdAndNameValid_ReturnsListOfChats()
        {
            int userId = 0;
            string name = "DefaultName";

            List<Chat> result = service.FilterChatsByName(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterChatsByName_WhenNameInvalid_ReturnsEmptyList()
        {
            int userId = 0;
            string name = "NotDefaultName";

            List<Chat> result = service.FilterChatsByName(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterChatsByName_WhenUserIdInvalid_ReturnsEmptyList()
        {
            int userId = 2;
            string name = "DefaultName";

            List<Chat> result = service.FilterChatsByName(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterChatsByName_WhenUserIdAndNameInvalid_ReturnsEmptyList()
        {
            int userId = 2;
            string name = "NotDefaultName";

            List<Chat> result = service.FilterChatsByName(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetContactLastMessages_WhenUserIdAndNameValid_ReturnsListOfContactLastMessages()
        {
            int userId = 0;
            string name = "DefaultName";

            List<ContactLastMessage> result = service.GetContactLastMessages(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetContactLastMessages_WhenUserIdInvalid_ReturnsEmptyListOfMessages()
        {
            int userId = 3;
            string name = "DefaultName";

            List<ContactLastMessage> result = service.GetContactLastMessages(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetContactLastMessages_WhenNameInvalid_ReturnsEmptyListOfMessages()
        {
            int userId = 0;
            string name = "NotDefaultName";

            List<ContactLastMessage> result = service.GetContactLastMessages(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetContactLastMessages_WhenUserIdAndNameInvalid_ReturnsEmptyListOfMessages()
        {
            int userId = 2;
            string name = "NotDefaultName";

            List<ContactLastMessage> result = service.GetContactLastMessages(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetContactName_WhenChatIdValid_ReturnsContactName()
        {
            int chatId = 0;

            string contactName = service.GetContactName(chatId);

            Assert.That(contactName, Is.EqualTo("DefaultName"));
        }

        [Test]
        public void GetContactName_WhenChatIdInvalid_ReturnsEmptyString()
        {
            int chatId = 1;

            string contactName = service.GetContactName(chatId);

            Assert.That(contactName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetContactProfilePhotoPath_WhenChatIdValid_ReturnsProfilePhotoPath()
        {
            int chatId = 0;

            string profilePhotoPath = service.GetContactProfilePhotoPath(chatId);

            Assert.That(profilePhotoPath, Is.EqualTo("david.png"));
        }

        [Test]
        public void GetContactProfilePhotoPath_WhenChatIdInvalid_ReturnsEmptyString()
        {
            int chatId = 1;

            string profilePhotoPath = service.GetContactProfilePhotoPath(chatId);

            Assert.That(profilePhotoPath, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetChatMessage_WhenChatIdValid_ReturnsListOfMessageModels()
        {
            int chatId = 0;

            List<MessageModel> result = service.GetChatMessages(chatId);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetChatMessage_WhenChatIdInvalid_ReturnsEmptyList()
        {
            int chatId = 2;

            List<MessageModel> result = service.GetChatMessages(chatId);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddTextMessageToChat_WhenChatIdValid_AddsTextMessageToChat()
        {
            int chatId = 0;
            int senderId = 1;
            string text = "Hello from testing part!";

            service.AddTextMessageToChat(chatId, senderId, text);

            List<Message> chatMessages = repo.GetChat(chatId).GetAllMessages();
            Assert.That(chatMessages.Count, Is.EqualTo(2));
            Assert.IsTrue(chatMessages.Exists(m => m.GetMessageContent() == text));
        }
    }
}
