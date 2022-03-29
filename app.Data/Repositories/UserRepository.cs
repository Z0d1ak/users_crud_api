using System.Threading;
using System.Threading.Tasks;
using app.Data.Entities;
using app.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Repositories
{
    public sealed class UserRepository
    {
        private readonly DataContext dataContext;
        private readonly UsersCache cache;

        public UserRepository(DataContext dataContext, UsersCache cache)
        {
            this.dataContext = dataContext;
            this.cache = cache;
        }

        public User Get(int userId)
        {
            return this.cache.Get(userId);
        }

        public async Task<User> SetStatusAsync(int userId, Status newStatus, CancellationToken ct = default)
        {
            var user = (User)this.cache.Get(userId)?.Clone();
            if (user is null)
                throw new ExceptionWithCode(ErrorCode.UserNotFound, "User not found");

            user.Status = newStatus;
            this.dataContext.Attach(user);
            this.dataContext.Entry(user).Property(x => x.Status).IsModified = true;

            try
            { 
                await dataContext.SaveChangesAsync(ct);
                this.cache.Update(user);
            }
            catch(DbUpdateConcurrencyException)
            {
                throw new ExceptionWithCode(ErrorCode.UserNotFound, "User not found");
            }
            return user;
        }


        public async Task<User> CreateAsync(User user, CancellationToken ct = default)
        {
            if (this.cache.Get(user.Id) is not null)
                return null;

            this.dataContext.Users.Add(user);

            try
            {
                await this.dataContext.SaveChangesAsync(ct);
                this.cache.Add(user);
                return user;
            }
            catch(DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public async Task<User> DeleteAsync(int userId, CancellationToken ct = default)
        {
            var user = this.cache.Get(userId);
            if (user is null)
                return null;

            this.dataContext.Entry(user).State = EntityState.Deleted;

            try
            {
                await this.dataContext.SaveChangesAsync(ct);
                this.cache.Delete(userId);
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

    }
}
