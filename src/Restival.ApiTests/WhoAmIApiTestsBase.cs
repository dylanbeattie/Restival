using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    public abstract class WhoAmIApiTestsBase<TApi> : ApiTestBase<TApi> where TApi : IApiUnderTest, new() {
        private WhoAmIResponse GetWhoAmI(string username = null, string password = null) {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("whoami");
            request.AddHeader("Accept", "application/json");
            request.Credentials = new NetworkCredential(username, password);
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<WhoAmIResponse>(request);
            foreach (var header in status.Headers) {
                Console.WriteLine(header.Name + ": " + header.Value);
            }
            Console.WriteLine(status.Content);
            return (status.Data);
        }

        [Test]
        public void GET_WhoAmI_Without_Authentication_Returns_401() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("whoami");
            request.AddHeader("Accept", "application/json");
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<WhoAmIResponse>(request);
            status.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        public IEnumerable<string[]> TestUsers {
            get {
                yield return new[] { "639E3C81-2398-E511-B599-005056C00008", "ali", "baba", "Ali Baba" };
                yield return new[] { "649E3C81-2398-E511-B599-005056C00008", "bob", "hope", "Bob Hope" };
                //yield return new[] { "659E3C81-2398-E511-B599-005056C00008", "cat", "flap", "Catherine Flap" };
                //yield return new[] { "669E3C81-2398-E511-B599-005056C00008", "dan", "dare", "Dan Dare" };
                //yield return new[] { "679E3C81-2398-E511-B599-005056C00008", "eli", "roth", "Eli Roth" };
                //yield return new[] { "689E3C81-2398-E511-B599-005056C00008", "fox", "trot", "Fox Trot" };
            }
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_WhoAmI_Returns_User_Id(string guid, string username, string password, string name) {
            var me = GetWhoAmI(username, password);
            me.Name.ShouldBe(name);
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_WhoAmI_Includes_Username(string guid, string username, string password, string name) {
            var me = GetWhoAmI(username, password);
            me.Username.ShouldBe(username);
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_WhoAmI_Includes_Name(string guid, string username, string password, string name) {
            var me = GetWhoAmI(username, password);
            me.Name.ShouldBe(name);
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_WhoAmI_Includes_Links(string guid, string username, string password, string name) {
            var me = GetWhoAmI(username, password);
            var self = me.Links["self"]["href"];
            var agencies = me.Links["agencies"]["href"];
            self.ShouldBe("/users/" + guid, Case.Insensitive);
            agencies.ShouldBe("/users/" + guid + "/agencies", Case.Insensitive);
        }
    }
}