using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymillWrapper.Utils
{
    [DataContract]
    internal class SingleResult<T>
    {
        [DataMember(Name = "data")]
        public T Data { get; set; }

        [DataMember(Name = "mode")]
        public string Mode { get; set; }
    }

    [DataContract]
    internal class MultipleResults<T>
    {
        [DataMember(Name = "data")]
        public List<T> Data { get; set; }

        [DataMember(Name = "mode")]
        public string Mode { get; set; }

        [DataMember(Name = "data_count")]
        public int Count { get; set; }
    }
}
