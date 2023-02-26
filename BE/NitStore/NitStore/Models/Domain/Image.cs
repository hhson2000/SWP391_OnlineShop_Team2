using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NitStore.Models.Domain
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string ImageURL { get; set; }
    }
}
