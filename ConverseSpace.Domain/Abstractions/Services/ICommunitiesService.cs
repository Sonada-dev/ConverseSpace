using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Services;

public interface ICommunitiesService
{
    Task<List<Community>> GetCommunities();
    Task<Result> CreateCommunity(Community community);
    Task<Result> DeleteCommunity(Guid id);
    Task<Result> UpdateCommunity(Community community);
}
