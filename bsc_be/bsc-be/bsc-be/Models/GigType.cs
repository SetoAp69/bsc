using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("GIG_TYPE")]
    public class GigType
    {
        [Key]
        public long Id { get; set; } = 0;
        [ForeignKey("Gig")]
        public long GigId { get; set; } = 0;
        [ForeignKey("Type")]
        public long TypeId { get; set; } = 0;
    }
}
