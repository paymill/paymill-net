using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using PaymillWrapper.Service;
using System.ComponentModel;

namespace SandboxConsole
{
    public class PreauthorizationSamples
    {
        Paymill pm = new Paymill(Properties.Settings.Default.ApiKey);
        public  void GetPreauthorizations()
        {
            List<Preauthorization> lstPreauthorizations = pm.PreauthorizationService.List();

            foreach (Preauthorization preauthorization in lstPreauthorizations)
            {
                Console.WriteLine(String.Format("PreauthorizationID:{0}", preauthorization.Id));
            }
            Console.Read();
        }
        public  void GetPreauthorizationsWithParameters()
        {
 
            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);

            List<Preauthorization> lstPreauthorizations = pm.PreauthorizationService.List(filter);

            foreach (Preauthorization preauthorization in lstPreauthorizations)
            {
                Console.WriteLine(String.Format("PreauthorizationID:{0}", preauthorization.Id));
            }

            Console.Read();
        }
        public  void AddPreauthorization()
        {
            Preauthorization newPreauthorization = pm.PreauthorizationService.CreateWithPayment("pay_4c159fe95d3be503778a", 3500, "EUR");
            Console.WriteLine("PreauthorizationID:" + newPreauthorization.Id);
            Console.Read();
        }
        public  void RemovePreauthorization()
        {
            Preauthorization newPreauthorization = pm.PreauthorizationService.CreateWithToken("098f6bcd4621d373cade4e832627b4f6", 3500, "EUR");
            Console.WriteLine("PreauthorizationID:" + newPreauthorization.Id);
            pm.PreauthorizationService.Remove(newPreauthorization.Id);
            Console.Read();
        }
        public  void GetPreauthorization()
        {
            string preauthorizationID = "preauth_96fe414f466f91ddb266";
            Preauthorization preauthorization = pm.PreauthorizationService.Get(preauthorizationID);

            Console.WriteLine("PreauthorizationID:" + preauthorization.Id);
            Console.WriteLine("Created at:" + preauthorization.CreatedAt.ToShortDateString());
            Console.Read();
        }

    }
}
