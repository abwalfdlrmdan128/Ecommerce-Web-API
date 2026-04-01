using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models.Product
{
    public class Photo:BaseEntity<int>
    {
        public string ImageName {  get; set; }

        public int ProductID { get; set; }

       // [JsonIgnore]
        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; }

    }
}
