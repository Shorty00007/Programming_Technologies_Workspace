using DataLayer;
using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class ConcreteDataRepository : IDataRepository
    {
        private readonly Library _dataContext = new Library();

        public Library DataContext => _dataContext;

        public Reader AddReader(Reader reader)
        {
            _dataContext.Readers.Add(reader);
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
namespace LibraryTest
{
    [TestFixture]
    public class DataLayerTests
    {
        private IDataRepository _dataRepository;

        [SetUp]
        public void SetUp()
        {
            _dataRepository = new ConcreteDataRepository();
        }

        [Test]
        public void TestAddRemoveGetAllBooks()
        {
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);
            _dataRepository.AddShelf(shelf1);
            _dataRepository.AddShelf(shelf2);

            var book1 = new Book(1, "Book 1", "Author 1");
            var book2 = new Book(2, "Book 2", "Author 2");
            var book3 = new Book(3, "Book 3", "Author 3");

            _dataRepository.AddBookToShelf(shelf1.Id, book1);
            _dataRepository.AddBookToShelf(shelf1.Id, book2);
            _dataRepository.AddBookToShelf(shelf2.Id, book3);

            var allBooks = _dataRepository.GetAllBooks();
            Assert.AreEqual(3, allBooks.Count);
            Assert.IsTrue(allBooks.Contains(book1));
            Assert.IsTrue(allBooks.Contains(book2));
            Assert.IsTrue(allBooks.Contains(book3));

            _dataRepository.RemoveBookFromShelf(shelf1.Id, book1.Id);
            allBooks = _dataRepository.GetAllBooks();
            Assert.AreEqual(2, allBooks.Count);
            Assert.IsFalse(allBooks.Contains(book1));
            Assert.IsTrue(allBooks.Contains(book2));
            Assert.IsTrue(allBooks.Contains(book3));
        }
        [Test]
        public void TestAddRemoveReader()
        {
            var reader = new Reader(1, "John Doe");
            _dataRepository.AddReader(reader);

            var allReaders = _dataRepository.GetAllReaders();
            Assert.IsTrue(allReaders.Contains(reader));

            _dataRepository.RemoveReader(reader.Id);
            allReaders = _dataRepository.GetAllReaders();
            Assert.IsFalse(allReaders.Contains(reader));
        }

        [Test]
        public void TestAddRemoveLibrarian()
        {
            var librarian = new Librarian(1, "Jane Smith");
            _dataRepository.AddLibrarian(librarian);

            var allLibrarians = _dataRepository.GetAllLibrarians();
            Assert.IsTrue(allLibrarians.Contains(librarian));

            _dataRepository.RemoveLibrarian(librarian.Id);
            allLibrarians = _dataRepository.GetAllLibrarians();
            Assert.IsFalse(allLibrarians.Contains(librarian));
        }
        [Test]
        public void TestAddRemoveShelf()
        {
            var shelf = new Shelf(1);
            _dataRepository.AddShelf(shelf);

            var allShelves = _dataRepository.GetAllShelves();
            Assert.IsTrue(allShelves.Contains(shelf));

            _dataRepository.RemoveShelf(shelf.Id);
            allShelves = _dataRepository.GetAllShelves();
            Assert.IsFalse(allShelves.Contains(shelf));
        }

        [Test]
        public void TestRemoveNonExistingBookFromShelf()
        {
            var shelf = new Shelf(1);
            var book = new Book(1, "Book 1", "Author 1");

            _dataRepository.AddShelf(shelf);
            _dataRepository.AddBookToShelf(shelf.Id, book);

            var result = _dataRepository.RemoveBookFromShelf(shelf.Id, 999);

            Assert.IsFalse(result);
        }
    }
}