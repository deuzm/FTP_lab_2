using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LabAAAAA
{
    [Serializable]
    public class Person
    {
        public int BusinessEntityID { get; set; }
        public int TerritoryID { get; set; }
        public double SalesQuota { get; set; }
        public float Bonus { get; set; }
        public double SalesLastYear { get; set; }

        public Person()
        {
        }
    }
}
