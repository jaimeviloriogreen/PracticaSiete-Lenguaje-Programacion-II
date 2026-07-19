using Microsoft.Data.Sqlite;

namespace PracticaSiete.Data;

public class Database {
  private readonly string _connectionString;
  public Database(string dbPath) {
    // TODO: Implementar manejo de excepciones
    _connectionString = $"Data Source={dbPath}";
    Initialize();
  }

  public SqliteConnection GetConnection() {
    var connection = new SqliteConnection(_connectionString);
    connection.Open();
    return connection;
  }

  public void Initialize() {
    using var connection = GetConnection();
    var command = connection.CreateCommand();
    command.CommandText = @"
      PRAGMA foreign_keys = ON;

      CREATE TABLE IF NOT EXISTS book(
        id INTEGER PRIMARY KEY,
        uuid TEXT NOT NULL UNIQUE,
        title TEXT NOT NULL,
        isbn TEXT NOT NULL,
        gender TEXT NOT NULL
      );

      CREATE TABLE IF NOT EXISTS author(
        id INTEGER PRIMARY KEY,
        uuid TEXT NOT NULL UNIQUE,
        fname TEXT NOT NULL,
        lname TEXT NOT NULL
      );

      CREATE TABLE IF NOT EXISTS book_author(
        book_id INTEGER NOT NULL,
        author_id INTEGER NOT NULL,
        FOREIGN KEY (book_id) REFERENCES book(id),
        FOREIGN KEY (author_id) REFERENCES author(id),
        PRIMARY KEY(book_id, author_id)
      );
    ";
    command.ExecuteNonQuery();
  }
}