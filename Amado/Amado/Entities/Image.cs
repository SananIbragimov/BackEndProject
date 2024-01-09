namespace Amado.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
