using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class PagosTarjetaDto
    {
        public string token { get; set; }

        public int installments { get; set; }

        public PayerDto payer { get; set; }

        public string payment_method_id { get; set; }
        
        public decimal transaction_amount { get; set; }

    }
}

