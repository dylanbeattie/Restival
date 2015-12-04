using System.IO;
using System.Text;
using Newtonsoft.Json;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web;

namespace Restival.Api.OpenRasta {
    [MediaType("application/json")]
    public class JsonCodec : IMediaTypeReader, IMediaTypeWriter {
        public object Configuration { get; set; }

        public object ReadFrom(IHttpEntity request, IType destinationType, string destinationName) {
            using (var reader = new StreamReader(request.Stream, Encoding.UTF8)) {
                return (JsonConvert.DeserializeObject(reader.ReadToEnd()));
            }
        }

        public void WriteTo(object entity, IHttpEntity response, string[] codecParameters) {
            using (TextWriter w = new StreamWriter(response.Stream)) {
                w.Write(JsonConvert.SerializeObject(entity));
            }
        }
    }
}