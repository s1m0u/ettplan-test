using Etteplan.Core.FileHandler;
using Etteplan.Core.Parser;
using System.Xml.Linq;
using Etteplan.Core.Models;

namespace Etteplan.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileHandler = new FileHandler();
            var parser = new XmlParser();

            Console.WriteLine("Etteplan - Arbetstest");
            Console.WriteLine("Simon Måtegen");
            Console.WriteLine("----------------------");
            Console.WriteLine();

            Console.WriteLine("Reading file gentext.xml from source");
            var fileName = "gentext.xml";

            try
            {
                var fileContent = fileHandler.ReadFromFile($"Source/{fileName}");

                if (!string.IsNullOrEmpty(fileContent))
                {
                    Console.WriteLine("File read successfully.");
                    Console.WriteLine("Parsing content... looking for 42014.");

                    var result = parser.Parse(fileContent, "42014");

                    var xmlResult = new XDocument(
                        new XElement("Result",
                            new XElement("Status", result.Status),
                            new XElement("Timestamp", result.Timestamp)
                        )
                    );

                    if (result is SuccessResult successResult)
                    {
                        Console.WriteLine("Parsing successful.");

                        xmlResult.Element("Result")?.Add(
                            new XElement("Id", successResult.Id),
                            new XElement("Value", successResult.Value)
                        );

                    }
                    else if (result is ErrorResult errorResult)
                    {
                        Console.WriteLine("Parsing failed.");
                        xmlResult.Element("Result")?.Add(
                            new XElement("Message", errorResult.Message)
                        );
                    }

                    Directory.CreateDirectory("Target");

                    fileHandler.WriteToFile(xmlResult.ToString(), "Target/result.xml");

                    Console.WriteLine("Result written to Target/result.xml");
                }
                else
                {
                    throw new Exception($"File {fileName} is empty.");
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File gentext.xml not found: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return;
            }
        }
    }
}
