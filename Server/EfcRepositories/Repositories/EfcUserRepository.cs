using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext _ctx;

    public EfcUserRepository(AppContext ctx)
    {
        _ctx = ctx;
    }


    public async Task<User> AddAsync(User user)
    {
        var entityEntry = await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!await _ctx.Users.AnyAsync(p => p.UserId == user.UserId))
        {
            throw new KeyNotFoundException(
                $"User with id {user.UserId} not found");
        }
    }

    public async Task DeleteAsync(int userId)
    {
        var user =
            await _ctx.Users.SingleOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            throw new KeyNotFoundException(
                $"User with id {userId} not found");
        }

        _ctx.Users.Remove(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int userId)
    {
        var user = await _ctx.Users.Include(u => u.Posts)
            .Include(u => u.Comments)
            .SingleOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {userId} not found");
        }

        return user;
    }

    public IQueryable<User> GetMany()
    {
        return _ctx.Users.AsQueryable();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _ctx.Users.SingleOrDefaultAsync(
            u => u.UserName == username);
    }

    public async Task<bool> ExistsAsync(int userId)
    {
        return await _ctx.Users.AnyAsync(u => u.UserId == userId);
    }
}