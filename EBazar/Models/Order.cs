using EBazar.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EBazar.Models
{
    public class Order
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int? total {  get; set; }
        public Address Address { get; set; }

        public DateTime? DelieveredDate { get;set; }

        public string? OrderName { get; set; }

        public Status? OrderStatus { get; set;}

        public DateTime? CreatedDate { get; set; }


       

        public Order()
        {
            CartItems = new List<CartItem>();   
        }

        



    }
}
