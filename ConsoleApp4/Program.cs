using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using System.Text.Json;
//using Newtonsoft.Json;
using System.Xml;

namespace svs_json
{


   


    class Program
    {
        static async void  PostRequestAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ruzditest.eisnot.ru:8280/services/ruzdiUploadNotificationPackageService_v1_1");
            
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.Headers.Add("SOAPAction", "http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService");
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("C://123.xml");
                string data1 = xDoc.OuterXml;
                Console.WriteLine(data1);

                byte[] Array = Encoding.UTF8.GetBytes(data1);

                request.ContentLength = Array.Length;


            using (Stream writer = request.GetRequestStream())
            {

                writer.Write(Array, 0, Array.Length);


               
            }
                    HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Console.WriteLine("Запрос создан");



            using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {

                            string q = reader.ReadToEnd();
                            Console.WriteLine(q);

                            XmlDocument xDocout = new XmlDocument();
                  
                            xDocout.LoadXml(q);
                            xDocout.Save("1234.xml");
                       

                        }
                    }

                
            




            Console.WriteLine("Запрос выполнен...");
        }

        static void Main(string[] args)
        {



            PostRequestAsync();
            Console.ReadLine();

        }
    }
}
