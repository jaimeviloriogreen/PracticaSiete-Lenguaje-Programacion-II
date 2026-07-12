using PracticaSiete.Models;
using PracticaSiete.Services;
using Spectre.Console;

namespace PracticaSiete.Screens;


public class MainScreen(BookService bookService) {
  private readonly BookService _service = bookService;
  private bool running = true;
  private readonly (string Text, int Value)[] choices = [
    ("1. Mostrar libros", 1),
    ("2. Eliminar libro", 2),
    ("3. Agregar libro", 3),
    ("4. Actualizar un libro", 4),
    ("5. Salir", 0)
  ];

  public void Show() {

    while (running) {
      var prompt = new SelectionPrompt<(string Text, int Value)>()
        .Title("Indica una acción a realizar: ")
        .AddChoices(choices)
        .HighlightStyle("Green")
        .UseConverter(c => $"{c.Text}");

      var option = AnsiConsole.Prompt(prompt);

      switch (option.Value) {
        case 1:
          var books = _service.FindAll();

          AnsiConsole.Clear();
          var table = new Table();

          table.AddColumn("Id");
          table.AddColumn("Título");
          table.AddColumn("Género");
          table.AddColumn("Autor");

          foreach (Book book in books) {
            table.AddRow(book.Uuid.ToString()[..8], book.Title, book.Gender, book.Author);
          }

          AnsiConsole.Write(table);

          break;
        case 2:
          Console.WriteLine("Opción 2");
          break;
        case 3:
          Console.WriteLine("Opción 3");
          break;
        case 4:
          Console.WriteLine("Opción 4");
          break;
        default:
          running = false;
          Console.WriteLine("Fin de la app!");
          break;
      }
    }
  }
}