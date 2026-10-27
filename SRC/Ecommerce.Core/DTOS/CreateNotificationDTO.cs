
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.DTOS
{
    public class CreateNotificationDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        public string? Type { get; set; }
    }
}
