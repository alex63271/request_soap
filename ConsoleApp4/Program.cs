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


    public class Class1
    {
        public object Message { get; set; }
        public bool Result { get; set; }
        public string SignerCertificate { get; set; }
        public Signercertificateinfo SignerCertificateInfo { get; set; }
        public Signatureinfo SignatureInfo { get; set; }
        public object AdditionalCertificateResult { get; set; }
    }

    public class Signercertificateinfo
    {
        public string SubjectName { get; set; }
        public string IssuerName { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime NotAfter { get; set; }
        public string SerialNumber { get; set; }
        public string Thumbprint { get; set; }
    }

    public class Signatureinfo
    {
        public string CAdESType { get; set; }
    }


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
            string data1=xDoc.OuterXml;
            Console.WriteLine(data1);
            
            byte[] Array = Encoding.UTF8.GetBytes(data1);

            request.ContentLength = Array.Length;


            using (Stream writer = request.GetRequestStream())
            {

                writer.Write(Array, 0, Array.Length);


                Console.WriteLine("Запрос создан");

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();




                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {

                        string q = reader.ReadToEnd();
                        Console.WriteLine(q);

                        XmlDocument xDocout = new XmlDocument();
                        xDocout.LoadXml(q);
                        xDocout.Save("1234.xml");
                        //Class1[] otvet = JsonSerializer.Deserialize<Class1[]>(q);
                        //Console.WriteLine("Message:");
                        //Console.WriteLine(otvet[0].Message);
                        //Console.WriteLine("SubjectName:");
                        //Console.WriteLine(otvet[0].SignerCertificateInfo.SubjectName);
                        //Console.WriteLine("Thumbprint:");
                        //Console.WriteLine(otvet[0].SignerCertificateInfo.Thumbprint);


                    }
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
