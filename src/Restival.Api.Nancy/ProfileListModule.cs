using System.Linq;
using Nancy;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.Nancy {
    public class ProfileListModule : NancyModule {
    //    private readonly IDataStore db;


    //    public ProfileListModule(IDataStore db) {
    //        this.db = db;
    //        Get["/profiles"] = _ => {
    //            var profiles = db.ListProfiles();
    //            var result = new ProfileListResponse() { Items = profiles.Select(p => ProfileExtensions.ToProfileResponse(p)).ToList() };
    //            return (result);
    //        };
    //    }
    }
}