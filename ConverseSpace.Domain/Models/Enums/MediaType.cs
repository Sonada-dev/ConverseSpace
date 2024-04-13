using NpgsqlTypes;

namespace ConverseSpace.Domain.Models.Enums;

public enum MediaType
{
    [PgName("img")]
    Img,
    [PgName("video")]
    Video,
    [PgName("audio")]
    Audio,
    [PgName("gif")]
    Gif
}