/* Unmerged change from project 'MauiApp1 (net8.0-ios)'
Before:
namespace MauiApp1.Model
After:
using MauiApp1.Model.Message;
using MauiApp1.Model.Message.Message;
using MauiApp1.Model.Message.Message.Message;

namespace MauiApp1.Model
*/

/* Unmerged change from project 'MauiApp1 (net8.0-windows10.0.19041.0)'
Before:
namespace MauiApp1.Model
After:
using MauiApp1.Model.Message;
using MauiApp1.Model.Message.Message;

namespace MauiApp1.Model
*/

/* Unmerged change from project 'MauiApp1 (net8.0-maccatalyst)'
Before:
namespace MauiApp1.Model
After:
using MauiApp1.Model.Message;

namespace MauiApp1.Model
*/

/* Unmerged change from project 'MauiApp1 (net8.0-windows10.0.19041.0)'
Before:
using MauiApp1.Model.Message.Message.Message;
using MauiApp1.Model.Message.Message.Message.Message;
After:
using MauiApp1.Model.Message.Message;
using MauiApp1.Model.Message.Message.Message;
*/

/* Unmerged change from project 'MauiApp1 (net8.0-maccatalyst)'
Before:
using MauiApp1.Model.Message.Message.Message.Message;
After:
using MauiApp1.Model.Message.Message;
*/
namespace MauiApp1.Model
{
    public class VoiceMessage : Message
    {
        private readonly string voiceNotePath;

        public VoiceMessage(int messageId, int chatId, int senderId, DateTime timestamp, string status, string voiceNotePath) : base(messageId, chatId, senderId, timestamp, status)
        {
            this.voiceNotePath = voiceNotePath;
        }

        public override string GetMessage()
        {
            return voiceNotePath;
        }
    }
}
