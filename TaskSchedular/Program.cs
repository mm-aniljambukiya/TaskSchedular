using Microsoft.Win32.TaskScheduler;
using System;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Enter Task Scheduler Path : ");
        string xmlFilePath = Console.ReadLine();
        string taskXml = System.IO.File.ReadAllText(xmlFilePath);
        Console.WriteLine("Enter BAT File Path : ");
        string filePath = Console.ReadLine();


        using (TaskService ts = new TaskService())
        {
            try
            {
                TaskDefinition taskDefinition = ts.NewTask();
                XDocument xdoc = XDocument.Load(xmlFilePath);
                XNamespace ns = "http://schemas.microsoft.com/windows/2004/02/mit/task";
                XElement argumentsElement = xdoc.Descendants(ns + "Arguments").FirstOrDefault();
                if (argumentsElement != null)
                {
                    // Update the value of <Arguments>
                    argumentsElement.Value = "/c start /min \"\" \""+ filePath + "\"";
                }
                else
                {
                    Console.WriteLine("<Arguments> element not found.");
                }

                taskDefinition.XmlText = xdoc.ToString();
                ts.RootFolder.RegisterTaskDefinition(@"\EveryMinutesTask", taskDefinition);
                Console.WriteLine("Task registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering task: " + ex.Message);
            }
        }
    }
}