using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DataRepository
    {
        public Library DataContext { get; set; }

        public DataRepository()
        {
            DataContext = new Library();
        }

        // Readers
        public Reader AddReader(Reader reader)
        {
            DataContext.Readers.Add(reader);
            return reader;
        }

        public bool RemoveReader(int readerId)
        {
            var readerToRemove = DataContext.Readers.FirstOrDefault(r => r.Id == readerId);
            if (readerToRemove != null)
            {
                DataContext.Readers.Remove(readerToRemove);
                return true;
            }
            return false;
        }

        public List<Reader> GetAllReaders()
        {
            return DataContext.Readers;
        }

        // Librarians
        public Librarian AddLibrarian(Librarian librarian)
        {
            DataContext.Librarians.Add(librarian);
            return librarian;
        }

        public bool RemoveLibrarian(int librarianId)
        {
            var librarianToRemove = DataContext.Librarians.FirstOrDefault(l => l.Id == librarianId);
            if (librarianToRemove != null)
            {
                DataContext.Librarians.Remove(librarianToRemove);
                return true;
            }
            return false;
        }

        public List<Librarian> GetAllLibrarians()
        {
            return DataContext.Librarians;
        }

        // Shelves
        public Shelf AddShelf(Shelf shelf)
        {
            DataContext.Shelves.Add(shelf);
            return shelf;
        }

        public bool RemoveShelf(int shelfId)
        {
            var shelfToRemove = DataContext.Shelves.FirstOrDefault(s => s.Id == shelfId);
            if (shelfToRemove != null)
            {
                DataContext.Shelves.Remove(shelfToRemove);
                return true;
            }
            return false;
        }

        public List<Shelf> GetAllShelves()
        {
            return DataContext.Shelves;
        }

        // Books
        public Book AddBookToShelf(int shelfId, Book book)
        {
            var shelf = DataContext.Shelves.FirstOrDefault(s => s.Id == shelfId);
            if (shelf != null)
            {
                shelf.Books.Add(book);
                return book;
            }
            else
            {
                throw new InvalidOperationException("Shelf not found.");
            }
        }

        public bool RemoveBookFromShelf(int shelfId, int bookId)
        {
            var shelf = DataContext.Shelves.FirstOrDefault(s => s.Id == shelfId);
            if (shelf != null)
            {
                var bookToRemove = shelf.Books.FirstOrDefault(b => b.Id == bookId);
                if (bookToRemove != null)
                {
                    shelf.Books.Remove(bookToRemove);
                    return true;
                }
            }
            return false;
        }

        public List<Book> GetAllBooks()
        {
            return DataContext.Shelves.SelectMany(s => s.Books).ToList();
        }


        public bool RemoveLibraryEvent(int eventId)
        {
            var eventToRemove = DataContext.Events.FirstOrDefault(e => e.Id == eventId);
            if (eventToRemove != null)
            {
                DataContext.Events.Remove(eventToRemove);
                return true;
            }
            return false;
        }

        public List<LibraryEvent> GetAllLibraryEvents()
        {
            return DataContext.Events;
        }
    }
}
