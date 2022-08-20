using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace BatchEPUB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                DirectoryInfo dir = new DirectoryInfo(arg);
                var name = dir.Name;
                var fullname = dir.FullName;
                var path = dir.Parent.FullName;

                var py = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "images2epub.py");

                var command = $"{py} -t \"{name}\" -d rtl {fullname} {fullname}.epub";
                Console.WriteLine(command);

                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = command;
                start.StandardOutputEncoding = Encoding.UTF8;
                start.FileName = "python";

                start.CreateNoWindow = true;

                start.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                start.RedirectStandardInput = true;
                //输出信息
                start.RedirectStandardOutput = true;
                // 输出错误
                start.RedirectStandardError = true;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string line = reader.ReadLine();//每次讀取一行命令列列印的資訊
                        Console.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
    }
}
