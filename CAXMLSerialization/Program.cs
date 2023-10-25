using System.Xml;
using System.Xml.Serialization;

namespace CAXMLSerialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var e1 = new Employee
            {
                Id = 1001,
                Fname = "Issam",
                Lname = "A.",
                Benefits = { "Pension", "Health Insurance" }
            };

            var xmlContent = SerializeToXmlString(e1);
            Console.WriteLine(xmlContent);
            File.WriteAllText("XmlDocument.xml", xmlContent);

            var xmlContent2 = File.ReadAllText("xmlDocument.xml");
            Employee e2 = DeserializeFromXmlString(xmlContent2);
            
            Console.ReadKey();  
        }

        // from string to Xml
        private static string SerializeToXmlString(Employee e1)
        {
            var result = "";
            var xmlSerializer = new XmlSerializer(e1.GetType());
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(writer, e1);
                    result = sw.ToString();
                }
            }
            return result;
        }

        // from xml to string
        private static Employee DeserializeFromXmlString(string xmlContent)
        {
            Employee? employee = null;
            var xmlSerializer = new XmlSerializer(typeof(Employee));
            using (TextReader reader = new StringReader(xmlContent))
            {
                employee = xmlSerializer.Deserialize(reader) as Employee;
            }
            return employee;
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public List<string> Benefits { get; set; } = new List<string>();
    }
}