namespace immfApi.Models
{
  public class Hangout
  {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public required LovedOne LovedOne { get; set; }

  }
}