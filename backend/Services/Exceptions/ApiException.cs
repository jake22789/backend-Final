namespace final_proj.Services.Exceptions;

public abstract class ApiException : Exception
{
    protected ApiException(string message) : base(message)
    {
    }
}

public class BadRequestApiException : ApiException
{
    public BadRequestApiException(string message) : base(message)
    {
    }
}

public class NotFoundApiException : ApiException
{
    public NotFoundApiException(string message) : base(message)
    {
    }
}

public class ConflictApiException : ApiException
{
    public ConflictApiException(string message) : base(message)
    {
    }
}

public class UnauthorizedApiException : ApiException
{
    public UnauthorizedApiException(string message) : base(message)
    {
    }
}
