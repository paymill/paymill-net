using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// Created a {@link Checksum} of {@link Checksum.Type} with amount in the given currency.
        /// </summary>
        /// <param name="amount">
        ///          Amount (in cents) which will be charged.</param>
        /// <param name="currency">
        ///          ISO 4217 formatted currency code.</param>
        /// <param name="returnUrl">
        ///          URL to redirect customers to after checkout has completed.</param>
        /// <param name="cancelUrl">
        ///          URL to redirect customers to after they have canceled the checkout. As a result, there will be no transaction.</param>
        /// <returns>
        /// Checksum object.
        /// </returns>
        public async Task<Checksum> CreateChecksumForPaypalAsync(int amount, String currency, String returnUrl, String cancelUrl)
        {
            return await CreateChecksumForPaypalWithDescriptionAsync(amount, currency, returnUrl, cancelUrl, null);
        }

        /// <summary>
        /// Created a {@link Checksum} of {@link Checksum.Type} with amount in the given currency.
        /// </summary>
        /// <param name="amount">
        ///          Amount (in cents) which will be charged.</param>
        /// <param name="currency">
        ///          ISO 4217 formatted currency code.</param>
        /// <param name="returnUrl">
        ///          URL to redirect customers to after checkout has completed.</param>
        /// <param name="cancelUrl">
        ///          URL to redirect customers to after they have canceled the checkout. As a result, there will be no transaction.</param>
        /// <param name="fee">
        ///          A {@link Fee}.</param>
        /// <param name="appId">
        ///          App (ID) that created this refund or null if created by yourself.</param>
        /// <returns>
        /// Checksum object.
        /// </returns>
        public async Task<Checksum> CreateChecksumForPaypalWithFeeAsync(int amount, String currency, String returnUrl, String cancelUrl,
            Fee fee, String appId)
        {
            return await CreateChecksumForPaypalWithFeeAndDescriptionAsync(amount, currency, returnUrl, cancelUrl, fee, null, appId);
        }

        /// <summary>
        /// Created a {@link Checksum} of {@link Checksum.Type} with amount in the given currency.
        /// </summary>
        /// <param name="amount">
        ///          Amount (in cents) which will be charged</param>.
        /// <param name="currency">
        ///          ISO 4217 formatted currency code.</param>
        /// <param name="returnUrl">
        ///          URL to redirect customers to after checkout has completed.</param>
        /// <param name="cancelUrl">
        ///          URL to redirect customers to after they have canceled the checkout. As a result, there will be no transaction.</param>
        /// <param name="description">
        ///          A short description for the transaction.</param>
        /// <returns>
        /// Checksum object.
        /// </returns>
        public async Task<Checksum> CreateChecksumForPaypalWithDescriptionAsync(int amount, String currency, String returnUrl,
            String cancelUrl, String description)
        {
            return await CreateChecksumForPaypalWithItemsAndAddressAsync(amount, currency, returnUrl, cancelUrl, description, null,
                null, null);
        }

        /// <summary>
        /// Created a {@link Checksum} of {@link Checksum.Type} with amount in the given currency.
        /// </summary>
        /// <param name="amount">
        ///          Amount (in cents) which will be charged.</param>
        /// <param name="currency">
        ///          ISO 4217 formatted currency code.</param>
        /// <param name="returnUrl">
        ///          URL to redirect customers to after checkout has completed.</param>
        /// <param name="cancelUrl">
        ///          URL to redirect customers to after they have canceled the checkout. As a result, there will be no transaction.</param>
        /// <param name="fee">
        ///          A {@link Fee}.</param>
        /// <param name="description">
        ///          A short description for the transaction.</param>
        /// <param name="appId">
        ///          App (ID) that created this refund or null if created by yourself.</param>
        /// <returns>
        /// Checksum object.
        /// </returns>
        public async Task<Checksum> CreateChecksumForPaypalWithFeeAndDescriptionAsync(int amount, String currency, String returnUrl,
            String cancelUrl, Fee fee, String description, String appId)
        {
            return await CreateChecksumForPaypalWithFeeAndItemsAndAddressAsync(amount, currency, returnUrl, cancelUrl, fee,
                description, null, null, null, appId);
        }

        /// <summary>
        /// Created a {@link Checksum} of {@link Checksum.Type} with amount in the given currency.
        /// </summary>
        /// <param name="amount">
        ///          Amount (in cents) which will be charged.</param>
        /// <param name="currency">
        ///          ISO 4217 formatted currency code.</param>
        /// <param name="returnUrl">
        ///          URL to redirect customers to after checkout has completed.</param>
        /// <param name="cancelUrl">
        ///          URL to redirect customers to after they have canceled the checkout. As a result, there will be no transaction.</param>
        /// <param name="description">
        ///          A short description for the transaction or <code>null</code>.</param>
        /// <param name="items">
        ///          {@link List} of {@link ShoppingCartItem}s</param>
        /// <param name="billing
        ///          Billing {@link Address} for this transaction.
        /// <param name="shipping">
        ///          Shipping {@link Address} for this transaction.
        /// <returns>
        /// Checksum object.
        /// </returns>
        public async Task<Checksum> CreateChecksumForPaypalWithItemsAndAddressAsync(int amount, String currency, String returnUrl,
            String cancelUrl, String description, List<ShoppingCartItem> items, Address shipping, Address billing)
        {
            return await CreateChecksumForPaypalWithFeeAndItemsAndAddressAsync(amount, currency, returnUrl, cancelUrl, null,
                description, items, shipping, billing, null);
        }

        /// <summary>
        /// Created a {@link Checksum} of {@link Checksum.Type} with amount in the given currency.
        /// </summary> 
        /// <param name="amount">
        ///          Amount (in cents) which will be charged.</param>
        /// <param name="currency">
        ///          ISO 4217 formatted currency code.</param>
        /// <param name="returnUrl">
        ///          URL to redirect customers to after checkout has completed.</param>
        /// <param name="cancelUrl">
        ///          URL to redirect customers to after they have canceled the checkout. As a result, there will be no transaction.</param>
        /// <param name="fee">
        ///          A {@link Fee}.
        /// <param name="description">
        ///          A short description for the transaction or <code>null</code>.</param>
        /// <param name="items">
        ///          {@link List} of {@link ShoppingCartItem}s</param>
        /// <param name="billing">
        ///          Billing {@link Address} for this transaction.</param>
        /// <param name="shipping">
        ///          Shipping {@link Address} for this transaction.</param>
        /// <param name="appId">
        ///          App (ID) that created this refund or null if created by yourself.</param>
        /// <returns>
        /// Checksum object.
        /// </returns>
        private async Task<Checksum> CreateChecksumForPaypalWithFeeAndItemsAndAddressAsync(int amount, String currency, String returnUrl,
            String cancelUrl, Fee fee, String description, List<ShoppingCartItem> items, Address shipping, Address billing, String appId)
        {
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);
            ValidationUtils.ValidatesUrl(cancelUrl);
            ValidationUtils.ValidatesUrl(returnUrl);
            ValidationUtils.ValidatesFee(fee);

            ParameterMap<String, String> paramsMap = new ParameterMap<String, String>();

            paramsMap.Add("checksum_type", "paypal");
            paramsMap.Add("amount", amount.ToString());
            paramsMap.Add("currency", currency);
            paramsMap.Add("cancel_url", cancelUrl);
            paramsMap.Add("return_url", returnUrl);

            if (String.IsNullOrWhiteSpace(description) == false)
            {
                paramsMap.Add("description", description);
            }
            if (fee != null && fee.Amount != null)
            {
                paramsMap.Add("fee_amount", fee.Amount.ToString());
            }

            if (fee != null && String.IsNullOrWhiteSpace(fee.Payment) == false)
            {
                paramsMap.Add("fee_payment", fee.Payment);
            }
            if (fee != null && String.IsNullOrWhiteSpace(fee.Currency) == false)
            {
                paramsMap.Add("fee_currency", fee.Currency);
            }
            if (String.IsNullOrWhiteSpace(appId) == false)
            {
                paramsMap.Add("app_id", appId);
            }

            this.parametrizeItems(items, paramsMap);
            this.parametrizeAddress(billing, paramsMap, "billing_address");
            this.parametrizeAddress(shipping, paramsMap, "shipping_address");

            return await createAsync(null,
                new UrlEncoder().EncodeParamsMap<string, string>(paramsMap));
        }

        private void parametrizeItems(List<ShoppingCartItem> items, ParameterMap<String, String> paramsMap)
        {
            if (items != null)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (String.IsNullOrWhiteSpace(items[i].Name) == false)
                    {
                        paramsMap.Add("items[" + i + "][name]", items[i].Name);
                    }
                    if (String.IsNullOrWhiteSpace(items[i].Description) == false)
                    {
                        paramsMap.Add("items[" + i + "][description]", items[i].Description);
                    }
                    if (items[i].Amount != 0)
                    {
                        paramsMap.Add("items[" + i + "][amount]", items[i].Amount.ToString());
                    }
                    if (items[i].Quantity != 0)
                    {
                        paramsMap.Add("items[" + i + "][quantity]", items[i].Quantity.ToString());
                    }
                    if (String.IsNullOrWhiteSpace(items[i].ItemNumber) == false)
                    {
                        paramsMap.Add("items[" + i + "][item_number]", items[i].ItemNumber);
                    }
                    if (String.IsNullOrWhiteSpace(items[i].Url) == false)
                    {
                        paramsMap.Add("items[" + i + "][url]", items[i].Url);
                    }
                }
            }
        }
        protected override string GetResourceId(Checksum obj)
        {
            return obj.Id;
        }
        private void parametrizeAddress(Address address, ParameterMap<String, String> paramsMap, String prefix)
        {
            if (address != null)
            {
                if (String.IsNullOrWhiteSpace(address.Name) == false)
                {
                    paramsMap.Add(prefix + "[name]", address.Name);
                }
                if (String.IsNullOrWhiteSpace(address.StreetAddress) == false)
                {
                    paramsMap.Add(prefix + "[street_address]", address.StreetAddress);
                }
                if (String.IsNullOrWhiteSpace(address.StreetAddressAddition) == false)
                {
                    paramsMap.Add(prefix + "[street_address_addition]", address.StreetAddressAddition);
                }
                if (String.IsNullOrWhiteSpace(address.City) == false)
                {
                    paramsMap.Add(prefix + "[city]", address.City);
                }
                if (String.IsNullOrWhiteSpace(address.State) == false)
                {
                    paramsMap.Add(prefix + "[state]", address.State);
                }
                if (String.IsNullOrWhiteSpace(address.PostalCode) == false)
                {
                    paramsMap.Add(prefix + "[postal_code]", address.PostalCode);
                }
                if (String.IsNullOrWhiteSpace(address.Country) == false)
                {
                    paramsMap.Add(prefix + "[country]", address.Country);
                }
                if (String.IsNullOrWhiteSpace(address.Phone) == false)
                {
                    paramsMap.Add(prefix + "[phone]", address.Phone);
                }
            }
        }

    }
}
