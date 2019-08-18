using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

public class Program
{
    public static void Main()
    {
       

        GetContentToFile();
        //ReadFromXml(reader);

        using (XmlReader reader = XmlReader.Create(@"D:\git\Treining\file.xml"))
        {

            while (reader.Read())
            {

                if (reader.IsStartElement())
                {

                    if (reader.Name.ToString() == "country")
                        Console.WriteLine("Your Location is : " + reader.ReadString());
                    if (reader.Name.ToString() == "name")
                        Console.WriteLine("Your City is : " + reader.ReadString());
                    if (reader.Name.ToString() == "type")
                        Console.WriteLine("Your Type is : " + reader.ReadString());
                    if (reader.Name.ToString() == "timezone")
                    {
                        string attribute;
                        attribute = reader["id"];
                        Console.WriteLine("id : " + attribute);
                        attribute = reader["utcoffsetMinutes"];
                        Console.WriteLine("id : " + attribute);
                    }
                    string date;
                    if (reader.Name.ToString() == "tabular")
                    {
                        string time;
                        //if (reader.LocalName.ToString() == "time")
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        Console.WriteLine(reader.ReadString());
                        string attribute;
                        attribute = reader["from"].Substring(0,10);
                        date = attribute;
                        Console.WriteLine("time from : " + attribute);
                        attribute = reader["to"].Substring(11,8);
                        Console.WriteLine("time to : " + attribute);

                        setDate(date);
                    }
                    reader.MoveToAttribute("symbol");
                    Console.WriteLine("Your Type is : " + reader.Name.ToString());
                    void setDate(string dat)
                    {
                        string dateToday = DateTime.Now.Year + "-" + "0" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                        Console.WriteLine(dateToday);
                        Console.WriteLine(date);
                        if (reader.Name.ToString() == "symbol")
                        {


                            if (dateToday == date)
                            {
                                Console.WriteLine("hola");
                                string attribute;
                                attribute = reader["name"];
                                Console.WriteLine("clouds : " + attribute);
                            }
                        }
                    }


                    //Console.WriteLine("Your Location is : " + country);

                    //return only when you have START tag  
                    //switch (reader.Name.ToString())
                    //{
                    //    case "sun rise":
                    //        Console.WriteLine("Name of the Element is : " + reader.ReadString());
                    //        break;
                    //    case "country":
                    //        Console.WriteLine("Your Location is : " + reader.ReadString());
                    //        break;
                    //}
                }


            }
        }
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
        //reader.MoveToContent();

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

