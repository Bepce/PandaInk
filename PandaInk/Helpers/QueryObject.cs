namespace PandaInk.API.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; }

        public string? SortBy { get; set; }

        public bool IsDescending { get; set; } = false;
    }
}
