namespace DataLayer
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> RentedBooks { get; set; }

        public Reader(int id, string name)
        {
            Id = id;
            Name = name;
            RentedBooks = new List<Book>();
        }
    }
}