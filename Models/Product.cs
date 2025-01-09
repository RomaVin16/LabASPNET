using System.ComponentModel.DataAnnotations.Schema;

namespace LabASPNET.Models
{
    [Table("products")]
    public class Product
    {
        [Column("productid")]
        public int ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("categoryid")]
        public int CategoryId { get; set; }
    }
}
