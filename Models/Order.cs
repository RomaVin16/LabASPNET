using System.ComponentModel.DataAnnotations.Schema;

namespace LabASPNET.Models
{
    [Table("orders")]
    public class Order
    {
        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("orderdate")]
        public DateTime OrderDate { get; set; }
    }
}
