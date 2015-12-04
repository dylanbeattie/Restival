using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using Restival.Data;
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
        public void GET_WhoAmI_Without_Authentication_Returns_WWWAuthenticate_Header() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("whoami");
            request.AddHeader("Accept", "application/json");
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<WhoAmIResponse>(request);
            status.Headers.ShouldContain(h => h.Name == "WWW-Authenticate");
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
            get
            {
                return(FakeDataStore.Users.Select(u => new[] {
                    u.Id.ToString(),
                    u.Username,
                    u.Password,
                    u.Name
                }));
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
    }
}