using PaymillWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace UnitTest.Net
{
    public class PaymillTest
    {
        protected PaymillContext _paymill = null;
        public String testToken = "098f6bcd4621d373cade4e832627b4f6";
        public String clientEmail = "john.rambo@qaiware.com";
        public String clientDescription = "Boom, boom, shake the room";

        public virtual void Initialize()
        {

            _paymill = new PaymillContext("9a4129b37640ea5f62357922975842a1");
            String ApiKey = "941569045353c8ac2a5689deb88871bb";
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var authInfo = ApiKey + ":";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

            String content = @"";

            HttpResponseMessage response = _httpClient.GetAsync(@"https://test-token.paymill.com/?transaction.mode=CONNECTOR_TEST&channel.id=941569045353c8ac2a5689deb88871bb&jsonPFunction=paymilljstests&account.number=4111111111111111&account.expiry.month=12&account.expiry.year=2015&account.verification=123&account.holder=Max%20Mustermann&presentation.amount3D=2800&presentation.currency3D=EUR").Result;

            String pattern = "(tok_)[a-z|0-9]+";

            content = response.Content.ReadAsStringAsync().Result;
            if (Regex.Matches(content, pattern).Count > 0)
            {
                this.testToken = Regex.Matches(content, pattern)[0].Value;
            }
        }
    }
}
