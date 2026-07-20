using PracticaSiete.Data;
using PracticaSiete.Models;

namespace PracticaSiete.Repositories;

public class BookRepository(Database db) {
  private readonly Database _database = db;
  public List<Book> FindAll() {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = @"
      SELECT uuid, title, isbn, gender FROM book;
    ";

    List<Book> books = [];

    using var reader = command.ExecuteReader();

    while (reader.Read()) {
      books.Add(
          new Book {
            Uuid = reader.GetGuid(0),
            Title = reader.GetString(1),
            Isbn = reader.GetString(2),
            Gender = reader.GetString(3)
          }
      );
    }

    return books;
  }

  public int Delete(Guid uuid) {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = "DELETE FROM book WHERE uuid = @uuid";

    command.Parameters.AddWithValue("@uuid", uuid.ToString());
    int rowsDeleted = command.ExecuteNonQuery();

    return rowsDeleted;
  }

  public int Create(Guid uuid, string title, string isbn, string gender) {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = @"
      INSERT INTO 
        book(uuid, title, isbn, gender) 
      VALUES
        (@uuid, @title, @isbn, @gender);
    ";

    command.Parameters.AddWithValue("@uuid", uuid.ToString());
    command.Parameters.AddWithValue("@title", title);
    command.Parameters.AddWithValue("@isbn", isbn);
    command.Parameters.AddWithValue("@gender", gender);

    int rowsInserted = command.ExecuteNonQuery();

    return rowsInserted;
  }

  public int Update(Guid uuid, string title, string isbn, string gender) {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = @"
      UPDATE book SET
        title = @title,
        isbn = @isbn,
        gender = @gender
      WHERE uuid = @uuid;
    ";

    command.Parameters.AddWithValue("@title", title);
    command.Parameters.AddWithValue("@isbn", isbn);
    command.Parameters.AddWithValue("@gender", gender);
    command.Parameters.AddWithValue("@uuid", uuid.ToString());

    int rowsUpdated = command.ExecuteNonQuery();

    return rowsUpdated;
  }
}