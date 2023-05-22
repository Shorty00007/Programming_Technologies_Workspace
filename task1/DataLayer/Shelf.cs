using System;
namespace DataLayer
{
    public class Shelf
    {
        public int Id { get; set; }
        public List<Book> Books { get; set; }

        public Shelf(int id)
        {
            Id = id;
            Books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }
    }
}
