using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models.Order
{
    public enum Status
    {
        Pending,
        PaymentReceived,
        PaymentFaild
    }
}
