using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    internal class ValidationUtils
    {

        static internal void ValidatesId(String id)
        {
            if (String.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id can not be blank");
        }

        static internal void ValidatesToken(String token)
        {
            if (String.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token can not be blank");
        }

        static internal void ValidatesTrialPeriodDays(int? trialPeriodDays)
        {
            if (trialPeriodDays.HasValue && trialPeriodDays.Value < 0)
                throw new ArgumentException("Trial period days can not blank or be negative");
        }

        static internal void ValidatesAmount(int? amount)
        {
            if (amount.HasValue || amount.Value < 0)
                throw new ArgumentException("Amount can not be blank or negative");
        }

        static internal void ValidatesCurrency(String currency)
        {
            if (String.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency can not be blank");
        }

        static internal void ValidatesName(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can not be blank");
        }

        static internal void ValidatesInterval(String interval)
        {
            if (String.IsNullOrWhiteSpace(interval))
                throw new ArgumentException("Interval can not be blank");
        }

        static internal void ValidatesFee(Fee fee)
        {
            if (fee != null)
            {
                if (fee.Amount.HasValue && String.IsNullOrWhiteSpace(fee.Payment))
                    throw new ArgumentException("When fee amount is given, fee payment is mandatory");
                if (fee.Amount.HasValue && String.IsNullOrEmpty(fee.Payment) == false)
                    throw new ArgumentException("When fee payment is given, fee amount is mandatory");

                if (fee.Amount.HasValue && String.IsNullOrEmpty(fee.Payment) == false)
                {
                    if (fee.Amount < 0)
                        throw new ArgumentException("Fee amount can not be negative");
                    if (!fee.Payment.StartsWith("pay_"))
                    {
                        throw new ArgumentException("Fee payment should statrt with 'pay_' prefix");
                    }
                }
            }
        }

        static internal void ValidatesPayment(Payment payment)
        {
            if (payment == null || String.IsNullOrWhiteSpace(payment.Id))
                throw new ArgumentException("Payment or its Id can not be blank");
        }

        static internal void ValidatesOffer(Offer offer)
        {
            if (offer == null || String.IsNullOrWhiteSpace(offer.Id))
                throw new ArgumentException("Offer or its  Id can not be blank");
        }

        static internal void ValidatesClient(Client client)
        {
            if (client == null || String.IsNullOrWhiteSpace(client.Id))
                throw new ArgumentException("Client or its  Id can not be blank");
        }
    }
}
