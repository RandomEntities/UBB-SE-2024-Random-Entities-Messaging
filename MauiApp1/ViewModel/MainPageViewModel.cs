﻿using System;
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
    public class MainPageViewModel
    {
        private int userId;
        private readonly Service service;
        public ObservableCollection<ContactLastMessage> Contacts { get; private set; }

        public MainPageViewModel(Service service, int userId)
        {
            this.service = service;
            this.userId = userId;
            List<ContactLastMessage> contacts = service.GetContactLastMessages(userId, string.Empty);
            Contacts = new ObservableCollection<ContactLastMessage>(contacts);
        }

        public void RefreshContacts(string searchText)
        {
            List<ContactLastMessage> contacts = service.GetContactLastMessages(userId, searchText);
            Contacts.Clear();
            foreach (var c in contacts)
            {
                Contacts.Add(c);
            }
        }

        public void FilterContacts(string searchText)
        {
            if (searchText == null)
            {
                searchText = string.Empty;
            }
            RefreshContacts(searchText);
        }
    }
}
