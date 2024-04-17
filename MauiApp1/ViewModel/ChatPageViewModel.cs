using MauiApp1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        private int userId;
        private readonly Service service;
        private int chatId;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _contactName;
        public string ContactName
        {
            get => _contactName;
            private set
            {
                if (_contactName != value)
                {
                    _contactName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _contactProfilePhotoPath;
        public string ContactProfilePhotoPath
        {
            get => _contactProfilePhotoPath;
            private set
            {
                if (value != _contactProfilePhotoPath)
                {
                    _contactProfilePhotoPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<MessageModel> Messages { get; private set; }

        public ChatPageViewModel(Service service, int userId)
        {
            this.service = service;
            this.userId = userId;
            Messages = new ObservableCollection<MessageModel>();
        }

        private void RefreshChatMessages()
        {
            List<MessageModel> messages = service.GetChatMessages(chatId);
            Messages.Clear();
            foreach (MessageModel m in messages)
            {
                Messages.Add(m);
            }
        }

        public void SetChatId(int chatId)
        {
            this.chatId = chatId;
            ContactName = service.GetContactName(chatId);
            ContactProfilePhotoPath = service.GetContactProfilePhotoPath(chatId);
            RefreshChatMessages();
        }

        public void AddTextMessageToChat(string text)
        {
            service.AddTextMessageToChat(chatId, userId, text);
            RefreshChatMessages();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
