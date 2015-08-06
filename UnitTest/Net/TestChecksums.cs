using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using PaymillWrapper.Exceptions;
using PaymillWrapper.Utils;
using System.Web;
using System.Text;

namespace UnitTest.Net
{
    [TestClass]
    public class TestChecksums : PaymillTest
    {
        private int amount = 9700;
        private String currency = "USD";
        private String returnUrl = "http://www.return.com";
        private String cancelUrl = "http://www.cancel.com";
        private String description = "Bebemen is cool";

        private List<ShoppingCartItem> items = null;
        private Address shippingAddress = null;
        private Address billingAddress = null;

        private Fee fee;
        private int feeAmount = 200;
        private String feePayment = "pay_3af44644dd6d25c820a8";
        private ChecksumService checksumService;

        [TestInitialize]
        public void Initialize()
        {
            base.Initialize();
            this.checksumService = _paymill.ChecksumService;

            this.items = new List<ShoppingCartItem>();
            this.items.Add(createShopingCardItem("Rambo Poster", "John J. Rambo", 2200, 3, "898-24342-343",
                "http://www.store.com/items/posters/12121-rambo"));
            this.items.Add(createShopingCardItem("Comando Poster", "John Matrix", 3100, 1, "898-24342-341",
                "http://www.store.com/items/posters/12121-comando"));

            this.billingAddress = createAddress("John Rambo", "TH", "Buriram", "Buriram", "Wat Sawai So 2", "23/4/14",
                "1527", "+66 32 12-555-23");
            this.shippingAddress = createAddress("Rocky Balboa", "US", "Pennsylvania", "Philadelphia",
                "1818 East Tusculum Street", "34/2B", "19134", "+1 215 23-555-32");

            this.fee = new Fee();
            this.fee.Amount = this.feeAmount;
            this.fee.Payment = this.feePayment;
        }


        [TestCleanup]
        public void TearDown()
        {
            this.checksumService = null;
            this.items = null;
            this.billingAddress = null;
            this.shippingAddress = null;
            this.fee = null;
        }

        [TestMethod]
        public void TestCreateWithMandatoryParameters()
        {
            Checksum checksum = _paymill.ChecksumService.CreateChecksumForPaypalAsync(this.amount, this.currency, this.returnUrl,
                 this.cancelUrl).Result;
            this.validateChecksum(checksum);
        }

        [TestMethod]
        public void TestCreateWithDescriptionParameters()
        {
            Checksum checksum = this.checksumService.CreateChecksumForPaypalWithDescriptionAsync(this.amount, this.currency,
                 this.returnUrl, this.cancelUrl, this.description).Result;
            this.validateChecksum(checksum);
            Assert.IsTrue(checksum.Data.ToLower().Contains(HttpUtility.UrlEncode(this.cancelUrl, Encoding.UTF8).ToLower()));
        }

