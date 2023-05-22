using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace LogicLayer
{
    public class DataService : ILibraryService
    {
        private IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Reader AddReader(string name)
        {
            var reader = new Reader(_dataRepository.DataContext.Readers.Count + 1, name);
            return _dataRepository.AddReader(reader);
        }

        public bool RemoveReader(int readerId)
        {
            return _dataRepository.RemoveReader(readerId);
        }

        public List<Reader> GetAllReaders()
        {
            return _dataRepository.GetAllReaders();
        }

        public Librarian AddLibrarian(string name)
        {
            var librarian = new Librarian(_dataRepository.DataContext.Librarians.Count + 1, name);
            return _dataRepository.AddLibrarian(librarian);
        }

        public bool RemoveLibrarian(int librarianId)
        {
            return _dataRepository.RemoveLibrarian(librarianId);
        }

        public List<Librarian> GetAllLibrarians()
        {
            return _dataRepository.GetAllLibrarians();
        }

        public Shelf AddShelf()
        {
            var shelf = new Shelf(_dataRepository.DataContext.Shelves.Count + 1);
            return _dataRepository.AddShelf(shelf);
        }

        public bool RemoveShelf(int shelfId)
        {
            return _dataRepository.RemoveShelf(shelfId);
        }

        public List<Shelf> GetAllShelves()
        {
            return _dataRepository.GetAllShelves();
        }

        public Book AddBookToShelf(int shelfId, string title, string author)
        {
            var book = new Book(_dataRepository.DataContext.Shelves.SelectMany(s => s.Books).Count() + 1, title, author);
            return _dataRepository.AddBookToShelf(shelfId, book);
        }

        public bool RemoveBookFromShelf(int shelfId, int bookId)
        {
            return _dataRepository.RemoveBookFromShelf(shelfId, bookId);
        }

        public List<Book> GetAllBooks()
        {
            return _dataRepository.GetAllBooks();
        }

        public void RentBook(int readerId, int bookId, int librarianId)
        {
            var reader = _dataRepository.DataContext.Readers.FirstOrDefault(r => r.Id == readerId);
            var book = _dataRepository.DataContext.Shelves.SelectMany(s => s.Books).FirstOrDefault(b => b.Id == bookId);
            var librarian = _dataRepository.DataContext.Librarians.FirstOrDefault(l => l.Id == librarianId);

            if (reader != null && book != null && librarian != null)
            {
                librarian.RentBook(reader, book, _dataRepository.DataContext);
            }
            else
            {
                throw new InvalidOperationException("Reader, book, or librarian not found.");
            }
        }

        public IEnumerable<Book> GetAllAvailableBooks()
        {
            return _dataRepository.DataContext.Shelves.SelectMany(s => s.Books);
        }

        public LibraryEvent LogEvent(string description, Reader reader, Book book, Librarian librarian)
        {
            var libraryEvent = new LibraryEvent(_dataRepository.DataContext.Events.Count + 1, description, DateTime.Now, reader, book, librarian);
            _dataRepository.DataContext.Events.Add(libraryEvent);
            return libraryEvent;
        }
    }
}