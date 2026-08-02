using Microsoft.EntityFrameworkCore;
using PracticaSiete.Models;

namespace PracticaSiete.Data;

public class AppDbConext(string dbPath) : DbContext {
  private readonly string _dbPath = dbPath;

  public DbSet<Book> Books => Set<Book>();

  protected override void OnConfiguring(DbContextOptionsBuilder option) {
    option.UseSqlite($"Data Source={_dbPath}");
  }
  protected override void OnModelCreating(ModelBuilder builder) {
    builder.Entity<Book>().ToTable("book");
  }

  public void Seed() {
    // Se asegura de que la base de datos haya sido previamente creada.
    // Database.EnsureCreated();

    if (!Books.Any()) {
      Books.Add(new Book {
        Title = "1984",
        Isbn = "ABC-001",
        Gender = "Política",
        Uuid = Guid.NewGuid()
      });

      SaveChanges();
    }
  }

}