using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class LinqWay
    {
        public void Run(CrmServiceClient svc)
        {
            Retrive(svc);
            RetriveEarlyBound(svc);
            Update(svc);
            Delete(svc);
        }
        //Retrieve Multiple Record using Linq
        private void Retrive(CrmServiceClient svc)
        {
            using (OrganizationServiceContext orgSvcContext = new OrganizationServiceContext(svc))
            {
                var query = from c in orgSvcContext.CreateQuery("contact")
                            where c["firstname"] != null && (int)c["statecode"] == 0
                            orderby c["firstname"]
                            select new
                            {
                                first_name = c["firstname"]

                            };

                if (query != null)
                {

                    foreach (var c in query)
                    {

                        Console.WriteLine(c.first_name);

                    }
                }
                else
                {
                    Console.WriteLine("No Data ");
                }
            }


            Console.WriteLine("Retrieved all Contacts usig linq...");
        }

        private void RetriveEarlyBound(CrmServiceClient svc)
        {
            using (OrganizationServiceContext orgSvcContext = new OrganizationServiceContext(svc))
            {
                var query = from c in orgSvcContext.CreateQuery<Contact>()
                            where c.FirstName != null && (int)c.StateCode == 0
                            orderby c.FirstName
                            select new
                            {
                                first_name = c.FirstName

                            };

                if (query != null)
                {

                    foreach (var contact in query)
                    {

                        Console.WriteLine(contact.first_name);

                    }
                }
                else
                {
                    Console.WriteLine("No Data ");
                }
            }


            Console.WriteLine("Retrieved all Contacts usig linq   Earlybound...");
        }

        //UPDATE Using Linq
        private void Update(CrmServiceClient svc)
        {

            using (OrganizationServiceContext orgSvcContext = new OrganizationServiceContext(svc))
            {
                var query = from c in orgSvcContext.CreateQuery("contact")
                            where (string)c["firstname"] == "baha" && (int)c["statecode"] == 0
                            orderby c["firstname"]
                            select c
                            ;

                if (query != null)
                {

                    foreach (var c in query)
                    {

                        Entity contact = new Entity("contact");
                        contact = svc.Retrieve(contact.LogicalName, c.Id, new ColumnSet(true));
                        contact["firstname"] = "Noor";
                        // svc.Update(contact);
                        Console.WriteLine("Updated contact");

                    }
                }
                else
                {
                    Console.WriteLine("No Data to Update");
                }

            }

        }
        //DELETE Using Linq
        private void Delete(CrmServiceClient svc)
        {

            using (OrganizationServiceContext orgSvcContext = new OrganizationServiceContext(svc))
            {
                var query = from c in orgSvcContext.CreateQuery("contact")
                            where (string)c["firstname"] == "Alex"

                            select c
                            ;
                if (query != null)
                {
                    foreach (var c in query)
                    {
                        //svc.Delete("contact", c.Id);
                    }

                }
                else
                {
                    Console.WriteLine("No Data to Delete");
                }
            }
        }

    }
}
