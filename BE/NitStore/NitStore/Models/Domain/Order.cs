using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace NitStore.Models.Domain
{
    [Table("Order")]
    public partial class Order
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public int CustomerId { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        [Required]
        public float Total { get; set; }
    }
}
