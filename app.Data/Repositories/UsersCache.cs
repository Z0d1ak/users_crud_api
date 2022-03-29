using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using app.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace app.Data.Repositories
{
    public sealed class UsersCache
    {
        private System.Threading.Timer timer;
        private readonly IDesignTimeDbContextFactory<DataContext> factory;
        private Dictionary<int, User> usersCache;
        static ReaderWriterLockSlim rwl = new();

        public UsersCache(IDesignTimeDbContextFactory<DataContext> factory)
        {
            this.factory = factory;
        }

        public void Start()
        {
            timer = new System.Threading.Timer(
                e => UpdateCache(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMinutes(10));
        }

        public User Get(int userId)
        {
            rwl.EnterReadLock();

            try
            {
                usersCache.TryGetValue(userId, out var user);
                return user;
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        public bool Add(User user)
        {
            rwl.EnterReadLock();

            try
            {
                usersCache.TryGetValue(user.Id, out var existentuser);
                if (existentuser is not null)
                {
                    return false;
                }
                else
                {
                    usersCache[user.Id] = user;
                    return true;
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        public bool Delete(int userId)
        {
            rwl.EnterReadLock();

            try
            {
                usersCache.TryGetValue(userId, out var existentuser);
                if (existentuser is not null)
                {
                    usersCache.Remove(userId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        public bool Update(User user)
        {
            rwl.EnterReadLock();

            try
            {
                usersCache.TryGetValue(user.Id, out var existentuser);
                if (existentuser is not null)
                {
                    usersCache[user.Id] = user;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        private async void UpdateCache()
        {
            Dictionary<int, User> users = null;
            if (usersCache is null)
            {
                rwl.EnterWriteLock();
                try
                {
                    using (var context = factory.CreateDbContext(Array.Empty<string>()))
                    {
                        usersCache = await context.Users.ToDictionaryAsync(x => x.Id, x => x).ConfigureAwait(false);
                    }
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
            }
            else
            {
                using (var context = factory.CreateDbContext(Array.Empty<string>()))
                {
                    users = await context.Users.ToDictionaryAsync(x => x.Id, x => x);
                }

                rwl.EnterWriteLock();

                try
                {
                    usersCache = users;
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
            }
        }
    }
}
