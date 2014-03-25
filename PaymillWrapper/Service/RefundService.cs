using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    public class RefundService : AbstractService<Refund>
    {
        public RefundService(HttpClient client, string apiUrl)
            : base(Resource.Refunds, client, apiUrl)
        {
        }


        /// <summary>
        /// This function refunds a <see cref="Transaction"/> that has been created previously and was refunded in parts or wasn’t refunded at
        /// all. The inserted amount will be refunded to the credit card / direct debit of the original <see cref="Transaction" />. There will
        /// be some fees for the merchant for every refund. 
        /// </summary>
        /// <param name="transaction">Transaction which will be refunded.</param>
        /// <param name="amount">Amount (in cents) which will be charged</param>
        /// <returns>A <see cref="Refund" /> for the given <see cref="Transaction" /></returns>
        public async Task<Refund> RefundTransactionAsync(Transaction transaction, int amount)
        {
            return await RefundTransactionAsync(transaction, amount, null);
        }

        /// <summary>
        /// This function refunds a <see cref="Transaction" /> that has been created previously and was refunded in parts or wasn’t refunded at
        /// all. The inserted amount will be refunded to the credit card / direct debit of the original <see cref="Transaction" />. There will
        /// be some fees for the merchant for every refund.
        /// Note:
        /// <ul>
        /// <li>You can refund parts of a transaction until the transaction amount is fully refunded. But be careful there will be a fee
        /// for every refund</li>
        /// <li>There is no need to define a currency for refunds, because they will be in the same currency as the original transaction</li>
        ///  </ul>
        /// </summary>
        /// <param name="transactionId">Id of <see cref="Transaction" />, which will be refunded.</param>
        /// <param name="amount">Amount (in cents) which will be charged.</param>
        /// <returns>A <see cref="Refund" /> for the given cref="Transaction" />.</returns>
        public async Task<Refund> RefundTransactionAsync(String transactionId, int amount)
        {
            return await RefundTransactionAsync(new Transaction(transactionId), amount, null);
        }

        /// <summary>
        ///  This function refunds a <see cref="Transaction" /> that has been created previously and was refunded in parts or wasn’t refunded at
        ///   all. The inserted amount will be refunded to the credit card / direct debit of the original <see cref="Transaction" />. There will
        ///  be some fees for the merchant for every refund. <br />
        ///  <br />
        ///  Note:
        ///  <ul>
        ///  <li>You can refund parts of a transaction until the transaction amount is fully refunded. But be careful there will be a fee
        ///  for every refund</li>
        ///  <li>There is no need to define a currency for refunds, because they will be in the same currency as the original transaction</li>
        ///  </ul>
        /// </summary>
        /// <param name="transactionId">Id of <see cref="Transaction" />, which will be refunded.</param>
        /// <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="description">Additional description for this refund.</param>
        /// <returns>A <see cref="Refund" /> for the given <see cref="Transaction" />.</returns>
        public async Task<Refund> RefundTransactionAsync(String transactionId, int amount, String description)
        {
            return await RefundTransactionAsync(new Transaction(transactionId), amount, description);
        }

        /// <summary>
        /// This function refunds a <see cref="Transaction"/> that has been created previously and was refunded in parts or wasn’t refunded at
        /// all. The inserted amount will be refunded to the credit card / direct debit of the original <see cref="Transaction" />. There will
        /// be some fees for the merchant for every refund. <br />
        /// <br />
        /// Note:
        /// <ul>
        /// <li>You can refund parts of a transaction until the transaction amount is fully refunded. But be careful there will be a fee
        /// for every refund</li>
        /// <li>There is no need to define a currency for refunds, because they will be in the same currency as the original transaction</li>
        /// </ul>
        /// </summary>
        /// <param name="transaction">The <see cref="Transaction" />, which will be refunded.</param>
        /// <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="description">Additional description for this refund.</param>
        /// <returns>A <see cref="Refund" /> for the given <see cref="Transaction" />.</returns>
        public async Task<Refund> RefundTransactionAsync(Transaction transaction, int amount, String description)
        {
            ValidationUtils.ValidatesAmount(amount);

            return await createAsync(transaction.Id,
                new UrlEncoder().EncodeObject(new
                {
                    Amount = amount,
                    Description = description
                }
                ));
        }
        public override async Task<Refund> UpdateAsync(Refund obj)
        {
            return await Task<Refund>.Factory.StartNew(() =>
            {
                throw new PaymillWrapper.Exceptions.PaymillException("Now Supported");
            });
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL Client objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL Client object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Refund>> ListAsync(Refund.Filter filter, Refund.Order order)
        {
            return await base.listAsync(filter, order, null, null);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/> of PAYMILL objects. In which order this list is returned depends on the
        /// optional parameters. If null is given, no filter or order will be applied, overriding the default count and
        /// offset.
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <param name="count">Max count of returned objects in the PaymillList</param>
        /// <param name="offset">The offset to start from.</param>
        /// <returns>PaymillList which contains a List of PAYMILL objects and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Refund>> ListAsync(Refund.Filter filter, Refund.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }

        protected override string GetResourceId(Refund obj)
        {
            return obj.Id;
        }
    }
}