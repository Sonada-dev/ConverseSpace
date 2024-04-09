using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class UsersRepository(CSDBContext context, IMapper mapper) : IUsersRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<User>> Get() =>
        await _context.Users
            .AsNoTracking()
            .Include(u => u.Communities)
            .ProjectTo<User>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<User> GetById(Guid id) =>
        _mapper.Map<User>(await _context.Users
            .Include(u => u.Communities)
            .FirstOrDefaultAsync(u => u.Id == id));

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(_mapper.Map<UserEntity>(user));
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .Include(u => u.Communities)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        if (userEntity is null)
            return null;

        return _mapper.Map<User>(userEntity);
    }

    public async Task<User?> GetByUsername(string username)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .Include(u => u.Communities)
            .FirstOrDefaultAsync(u =>
                u.Username.ToLower() == username.ToLower());

        if (userEntity is null)
            return null;

        return _mapper.Map<User>(userEntity);
    }
}