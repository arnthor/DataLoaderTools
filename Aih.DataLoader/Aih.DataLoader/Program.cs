using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aih.DataLoader.Tools;

namespace Aih.DataLoader
{
    class Program
    {
        /// <summary>
        /// Entrypoint to the program that has the sole purpose of running batch jobs to move data from one place to another.  The data is moved by implementing BaseDataLoader from Aih.DataLoader.Tools.
        /// This program takes in a parameter with a dll name, looks for that dll in the Loaders folder (might be configurable in the future), finds all classes that implement BaseDataLoader in it, creates
        /// one instance of each and calls RunDataLoader() on all of them.
        /// </summary>
        /// <param name="args">
        /// -dll : Name of the assembly that contains the dataloaders.
        /// -n: (optional).  If you only want to create a specific dataloader within a dll pass its name here. If the -n parmeter is empty, an instance is created for all classes that implement BaseDataLoader.
        /// </param>
        static int Main(string[] args)
        {

            //Parse arguments 
            Dictionary<string, string> config = CommandLineParser.GetConfig(args);
            if (config["DLL"] == "")
                return 1;

            string reportStoreType = "";
            string reportStoreConnectionString = "";
            string porpertyStoreType = "";
            string propertyStoreConnectionString = "";

            try
            {
                IniParser ini = new IniParser("conf.ini");
                reportStoreType = ini.GetSetting("STATUS_REPORT", "REPORT_TO");
                reportStoreConnectionString = ini.GetSetting("STATUS_REPORT", "REPORT_CONNECTION");
                porpertyStoreType = ini.GetSetting("PROPERTY_STORE", "TYPE");
                propertyStoreConnectionString = ini.GetSetting("PROPERTY_STORE", "CONNECTION");
            }
            catch(System.IO.FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }

            //Setup property store
            //Currently we hardcode this to SQL Server, the infrastructure to reflect on it is in place
            IPropertyHandler propertyStore = new Aih.DataLoader.Tools.PropertyHandlers.SQLServerPropertyHandler(propertyStoreConnectionString);


            //Setup where to report status to
            //Currently we hardcode this to SQL Server, the infrastructure to reflect on it is in place
            IStatusHandler statusHandler = new Aih.DataLoader.Tools.StatusHandlers.SQLServerStatusHandler(reportStoreConnectionString);

            return 0;
        }
    }




    internal static class CommandLineParser
    {
        public static Dictionary<string, string> GetConfig(string[] args)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();

            string dllPath = GetValue(args, "-dll");
            string typename = GetValue(args, "-n");

            config.Add("DLL", dllPath);
            config.Add("TYPENAME", typename);

            return config;
        }


        private static string GetValue(string[] args, string parameter)
        {
            if ((args != null) && (args.Length > 0))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].ToLower() == parameter)
                    {
                        return args[i + 1];
                    }
                }
            }

            return "";
        }
    }
}
