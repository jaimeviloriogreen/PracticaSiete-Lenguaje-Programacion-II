using PracticaSiete.Models;

namespace PracticaSiete.Repositories;

public class BookRepository {
  private readonly List<Book> _books = [
    new(){
      Id = 1,
      Uuid = Guid.NewGuid(),
      Title = "1984",
      Gender = "Ficción",
      Author = "George Orwell"
    },
    new(){
      Id = 2,
      Uuid = Guid.NewGuid(),
      Title = "El diario de Ana Frank",
      Gender = "Biográfico",
      Author = "Ana Frank"
    }
  ];
  public List<Book> FindAll() {
    return _books;
  }
}