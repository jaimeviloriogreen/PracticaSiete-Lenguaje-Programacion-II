using PracticaSiete.Data;
using PracticaSiete.Models;

namespace PracticaSiete.Repositories;

public class BookRepository(Database db) {
  private readonly Database _database = db;
  public List<Book> FindAll() {
    using var connection = _database.GetConnection();
    var command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM book";

    List<Book> books = [];

    using var reader = command.ExecuteReader();

    while (reader.Read()) {
      books.Add(
          new Book {
            Id = reader.GetInt32(0),
            Uuid = reader.GetGuid(1),
            Title = reader.GetString(2),
            Isbn = reader.GetString(3),
            Gender = reader.GetString(4)
          }
      );
    }

    return books;
  }
}