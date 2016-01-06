using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Restival.Api.Common.Resources;
using RestSharp;

namespace Restival.Workbench {
    internal class Program {
        private static string[] endpoints = new[] {
            "webapi",
            "servicestack",
            "openrasta",
            "nancy"
        };

        private static void Main(string[] args) {
            using (var output = new StreamWriter(@"..\..\results.txt")) {
                foreach (var ep in endpoints) {
                    var url = String.Format("http://restival.local/api.{0}", ep);
                    var client = new RestClient(url);
                    var request = new RestRequest("whoami");
                    request.AddHeader("Accept", "application/json");
                    request.Credentials = new NetworkCredential("bob", "hope");
                    Console.WriteLine(client.BuildUri(request));
                    var response = client.Execute<WhoAmIResponse>(request);

                    output.WriteLine(ep);
                    output.WriteLine(response.Content);
                    output.WriteLine(String.Empty.PadLeft(72, '='));
                }
            }
        }
    }
}
