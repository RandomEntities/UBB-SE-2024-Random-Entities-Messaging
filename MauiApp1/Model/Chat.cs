using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MauiApp1.Model
{
    public class Chat
    {

        public int chatId { get; private set; }
        public int senderId { get; private set; }
        public int receiverId { get; private set;  }

        private List<Message> messageList = new List<Message>();


        public Chat(int chatId, int senderId, int receiverId)
        {
            this.chatId = chatId;
            this.senderId = senderId;
            this.receiverId = receiverId;
        }

        public void addMessage(Message newMessage)
        {
            this.messageList.Add(newMessage);

        }

        public Message getLastMessage()
        {
            return this.messageList.Last();
        }

        public void setMessageList(List<Message> newMessageList)
        {
            this.messageList = newMessageList;
        }

        public List<Message> getAllMessages()
        {
            return this.messageList;
        }

    }

}
