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

            Shelf shelf = Shelves.FirstOrDefault(s => s.Books.Contains(book));

            
            if (shelf != null)
            {
             
                shelf.RemoveBook(book);

                reader.RentedBooks.Add(book);
            }
            else
            {
                throw new InvalidOperationException("The book is not available.");
            }
        }
    }
}