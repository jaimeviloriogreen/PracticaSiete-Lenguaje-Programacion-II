using PracticaSiete.Data;
using PracticaSiete.Repositories;
using PracticaSiete.Screens;
using PracticaSiete.Services;

class Program {
  public static void Main(string[] args) {
    Database db = new("Database/bookcase.db");

    BookRepository bookRepository = new(db);

    BookService bookService = new(bookRepository);

    MainScreen screen = new(bookService);

    screen.Show();
  }
}