using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Services;

public interface ICommunitiesService
{
    Task<List<Community>> GetCommunities();
    Task<string> CreateCommunity(Community community);
    Task<string> DeleteCommunity(Guid id);
}
