namespace Restival.ApiTests.OpenRasta {
    public class OpenRastaApiTests : ApiTestsBase {
        protected override string BaseUri {
            get { return ("http://restival.local/api.openrasta"); }
        }
    }
}