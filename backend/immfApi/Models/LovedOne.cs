namespace immfApi.Models
{
    public class LovedOne
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Relationship Relationship { get; set; }
        public ICollection<Hangout>? Hangouts { get; set; }

    }

    public enum Relationship
    {
        Friend,
        Family
    }
}