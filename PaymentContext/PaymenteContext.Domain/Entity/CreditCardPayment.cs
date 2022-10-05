using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymenteContext.Domain.Entity
{
    public class CreditCardPayment
    {
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public string? LastTransactionNumber { get; set; }
    }
}