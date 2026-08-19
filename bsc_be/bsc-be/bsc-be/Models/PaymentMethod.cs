using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("PAYEMENT_METHOD")]
    public class PaymentMethod
    {
        [Key]
        public long Id { get; set; } = 0;
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName ="Decimal(10,2)")]
        public decimal Rate { get; set; } = 0;    
    }
}
