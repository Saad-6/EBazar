using Microsoft.AspNetCore.Identity;

namespace EBazar.Models
{
    public class AppUser:IdentityUser
    {
        public string? AppUserName { get; set; }
        public string? StoreName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public Cart Cart { get; set; }

        public AppUser()
        {

            Cart = new Cart();


        }
    }
}
