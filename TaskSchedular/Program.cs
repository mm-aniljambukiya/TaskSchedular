using Microsoft.Win32.TaskScheduler;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Path to the XML file (you can directly provide the XML string here if preferred)
        string xmlFilePath = @"C:\Users\MagnusMinds\Desktop\Every minutes.xml";

        // Load the XML content
        string taskXml = System.IO.File.ReadAllText(xmlFilePath);

        // Create the TaskService instance to work with the local Task Scheduler
        using (TaskService ts = new TaskService())
        {
            try
            {
                // Register the task from XML
                TaskDefinition taskDefinition = ts.NewTask();

                // Import XML into the TaskDefinition
                taskDefinition.XmlText = taskXml;

                // Register the task in the scheduler (it will appear in Task Scheduler Library)
                // Provide task path and name in RegisterTaskDefinition ("/" + TaskName)
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