namespace EBazar.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int? rating { get; set; }
        public string? Comment { get; set; }
        public AppUser? AppUser { get; set; }

        public Rating()
        {
            Comment = string.Empty;

        }

    }
}
