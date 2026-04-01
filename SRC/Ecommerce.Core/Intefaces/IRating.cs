using Ecommerce.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Intefaces
{
    public interface IRating
    {
        Task<bool> AddRatingAsync(RatingDTO ratingDTO, string email);
        Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingForProductAsync(int productId);
    }
}
