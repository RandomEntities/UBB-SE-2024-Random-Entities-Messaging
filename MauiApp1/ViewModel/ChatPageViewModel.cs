using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Model;

namespace MauiApp1.ViewModel
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        private int userId;
        private int chatId;
        private readonly IService service;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string contactName;
        public string ContactName
        {
            get => contactName;
            private set
            {
                if (contactName != value)
                {
                    contactName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string contactProfilePhotoPath;
        public string ContactProfilePhotoPath
        {
            get => contactProfilePhotoPath;
            private set
            {
                if (value != contactProfilePhotoPath)
                {
                    contactProfilePhotoPath = value;
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

        public void SetChatId(int chatId) // This is called from the ChatPage whenever the chatId is changed there.
        {
            this.chatId = chatId;
            ContactName = service.GetChatSummary(userId, chatId).PaticipantsNames;
            ContactProfilePhotoPath = service.GetChatSummary(userId, chatId).PhotoUrl;
            RefreshChatMessages();
        }

        private void RefreshChatMessages() // Refresh the list with the messages based on the chat id.
        {
            // Clear the list with the old messages and add the ones from returned by the service.
            Messages.Clear();
            foreach (MessageModel messageModel in service.GetChatMessageModels(chatId, userId))
            {
                Messages.Add(messageModel);
            }
        }

        public void AddTextMessageToChat(string text)
        {
            service.AddTextMessageToChat(chatId, userId, text);
            RefreshChatMessages();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
