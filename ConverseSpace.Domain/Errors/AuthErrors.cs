namespace ConverseSpace.Domain.Errors;

public static class AuthErrors
{
    public static readonly Error InvalidCredentials = new Error(
        401, "Неправильные имя пользователя или пароль.");
    
    public static readonly Error AccountNotFound = new Error(
        404, "Аккаунт пользователя не найден.");
    
    public static readonly Error UsernameExist = new Error(
        409, "Пользователь с таким именем уже существует.");
    
    public static readonly Error EmailExist = new Error(
        409, "Пользователь с таким email уже существует.");
}