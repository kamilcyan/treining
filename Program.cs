using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

public class Program
{
    public static void Main()
    {
        getContent();

        async Task TestReader(System.IO.Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(@"D:\ProjektyCsh\Trening\ConsoleApp11\ConsoleApp11\file.xml", settings))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.IsEmptyElement)
                            Console.WriteLine("<{0}/>", reader.Name);
                        else
                        {
                            Console.Write("<{0}> ", reader.Name);
                            reader.Read(); // Read the start tag.
                            if (reader.IsStartElement())  // Handle nested elements.
                                Console.Write("\r\n<{0}>", reader.Name);
                            Console.WriteLine(reader.ReadString());  //Read the text content of the element.
                        }
                    }
                }
            }
        }

        Console.ReadLine();
    }

    private static void getContent()
    {

        var url = "https://www.yr.no/place/Poland/Lublin/Pu%C5%82awy/forecast.xml";

        var httpClient = new HttpClient();
        var html = httpClient.GetStringAsync(url);
        
        System.IO.File.WriteAllText(@"D:\git\Treining\file.xml", html.Result);
    }
}
