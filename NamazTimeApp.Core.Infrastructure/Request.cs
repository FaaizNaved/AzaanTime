namespace NamazTimeApp.Core.Infrastructure
{
    public class Request<TModel>
    {
        public TModel Model { get; set; } = default!;
    }
}
