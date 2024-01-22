using ThreePointZero.Data.Enums.Address;

namespace EBazar.Models
{
    public class Address
    {
        public int Id { get; set; }
        public Province? Province { get; set; }
        public string? District { get; set; }
        public string? StreetAddress { get; set; }

    }
}
