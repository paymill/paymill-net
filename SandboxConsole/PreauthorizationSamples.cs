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
        static void getPreauthorizations()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Console.WriteLine("Waiting request list preauthorizations...");
            List<Preauthorization> lstPreauthorizations = preauthorizationService.GetPreauthorizations();

            foreach (Preauthorization preauthorization in lstPreauthorizations)
            {
                Console.WriteLine(String.Format("PreauthorizationID:{0}", preauthorization.Id));
            }

            Console.Read();
        }
        static void getPreauthorizationsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Console.WriteLine("Waiting request list preauthorizations...");

            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);

            List<Preauthorization> lstPreauthorizations = preauthorizationService.GetPreauthorizationsByFilter(filter);

            foreach (Preauthorization preauthorization in lstPreauthorizations)
            {
                Console.WriteLine(String.Format("PreauthorizationID:{0}", preauthorization.Id));
            }

            Console.Read();
        }
        static void addPreauthorization()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Preauthorization preauthorization = new Preauthorization();
            preauthorization.Amount = 3500;
            preauthorization.Currency = "EUR";
            //preauthorization.Token = "098f6bcd4621d373cade4e832627b4f6";
            preauthorization.Payment = new Payment() { Id = "pay_4c159fe95d3be503778a" };

            Preauthorization newPreauthorization = preauthorizationService.Create(preauthorization);

            Console.WriteLine("PreauthorizationID:" + newPreauthorization.Id);
            Console.Read();
        }
        static void getPreauthorization()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Console.WriteLine("Solicitando preauthorization...");
            string preauthorizationID = "preauth_96fe414f466f91ddb266";
            Preauthorization preauthorization = preauthorizationService.Get(preauthorizationID);

            Console.WriteLine("PreauthorizationID:" + preauthorization.Id);
            Console.WriteLine("Created at:" + preauthorization.Created_At.ToShortDateString());
            Console.Read();
        }

    }
}
