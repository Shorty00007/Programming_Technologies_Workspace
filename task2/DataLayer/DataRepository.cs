using System;
namespace DataLayer
{

    public class DataRepository
    {
        public Library DataContext { get; set; }

        public DataRepository()
        {
            DataContext = new Library();
        }

        // Add methods to interact with DataContext (Library), such as adding and removing entities, and retrieving data.
    }
}
