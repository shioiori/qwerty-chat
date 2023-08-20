using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;
using qwerty_chat_api.Repositories.Interface;
using qwerty_chat_api.Services.Interface;

namespace qwerty_chat_api.Services
{
    public class UsersService : IUser
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(User obj)
        {
           await _userRepository.CreateAsync(obj);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetAsync(string id)
        {
            return _userRepository.GetAsync(id);
        }

        public Task RemoveAsync(string id)
        {
            return _userRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(string id, User obj)
        {
            await _userRepository.UpdateAsync(id, obj);
        }
    }
}
