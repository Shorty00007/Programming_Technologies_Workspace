using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;


namespace LogicLayer
{
public class DataServiceFactory
    {
        public ILibraryService Create()
        {
            var dataRepository = new ConcreteDataRepository();

            var dataService = new DataService(dataRepository);

            return dataService;
        }
    }
}
