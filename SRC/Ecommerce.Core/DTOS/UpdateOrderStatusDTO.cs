using Ecommerce.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOS
{

    public class UpdateOrderStatusDTO
    {
        public Status Status { get; set; }
    }

}