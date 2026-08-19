using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("RATING")]
    public class Rating
    {
        [Key]
        public long Id { get; set; }
        [Column(TypeName = "Decimal(10,2)")]
        public decimal Star { get; set; } = 0;
        [MaxLength(100)]
        public string Comment { get; set; } = string.Empty;
        public Transaction? transaction = null;
    }
}
