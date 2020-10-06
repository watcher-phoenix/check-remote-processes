using Sabs.Dlls.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningProcesses
{
    public class EmailRunnerStatus
    {
        public List<AppsToCheck> LoadApps()
        {
            var appsToCheck = new List<AppsToCheck>
            {
                new AppsToCheck
                {
                    ApplicationName = "Command Line Utility Program",
                    ApplicationProcessName = "rundll32",
                    Status = "Not Running"
                },
                new AppsToCheck
                {
                    ApplicationName = "Indexing Service",
                    ApplicationProcessName = "cidaemon",
                    Status = "Not Running"
                },
                new AppsToCheck
                {
                    ApplicationName = "Printer Spool",
                    ApplicationProcessName = "spoolsv",
                    Status = "Not Running"
                }
            };

            return appsToCheck;

        }

        public void CheckRunnerStatus()
        {
            //Load the Data
            List<AppsToCheck> checkedApps = LoadApps();

            var host = "localhost";
            var path = string.Format("\\\\{0}\\root\\CIMV2");

            var listToCheck = new List<AppsToCheck>();

            // Loop Through List of Processes
            foreach (var app in checkedApps)
            {
                var applicationName = app.ApplicationName;
                

                // Connect to Server for Process
                RemoteConnection.Connection(host, path, applicationName);

                // Check Process Status
                string status = RemoteConnection.GetApplicationStatus(app.ApplicationName);

                listToCheck.Add(new AppsToCheck
                {
                    ApplicationName = app.ApplicationName,
                    ApplicationProcessName = app.ApplicationProcessName,
                    Status = status
                });

            }
            PrintStatus(listToCheck);

        }

        public void PrintStatus(List<AppsToCheck> checkedApps)
        {
           
            // Create Header
            var head = "RUNNER" + "\t\t\t\t\t\t" + "STATUS\n";
            Console.WriteLine(head);

            // Loop the Process List
            foreach (var app in checkedApps)
            {
                // Add Process and Status to Email
                if (app.ApplicationName.Length > 24)
                {
                    head += app.ApplicationName + "\t\t\t" + app.Status + "\n";
                }
                else
                {
                    head += app.ApplicationName + "\t\t\t\t" + app.Status + "\n";
                }
            }

            Console.WriteLine(head);
        }
    }
}
