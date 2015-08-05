using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{

    public class ChecksumService : AbstractService<Checksum>
    {
        public ChecksumService(HttpClient client,
            string apiUrl)
            : base(Resource.Checksums, client, apiUrl)
        {
        }
        
    }
}