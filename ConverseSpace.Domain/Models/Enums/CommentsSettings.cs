using NpgsqlTypes;

namespace ConverseSpace.Domain.Models.Enums;

public enum CommentsSettings
{
    [PgName("closed")]
    Closed,
    [PgName("open")]
    Open,    
    [PgName("open_for_followers")]
    OpenForFollowers
}