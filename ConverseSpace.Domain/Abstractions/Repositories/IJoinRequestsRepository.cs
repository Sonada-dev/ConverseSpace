using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IJoinRequestsRepository
{
    Task<Result<List<JoinRequest>>> Get(Guid communityId);
    Task<Result<JoinRequest>> GetById(Guid requestId);
    Task<Result> Add(JoinRequest request);
    Task<Result> Update(JoinRequest request);
}