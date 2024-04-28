using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Errors;
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
        var community = await _communitiesRepository.GetByIdFull(id);

        if (community is null)
            return Result.Failure(CommunitiesErrors.CommunityNotFound);
        
        
        community.Delete(true);
         
        await _communitiesRepository.Update(community);
        return Result.Success();
    }

    public async Task<Result> UpdateCommunity(Community newCommunity)
    {
        var community = await _communitiesRepository.GetById(newCommunity.Id);

        if (community is null)
            return Result.Failure(CommunitiesErrors.CommunityNotFound);

        community.UpdateTitle(newCommunity.Title);
        community.UpdateDescription(newCommunity.Description);
        community.UpdatePrivate(newCommunity.Private);
        community.UpdateCommentsSettings(newCommunity.Comments);
        
        await _communitiesRepository.Update(community);
        return Result.Success();
    }
}