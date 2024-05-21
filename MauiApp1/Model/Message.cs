using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Model
{
    public abstract class Message
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }

        public Message(int messageId, int chatId, int senderId, DateTime timestamp, string status)
        {
            this.MessageId = messageId;
            this.ChatId = chatId;
            this.SenderId = senderId;
            this.Timestamp = timestamp;
            this.Status = status;
        }

        public abstract string GetMessageContent();
    }
}
