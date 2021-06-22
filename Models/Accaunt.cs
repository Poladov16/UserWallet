using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserWallet.Models
{
    public class Accaunt
    {
        [Key]
        public int AccountId { get; set; }

        [JsonProperty("AccountNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountNumber { get; set; }

        [JsonProperty("Amount", NullValueHandling = NullValueHandling.Ignore)]
        public float Amount { get; set; }

        [JsonProperty("IsActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsActive { get; set; }

        [JsonProperty("CuurrencyCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CuurrencyCode { get; set; }

        [JsonProperty("CurrencyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyName { get; set; }

        [JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore)]
        public int UserId { get; set; }

        [JsonProperty("Users", NullValueHandling = NullValueHandling.Ignore)]
        public virtual IEnumerable<User> Users { get; set; }

    }
}
