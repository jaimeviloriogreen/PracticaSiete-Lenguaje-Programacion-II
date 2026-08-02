
using PracticaSiete.Data;
using PracticaSiete.Repositories;
using PracticaSiete.Screens;
using PracticaSiete.Services;

class Program {
  public static void Main(string[] args) {
    // Database db = new("Database/bookcase.db");

    AppDbConext dbConext = new("Database/bookcase.db");
    dbConext.Seed();

    BookRepository bookRepository = new(dbConext);

    BookService bookService = new(bookRepository);

    MainScreen screen = new(bookService);

    screen.Show();
  }
}