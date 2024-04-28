namespace ConverseSpace.Domain.Errors;

public class PostsErrors
{
    public static readonly Error PostNotFound = new Error(
        404, "Пост не найден");
    
    public static readonly Error NotFollower = new Error(
        403, "Пользователь не подписчик сообщества");
}