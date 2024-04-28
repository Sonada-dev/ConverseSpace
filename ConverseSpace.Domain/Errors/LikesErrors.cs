namespace ConverseSpace.Domain.Errors;

public class LikeErrors
{
    public static readonly Error PostNotFound = new Error(
        404, "Пост не найден");
    
    public static readonly Error UserNotFound = new Error(
        404, "Пользователь не найден");

    public static readonly Error LikeAlreadyExists = new Error(
        400, "Пользователь уже поставил лайк на этот пост");

    public static readonly Error LikeDoesNotExist = new Error(
        400, "Пользователь еще не поставил лайк на этот пост");

    public static readonly Error DislikeAlreadyExists = new Error(
        400, "Пользователь уже поставил дизлайк на этот пост");

    public static readonly Error DislikeDoesNotExist = new Error(
        400, "Пользователь еще не поставил дизлайк на этот пост");
    
    
}
