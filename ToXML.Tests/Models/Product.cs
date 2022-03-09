using System;
using System.Collections.Generic;
using ToXML.Attributes;

namespace ToXML.Tests.Models
{
    [XMLName("Product")]
    class Product
    {
        public Guid ProductId { get; set; }
        [XMLName("ProductTitle")]
        public string ProductName { get; set; }
        public float TradePrice { get; set; }
        public float RetailPrice { get; set; }
        [XMLParsable(false)]
        public string Description { get; set; }
        public virtual List<DealProduct> DealProducts { get; set; }
    }
}
