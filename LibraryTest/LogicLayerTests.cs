using LogicLayer;
using NUnit.Framework;

namespace LibraryTest
{
    [TestFixture]
    public class LogicLayerTests
    {
        private ILibraryService _libraryService;
        private DataServiceFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new DataServiceFactory();
            _libraryService = _factory.Create();
        }

        [Test]
        public void TestAddReader()
        {
            var reader = _libraryService.AddReader("John Doe");
            Assert.AreEqual("John Doe", reader.Name);
        }

        [Test]
        public void TestAddLibrarian()
        {
            var librarian = _libraryService.AddLibrarian("Jane Smith");
            Assert.AreEqual("Jane Smith", librarian.Name);
        }

        [Test]
        public void TestAddShelf()
        {
            var shelf = _libraryService.AddShelf();
            Assert.IsNotNull(shelf);
        }

        [Test]
        public void TestAddBookToShelf()
        {
            var shelf = _libraryService.AddShelf();
            var book = _libraryService.AddBookToShelf(shelf.Id, "Sample Book", "Sample Author");
            Assert.AreEqual("Sample Book", book.Title);
            Assert.AreEqual("Sample Author", book.Author);
        }
        [Test]
        public void TestRentBook()
        {
            var shelf = _libraryService.AddShelf();
            var book = _libraryService.AddBookToShelf(shelf.Id, "Sample Book", "Sample Author");
            var reader = _libraryService.AddReader("John Doe");
            var librarian = _libraryService.AddLibrarian("Jane Smith");

            _libraryService.RentBook(reader.Id, book.Id, librarian.Id);

            var shelfBooks = _libraryService.GetAllBooks();

            Assert.IsFalse(shelfBooks.Contains(book));
        }
        [Test]
        public void TestGetAllAvailableBooks()
        {
            var shelf1 = _libraryService.AddShelf();
            var book1 = _libraryService.AddBookToShelf(shelf1.Id, "Sample Book 1", "Sample Author 1");
            var book2 = _libraryService.AddBookToShelf(shelf1.Id, "Sample Book 2", "Sample Author 2");

            var reader = _libraryService.AddReader("John Doe");
            var librarian = _libraryService.AddLibrarian("Jane Smith");

            _libraryService.RentBook(reader.Id, book1.Id, librarian.Id);

            var availableBooks = _libraryService.GetAllAvailableBooks().ToList();

            Assert.AreEqual(1, availableBooks.Count);
            Assert.AreEqual("Sample Book 2", availableBooks[0].Title);
        }
    }
}
