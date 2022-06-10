using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TTHK5._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, char> possibleOperations = new Dictionary<string, char>();

            possibleOperations.Add("create", 'C');
            possibleOperations.Add("move", 'M');
            possibleOperations.Add("delete", 'D');

            Directory.CreateDirectory(string.Join("\\", Environment.CurrentDirectory, "Data"));
            Directory.CreateDirectory(string.Join("\\", Environment.CurrentDirectory, "myData"));

            char operation = chooseOperation("Please, input operation:", "Try again..", possibleOperations);

            switch (operation)
            {
                case 'C': createFile(); break;
                case 'M': moveFile(); break;
                case 'D': deleteFile(); break;
                default:
                    break;
            }
        }

        private static void createFile()
        {
            Console.WriteLine("Please, input a file name:");

            string fileName = Console.ReadLine();

            if (File.Exists(string.Join("\\", Environment.CurrentDirectory, "Data", fileName)))
            {
                Console.WriteLine("File already exists..");
                return;
            }

            if (File.Create(string.Join("\\", Environment.CurrentDirectory, "Data", fileName)) != null)
                Console.WriteLine("File {0} successfylly created..", fileName);
            else
                Console.WriteLine("Unhandled error..");
        }

        private static void moveFile()
        {
            Console.WriteLine("Please, input a file name:");

            string fileName = Console.ReadLine();

            if (!File.Exists(string.Join("\\", Environment.CurrentDirectory, "Data", fileName)))
            {
                Console.WriteLine("File is not exists..");
                return;
            }

            try
            {
                File.Move(string.Join("\\", Environment.CurrentDirectory, "Data", fileName), string.Join("\\", Environment.CurrentDirectory, "myData", fileName));
                Console.WriteLine("File {0} successfylly moved..", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void deleteFile()
        {
            Console.WriteLine("Please, input a file name:");

            string fileName = Console.ReadLine();

            if (!File.Exists(string.Join("\\", Environment.CurrentDirectory, "Data", fileName)))
            {
                Console.WriteLine("File is not exists..");
                return;
            }

            try
            {
                File.Delete(string.Join("\\", Environment.CurrentDirectory, "Data", fileName));
                Console.WriteLine("File {0} successfylly deleted..", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static char chooseOperation(string reqMsg, string errMsg, Dictionary<string, char> possibleOperations)
        {
            Console.WriteLine("{0} ({1})", reqMsg, string.Join(", ", possibleOperations.Keys));

            string operation = default;
            char ret = default;

            while (true)
            {
                try
                {
                    operation = Console.ReadLine().ToLower();

                    if (possibleOperations.TryGetValue(operation, out ret))
                        break;

                    throw new Exception();
                }
                catch
                {
                    Console.WriteLine(errMsg);
                }
            }

            return ret;
        }
    }
}
