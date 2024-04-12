using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface ICommunitiesRepository
{
    Task<List<Community>> Get();
    Task<Community> GetById(Guid id);
    Task<Community> GetByIdFull(Guid id);
    Task Add(Community community);
    Task Update(Community community);
    Task Unfollow(Guid communityId, Guid userId);
}