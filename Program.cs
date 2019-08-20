using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

public class Program
{
    public static void Main()
    {
        bool isSameDate;
        int liczba = 0;
        GetContentToFile();
        //ReadFromXml(reader);

        string [] times = new string[40];
        string[] symbols = new string[40];
        string[] windDir = new string[40];
        string[] temperatures = new string[40];

        int licznikTemp = 0;
        int licznikWindDir = 0;
        int licznikTime = 0;
        int licznikSymb = 0;

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
                        attribute = reader["from"].Substring(0, 10);
                        date = attribute;
                        Console.WriteLine("time from : " + attribute);
                        attribute = reader["to"]/*.Substring(11,8)*/;
                        Console.WriteLine("time to : " + attribute);


                        //reader.MoveToAttribute("symbol");
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        attribute = reader["name"];
                        Console.WriteLine("cloudines : " + attribute);
                        attribute = reader["code"];
                        Console.WriteLine("wind direction : " + attribute);
                        reader.MoveToNextAttribute();
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        reader.MoveToNextAttribute();
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        reader.MoveToNextAttribute();
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        reader.MoveToNextAttribute();

                        reader.MoveToAttribute("symbol");
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        reader.MoveToNextAttribute();
                        time = reader.Name.ToString();
                        Console.WriteLine(time);
                        //setDate(date);
                    }

                    //void setDate(string dat)
                    //{
                    //    string dateToday = "2019-08-19";
                    //        /*DateTime.Now.Year + "-" + "0" + DateTime.Now.Month + "-" + DateTime.Now.Day;*/
                    //    Console.WriteLine(dateToday + date);

                    //    if (date == dateToday)
                    //    {
                    //        Console.WriteLine("time fromljnkjhoho : " );
                    //Console.WriteLine(liczba);

                    if (reader.Name.ToString() == "time")
                    {
                        string attribute;
                        attribute = reader["from"];
                        attribute = reader["to"];
                        times[licznikTime] = reader["from"].Substring(0, 10);
                        licznikTime++;
                    }


                    if (reader.Name.ToString() == "symbol")
                    {
                        string attribute;
                        attribute = reader["name"];
                        symbols[licznikSymb] = reader["name"];
                        licznikSymb++;
                    }

                    if (reader.Name.ToString() == "windDirection")
                    {
                        string attribute;
                        attribute = reader["code"];
                        windDir[licznikWindDir] = reader["code"];
                        licznikWindDir++;
                    }

                    if (reader.Name.ToString() == "temperature")
                    {
                        string attribute;
                        attribute = reader["value"];
                        temperatures[licznikTemp] = reader["value"];
                        licznikTemp++;
                    }

                    liczba++;


                }

            }

            Console.WriteLine("temp first day : " + temperatures[0]);
            Console.WriteLine("cloudines first day : " + symbols[0]);
            Console.WriteLine("wind first day : " + windDir[0]);
            Console.WriteLine("time first day : " + times[0]);

            Console.WriteLine("temp second day : " + temperatures[1]);
            Console.WriteLine("cloudines second day : " + symbols[1]);
            Console.WriteLine("wind second day : " + windDir[1]);
            Console.WriteLine("time second day : " + times[1]);
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

