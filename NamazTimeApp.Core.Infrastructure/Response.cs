namespace NamazTimeApp.Core.Infrastructure
{
    public class Response<TModel> : ResponseBase
    {
        public TModel Model { get; set; }

        public int TotalRows { get; set; }

        public bool CanApply { get; set; }
        public int TodayCount { get; set; }
    }
}
