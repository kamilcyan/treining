using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

public class Program
{
    public static void Main()
    {
        XmlReader reader = XmlReader.Create(@"d:\git\Treining\file.xml");

        GetContentToFile();
        ReadFromXml(reader);


        //using (XmlReader reader = XmlReader.Create(@"d:\git\Treining\file.xml"))
        //{
        //    while (reader.Read())
        //    {
        //        if (reader.IsStartElement())
        //        {
        //            //return only when you have START tag  
        //            switch (reader.Name.ToString())
        //            {
        //                case "Name":
        //                    Console.WriteLine("Name of the Element is : " + reader.ReadString());
        //                    break;
        //                case "Location":
        //                    Console.WriteLine("Your Location is : " + reader.ReadString());
        //                    break;
        //            }
        //        }
        //        Console.WriteLine("");
        //    }
        //}
        Console.ReadKey();
    }

    private static void GetContentToFile()
    {

        var url = "https://www.yr.no/place/Poland/Lublin/Pu%C5%82awy/forecast.xml";

        var httpClient = new HttpClient();
        var html = httpClient.GetStringAsync(url);
        
        System.IO.File.WriteAllText(@"D:\git\Treining\file.xml", html.Result);
    }

    public static void ReadFromXml(XmlReader reader)
    {
        reader.MoveToContent();

        // Read node attributes
        string name = reader.GetAttribute("name");
        string sunRise = reader.GetAttribute("sun rise");

        if (reader.IsEmptyElement) { reader.Read(); return; }

        reader.Read();
        while (!reader.EOF)
        {
            if (reader.IsStartElement())
            {
                switch (reader.Name)
                {
                    // Read element for a property of this class
                    case "name":
                        name = reader.ReadElementContentAsString();
                        break;

                    // Starting sub-list
                    case "sun rise":
                        sunRise = reader.ReadElementContentAsString();
                        break;

                    //default:
                        //.Skip();
                }
            }
            else
            {
                reader.Read();
                break;
            }
        }
    }
}

