using Parbad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.PurchaseRequest
{
    public class PurchaseRequestResultDTO
    {
        public bool HasPayment { get; set; }
        public IPaymentRequestResult Payment { get; set; }
        public int RequestId { get; set; }
    }
}
