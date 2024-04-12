namespace ConverseSpace.Domain.Errors;

public static class CommunitiesErrors
{
    public static readonly Error CommunityNotFound = new Error(
        404, "Сообщество не найдено");

    public static readonly Error CommunityLimit = new Error(
        403, "Обычному пользователю можно создать лишь 3 сообщества");


}