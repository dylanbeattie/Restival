using NUnit.Framework;

namespace Restival.ApiTests {
    [TestFixture]
    public abstract class ApiTestBase<TApi> where TApi : IApiUnderTest, new() {
        protected TApi Api = new TApi();

        protected string BaseUri {
            get { return ("http://restival.local/" + Api.ApiUriPath); }
        }
    }
}
