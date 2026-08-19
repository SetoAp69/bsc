using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("ITEM")]
    public class Item
    {
        [Key]
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
