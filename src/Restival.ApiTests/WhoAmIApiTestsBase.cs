using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Data;
using RestSharp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Restival.ApiTests {
    public abstract class WhoAmIApiTestsBase<TApi> : ApiTestBase<TApi> where TApi : IApiUnderTest, new() {

        private IRestResponse<T> GetResponse<T>(string username, string password, string resource) where T : new() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest(resource ?? "whoami");
            request.AddHeader("Accept", "application/json");
            request.Credentials = new NetworkCredential(username, password);
            Console.WriteLine(client.BuildUri(request));
            return (client.Execute<T>(request));
        }

        private IRestResponse<WhoAmIResponse> GetWhoAmIResponse(string username, string password) {
            return (GetResponse<WhoAmIResponse>(username, password, "whoami"));
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
                    u.Name,
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
        public void GET_WhoAmI_Includes_HAL_Links(string guid, string username, string password, string name) {
            var json = GetWhoAmIJson(username, password);
            Console.WriteLine(json);
            var jo = JObject.Parse(json);
            var links = jo.SelectToken("$._links");
            links.ShouldNotBe(null);
        }

        private JToken GetLink(string username, string password, string linkName) {
            var json = GetWhoAmIJson(username, password);
            Console.WriteLine(json);
            var jo = JObject.Parse(json);
            var path = String.Format("$._links.{0}", linkName);
            return (jo.SelectToken(path));
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_WhoAmI_Includes_HAL_Link_To_Profiles(string guid, string username, string password, string name) {
            var link = GetLink(username, password, "profiles");
            link.ShouldNotBe(null);
            link["href"].ShouldNotBe(null);
            link["href"].ShouldBe(String.Format("/users/{0}/profiles", guid));
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_Linked_Profiles_Is_Paginated(string guid, string username, string password, string name) {
            var profilesFromData = FakeDataStore.Users.First(u => u.Username == username).Profiles;
            var resource = RetrieveLinkedProfiles(username, password);
            resource["_embedded"]["profiles"].Count().ShouldBe(profilesFromData.Count);
            resource["count"].ShouldBe(profilesFromData.Count);
            resource["total"].ShouldBe(profilesFromData.Count);
            resource["index"].ShouldBe(0);
        }

        [Test]
        [TestCaseSource("TestUsers")]
        public void GET_Linked_Profiles_Is_Linked(string guid, string username, string password, string name) {
            var resource = RetrieveLinkedProfiles(username, password);
            var profiles = resource["_embedded"]["profiles"];
            for (var i = 0; i < profiles.Count(); i++) {
                var profileResource = resource["_embedded"]["profiles"][i];
                profileResource.ShouldNotBe(null);
                var profileName = username + i.ToString("0000");
                profileResource["name"].ShouldBe(profileName);
                profileResource["_links"]["self"]["href"].ShouldBe(String.Format("/users/{0}/profiles/{1}", guid, profileName));
            }
        }

        [Test]
        public void GET_Profiles_Returns_Unauthorized_For_Wrong_User() {
            var ali = FakeDataStore.Users[0];
            var bob = FakeDataStore.Users[1];
            var profiles = GetResponse<ProfilesResponse>(ali.Username, ali.Password, String.Format("/users/{0}/profiles", bob.Id));
            profiles.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Test]
        public void GET_Profiles_Returns_Unauthorized_With_Custom_StatusDescription() {
            var ali = FakeDataStore.Users[0];
            var bob = FakeDataStore.Users[1];
            var profiles = GetResponse<ProfilesResponse>(ali.Username, ali.Password, String.Format("/users/{0}/profiles", bob.Id));
            profiles.StatusDescription.ShouldBe(Messages.YouDoNotHavePermissionToViewThoseProfiles);
        }

        private JObject RetrieveLinkedProfiles(string username, string password) {
            var link = GetLink(username, password, "profiles");
            var profiles = GetResponse<ProfilesResponse>(username, password, (string)link["href"]);
            Console.WriteLine(String.Empty.PadRight(72, '-'));
            Console.WriteLine(profiles.Content);
            Console.WriteLine(String.Empty.PadRight(72, '-'));
            var resource = JObject.Parse(profiles.Content);
            return resource;
        }
    }
}