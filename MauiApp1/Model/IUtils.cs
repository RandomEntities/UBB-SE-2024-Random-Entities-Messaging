using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Model
{
    public interface IUtils
    {
        List<User> ReadUsersFromXml(string filePath);
        void WriteUserToXml(User user, string filePath);
        void WriteUserToXmlAppending(User user, string filePath);
        List<Chat> ReadChatsFromXml(string filePath);
        void WriteChatsToXml(List<Chat> chats, string filePath);
        string ToStringWithLeadingZero(int number);
    }
}
