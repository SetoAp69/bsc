namespace bsc_be.DTOs
{
    public class GigDetailResponse
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public decimal Stars { get; set; } = 0;
        public Creator GigCreator{get;set;} = new Creator();
        public List<Type> Types {get;set;}= new List<Type>();

        public class Creator
        {
            public long Id { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
        }

        public class Type
        {
            public long Id { get; set;} = 0;
            public string Name { get; set;} = string.Empty;
        }
    }
}