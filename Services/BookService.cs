using PracticaSiete.Models;
using PracticaSiete.Repositories;

namespace PracticaSiete.Services;

public class BookService(BookRepository bookRepository) {
  private readonly BookRepository _repository = bookRepository;

  public List<Book> FindAll() {
    return _repository.FindAll();
  }

  public int Delete(Guid uuid) {
    return _repository.Delete(uuid);
  }

  public int Create(Guid uuid, string title, string isbn, string gender) {
    return _repository.Create(uuid, title, isbn, gender);
  }

  public int Update(Guid uuid, string title, string isbn, string gender) {
    return _repository.Update(uuid, title, isbn, gender);
  }
}