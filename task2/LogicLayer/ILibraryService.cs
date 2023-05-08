using System;
using DataLayer;
namespace LogicLayer
{
    public interface ILibraryService
    {
        Reader AddReader(string name);
        Librarian AddLibrarian(string name);
        Shelf AddShelf();
        Book AddBookToShelf(int shelfId, string title, string author);
        void RentBook(int readerId, int bookId, int librarianID);
        IEnumerable<Book> GetAllAvailableBooks();
        LibraryEvent LogEvent(string description, Reader reader, Book book, Librarian librarian);
    }
}
