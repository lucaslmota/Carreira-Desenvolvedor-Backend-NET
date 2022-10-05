using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymenteContext.Domain.Entity
{
    public class BoletoPayment
    {
        public string? BarCode { get; set; }
        public string? BoletoNumber { get; set; }

    }
}