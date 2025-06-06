namespace Immf.Models
{
    public class LovedOne
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Relationship Relationship { get; set; }
        public string? Date { get; set; } //TODO: find Date format.

    }
    
    public enum Relationship
    {
        Friend,
        Family
    }
}