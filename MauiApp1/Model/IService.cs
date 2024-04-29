using MauiApp1.ViewModel;

namespace MauiApp1.Model
{
    public interface IService
    {
        void AddTextMessageToChat(int chatId, int senderId, string text);
        List<MessageModel> GetChatMessages(int chatId);
        List<ContactLastMessage> GetContactLastMessages(int userId, string name);
        string GetContactName(int chatId);
        string GetContactProfilePhotoPath(int chatId);
    }
}