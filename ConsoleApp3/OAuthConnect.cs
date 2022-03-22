

using Microsoft.Xrm.Tooling.Connector;
using System;

using System.Configuration;


namespace ConsoleApp3
{
    public class OAuthConnect
    {
        public CrmServiceClient ConnectWithOAuth()
        {
            try
            {
                Console.WriteLine("Connecting to D365 Server...");

                CrmServiceClient svc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["MyCRMServer"].ConnectionString);
                if (svc.IsReady)
                {
                    Console.WriteLine("Connected to D365 Server ..");

                    return svc;
                }
                else
                {
                    Console.WriteLine("Could not connected to D365 Server ...");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
