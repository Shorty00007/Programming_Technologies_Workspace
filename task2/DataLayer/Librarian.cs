using System;

namespace DataLayer { 
    public class Librarian
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Librarian(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void RentBook(Reader reader, Book book, Library library)
        {
            library.RentBook(reader, book);
        }
    }
}
