namespace Restival.ApiTests.Nancy {
    public class NancyApiTests : ApiTestsBase {
        protected override string BaseUri {
            get { return ("http://restival.local/api.nancy"); }
        }
    }
}