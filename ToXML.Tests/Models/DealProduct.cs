using System;

namespace ToXML.Tests.Models
{
    class DealProduct
    {
        public Guid DealProductId { get; set; }
        public Deal Deal { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
