using System;
using System.Collections.Generic;
using System.Linq;
namespace DataLayer
{
    public interface IDataRepository
    {
        Library DataContext { get; }

        Reader AddReader(Reader reader);
        bool RemoveReader(int readerId);
        List<Reader> GetAllReaders();
        Librarian AddLibrarian(Librarian librarian);
        bool RemoveLibrarian(int librarianId);
        List<Librarian> GetAllLibrarians();
        Shelf AddShelf(Shelf shelf);
        bool RemoveShelf(int shelfId);
        List<Shelf> GetAllShelves();
        Book AddBookToShelf(int shelfId, Book book);
        bool RemoveBookFromShelf(int shelfId, int bookId);
        List<Book> GetAllBooks();
        bool RemoveLibraryEvent(int eventId);
        List<LibraryEvent> GetAllLibraryEvents();
    }
}