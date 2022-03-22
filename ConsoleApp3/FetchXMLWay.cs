using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class FetchXMLWay
    {
        public void Run(CrmServiceClient svc)
        {
            Retrive(svc);
            RetriveEarlyBound(svc);
            Update(svc);
            Delete(svc);
        }
        //Retrieve Multiple Record using Fetch XML
        private void Retrive(CrmServiceClient svc)
        {
            string fetchXmlString = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                            <entity name='contact'>
                                             <attribute name='firstname' />
                                     <order attribute='firstname' descending='false' />
                                          <filter type='and'>
                                              <condition attribute='firstname' operator='not-null' />
                          <condition attribute='statecode' operator='eq' value='0' />
                                              </filter>
                     </entity>
                                                            </fetch>";
            EntityCollection f = ExecuteFetch(fetchXmlString, svc);
            if (f != null)
            {

                foreach (var c in f.Entities)
                {


                    Console.WriteLine(c.Attributes["firstname"]);

                }
                Console.WriteLine("Retrieved all Contacts usig fetchXML...");
            }
            else
            {
                Console.WriteLine("No Data");
            }
        }
        private void RetriveEarlyBound(CrmServiceClient svc)
        {
            string fetchXmlString = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                            <entity name='contact'>
                                             <attribute name='firstname' />
                                     <order attribute='firstname' descending='false' />
                                          <filter type='and'>
                                              <condition attribute='firstname' operator='not-null' />
                          <condition attribute='statecode' operator='eq' value='0' />
                                              </filter>
                     </entity>
                                                            </fetch>";
            EntityCollection data = ExecuteFetch(fetchXmlString, svc);
            List<Contact> contacts = data.Entities.Select(e => e.ToEntity<Contact>()).ToList();
            if (contacts != null)
            {

                foreach (Contact contact in contacts)
                {


                    Console.WriteLine(contact.FirstName);

                }
                Console.WriteLine("Retrieved all Contacts usig fetchXML  Early Bound...");
            }
            else
            {
                Console.WriteLine("No Data");
            }
        }
        //UPDATE Using FetchXml
        private void Update(CrmServiceClient svc)
        {
            string fetchXmlString = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
              <entity name='contact'>
                <attribute name='firstname' />
                <filter type='and'>
                  <condition attribute='firstname' operator='eq' value='Noor' />
                  <condition attribute='firstname' operator='not-null' />
                </filter>
              </entity>
            </fetch>";
            EntityCollection f = ExecuteFetch(fetchXmlString, svc);
            if (f != null)
            {
                foreach (var c in f.Entities)
                {
                    Entity contact = new Entity("contact");
                    contact = svc.Retrieve(contact.LogicalName, c.Id, new ColumnSet(true));
                    contact["firstname"] = "Softchief";
                    //svc.Update(contact);
                    Console.WriteLine("Updated contact using FetchXML");


                }
            }
            else
            {
                Console.WriteLine("No Data to Update");
            }
        }
        //DELETE Using FetchXml
        private void Delete(CrmServiceClient svc)
        {
            string fetchXmlString = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
              <entity name='contact'>
                <attribute name='firstname' />
                <filter type='and'>
                  <condition attribute='firstname' operator='eq' value='Alex' />
                <condition attribute='firstname' operator='not-null' />
                </filter>
              </entity>
            </fetch>";
            EntityCollection f = ExecuteFetch(fetchXmlString, svc);
            if (f != null)
            {

                //svc.Delete("contact", f.Entities[0].Id);
            }
            else
            {
                Console.WriteLine("No Data to Delete");
            }

        }
        private EntityCollection ExecuteFetch(string fetchXmlString, CrmServiceClient svc)
        {
            return svc.RetrieveMultiple(new FetchExpression(fetchXmlString));
        }
    }
}
