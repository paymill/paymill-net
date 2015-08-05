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
    /// An address object belongs to exactly one transaction and can represent either its shipping address or billing address. Note
    /// that state and postal_code are mandatory for PayPal transactions in certain countries, please consult PayPal documentation for
    /// more details.
    /// </summary>
    /// 

    [JsonConverter(typeof(StringToBaseModelConverter<Address>))]
    public class Address {
        
    /// <summary>
    /// Name of recipient, max. 128 characters
    /// </summary>
    /// 
    public String Name;

    /// <summary>
    /// Street address (incl. street number), max. 100 characters
    /// </summary>
    /// 
    [DataMember(Name = "street_address")]
    private String StreetAddress;

    /**
    * Addition to street address (e.g. building, floor, or c/o), max. 100 characters
    */
    [DataMember(Name = "street_address_addition")]
    private String StreetAddressAddition;

    /**
    * City, max. 40 characters
    */
    private String city;

    /**
    * State or province, max. 40 characters
    */
    private String state;

    /**
    * Country-specific postal code, max. 20 characters
    */
    [DataMember(Name = "postal_code")]
    private String PostalCode;

    /**
    * 2-letter country code according to ISO 3166-1 alpha-2
    */ 
    private String Country;
    /**
    * Contact phone number, max. 20 characters
    */ 
    private String Phone;




    }

}