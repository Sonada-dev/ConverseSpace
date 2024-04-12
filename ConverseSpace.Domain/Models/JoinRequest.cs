using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Domain.Models;

public class JoinRequest
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid User { get; private set; }
    public Guid Community { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public StatusRequest? Status { get; set; } = StatusRequest.Pending;
    

    // Конструктор
    protected JoinRequest() { }

    public JoinRequest(Guid userId, Guid communityId)
    {
        User = userId;
        Community = communityId;
    }

    // Методы изменения свойств
    public void UpdateStaus(StatusRequest status)
    {
        Status = status;
    }
}