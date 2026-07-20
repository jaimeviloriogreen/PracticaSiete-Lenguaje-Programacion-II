using Microsoft.Data.Sqlite;

namespace PracticaSiete.Data;

public class Database {
  private readonly string _connectionString;
  public Database(string dbPath) {
    // TODO: Implementar manejo de excepciones
    _connectionString = $"Data Source={dbPath}";

    Initialize();
    Seed();
  }

  public SqliteConnection GetConnection() {
    var connection = new SqliteConnection(_connectionString);
    connection.Open();
    return connection;
  }

  /// <summary>
  /// Create initial tables.
  /// </summary>
  public void Initialize() {
    using var connection = GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = @"
      PRAGMA foreign_keys = ON;

      CREATE TABLE IF NOT EXISTS book(
        id INTEGER PRIMARY KEY,
        uuid TEXT NOT NULL UNIQUE,
        title TEXT NOT NULL,
        isbn TEXT NOT NULL,
        gender TEXT NOT NULL
      );
    ";
    command.ExecuteNonQuery();
  }

  /// <summary>
  /// Insert initial data if not exists any rows.
  /// </summary>
  public void Seed() {
    using var connection = GetConnection();

    using var exitsCommand = connection.CreateCommand();
    exitsCommand.CommandText = @"
      SELECT EXISTS(
        SELECT 1 FROM book
      );";

    // Convert 1 | 0 -> true | false
    bool exist = Convert.ToBoolean(exitsCommand.ExecuteScalar());

    if (!exist) {
      using var seedCommand = connection.CreateCommand();

      seedCommand.CommandText = @"
        INSERT INTO 
          book(uuid, title, isbn, gender) 
        VALUES
          ('bbf988f3-2109-4b49-8636-5547072a142f','1984', 'ABC-001', 'Ficción');
      ";

      seedCommand.ExecuteNonQuery();
    }


  }
}