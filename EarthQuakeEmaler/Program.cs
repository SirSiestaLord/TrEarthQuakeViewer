using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TrEarthQuakeViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/earthquakes.txt";
            string line; int i = 0; 
            string webURL = "http://www.koeri.boun.edu.tr/scripts/lasteq.asp";
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Only a Header!");
            byte[] rawByteArray = wc.DownloadData(webURL);
            string webContent = Encoding.ASCII.GetString(rawByteArray);
            var result = Regex.Replace(webContent, "<.*?>", String.Empty);
            string[] lines = new string[result.Length];
           
            using (StringReader reader = new StringReader(result))
            {
               
                while ((line = reader.ReadLine()) != null)
                {
                    StreamWriter writer = new StreamWriter(filepath);
                    writer.Write(result);
                    writer.Close();
                }
               

            }

            while (true)
            {
                Console.WriteLine("\n1-Tüm Depremleri Listele\n\n2-Deprem Ara");
                int menu = int.Parse(Console.ReadLine());
                switch (menu)
                {

                    case 1:
                        {
                            int nullcounter = 0,sayar=0;
                            StreamReader streamReader = new StreamReader(filepath);
                            for (int x=0;x< result.Length;x++) { 
                               
                                lines[x]=streamReader.ReadLine(); sayar++;
                                if (lines[x]==null|| (lines[x] ==" ")){
                                    nullcounter++;

                                }
                                
                                if (nullcounter == 5) { break; }
                                else if(615>sayar&& sayar > 100){ Console.WriteLine(lines[x]);  }
                            }
                            
                           
                            break;
                        }
                    case 2:
                        {

                            int nullcounter = 0, sayar = 0;string aranan = "";
                            Console.WriteLine("Aranan Değeri Girin :");
                            aranan=Console.ReadLine();
                            StreamReader streamReader = new StreamReader(filepath);
                            for (int x = 0; x < result.Length; x++)
                            {

                                lines[x] = streamReader.ReadLine(); sayar++;
                                if (lines[x] == null || (lines[x] == " "))
                                {
                                    nullcounter++;

                                }

                                if (nullcounter == 5) { break; }
                                else if (615 > sayar && sayar > 100) {
                                    if (lines[x].Contains(aranan)) { Console.WriteLine(lines[x]); }
                                }
                            }


                            break;
                        }
                    default: { break; }
                }
            }
      
        }
       

    }
}
