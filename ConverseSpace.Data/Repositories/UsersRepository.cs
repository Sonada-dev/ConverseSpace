using AutoMapper;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class UsersRepository(CSDBContext context, IMapper mapper) : IUsersRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(_mapper.Map<UserEntity>(user));
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        if (userEntity is null)
            return null;

        return _mapper.Map<User>(userEntity);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (userEntity is null)
            return null;

        return _mapper.Map<User>(userEntity);
    }
}