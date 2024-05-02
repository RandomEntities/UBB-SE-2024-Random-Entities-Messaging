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
        private Repository repo;

        [SetUp]
        public void Setup()
        {
            string localPath = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.Parent.FullName;
            string usersFilePath = localPath + @"\MauiApp1\Data\users.xml";
            string chatsFilePath = localPath + @"\MauiApp1\Data\chats.xml";
            repo = new Repository(usersFilePath, chatsFilePath);
            service = new Service(repo);
        }

        [Test]
        public void GetContactLastMessages_WhenUserIdAndNameValid_ReturnsListOfContactLastMessages()
        {
            int userId = 1;
            string name = "Iulius Uriesu";

            List<ContactLastMessage> result = service.GetContactLastMessages(userId, name);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetContactName_WhenChatIdValid_ReturnsContactName()
        {
            int chatId = 1;

            string contactName = service.GetContactName(chatId);

            Assert.That(contactName, Is.EqualTo("Razvan Uzum"));
        }

        [Test]
        public void GetContactProfilePhotoPath_WhenChatIdValid_ReturnsProfilePhotoPath()
        {
            int chatId = 1;

            string profilePhotoPath = service.GetContactProfilePhotoPath(chatId);

            Assert.That(profilePhotoPath, Is.EqualTo("razvan.png"));
        }

        [Test]
        public void GetChatMessage_WhenChatIdValid_ReturnsListOfMessageModels()
        {
            int chatId = 3;

            List<MessageModel> result = service.GetChatMessages(chatId);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void AddTextMessageToChat_WhenChatIdAndSenderIdValid_AddsTextMessageToChat()
        {
            int chatId = 2;
            int senderId = 3;
            string text = "Hello from testing part!";

            service.AddTextMessageToChat(chatId, senderId, text);

            List<Message> chatMessages = repo.GetChat(chatId).GetAllMessages();
            Assert.IsTrue(chatMessages.Exists(m => m.GetMessage() == text));
        }
    }
}
