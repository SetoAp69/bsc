using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("TYPE")]
    public class Type
    {
        [Key]
        public long Id { get; set; } = 0;
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<GigType> GigTypes {get;set;} = new List<GigType>();
    }
}
