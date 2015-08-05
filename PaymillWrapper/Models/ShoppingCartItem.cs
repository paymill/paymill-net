using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PaymillWrapper.Utils;

namespace PaymillWrapper.Models
{
    /// <summary>
    ///A shopping cart item object belongs to exactly one transaction. It represents the merchants item which will be given to paypal.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<ShoppingCartItem>))]
    public class ShoppingCartItem 
    {
        /// <summary>
        /// Item name, max. 127 characters
        /// </summary>
        public String Name;

        /// <summary>
        /// Additional description, max. 127 characters
        /// </summary>
        public String Description;

        /// <summary>
        /// Price > 0 for a single item, including tax, can also be negative to act as a discount
        /// </summary>
        public int Amount;

        /// <summary>
        /// Quantity of this item
        /// </summary>
        public int Quantity;

        /// <summary>
        /// Item number or other identifier (SKU/EAN), max. 127 characters
        /// </summary>
        [DataMember(Name = "item_number")]
        public String ItemNumber;

        /// <summary>
        /// URL of the item in your store, max. 2000 characters.
        /// </summary>
        public String Url;

 
    }


}