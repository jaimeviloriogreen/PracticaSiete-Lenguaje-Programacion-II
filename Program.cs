using PracticaSiete.Repositories;
using PracticaSiete.Screens;
using PracticaSiete.Services;

class Program {
  public static void Main(string[] args) {
    BookRepository bookRepository = new();

    BookService bookService = new(bookRepository);

    MainScreen screen = new(bookService);

    screen.Show();
  }
}