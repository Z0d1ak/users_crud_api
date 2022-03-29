using System.Threading;
using System.Threading.Tasks;
using app.Data.Entities;
using app.Data.Repositories;

namespace app.Services.Services
{
    public sealed class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Get(int userId)
        {
            return this.userRepository.Get(userId);
        }

        public Task<User> SetStatusAsync(int userId, Status newStatus, CancellationToken ct = default)
        {
            return this.userRepository.SetStatusAsync(userId, newStatus, ct);
        }


        public Task<User> CreateAsync(User user, CancellationToken ct = default)
        {
            return this.userRepository.CreateAsync(user, ct);
        }

        public Task<User> DeleteAsync(int userId, CancellationToken ct = default)
        {
            return this.userRepository.DeleteAsync(userId, ct);
        }
    }
}
