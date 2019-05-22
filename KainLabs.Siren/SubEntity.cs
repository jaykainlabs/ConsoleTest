using Newtonsoft.Json;

namespace KainLabs.Siren
{
    [JsonConverter(typeof(SubEntityJsonConverter))]
    public abstract class SubEntity
    {   
    }
}
