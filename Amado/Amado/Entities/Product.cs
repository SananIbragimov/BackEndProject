namespace Amado.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
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
