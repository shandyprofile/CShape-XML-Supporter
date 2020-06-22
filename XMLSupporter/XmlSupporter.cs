using System;
using System.IO;
using System.Xml.Serialization;

namespace XMLSupporter
{
    class Process
    {
        static void Main(string[] args)
        {
            TestClass test = new TestClass { Param1 = "Param 1 test", Param2 = "Param 2 test" };
            XmlSupporter.SerializeObject("text.xml", test);

            TestClass testResult = XmlSupporter.DeserializeObject<TestClass>("text.xml");

            Console.WriteLine(testResult.Param1 + testResult.Param2);
            Console.ReadLine();
        }
    }

    public class TestClass
    {
        [XmlElement("Param1")]
        public string Param1 { get; set; }

        [XmlElement("Param2")]
        public string Param2 { get; set; }
    }

    public static class XmlSupporter
    {
        public static void SerializeObject<T>(string filename, T data) where T : class
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));

                using (TextWriter writer = new StreamWriter(filename))
                {
                    s.Serialize(writer, data);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static T DeserializeObject<T>(string filename) where T : new()
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer x = new XmlSerializer(typeof(T));
                    return (T)x.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                return new T();
            }
        }
    }
}
