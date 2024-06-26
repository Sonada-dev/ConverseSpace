namespace ConverseSpace.Domain.Errors;

public static class FollowsErrors
{
    public static readonly Error SameUser = new Error(
        400, "Нельзя подписаться на собственное сообщество");
    
    public static readonly Error UserNotFound = new Error(
        404, "Пользователь не найден");
    
    public static readonly Error CommunityNotFound = new Error(
        404, "Сообщество не найдено");
    
    public static readonly Error AlreadyFollowing = new Error(
        409, "Уже подписан");
    
    public static readonly Error AlreadyUnfollowing = new Error(
        404, "Не подписан");
    
    public static readonly Error Request = new Error(
        200, "Заявка на вступление отправлена");
}