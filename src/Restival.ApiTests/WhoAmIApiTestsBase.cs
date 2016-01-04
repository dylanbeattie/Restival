﻿using Newtonsoft.Json.Linq;
using NUnit.Framework;
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
        public void GET_WhoAmI_Follow_Profiles_Link_Returns_Profiles(string guid, string username, string password, string name) {
            var link = GetLink(username, password, "profiles");
            var profiles = GetResponse<ProfilesResponse>(username, password, (string)link["href"]);
            Console.WriteLine(profiles.Content);
            var profilesFromData = FakeDataStore.Users.First(u => u.Username == username).Profiles;
            profiles.Data.Count.ShouldBe(profilesFromData.Count);
            var entities = (IList<ProfileResponse>)profiles.Data.Embedded;
            entities.Count().ShouldBe(profilesFromData.Count);
        }

        //[Test]
        //[TestCaseSource("TestUsers")]
        //public void GET_WhoAmI_Includes_HAL_Link_To_Self(string guid, string username, string password, string name) {
        //    var link = GetLink(username, password, "self");
        //    link.ShouldNotBe(null);
        //    link["href"].ShouldNotBe(null);
        //}
    }
}