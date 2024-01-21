using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public Color Color { get; set; }
    }
}
