namespace bsc_be.DTOs
{
    public class GigQueryParams
    {
        public string? Search = null;
        public int Limit = 0;
        public int Page = 1;
        public long? UserId = null;
        public List<string> Types = new List<string>();

    }
}