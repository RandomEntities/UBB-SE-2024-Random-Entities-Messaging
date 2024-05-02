using MauiApp1.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace MauiApp1Test
{
    public class UtilsTests
    {
        [Test]
        public void ReadUsersFromXml_LocallyInstantiatedDataWithDummyData_ReturnsDataFromFileCorrectly()
        {
            string testFilePath = "test_data.xml";

            List<User> expectedUsers = new List<User>
            {
                new User(1, "John Doe", "/path/to/profile1.jpg"),
                new User(2, "Alice Smith", "/path/to/profile2.jpg"),
                new User(3, "Bob Johnson", "/path/to/profile3.jpg")
            };
            Utils.WriteUserToXml(expectedUsers[0], testFilePath);
            Utils.WriteUserToXmlAppending(expectedUsers[1], testFilePath);
            Utils.WriteUserToXmlAppending(expectedUsers[2], testFilePath);

            // Act
            var actualUsers = Utils.ReadUsersFromXml(testFilePath);

            // Assert
            Assert.That(actualUsers, Is.Not.Null);
            Assert.That(actualUsers.Count, Is.EqualTo(expectedUsers.Count));
            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.That(actualUsers[i].UserId, Is.EqualTo(expectedUsers[i].UserId));
                Assert.That(actualUsers[i].Name, Is.EqualTo(expectedUsers[i].Name));
                Assert.That(actualUsers[i].ProfilePhotoPath, Is.EqualTo(expectedUsers[i].ProfilePhotoPath));
            }

            // Clean up
            File.Delete(testFilePath);
        }

        [Test]
        public void ReadChatsFromXml_LocallyInstantiatedDataWithDummyData_ReturnsDataFromFileCorrectly()
        {
            string testFilePath = "test_data.xml";

            List<Chat> expectedChats = new List<Chat>
            {
                new Chat(1, 1, 2),
                new Chat(2, 2, 3)
            };
            expectedChats[0].AddMessage(new TextMessage(1, 1, 2, DateTime.Now, "Delivered", "Hi Alice!"));
            expectedChats[0].AddMessage(new TextMessage(2, 1, 2, DateTime.Now, "Read", "How are you?"));
            expectedChats[1].AddMessage(new TextMessage(3, 2, 3, DateTime.Now, "Delivered", "Hey Bob!"));
            expectedChats[1].AddMessage(new TextMessage(4, 2, 3, DateTime.Now, "Read", "I'm good, thanks!"));

            Utils.WriteChatsToXml(expectedChats, testFilePath);

            List<Chat> actualChats = Utils.ReadChatsFromXml(testFilePath);

            Assert.That(actualChats, Is.Not.Null);
            Assert.That(actualChats.Count, Is.EqualTo(expectedChats.Count));

            for (int i = 0; i < expectedChats.Count; i++)
            {
                Assert.That(actualChats[i].ChatId, Is.EqualTo(expectedChats[i].ChatId));
                Assert.That(actualChats[i].SenderId, Is.EqualTo(expectedChats[i].SenderId));
                Assert.That(actualChats[i].ReceiverId, Is.EqualTo(expectedChats[i].ReceiverId));

                var expectedMessages = expectedChats[i].GetAllMessages();
                var actualMessages = actualChats[i].GetAllMessages();

                Assert.That(actualMessages.Count, Is.EqualTo(expectedMessages.Count));
                for (int j = 0; j < expectedMessages.Count; j++)
                {
                    Assert.That(actualMessages[j].GetMessageId(), Is.EqualTo(expectedMessages[j].GetMessageId()));
                    Assert.That(actualMessages[j].GetChatId(), Is.EqualTo(expectedMessages[j].GetChatId()));
                    Assert.That(actualMessages[j].GetSenderId(), Is.EqualTo(expectedMessages[j].GetSenderId()));
                    //Assert.That(actualMessages[j].GetTimestamp(), Is.EqualTo(expectedMessages[j].GetTimestamp()));
                    Assert.That(actualMessages[j].GetMessageContent(), Is.EqualTo(expectedMessages[j].GetMessageContent()));
                    Assert.That(actualMessages[j].GetStatus(), Is.EqualTo(expectedMessages[j].GetStatus()));
                }
            }

            // Clean up
            File.Delete(testFilePath);
        }

        [Test]
        public void WriteUserToXml_LocallyInstantiatedUser_UserWrittenToFile()
        {
            // Arrange
            string filePath = "test_write_user.xml";
            User user = new User(1, "John Doe", "/path/to/profile1.jpg");

            // Act
            Utils.WriteUserToXml(user, filePath);

            // Assert
            Assert.That(File.Exists(filePath), Is.True, "File should exist after writing user data.");

            // Clean up
            File.Delete(filePath);
        }

        [Test]
        public void WriteUserToXmlAppending_LocallyInstantiatedUser_UserAppendedToFile()
        {
            // Arrange
            string filePath = "test_append_user.xml";
            User user = new User(2, "Alice Smith", "/path/to/profile2.jpg");

            // Act
            Utils.WriteUserToXmlAppending(user, filePath);

            // Assert
            Assert.That(File.Exists(filePath), Is.True, "File should exist after appending user data.");

            // Clean up
            File.Delete(filePath);
        }

        [Test]
        public void WriteChatsToXml_LocallyInstantiatedChat_ChatsWrittenToFile()
        {
            // Arrange
            string filePath = "test_write_chats.xml";
            List<Chat> chats = new List<Chat>();
            Chat chat = new Chat(0, 0, 1);
            chats.Add(chat);

            // Act
            Utils.WriteChatsToXml(chats, filePath);

            // Assert
            Assert.That(File.Exists(filePath), Is.True, "File should exist after writing chat data.");

            // Clean up
            File.Delete(filePath);
        }

        [Test]
        public void ToStringWithLeadingZero_ProvidedTwoDummyNumbers_ReturnsStringWithLeadingZero()
        {
            // Act
            int firstNumber = 5;
            int secondNumber = 12;

            // Arrange
            string firstResult = Utils.ToStringWithLeadingZero(firstNumber);
            string secondResult = Utils.ToStringWithLeadingZero(secondNumber);

            // Assert
            Assert.That(firstResult, Is.EqualTo("05"));
            Assert.That(secondResult, Is.EqualTo("12"));
        }
    }
}
