using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services.Interface
{
    public interface IMessage : IBaseService<Message>
    {
        Task StoredMessage(string id);
    }
}
