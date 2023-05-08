using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace LogicLayer
{
    public class DataService : ILibraryService
    {
        private DataRepository _dataRepository;

        public DataService()
        {
            _dataRepository = new DataRepository();
        }

        public Reader AddReader(string name)
        {
            int readerId = _dataRepository.DataContext.Readers.Count + 1;
            var reader = new Reader(readerId, name);
            _dataRepository.DataContext.AddReader(reader);
            return reader;
        }

        public Librarian AddLibrarian(string name)
        {
            int librarianId = _dataRepository.DataContext.Librarians.Count + 1;
            var librarian = new Librarian(librarianId, name);
            _dataRepository.DataContext.AddLibrarian(librarian);
            return librarian;
        }

        public Shelf AddShelf()
        {
            int shelfId = _dataRepository.DataContext.Shelves.Count + 1;
            var shelf = new Shelf(shelfId);
            _dataRepository.DataContext.AddShelf(shelf);
            return shelf;
        }

        public Book AddBookToShelf(int shelfId, string title, string author)
        {
            int bookId = _dataRepository.DataContext.Shelves.SelectMany(s => s.Books).Count() + 1;
            var book = new Book(bookId, title, author);
            var shelf = _dataRepository.DataContext.Shelves.FirstOrDefault(s => s.Id == shelfId);

            if (shelf != null)
            {
                shelf.AddBook(book);
                return book;
            }
            else
            {
                throw new InvalidOperationException("Shelf not found.");
            }
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
            int eventId = _dataRepository.DataContext.Events.Count + 1;
            var libraryEvent = new LibraryEvent(eventId, description, DateTime.Now, reader, book, librarian);
            _dataRepository.DataContext.Events.Add(libraryEvent);
            return libraryEvent;
        }
    }
}