using System;
using System.Collections.Generic;

namespace LabAAAAA
{
    public class Models
    {
        public List<Person> People { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Store> Stores { get; set; }

        public Models(List<Person> people, List<Customer> customers, List<Store> stores)
        {
            People = people;
            Customers = customers;
            Stores = stores;
        }
    }
}
