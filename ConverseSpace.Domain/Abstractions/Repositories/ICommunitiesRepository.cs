using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface ICommunitiesRepository
{
    Task<List<Community>> Get();
    Task Add(Community community);
    Task Delete(Guid id);
}