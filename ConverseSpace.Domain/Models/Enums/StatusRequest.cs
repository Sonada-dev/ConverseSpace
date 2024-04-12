using NpgsqlTypes;

namespace ConverseSpace.Domain.Models.Enums;

public enum StatusRequest
{
    [PgName("pending")]
    Pending,
    [PgName("approved")]
    Approved,    
    [PgName("rejected")]
    Rejected
}