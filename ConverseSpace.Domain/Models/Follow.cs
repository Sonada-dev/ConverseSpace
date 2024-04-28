namespace ConverseSpace.Domain.Models;

public class Follow
{
    public Follow(Guid follower, Guid community)
    {
        Follower = follower;
        Community = community;
    }
    
    protected Follow() {}

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid Follower { get; private set; }
    public Guid Community { get; private set; }
}