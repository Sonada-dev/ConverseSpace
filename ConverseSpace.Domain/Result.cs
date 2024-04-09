namespace ConverseSpace.Domain;

public class Result
{
    private protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    private Result(bool isSuccess, Error error, T? value) : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value 
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException("Cannot access value on a failed result.");
            return _value!; // _value is guaranteed to be non-null if IsSuccess is true
        }
    }

    public static Result<T> Success(T value) => new(true, Error.None, value);

    public new static Result<T> Failure(Error error) => new(false, error, default); 
}

public sealed record Error(int? Code, string Description)
{
    public static readonly Error None = new(null, string.Empty);
}
