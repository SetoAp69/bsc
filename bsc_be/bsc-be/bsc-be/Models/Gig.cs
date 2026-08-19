using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace bsc_be.Models
{
    [Table("GIG")]
    public class Gig
    {
        [Key]
        public long Id { get; set; } = 0;
        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public long UserId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public ICollection<Transaction> Transactions { set; get; } = new List<Transaction>();
        public ICollection<GigType> GigTypes { set; get; } = new List<GigType>();

        public User User { get; set; } = new User();
    }
}
