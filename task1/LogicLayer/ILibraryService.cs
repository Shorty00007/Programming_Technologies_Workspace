using System;
using DataLayer;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ILibraryService
    {
        Reader AddReader(string name);
        bool RemoveReader(int readerId);
        List<Reader> GetAllReaders();

        Librarian AddLibrarian(string name);
        bool RemoveLibrarian(int librarianId);
        List<Librarian> GetAllLibrarians();

        Shelf AddShelf();
        bool RemoveShelf(int shelfId);
        List<Shelf> GetAllShelves();

        Book AddBookToShelf(int shelfId, string title, string author);
        bool RemoveBookFromShelf(int shelfId, int bookId);
        List<Book> GetAllBooks();

        void RentBook(int readerId, int bookId, int librarianID);
        IEnumerable<Book> GetAllAvailableBooks();

        LibraryEvent LogEvent(string description, Reader reader, Book book, Librarian librarian);
    }
}