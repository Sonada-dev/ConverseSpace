using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Communities.Services;

public class CommunitiesService(ICommunitiesRepository communitiesRepository) : ICommunitiesService
{
    private readonly ICommunitiesRepository _communitiesRepository = communitiesRepository;
    
    public async Task<List<Community>> GetCommunities() =>
        await _communitiesRepository.Get();

    public async Task<Result> CreateCommunity(Community community)
    {
        try
        {
            await _communitiesRepository.Add(community);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(500, ex.Message));
        }
    }

    public async Task<Result> DeleteCommunity(Guid id)
    {
        await _communitiesRepository.Delete(id);
        return Result.Success();
    }
}