        [TestMethod]
        public void TestCreateWithItemsParameters()
        {
            Checksum checksum = _paymill.ChecksumService.CreateChecksumForPaypalWithItemsAndAddressAsync(this.amount, this.currency,
                this.returnUrl, this.cancelUrl, this.description, this.items, null, null).Result;
            validateChecksum(checksum);
            Assert.IsTrue(checksum.Data.Contains(HttpUtility.UrlEncode(this.description, Encoding.UTF8)));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[0][name]=Rambo Poster"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[0][description]=John J. Rambo"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[0][quantity]=3"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[0][amount]=2200"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[0][item_number]=898-24342-343"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8)
                .Contains("items[0][url]=http://www.store.com/items/posters/12121-rambo"));

            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[1][name]=Comando Poster"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[1][description]=John Matrix"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[1][quantity]=1"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[1][amount]=3100"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("items[1][item_number]=898-24342-341"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8)
                .Contains("items[1][url]=http://www.store.com/items/posters/12121-comando"));
        }

        [TestMethod]
        public void TestCreateWithBillingAddressParameters()
        {
            Checksum checksum = _paymill.ChecksumService.CreateChecksumForPaypalWithItemsAndAddressAsync(this.amount, this.currency,
                this.returnUrl, this.cancelUrl, this.description, this.items, null, this.billingAddress).Result;
            this.validateChecksum(checksum);
            Assert.IsTrue(checksum.Data.Contains(HttpUtility.UrlEncode(this.description, Encoding.UTF8)));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[postal_code]=1527"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[name]=John Rambo"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[country]=TH"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[city]=Buriram"));
            Assert
                .IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[phone]=+66 32 12-555-23"));
            Assert.IsTrue(
                HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[street_address]=Wat Sawai So 2"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[state]=Buriram"));
            Assert.IsTrue(
                HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("billing_address[street_address_addition]=23/4/14"));
        }
        [TestMethod]
        public void TestCreateWithShippingAddressParameters()
        {
            Checksum checksum = _paymill.ChecksumService.CreateChecksumForPaypalWithItemsAndAddressAsync(this.amount, this.currency,
                this.returnUrl, this.cancelUrl, this.description, this.items, this.shippingAddress, null).Result;
            this.validateChecksum(checksum);
            Assert.IsTrue(checksum.Data.Contains(HttpUtility.UrlEncode(this.description, Encoding.UTF8)));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[postal_code]=19134"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[city]=Philadelphia"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[country]=US"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[name]=Rocky Balboa"));
            Assert.IsTrue(
                HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[phone]=+1 215 23-555-32"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[state]=Pennsylvania"));
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8)
                .Contains("shipping_address[street_address]=1818 East Tusculum Street"));
            Assert.IsTrue(
                HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains("shipping_address[street_address_addition]=34/2B"));
        }

        [TestMethod]
        public void TestCreateWithFee()
        {
            Checksum checksum = _paymill.ChecksumService.CreateChecksumForPaypalWithFeeAsync(this.amount, this.currency, this.returnUrl,
                this.cancelUrl, this.fee, "app_fake").Result;
            this.validateChecksum(checksum);
            Assert.IsTrue(HttpUtility.UrlDecode(checksum.Data, Encoding.UTF8).Contains(""));
        }

        private void validateChecksum(Checksum checksum)
        {
            Assert.IsNotNull(checksum);

            Assert.IsTrue(checksum.Id.StartsWith("chk_"));
            Assert.AreEqual(checksum.Type, "paypal");
            Assert.AreEqual(checksum.Value.Length, 128);
            Assert.IsNull(checksum.AppId);
            Assert.IsNotNull(checksum.CreatedAt);
            Assert.IsNotNull(checksum.UpdatedAt);

            Assert.IsTrue(checksum.Data.Contains(this.amount.ToString()));
            Assert.IsTrue(checksum.Data.Contains(this.currency));
            Assert.IsTrue(checksum.Data.ToLower().Contains(HttpUtility.UrlEncode(this.cancelUrl, Encoding.UTF8).ToLower()));
            Assert.IsTrue(checksum.Data.ToLower().Contains(HttpUtility.UrlEncode(this.returnUrl, Encoding.UTF8).ToLower()));
        }

        private ShoppingCartItem createShopingCardItem(String name, String description, int amount, int quantity,
            String itemNumber, String url)
        {
            ShoppingCartItem item = new ShoppingCartItem();

            item.Name = name;
            item.Description = description;
            item.Amount = amount;
            item.Quantity = quantity;
            item.ItemNumber = itemNumber;
            item.Url = url;

            return item;
        }

        private Address createAddress(String name, String country, String state, String city, String streetAddress,
            String streetAddressAddition, String postalCode, String phone)
        {
            Address address = new Address();

            address.Name = name;
            address.Country = country;
            address.State = state;
            address.City = city;
            address.StreetAddress = streetAddress;
            address.StreetAddressAddition = streetAddressAddition;
            address.PostalCode = postalCode;
            address.Phone = phone;

            return address;
        }
    }
}
