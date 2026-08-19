using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("USER")]
    public class User
    {
        [Key]
        public long Id { get; set; } = 0;
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        public ICollection<Gig> Gigs { get; set; } = new List<Gig>();
        public ICollection<Transaction> Transactions = new List<Transaction>();

    }
}
