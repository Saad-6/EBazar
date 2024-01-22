namespace EBazar.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public CartItem() { 
        
        }
    }
}
