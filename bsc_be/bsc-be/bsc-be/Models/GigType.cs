using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("GIG_TYPE")]
    public class GigType
    {
        [Key]
        public long Id { get; set; } = 0;
        [Column("GIG_ID")]
        [ForeignKey(nameof(Gig))]
        public long GigId { get; set; } = 0;
        [Column("TYPE_ID")]
        [ForeignKey(nameof(Type))]
        public long TypeId { get; set; } = 0;
        public Gig Gig {get;set;} 
        public Type Type {get;set;}
    }
}
