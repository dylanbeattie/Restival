using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using Restival.Data;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    public abstract class WhoAmIApiTestsBase<TApi> : ApiTestBase<TApi> where TApi : IApiUnderTest, new() {
        private IRestResponse<WhoAmIResponse> GetWhoAmIResponse(string username, string password) {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("whoami");
            request.AddHeader("Accept", "application/json");
            request.Credentials = new NetworkCredential(username, password);
            Console.WriteLine(client.BuildUri(request));
            return (client.Execute<WhoAmIResponse>(request));
        }
        private WhoAmIResponse GetWhoAmI(string username = null, string password = null) {
            var response = GetWhoAmIResponse(username, password);
            return (response.Data);
        }

        private string GetWhoAmIJson(string username = null, string password = null) {
            var response = GetWhoAmIResponse(username, password);
            return (response.Content);
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
            get {
                return (FakeDataStore.Users.Select(u => new[] {
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

        //[Test]
        //[TestCaseSource("TestUsers")]
        //public void GET_WhoAmI_Includes_Self_Link(string guid, string username, string password, string name) {
        //    var me = GetWhoAmI(username, password);
        //    var self = me.Links["self"]["href"];
        //    var agencies = me.Links["agencies"]["href"];
        //    self.ShouldBe("/users/" + guid, Case.Insensitive);
        //    agencies.ShouldBe("/users/" + guid + "/agencies", Case.Insensitive);
        //}

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_WhoAmI_Includes_Exact_HAL_Json(string guid, string username, string password, string name) {
            var json = GetWhoAmIJson(username, password);
            var jo = JObject.Parse(json);
            var links = jo.SelectToken("$._links");
            links.ShouldNotBe(null);
            Console.WriteLine(json);
        }
    }
}