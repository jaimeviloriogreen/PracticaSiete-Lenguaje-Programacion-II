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
    AnsiConsole.Clear();
    var figlet = new FigletText("Bookcase @app") {
      Color = Color.Green,
      Justification = Justify.Center
    };

    AnsiConsole.Write(figlet);

    while (running) {
      var prompt = new SelectionPrompt<(string Text, int Value)>()
        .Title("Indica una acción a realizar: ")
        .AddChoices(choices)
        .HighlightStyle("Green")
        .WrapAround()
        .UseConverter(c => $"{c.Text}");

      var option = AnsiConsole.Prompt(prompt);

      switch (option.Value) {
        case 1:
          var books = _service.FindAll();

          AnsiConsole.Clear();
          var table = new Table();

          table.AddColumn("Uuid");
          table.AddColumn("Título");
          table.AddColumn("Isbn");
          table.AddColumn("Género");

          foreach (Book book in books) {
            table.AddRow(book.Uuid.ToString(), book.Title, book.Isbn, book.Gender);
          }

          AnsiConsole.Write(table);

          break;
        case 2:
          AnsiConsole.Clear();
          Guid uuid = AnsiConsole.Ask<Guid>("Ingresa el uuid del libro: ");
          bool confirmDelete = AnsiConsole.Confirm("¿Estas seguro?");

          AnsiConsole.Clear();

          if (confirmDelete) {
            _service.Delete(uuid);
            AnsiConsole.MarkupLine("[GreenYellow]¡Libro eliminado![/]");
          }
          else {
            AnsiConsole.MarkupLine($"[DarkOrange]¡Operación cancelada![/]");
          }
          break;
        case 3:
          AnsiConsole.Clear();
          string title = AnsiConsole.Ask<string>("Ingrese el título: ");
          string isbn = AnsiConsole.Ask<string>("Ingrese el ISBN: ");
          string gender = AnsiConsole.Ask<string>("Ingrese el género: ");

          bool confirmCreate = AnsiConsole.Confirm("¿Estas seguro?");

          AnsiConsole.Clear();

          if (confirmCreate) {
            _service.Create(Guid.NewGuid(), title, isbn, gender);
            AnsiConsole.MarkupLine("[GreenYellow]¡Libro agregado exitosamente![/]");
          }
          else {
            AnsiConsole.MarkupLine($"[DarkOrange]¡Operación cancelada![/]");
          }
          break;
        case 4:
          AnsiConsole.Clear();
          Guid uuidToUpdate = AnsiConsole.Ask<Guid>("Ingresa el uuid del libro: ");
          string newTitle = AnsiConsole.Ask<string>("Ingrese el título: ");
          string newIsbn = AnsiConsole.Ask<string>("Ingrese el ISBN: ");
          string newGender = AnsiConsole.Ask<string>("Ingrese el género: ");

          bool confirmUpdate = AnsiConsole.Confirm("¿Estas seguro?");

          AnsiConsole.Clear();

          if (confirmUpdate) {
            _service.Update(uuidToUpdate, newTitle, newIsbn, newGender);
            AnsiConsole.MarkupLine("[GreenYellow]¡Libro actualizado exitosamente![/]");
          }
          else {
            AnsiConsole.MarkupLine($"[DarkOrange]¡Operación cancelada![/]");
          }
          break;
        default:
          running = false;
          Console.WriteLine("Fin de la app!");
          break;
      }
    }
  }
}