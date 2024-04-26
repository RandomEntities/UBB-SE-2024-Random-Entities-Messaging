using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MauiApp1.Model
{
    public class Utils
    {
        public static List<User> ReadUsersFromXml(string filePath)
        {
            List<User> users = new List<User>();
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    return users;
                }
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "User")
                        {
                            int userId = 0;
                            string name = string.Empty;
                            string profilePhotoPath = string.Empty;
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "UserID":
                                            userId = int.Parse(reader.ReadString());
                                            break;
                                        case "Name":
                                            name = reader.ReadString();
                                            break;
                                        case "ProfilePhotoPath":
                                            profilePhotoPath = reader.ReadString();
                                            break;
                                    }
                                }
                                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "User")
                                {
                                    users.Add(new User(userId, name, profilePhotoPath));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return users;
        }

        public static void WriteUserToXml(User user, string filePath)
        {
            bool fileExists = File.Exists(filePath);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                if (!fileExists)
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Users");
                }

                writer.WriteStartElement("User");
                writer.WriteElementString("UserID", user.UserId.ToString());
                writer.WriteElementString("Name", user.Name);
                writer.WriteElementString("ProfilePhotoPath", user.ProfilePhotoPath);
                writer.WriteEndElement();
                if (!fileExists)
                {
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }

            Console.WriteLine($"User data written to XML file: {filePath}");
        }

        public static void WriteUserToXmlAppending(User user, string filePath)
        {
            List<User> existingUsers = ReadUsersFromXml(filePath);
            existingUsers.Add(user);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Users");
                foreach (User u in existingUsers)
                {
                    writer.WriteStartElement("User");
                    writer.WriteElementString("UserID", u.UserId.ToString());
                    writer.WriteElementString("Name", u.Name);
                    writer.WriteElementString("ProfilePhotoPath", u.ProfilePhotoPath);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static List<Chat> ReadChatsFromXml(string filePath)
        {
            List<Chat> chats = new List<Chat>();
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    return chats;
                }

                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Chat")
                        {
                            int chatId = 0;
                            int senderId = 0;
                            int receiverId = 0;
                            List<Message> messages = new List<Message>();
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "ChatId":
                                            chatId = int.Parse(reader.ReadElementContentAsString());
                                            break;
                                        case "SenderId":
                                            senderId = int.Parse(reader.ReadElementContentAsString());
                                            break;
                                        case "ReceiverId":
                                            receiverId = int.Parse(reader.ReadElementContentAsString());
                                            break;
                                        case "Messages":
                                            // Read messages within the chat
                                            messages = ReadMessages(reader);
                                            break;
                                    }
                                }

                                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Chat")
                                {
                                    Chat c1 = new Chat(chatId, senderId, receiverId);
                                    c1.SetMessageList(messages);
                                    chats.Add(c1);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return chats;
        }

        private static List<Message> ReadMessages(XmlReader reader)
        {
            List<Message> messages = new List<Message>();

            if (reader.ReadToDescendant("Message"))
            {
                do
                {
                    int messageId = 0, chatId = 0, senderId = 0;
                    DateTime timestamp = DateTime.MinValue;
                    string content = string.Empty, status = string.Empty, type = string.Empty; // Added type field

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case "MessageId":
                                    messageId = int.Parse(reader.ReadElementContentAsString());
                                    break;
                                case "ChatId":
                                    chatId = int.Parse(reader.ReadElementContentAsString());
                                    break;
                                case "SenderId":
                                    senderId = int.Parse(reader.ReadElementContentAsString());
                                    break;
                                case "Timestamp":
                                    timestamp = DateTime.Parse(reader.ReadElementContentAsString());
                                    break;
                                case "Content":
                                    content = reader.ReadElementContentAsString();
                                    break;
                                case "Status":
                                    status = reader.ReadElementContentAsString();
                                    break;
                                case "Type":
                                    type = reader.ReadElementContentAsString(); // Read the type field
                                    break;
                            }
                        }

                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Message")
                        {
                            // messages.Add(new Message(messageId, chatId, senderId, timestamp, content, status, type)); // Pass type to Message constructor
                            // break;
                            Message message;
                            switch (type)
                            {
                                case "voice":
                                    message = new VoiceMessage(messageId, chatId, senderId, timestamp, status, content);
                                    break;
                                case "text":
                                    message = new TextMessage(messageId, chatId, senderId, timestamp, status, content);
                                    break;
                                case "photo":
                                    message = new PhotoMessage(messageId, chatId, senderId, timestamp, status, content);
                                    break;
                                case "video":
                                    message = new VideoMessage(messageId, chatId, senderId, timestamp, status, content);
                                    break;
                                case "file":
                                    message = new FileMessage(messageId, chatId, senderId, timestamp, status, content);
                                    break;

                                default:
                                    // message = new Message(messageId, chatId, senderId, timestamp, status, content);
                                    message = new TextMessage(messageId, chatId, senderId, timestamp, status, content);

                                    break;
                            }
                            messages.Add(message);
                            break;
                        }
                    }
                }
                while (reader.ReadToNextSibling("Message"));
            }

            return messages;
        }

        public static void WriteChatsToXml(List<Chat> chats, string filePath)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Chats");

                foreach (Chat c in chats)
                {
                    writer.WriteStartElement("Chat");
                    writer.WriteElementString("ChatId", c.ChatId.ToString());
                    writer.WriteElementString("SenderId", c.SenderId.ToString());
                    writer.WriteElementString("ReceiverId", c.ReceiverId.ToString());

                    writer.WriteStartElement("Messages");

                    foreach (Message m in c.GetAllMessages())
                    {
                        writer.WriteStartElement("Message");
                        writer.WriteElementString("MessageId", m.GetMessageId().ToString());
                        writer.WriteElementString("ChatId", m.GetChatId().ToString());
                        writer.WriteElementString("SenderId", m.GetSenderId().ToString());
                        writer.WriteElementString("Timestamp", m.GetTimestamp().ToString());
                        writer.WriteElementString("Content", m.GetMessage());
                        writer.WriteElementString("Status", m.GetStatus());

                        if (m.GetType() == typeof(TextMessage))
                        {
                            writer.WriteElementString("Type", "text");
                        }

                        if (m.GetType() == typeof(VideoMessage))
                        {
                            writer.WriteElementString("Type", "video");
                        }

                        if (m.GetType() == typeof(VoiceMessage))
                        {
                            writer.WriteElementString("Type", "voice");
                        }

                        if (m.GetType() == typeof(PhotoMessage))
                        {
                            writer.WriteElementString("Type", "photo");
                        }

                        if (m.GetType() == typeof(FileMessage))
                        {
                            writer.WriteElementString("Type", "file");
                        }

                        // writer.WriteElementString("Type", m.Type);
                        writer.WriteEndElement(); // Message
                    }

                    writer.WriteEndElement(); // Messages
                    writer.WriteEndElement(); // Chat
                }
            }
        }

        public static string ToStringWithLeadingZero(int number)
        {
            if (number <= 9)
            {
                return "0" + number.ToString();
            }
            else
            {
                return number.ToString();
            }
        }
    }
}