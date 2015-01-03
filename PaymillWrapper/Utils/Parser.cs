using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Utils
{

    public static  class Parser
    {
        public static TE ReadValue<TE>(string data)
        {
            return JsonConvert.DeserializeObject<TE>(data, customConverters);
        }
        internal static Newtonsoft.Json.JsonConverter[] customConverters = { new UnixTimestampConverter(),
                                                                            new StringToNIntConverter()};
    }
}
