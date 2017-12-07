using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{

    [Serializable]
    class Getfiles
    {
        public List<string> GetAllFiles(string sDirt)
        {
            List<string> files = new List<string>();

            try
            {
                foreach (string file in Directory.GetFiles(sDirt))
                {
                    files.Add(file);
                }
                foreach (string fl in Directory.GetDirectories(sDirt))
                {
                    files.AddRange(GetAllFiles(fl));
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return files;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Getfiles get = new Getfiles();
            //Вибір папки
            List<string> files = get.GetAllFiles(@"D:\Test");


            string fileName = "test.dat";
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                
                binaryFormatter.Serialize(fs, files);
            }

            using (var fs = new FileStream(fileName, FileMode.Open))
            {

                    var dsUser = binaryFormatter.Deserialize(fs);

                    
                    foreach (string f in files)
                    {
                        Console.WriteLine(f);
                    }
                
            }
            Console.WriteLine(new string('-',50));

            using (StreamReader sr = new StreamReader(fileName))
            {
               
                String line = sr.ReadToEnd();
                Console.WriteLine(line);
            }   

            Console.ReadLine();


        }
    }
}
