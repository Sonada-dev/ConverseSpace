using NpgsqlTypes;

namespace ConverseSpace.Domain.Models.Enums;

public enum StatusPost : byte
{
    [PgName("published")]
    Published,
    [PgName("suggested")]
    Suggested,
    [PgName("rejected")]
    Rejected
}