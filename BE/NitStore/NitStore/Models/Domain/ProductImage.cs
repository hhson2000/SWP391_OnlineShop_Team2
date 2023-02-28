using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NitStore.Models.Domain
{
    [Table("ProductImage")]
    public partial class ProductImage
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ImageId { get; set; }
    }
}
