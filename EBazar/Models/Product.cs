using EBazar.Data.Enums;

namespace EBazar.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PictureUrl { get; set; }
        public string? Description { get; set; }
        public Category Category { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
        public List<Rating>? Rating { get; set; }
        public int? StoreId { get; set; }

    }
}
