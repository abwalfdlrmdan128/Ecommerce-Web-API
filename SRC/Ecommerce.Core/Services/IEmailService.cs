using Ecommerce.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries.Service
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO emailDTO);
    }
}
