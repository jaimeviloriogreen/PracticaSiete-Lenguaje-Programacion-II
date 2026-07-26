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

          if (_service.ExistOne()) {
            var table = new Table();

            table.AddColumn("Uuid");
            table.AddColumn("Título");
            table.AddColumn("Isbn");
            table.AddColumn("Género");

            foreach (Book book in books) {
              table.AddRow(book.Uuid.ToString()[..8], book.Title, book.Isbn, book.Gender);
            }
            AnsiConsole.Write(table);

            break;
          }
          AnsiConsole.MarkupLine($"[DarkOrange]¡No existe libros para mostrar![/]");

          break;
        case 2:
          AnsiConsole.Clear();

          // Si no hay libros para el proceso.
          if (!_service.ExistOne()) {
            AnsiConsole.MarkupLine($"[DarkOrange]¡No hay libros para eliminar![/]");
            break;
          }

          var bookChoices = _service.FindAll().Select(b => (b.Uuid, b.Title));

          var bookSelectioPrompt = new SelectionPrompt<(Guid Uuid, string Title)>()
            .Title("Indica el libro a eliminar: ")
            .AddChoices(bookChoices)
            .HighlightStyle(Color.GreenYellow)
            .EnableSearch()
            .SearchPlaceholderText("Escribe el libro que quieres eliminar...")
            .PageSize(5)
            .WrapAround()
            .UseConverter(c => $"{c.Title}");

          var bookSelected = AnsiConsole.Prompt(bookSelectioPrompt);

          bool confirmDelete = AnsiConsole.Confirm("¿Estas seguro?");

          AnsiConsole.Clear();

          if (confirmDelete) {
            int rowsDeleted = _service.Delete(bookSelected.Uuid);

            if (rowsDeleted < 1) {
              AnsiConsole.MarkupLine($"[DarkOrange]El libro no existe![/]");
              break;
            }
            AnsiConsole.MarkupLine("[GreenYellow]¡Libro eliminado![/]");
            break;
          }

          AnsiConsole.MarkupLine($"[DarkOrange]¡Operación cancelada![/]");
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
            break;
          }

          AnsiConsole.MarkupLine($"[DarkOrange]¡Operación cancelada![/]");
          break;
        case 4:
          AnsiConsole.Clear();

          // Si no hay libros para el proceso.
          if (!_service.ExistOne()) {
            AnsiConsole.MarkupLine($"[DarkOrange]¡No hay libros para eliminar![/]");
            break;
          }

          var bookChoicesToUpdate = _service.FindAll().Select(b => (b.Uuid, b.Title));

          var bookSelectionPromptToUpdate = new SelectionPrompt<(Guid Uuid, string Title)>()
            .Title("Indica el libro a actualizar: ")
            .AddChoices(bookChoicesToUpdate)
            .HighlightStyle(Color.GreenYellow)
            .EnableSearch()
            .SearchPlaceholderText("Escribe el libro que quieres eliminar...")
            .PageSize(5)
            .WrapAround()
            .UseConverter(c => $"{c.Title}");

          var selectionPromptBookToUpdate = AnsiConsole.Prompt(bookSelectionPromptToUpdate);

          // Estoy seguro que el libro existe en este punto por eso puse el operador '!'
          Book bookToUpdated = _service.FindOne(selectionPromptBookToUpdate.Uuid)!;

          string newTitle = AnsiConsole.Ask<string>("Ingrese el título", bookToUpdated.Title);
          string newIsbn = AnsiConsole.Ask<string>("Ingrese el ISBN ", bookToUpdated.Isbn);
          string newGender = AnsiConsole.Ask<string>("Ingrese el género", bookToUpdated.Gender);

          AnsiConsole.WriteLine();
          bool confirmUpdate = AnsiConsole.Confirm("¿Estas seguro?");

          AnsiConsole.Clear();

          if (confirmUpdate) {
            _service.Update(selectionPromptBookToUpdate.Uuid, newTitle, newIsbn, newGender);
            AnsiConsole.MarkupLine("[GreenYellow]¡Libro actualizado exitosamente![/]");
            break;
          }

          AnsiConsole.MarkupLine($"[DarkOrange]¡Operación cancelada![/]");
          break;
        default:
          running = false;
          Console.WriteLine("Fin de la app!");
          break;
      }
    }
  }
}