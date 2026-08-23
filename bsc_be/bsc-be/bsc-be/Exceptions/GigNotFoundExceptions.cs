namespace bsc_be.DTOs
{
    public class GigNotFoundException : Exception
    {
        public GigNotFoundException(long gigId)
            : base($"Gig with ID {gigId} not found")
        {

        }
    }
}