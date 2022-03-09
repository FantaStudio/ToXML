using System;
using System.IO;
using System.Xml.Linq;
using ToXML.Example.Models;

namespace ToXML.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product
            {
                Title = "Milk",
                Description = "Tasty Tasty milk",
                Price = 10,
                Discount = 5,
                AddedDate = DateTime.Now
            };

            Console.WriteLine("Hello, now we save test Class named \"Product\" as xml");

            while(true)
            {
                Console.Write("Enter directory path to save .xml: ");
                var path = Console.ReadLine();
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Path is wrong");
                }
                var fullPath = $"{path}/test.xml";

                Console.WriteLine("creating document...");
                XDocument document = new XDocument();
                document.Add(product.ToXML());

                Console.WriteLine($"saving document at {fullPath}...");
                document.Save(fullPath);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Document saved successfully!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
