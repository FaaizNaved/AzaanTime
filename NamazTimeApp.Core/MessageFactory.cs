using NamazTimeApp.Core;

public static class MessageFactory
{
    /// <summary>
    /// Creates the error message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Error message.</returns>
    public static Message CreateErrorMessage(string message, string fieldName = "")
    {
        return new Message { MessageType = MessageType.Error, Value = message, FieldName = fieldName };
    }
}
