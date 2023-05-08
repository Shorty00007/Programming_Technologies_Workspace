using LogicLayer;
using NUnit.Framework;
using System.Linq;

namespace LibraryTest
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private ILibraryService _libraryService;

        [SetUp]
        public void SetUp()
        {
            _libraryService = new DataService();
        }

        [Test]
        public void TestAddReader()
        {
            var reader = _libraryService.AddReader("John Doe");

            Assert.AreEqual(1, reader.Id);
            Assert.AreEqual("John Doe", reader.Name);
        }

        [Test]
        public void TestAddLibrarian()
        {
            var librarian = _libraryService.AddLibrarian("Jane Smith");

            Assert.AreEqual(1, librarian.Id);
            Assert.AreEqual("Jane Smith", librarian.Name);
        }

        [Test]
        public void TestAddShelf()
        {
            var shelf = _libraryService.AddShelf();

            Assert.AreEqual(1, shelf.Id);
        }

        [Test]
        public void TestAddBookToShelf()
        {
            var shelf = _libraryService.AddShelf();
            var book = _libraryService.AddBookToShelf(shelf.Id, "Sample Book", "Sample Author");

            Assert.AreEqual(1, book.Id);
            Assert.AreEqual("Sample Book", book.Title);
            Assert.AreEqual("Sample Author", book.Author);
            Assert.IsTrue(shelf.Books.Contains(book));
        }

        [Test]
        public void TestRentBook()
        {
            var shelf = _libraryService.AddShelf();
            var book = _libraryService.AddBookToShelf(shelf.Id, "Sample Book", "Sample Author");
            var reader = _libraryService.AddReader("John Doe");
            var librarian = _libraryService.AddLibrarian("John Stones");
            _libraryService.RentBook(reader.Id, book.Id, librarian.Id);

            Assert.IsFalse(shelf.Books.Contains(book));
            Assert.IsTrue(reader.RentedBooks.Contains(book));
        }

        [Test]
        public void TestGetAllAvailableBooks()
        {
            var shelf1 = _libraryService.AddShelf();
            var book1 = _libraryService.AddBookToShelf(shelf1.Id, "Sample Book 1", "Sample Author 1");

            var shelf2 = _libraryService.AddShelf();
            var book2 = _libraryService.AddBookToShelf(shelf2.Id, "Sample Book 2", "Sample Author 2");

            var availableBooks = _libraryService.GetAllAvailableBooks().ToList();

            Assert.AreEqual(2, availableBooks.Count);
            Assert.IsTrue(availableBooks.Contains(book1));
            Assert.IsTrue(availableBooks.Contains(book2));
        }

        [Test]
        public void TestRentBookWithLibrarian()
        {
            // Arrange
            var shelf = _libraryService.AddShelf();
            var book = _libraryService.AddBookToShelf(shelf.Id, "Sample Book", "Sample Author");
            var reader = _libraryService.AddReader("John Doe");
            var librarian = _libraryService.AddLibrarian("Jane Smith");

            // Act
            _libraryService.RentBook(reader.Id, book.Id, librarian.Id);

            // Assert
            Assert.IsFalse(shelf.Books.Contains(book));
            Assert.IsTrue(reader.RentedBooks.Contains(book));
        }

        [Test]
        public void TestLogEvent()
        {
            var reader = _libraryService.AddReader("John Doe");
            var librarian = _libraryService.AddLibrarian("Jane Smith");
            var shelf = _libraryService.AddShelf();
            var book = _libraryService.AddBookToShelf(shelf.Id, "Sample Book", "Sample Author");

            var libraryEvent = _libraryService.LogEvent("Renting a book", reader, book, librarian);

            Assert.AreEqual(1, libraryEvent.Id);
            Assert.AreEqual("Renting a book", libraryEvent.Description);
            Assert.AreEqual(reader, libraryEvent.Reader);
            Assert.AreEqual(book, libraryEvent.Book);
            Assert.AreEqual(librarian, libraryEvent.Librarian);
            Assert.AreEqual(DateTime.Now.Date, libraryEvent.Date.Date);
        }
    }
}