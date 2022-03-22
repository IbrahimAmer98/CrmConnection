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
    public class QueryExpressionWay
    {
        public void Run(CrmServiceClient svc)
        {
            Retrive(svc);
            RetriveEarlyBound(svc);
            Update(svc);
            Delete(svc);
        }
        //Retrieve Multiple Record using QueryExpression
        private void Retrive(CrmServiceClient svc)
        {
            QueryExpression qe = new QueryExpression("contact");
            qe.ColumnSet = new ColumnSet("firstname", "lastname");
            EntityCollection ec = svc.RetrieveMultiple(qe);
            if (ec != null)
            {

                for (int i = 0; i < ec.Entities.Count; i++)
                {
                    if (ec.Entities[i].Attributes.ContainsKey("firstname"))
                    {
                        Console.WriteLine(ec.Entities[i].Attributes["firstname"]);
                    }
                }
                Console.WriteLine("Retrieved all Contacts using QueryExpression ...");
            }
            else
            {
                Console.WriteLine("No Data");
            }
        }

        private void RetriveEarlyBound(CrmServiceClient svc)
        {
            QueryExpression qe = new QueryExpression();
            qe.EntityName = Contact.EntityLogicalName;
            qe.ColumnSet = new ColumnSet("firstname", "lastname");
            EntityCollection ec = svc.RetrieveMultiple(qe);
            List<Contact> contacts = ec.Entities.Select(e => e.ToEntity<Contact>()).ToList();


            if (contacts != null)
            {
                foreach (Contact contact in contacts)
                {
                    if (contact.FirstName != "")
                    {
                        Console.WriteLine(contact.FirstName);
                    }
                }
                Console.WriteLine("Retrieved all Contacts using QueryExpression Early Bound...");
            }
            else
            {
                Console.WriteLine("No Data");
            }
        }

        //UPDATE Using QueryExpression
        private void Update(CrmServiceClient svc)
        {
            ConditionExpression condition1 = new ConditionExpression();
            condition1.AttributeName = "firstname";
            condition1.Operator = ConditionOperator.Equal;
            condition1.Values.Add("Softchief");
            FilterExpression filter1 = new FilterExpression();
            filter1.Conditions.Add(condition1);
            QueryExpression query2 = new QueryExpression("contact");
            query2.ColumnSet.AddColumns("firstname", "lastname");
            query2.Criteria.AddFilter(filter1);

            EntityCollection result1 = svc.RetrieveMultiple(query2);
            if (result1 != null)
            {


                foreach (var c in result1.Entities)
                {
                    Console.WriteLine("Found contact: " + c.Attributes["firstname"]);


                    Entity contact = new Entity("contact");
                    contact = svc.Retrieve(contact.LogicalName, c.Id, new ColumnSet(true));
                    contact["firstname"] = "baha";
                    //svc.Update(contact);
                    Console.WriteLine("Updated contact");
                }
            }
            else
            {
                Console.WriteLine("No Data to Update");
            }
        }
        //DELETE usnig  QueryExpression
        private void Delete(CrmServiceClient svc)
        {
            ConditionExpression condition1 = new ConditionExpression();
            condition1.AttributeName = "firstname";
            condition1.Operator = ConditionOperator.Equal;
            condition1.Values.Add("Alex");
            FilterExpression filter1 = new FilterExpression();
            filter1.Conditions.Add(condition1);
            QueryExpression query2 = new QueryExpression("contact");
            query2.ColumnSet.AddColumns("firstname", "lastname");
            query2.Criteria.AddFilter(filter1);

            EntityCollection result1 = svc.RetrieveMultiple(query2);

            if (result1 != null)
            {
                //svc.Delete("contact", result1.Entities[0].Id);
            }
            else
            {
                Console.WriteLine("No Data to Delete");
            }
        }

    }
}
