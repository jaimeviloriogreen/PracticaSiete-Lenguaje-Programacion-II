PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS book(
  id INTEGER PRIMARY KEY,
  uuid TEXT NOT NULL UNIQUE,
  title TEXT NOT NULL,
  isbn TEXT NOT NULL,
  gender TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS author(
  id INTEGER PRIMARY KEY,
  uuid TEXT NOT NULL UNIQUE,
  fname TEXT NOT NULL,
  lname TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS book_author(
  book_id INTEGER NOT NULL,
  author_id INTEGER NOT NULL,
  FOREIGN KEY (book_id) REFERENCES book(id),
  FOREIGN KEY (author_id) REFERENCES author(id),
  PRIMARY KEY(book_id, author_id)
);

INSERT INTO 
  book(uuid, title, isbn, gender) 
VALUES
  ('bbf988f3-2109-4b49-8636-5547072a142f','1984', 'ABC-001', 'Ficción');

INSERT INTO 
  author(uuid, fname, lname) 
VALUES
    ('b68a5272-f652-42ac-a70b-7f35238c506b','George', 'Orwell');

INSERT INTO
  book_author
VALUES 
  (1, 1);

-- Consultar libros y autores con INNER JOIN
SELECT 
  fname, 
  lname, 
  title AS 'libro' 
FROM book_author
  INNER JOIN author ON 
    book_author.id = author.id
  INNER JOIN libro ON 
    book_author.id = book.id;
