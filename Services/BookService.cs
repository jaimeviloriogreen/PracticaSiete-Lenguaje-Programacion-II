using PracticaSiete.Models;
using PracticaSiete.Repositories;

namespace PracticaSiete.Services;

public class BookService(BookRepository bookRepository) {
  private readonly BookRepository _repository = bookRepository;

  public List<Book> FindAll() {
    return _repository.FindAll();
  }
}