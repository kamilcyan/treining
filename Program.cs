using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

public class Program
{
    public static void Main()
    {
        getContentToFile();

        using (XmlReader reader = XmlReader.Create(@"d:\git\Treining\file.xml"))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    //return only when you have START tag  
                    switch (reader.Name.ToString())
                    {
                        case "Name":
                            Console.WriteLine("Name of the Element is : " + reader.ReadString());
                            break;
                        case "Location":
                            Console.WriteLine("Your Location is : " + reader.ReadString());
                            break;
                    }
                }
                Console.WriteLine("");
            }
        }
        Console.ReadKey();

        Console.ReadLine();
    }

    private static void getContentToFile()
    {

        var url = "https://www.yr.no/place/Poland/Lublin/Pu%C5%82awy/forecast.xml";

        var httpClient = new HttpClient();
        var html = httpClient.GetStringAsync(url);
        
        System.IO.File.WriteAllText(@"D:\git\Treining\file.xml", html.Result);
    }
}
