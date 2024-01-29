namespace EBazar.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } 
        public int  TotalOrders {  get; set; }
         public int TotalRevenue { get; set; }
        public Store() { 
            Products = new List<Product>();
            TotalOrders = 0;
            TotalRevenue = 0;
            Name = string.Empty;
            Description = string.Empty;

        }




    }
}
