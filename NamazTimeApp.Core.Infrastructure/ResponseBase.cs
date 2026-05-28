namespace NamazTimeApp.Core.Infrastructure
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            Messages = new List<Message>();
        }

        public IList<Message> Messages { get; set; }
    }
}
