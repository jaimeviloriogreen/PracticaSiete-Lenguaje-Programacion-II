using System.Net.NetworkInformation;
using PracticaSiete.Data;
using PracticaSiete.Models;

namespace PracticaSiete.Repositories;

public class BookRepository(AppDbConext dbConext) {
  private readonly AppDbConext _database = dbConext;
  public List<Book> FindAll() {
    List<Book> books = _database.Books.ToList();
    return books;
  }

  public bool ExistOne() {
    return _database.Books.Any();
  }

  public int Delete(Guid uuid) {
    var book = FindOne(uuid);

    if (book is null) return 0;

    _database.Remove(book);
    _database.SaveChanges();

    return 1;
  }

  public Book? FindOne(Guid uuid) {
    var book = _database.Books.FirstOrDefault(b => b.Uuid == uuid);
    return book;
  }

  public int Create(Guid uuid, string title, string isbn, string gender) {
    Book book = new() { Uuid = uuid, Title = title, Isbn = isbn, Gender = gender };

    _database.Books.Add(book);

    return _database.SaveChanges();

  }

  public int Update(Guid uuid, string title, string isbn, string gender) {
    var book = FindOne(uuid);

    if (book is null) return 0;

    book.Title = title;
    book.Isbn = isbn;
    book.Gender = gender;

    return _database.SaveChanges();

  }
}