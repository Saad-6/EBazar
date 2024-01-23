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
            ProfilePictureUrl = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBwgHBgkIBwgKCgkLDRYPDQwMDRsUFRAWIB0iIiAdHx8kKDQsJCYxJx8fLT0tMTU3Ojo6Iys/RD84QzQ5OjcBCgoKDQwNGg8PGjclHyU3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3N//AABEIAJQAlwMBIgACEQEDEQH/xAAbAAEAAgMBAQAAAAAAAAAAAAAABAUBAgMGB//EADEQAQACAQIDBAgGAwAAAAAAAAABAgMEEQUhMSJBUXESEyMyQlJhgTOxwdHh8BWRof/EABQBAQAAAAAAAAAAAAAAAAAAAAD/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwD6qAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAi6rX4dPM1mfTvHw17vOe5H4nrpxTOHBbt/FaPh+io69QT8nFdRaexFKR5by5f5DV77+uny9GP2RQE/HxXPT8SKZI8tpWOl12HU8qz6N9vdt/ebz50neOUg9SK/hmunN7HNPtNuzPj/KwAAAAAAAAAAActXm9Rp75O+I5efc6q3jl/ZYqd023/AL/sFRO82m0zMzPOZkAAAAAGa2tS0WpO1q84l6TTZoz6fHlj4o5+fe80uOCW3096/Lb8wWIAAAAANa2369Wzg3rfukHQIncAVfHInbDPdG/6LRE4ri9bpJmvvUn0v3BQgAAAAALfgcezzW7ptH5Kiej0HDcU4tJSLR2rdqfv/GwJIAAebS1/AG1rRX6+Q4gAAMxMx0lvW/i5gO++/QmO6XCOXRtF5BS8Q0k6bLvWPZ2nsz4fRFekyzjyY5plrE1nruo9XhxYbTOLPS9fl37UAjjG5uDIxulaPBhzWic2alI+XftSDfhuknUZfTtHsqTvO/fPgvXOk46UrTFWIrHSIJvPkDpvHi1nJEdObnM7sAza026sAAAAAADTNlphxzfJO0R/0G1rRSs2tMREdZmeit1HFNt66eOfzW/SEPV6q+pvvadqb9msOAN8ubJlnfJe1vOWgAAAAA6Yc2XDO+PJav035LDTcUiZiuojafnr0+8KsB6WsxaImsxMT0mJ5MqHSau+mty7WOetZn8l5jyUy0i+Od6z0BsAAAAABM7RM+Ci12qnU5eX4dfdj9U/i2f0MMYqzzydfJTgAAAAAAAAAAJWg1U6fL2p9nae19PqigPTCFwrN63B6Ez2sfL7dyaAAAACi4jk9ZrL+FezH2RmbTve0z1mZmWAAAAAAAAAAAAAS+GZPV6usd1+zK7ecw29HNjt4WiXowAAGLe7PkAPM90MgAAAAAAAAAAAADMe9Hm9KAAAP//Z";


        }
    }
}
