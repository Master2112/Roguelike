using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace TimGame.Engine
{
    static class DataSaver
    {
        public static string SaveFolder
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + saveFolderName;
            }
        }

        private static string saveFolderName = "Saves";

		public static string[] Files
        {
            get
            {
                string[] files = System.IO.Directory.GetFiles(SaveFolder);
 
                for(int i = 0; i < files.Length; i++)
                {
                    files[i] = files[i].Replace(SaveFolder + "\\", "");
                }

                return files;
            }
        }

        public static void DeleteFile(string fileName)
        {
            if (Directory.Exists(SaveFolder))
            {
                if (File.Exists(SaveFolder + "\\" + fileName))
                {
                    File.Delete(SaveFolder + "\\" + fileName);
                }
            }
        }
		
        public static bool SaveAsFile<T>(this T data, string fileName)
        {
            Console.WriteLine("Saving File: " + SaveFolder + "\\" + fileName);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string jsonData = serializer.Serialize(data);

            bool canDo = true;

            if(!Directory.Exists(SaveFolder))
            {
                DirectoryInfo info = Directory.CreateDirectory(SaveFolder);
                canDo = info.Exists;
            }

            if(canDo)
            {
                StreamWriter writer = File.CreateText(SaveFolder + "\\" + fileName);
                writer.Write(jsonData);
                writer.Close();

                return true;
            }

            return false;
        }

        public static T LoadFile<T>(string fileName)
        {
            Console.WriteLine("Loading File: " + SaveFolder + "\\" + fileName);
            
            if (!Directory.Exists(SaveFolder))
            {
                return default(T);
            }
            else
            {
                if(File.Exists(SaveFolder + "\\" + fileName))
                {
                    StreamReader reader = File.OpenText(SaveFolder + "\\" + fileName);
                    string json = reader.ReadToEnd();
					reader.Close();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    return serializer.Deserialize<T>(json);
                }
            }

            return default(T);
        }
    }
}
