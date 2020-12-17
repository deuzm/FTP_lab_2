using System;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections.Generic;

namespace LabAAAAA
{
    public class XmlGeneratorService : ServiceLayer.IXmlGeneratorService
    {
        public XmlGeneratorService()
        {
        }

        public void GenerateXmlFile(string path, List<Customer> obj)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(List<Customer>));
                writer.Serialize(fs, obj);
            }
        }
        public void GenerateXmlFile(string path, List<Person>  obj)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(List<Person>));
                writer.Serialize(fs, obj);
            }
        }
        public void GenerateXmlFile(string path, List<Store> obj)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(List<Store>));
                writer.Serialize(fs, obj);
            }
        }

        public void GenerateXsdFromXml(string path)
        {
            string fileName = Path.GetFileName(path);
            XmlReader reader = XmlReader.Create(fileName);
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();
            schemaSet = schema.InferSchema(reader);

            foreach (XmlSchema s in schemaSet.Schemas())
            {
                XmlWriter writer;
                int count = 0;
                foreach (XmlSchema ss in schemaSet.Schemas())
                {
                    writer = XmlWriter.Create(fileName.Replace(".xml", ".xsd"));
                    ss.Write(writer);
                    writer.Close();
                    Console.WriteLine("Done " + count);
                }
                reader.Close();
            }
        }
    }
}
