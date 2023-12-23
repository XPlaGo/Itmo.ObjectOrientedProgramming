using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(long id);

    public Task<User?> GetUserByUsernameAsync(string username);

    public Task<List<User>> GetAllAsync();

    public Task<long> AddAsync(User entity);

    public Task UpdateAsync(User entity);

    public Task DeleteAsync(User entity);
}