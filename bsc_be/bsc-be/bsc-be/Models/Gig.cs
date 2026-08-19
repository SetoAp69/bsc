using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("GIG")]
    public class Gig
    {
        [Key]
        public long Id { get; set; } = 0;
        [ForeignKey("User")]
        public long UserId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public decimal Price { get; set; } = 0;
    }
}
