using PaymillWrapper;
using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Application.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Submit(Models.PaymillForm form)
        {
            PaymillContext paymill = new PaymillContext("YOUR Private Key");
            string token = Request.Form["hToken"];
            try
            {
                Transaction transaction = paymill.TransactionService.CreateWithTokenAsync(token, form.Amount, form.Currency).Result;
                Transaction.TransactionStatus status = transaction.Status;
                // You can check the transaction status like this : 
                if (status == Transaction.TransactionStatus.PENDING)
                {
                    return Content("Payment PENDING");
                }
            }
            catch(AggregateException  ex){
                // or returns Error from server
                return Content(ex.InnerException.Message);
            }
            return Content(""); 
        } 
    }
}
