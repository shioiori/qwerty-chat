using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services.Interface
{
    public interface IChat : IBaseService<Chat>
    {
        Task StoredChat(string id);
    }
}
