using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using Restival.Api.Common;
using Restival.Api.Common.Entities;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    [TestFixture]
    public abstract class ApiTestsBase {

        protected abstract string BaseUri { get; }

        [Test]
        public void GET_Hello_Returns_Greeting() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("hello");
            var status = client.Execute<Greeting>(request);
            status.Data.Message.ShouldBe("Hello, World!");
        }

        [Test]
        [TestCase("Alice")]
        [TestCase("Bryan")]
        [TestCase("Carol")]
        public void Get_Hello_Name_Returns_Greeting(string name) {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("hello");
            request.AddParameter("name", name);
            var status = client.Execute<Greeting>(request);
            status.Data.Message.ShouldBe(String.Format("Hello, {0}!", name));
        }
    }
}
