using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserWallet.Models
{
    public class RequestModel
    {
        [Required]
        public int UserID { get; set; }

        public float Amount { get; set; }

        [StringLength(3)]
        public string FromCurrency { get; set; }
        [StringLength(3)]
        public string ToCurrency { get; set; }
    }
}
