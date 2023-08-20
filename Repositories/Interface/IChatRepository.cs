using qwerty_chat_api.Models;

namespace qwerty_chat_api.Repositories.Interface
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<List<Chat>> GetUserChat(string user_id);
    }
}
