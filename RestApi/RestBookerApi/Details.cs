using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBookerApi
{
    internal class Details
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("totalprice")]
        public int TotalPrice { get; set; }
        [JsonProperty("depositpaid")]
        public string DepositPaid { get; set; }
        [JsonProperty("additionalneeds")]
        public string? AdditionalDetails { get; set; }
        
        [JsonProperty("checkin")]
        public string? CheckIn { get; set; }
        [JsonProperty("checkout")]
        public string? CheckOut { get; set; }
    }
}
