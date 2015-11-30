using System;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    public abstract class WhoAmIApiTestsBase<TApi> : ApiTestBase<TApi> where TApi : IApiUnderTest, new() {
        private WhoAmIResponse GetWhoAmI() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("whoami");
            request.AddHeader("Accept", "application/json");
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<WhoAmIResponse>(request);
            foreach (var header in status.Headers) {
                Console.WriteLine(header.Name + ": " + header.Value);
            }
            Console.WriteLine(status.Content);
            Console.WriteLine(String.Join(",", Encoding.UTF8.GetBytes(status.Content).Select(b => b.ToString()).ToArray()));
            return (status.Data);
        }

        [Test]
        public void GET_WhoAmI_Returns_User_Id() {
            var me = GetWhoAmI();
            me.Id.ShouldBe(12345);
        }

        [Test]
        public void GET_WhoAmI_Includes_Username() {
            var me = GetWhoAmI();
            me.Username.ShouldBe("username");
        }

        [Test]
        public void GET_WhoAmI_Includes_Name() {
            var me = GetWhoAmI();
            me.Name.ShouldBe("Test User");
        }

        [Test]
        public void GET_WhoAmI_Includes_Links() {
            var me = GetWhoAmI();
            var self = me.Links["self"]["href"] as String;
            var agencies = me.Links["agencies"]["href"] as String;
            self.ShouldBe("/users/12345");
            agencies.ShouldBe("/users/12345/agencies");
        }
    }
}