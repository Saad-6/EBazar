using Microsoft.AspNetCore.Identity;

namespace EBazar.Models
{
    public class AppUser:IdentityUser
    {
        public string? AppUserName { get; set; }
        public Address? Address { get; set; }
        public string? StoreName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public Cart Cart { get; set; }

        public AppUser()
        {
            Address = new Address();

            Cart = new Cart();


        }
    }
}
