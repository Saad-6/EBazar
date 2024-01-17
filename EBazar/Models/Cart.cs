namespace EBazar.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public int? total { get; set; }
        public Cart()
        {

            CartItems = new List<CartItem>();
            total = 0;
        }
    }
}

