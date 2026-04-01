using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Sharing
{
    public class ProductParameters
    {
        public string? sort {  get; set; }
        public int? categoryID {  get; set; }
        public int MaxPageSize { get; set; } = 6;
        public string? Search {  get; set; }
        private int _PageSize=3;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value>MaxPageSize?MaxPageSize:value; }
        }
        public int PageNumber { get; set; } = 1;
    }
}
