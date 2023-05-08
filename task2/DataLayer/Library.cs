using System;
namespace DataLayer
{

    public class Library
    {
        public List<Shelf> Shelves { get; set; }
        public List<Reader> Readers { get; set; }
        public List<Librarian> Librarians { get; set; }
        public List<LibraryEvent> Events { get; set; }

        public Library()
        {
            Shelves = new List<Shelf>();
            Readers = new List<Reader>();
            Librarians = new List<Librarian>();
            Events = new List<LibraryEvent>();
        }

        public void AddShelf(Shelf shelf)
        {
            Shelves.Add(shelf);
        }

        public void AddReader(Reader reader)
        {
            Readers.Add(reader);
        }

        public void AddLibrarian(Librarian librarian)
        {
            Librarians.Add(librarian);
        }

        public void RentBook(Reader reader, Book book)
        {
            // Find the shelf containing the book
            Shelf shelf = Shelves.FirstOrDefault(s => s.Books.Contains(book));

            // Check if the book is available
            if (shelf != null)
            {
                // Remove the book from the shelf
                shelf.RemoveBook(book);

                // Add the book to the reader's rented books
                reader.RentedBooks.Add(book);
            }
            else
            {
                throw new InvalidOperationException("The book is not available.");
            }
        }
    }
}