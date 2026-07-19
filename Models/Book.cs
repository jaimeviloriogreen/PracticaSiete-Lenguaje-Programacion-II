namespace PracticaSiete.Models;

public class Book : BaseEntity {
  public required string Title { get; set; }
  public required string Isbn { get; set; }
  public required string Gender { get; set; }
}
