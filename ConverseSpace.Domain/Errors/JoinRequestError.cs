namespace ConverseSpace.Domain.Errors;

public class JoinRequestError
{
    public static readonly Error RequestNotFound = new Error(
        404, "Заявка не найдена");
}