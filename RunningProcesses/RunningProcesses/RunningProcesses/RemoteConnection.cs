using System;
using System.Management;
using RunningProcesses;
using System.Collections.Generic;
using System.Diagnostics;

namespace RunningProcesses
{
    public class RemoteConnection
    {
        public static ManagementObjectSearcher searcher;
        public static ManagementObject service;

        public static string ApplicationName { get; set; }
        public static void Connection(string host, string path, string applicationName, string commandLine)
        {
            //TODO: Need to change host and path
            string Host = host;
            string Path = path;


            //TODO: Username and Password need changed
            var options = new ConnectionOptions();
            options.Username = "";
            options.Password = "";
            options.EnablePrivileges = true;
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Packet;


            var scope = new ManagementScope(Path, options);
            searcher = new ManagementObjectSearcher(scope, new SelectQuery("SELECT * FROM Win32_Process"));
            service = new ManagementObject();
            bool isRunning = searcher.Get().Count > 0;

        }

        public static string GetApplicationStatus(string applicationName)
        {

            foreach (ManagementObject service in searcher.Get())
            {//service.GetPropertyValue("Execution State").ToString().Equals("Running")

                try
                {
                   // if (service.GetPropertyValue("Status").ToString().ToLower().Equals("running"))
                    //{
                        if (service["Name"].ToString().Contains(applicationName))
                        {
                            return "Running";
                        }
                   // }
                }
                catch (Exception)
                {
                    Debug.WriteLine("-----------------------------------");
                    Debug.WriteLine("Win32_Process instance");
                    Debug.WriteLine("-----------------------------------");
                    Debug.WriteLine("Name: {0}", service["Name"]);
                    Debug.WriteLine("Status: {0}", service["Status"]);

                }
               
            } return "Not Running";
        }
    }
}
