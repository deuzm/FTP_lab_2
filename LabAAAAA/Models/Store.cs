using System;
namespace LabAAAAA
{
    //    "SELECT [Store].[BusinessEntityID]" +
    //    "[Store].[Name]" +
    //    "[Store].[SalesPersonID]" +
    //    "[Store].[Demographics]" +
    //    "[Store].[rowguid]" +
    //    "[Store].[ModifiedDate]" +
    //    "FROM[AdventureWorks2019].[Sales].[Store]";
    public class Store
    {
        public int BusinessEntityID { get; set; }
        public string Name { get; set; }
        public double SalesPersonID { get; set; }

        public Store()
        {
        }
    }
}
