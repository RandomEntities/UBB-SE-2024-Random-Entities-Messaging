﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MauiApp1.Model
{
    public class Chat
    {
        public int ChatId { get; private set; }

        public Chat(int chatId)
        {
            this.ChatId = chatId;
        }
    }
}
