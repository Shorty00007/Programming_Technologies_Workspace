using System;
namespace DataLayer
{
    public class LibraryEvent
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public Librarian Librarian { get; set; }
        public DateTime Date { get; private set; }

        public LibraryEvent(int id, string description, DateTime date, Reader reader, Book book, Librarian librarian)
        {
            Id = id;
            Description = description;
            Date = date; // Add this line
            Reader = reader;
            Book = book;
            Librarian = librarian;
        }
    }
}
