using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Communities.Services;

public class CommunitiesService(ICommunitiesRepository communitiesRepository) : ICommunitiesService
{
    private readonly ICommunitiesRepository _communitiesRepository = communitiesRepository;
    
    public async Task<List<Community>> GetCommunities() =>
        await _communitiesRepository.Get();

    public async Task<string> CreateCommunity(Community community) =>
        await HandleException.HandleExceptionAsync(async () => await _communitiesRepository.Add(community));
    
    public async Task<string> DeleteCommunity(Guid id) =>
        await HandleException.HandleExceptionAsync(async () => await _communitiesRepository.Delete(id));
}