using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MVCForum.Website.ViewModels
{
    public class RSS
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
        public DateTime Date
        {
            get
            {
                DateTime dt;
                if (DateTime.TryParseExact(PubDate,
                                            "M/d/yyyy hh:mm:ss tt",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                    out dt))
                {
                    return dt;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }
        public bool Check {
            get
            {
                TimeSpan check = DateTime.Now - Date;
                if (check.TotalDays <= 3)
                {
                    return true;
                }
                return false;
            }
            set { }
        }
      

    }
    public class RSSReader
    {
        private static string _URL = "";
      
        public static IEnumerable<RSS> GetRSSFeed(string url)
        {
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                //myRequest.UserAgent = "Fiddler";

                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; .NET CLR 3.5.30729; )";
                myRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
                myRequest.Accept = "text/xml, */*";
                myRequest.KeepAlive = true;
                myRequest.AutomaticDecompression = DecompressionMethods.Deflate |
                                            DecompressionMethods.GZip;
                WebResponse myResponse;
                myResponse = myRequest.GetResponse();
                XmlDocument _xmlDoc = new XmlDocument();
                using (Stream responseStream = myResponse.GetResponseStream())
                {
                    _xmlDoc.Load(responseStream);
                }
                var vm = XElement.Load(new XmlNodeReader(_xmlDoc));


                //var rep = myRequest.GetResponse();
                //var reader = XmlReader.Create(myResponse.GetResponseStream());
                //var doc = XDocument.Load(reader, LoadOptions.None);
                //return (from i in doc.Descendants("channel").Elements("item")
                return (from i in vm.Descendants("channel").Elements("item")
                        select new RSS
                        {
                            Title = i.Element("title").Value,
                            Link = i.Element("link").Value,
                            Description = i.Element("description").Value,
                            PubDate =i.Element("pubDate").Value,
                            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
                            //Date = DateTime.ParseExact(i.Element("pubDate").Value, "MM/dd/yyyy hh:mm:ss tt", null)//,DateTimeStyles.None)
                            //Date = DateTime.ParseExact(i.Element("pubDate").Value, "MM/dd/yyyy hh:mm:ss tt",CultureInfo.InvariantCulture)

                        }).ToList();
            }
            catch 
            {
                return new List<RSS>();
            }
           
        }

       

        private static String[,] Rss_read(string connection)
        {
            WebRequest feedRqst = WebRequest.Create(connection);
            WebResponse feedRspns = feedRqst.GetResponse();

            Stream rssStream = feedRspns.GetResponseStream(); // Returning the feed stream;

            StreamReader sr = new StreamReader(rssStream);

            while (!sr.EndOfStream)
            {
                //Do some logic
            }
            return null;
        }
    

    }
    public class RssViewModel
    {
        public List<RSS> Vanbans { get; set; }
        public List<RSS> Thongbaos { get; set; }
        public List<RSS> Thumois { get; set; }
        public List<RSS> LichCogtacs { get; set; }
        public List<RSS> Tintucs { get; set; }
        public RssViewModel()
        {
            Vanbans = new List<RSS>();
            Thongbaos = new List<RSS>();
            Thumois = new List<RSS>();
            LichCogtacs = new List<RSS>();
            Tintucs = new List<RSS>();

        }
       

    }
}