using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using MauiApp1.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace MauiApp1.Model
{
    public class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            Test();
        }

        private async void Test()
        {
            var response = await httpClient.GetAsync($"api/chat/getAllMessagesExample");
            response.EnsureSuccessStatusCode();
            var messages = await response.Content.ReadFromJsonAsync<List<TextMessage>>();
        }

        public List<Chat> GetUserChats(int userId)
        {
            var response = httpClient.GetAsync($"api/user/{userId}/chats").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<List<Chat>>().Result;
        }

        public Chat GetChat(int chatId)
        {
            var response = httpClient.GetAsync($"api/chat/{chatId}").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Chat>().Result;
        }

        public List<User> GetChatParticipants(int chatId)
        {
            var response = httpClient.GetAsync($"api/chat/{chatId}/participants").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<List<User>>().Result;
        }

        public List<Message> GetChatMessages(int chatId)
        {
            var response = httpClient.GetAsync($"api/chat/{chatId}/messages").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<List<Message>>().Result;
        }

        public void AddMessage(Message message)
        {
            var response = httpClient.PostAsJsonAsync("api/chat", message).Result;
            response.EnsureSuccessStatusCode();
        }

        public Message GetChatLastMessage(int chatId)
        {
            var response = httpClient.GetAsync($"api/chat/{chatId}/lastmessage").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Message>().Result;
        }

        public void AddMessageToChat(int chatId, Message message)
        {
            var response = httpClient.PostAsJsonAsync($"api/chat/{chatId}/addmessage", message).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
