using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBookerApi
{
    public class Details
    {
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
        [JsonProperty("bookingdates")]
        public BookingDate BookingDates { get; set; }
        
        
    }
    public class BookingDate
    {
        [JsonProperty("checkin")]
        public DateOnly CheckIn { get; set; }
        [JsonProperty("checkout")]
        public DateOnly CheckOut { get; set; }
    }
    public class CreateBookingRes
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public Details Booking  { get; set; }
    }
}
