using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {


                #region oAUTH
                OAuthConnect oAuth = new OAuthConnect();
                CrmServiceClient svc = oAuth.ConnectWithOAuth();
                LinqWay linqWay = new LinqWay();
                FetchXMLWay fetchXMLWay = new FetchXMLWay();
                QueryExpressionWay queryExpressionWay = new QueryExpressionWay();
                if (svc != null)
                {
                    queryExpressionWay.Run(svc);

                    linqWay.Run(svc);

                    fetchXMLWay.Run(svc);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion

        }
    }
}
