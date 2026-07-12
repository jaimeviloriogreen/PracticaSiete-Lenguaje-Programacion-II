namespace PracticaSiete.Models;

public class Book {
  public Guid Uuid { get; set; }
  public int Id { get; set; }
  public required string Title { get; set; }
  public required string Gender { get; set; }
  public required string Author { get; set; }
}
