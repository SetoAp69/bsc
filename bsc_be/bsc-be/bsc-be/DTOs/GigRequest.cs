using System.ComponentModel.DataAnnotations;

namespace bsc_be.DTOs
{
    public class GigRequest
    {
        [Required(ErrorMessage = "Gig name is required.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration can't be less than 1")]
        public int Duration { get; set; } = 0;
        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price can't be less than 1")]
        public int Price { get; set; } = 0;
        public List<long> Types {get;set;}= [];
    }
}