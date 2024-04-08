using Npgsql;

namespace ConverseSpace.Application;

public static class HandleException
{
    public static async Task<string> HandleExceptionAsync(Func<Task> action)
    {
        try
        {
            await action();
            return "Операция успешно завершена";
        }
        catch (PostgresException ex)
        {
            return $"Возникла ошибка на стороне БД: {ex}";
        }
        catch (Exception ex)
        {
            return $"Непредвиденная ошибка: {ex}";
        }
    }

    public static bool CheckHandleException(string result) => 
        result == "Операция успешно завершена";
}