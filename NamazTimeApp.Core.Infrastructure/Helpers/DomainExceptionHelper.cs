namespace NamazTimeApp.Core.Infrastructure.Helpers
{
    public static class DomainExceptionHelper
{
    /// <summary>
    /// Maps the exception to response.
    /// </summary>
    /// <param name="domainException">The domain exception.</param>
    /// <returns>An instance of <see cref="ResponseBase"/> that contains all messages from the <see cref="domainException"/></returns>
    public static ResponseBase MapExceptionToResponseBase(this DomainException domainException)
    {
        if (domainException == null)
        {
            throw new ArgumentNullException("domainException");
        }

        var messages = domainException.Messages.ToList();

        var result = new ResponseBase();
        messages.ForEach(item => result.Messages.Add(item));

        return result;
    }

    /// <summary>
    /// Maps the exception to response.
    /// </summary>
    /// <typeparam name="T">Type parameter.</typeparam>
    /// <param name="domainException">The domain exception.</param>
    /// <returns>An instance of <see cref="Response"/> that contains all messages from the <see cref="domainException"/></returns>
    public static Response<T> MapExceptionToResponse<T>(this DomainException domainException) where T : class
    {
        if (domainException == null)
        {
            throw new ArgumentNullException("domainException");
        }

        var messages = domainException.Messages.ToList();

        var result = new Response<T>();
        messages.ForEach(item => result.Messages.Add(item));

        return result;
    }
}
}
