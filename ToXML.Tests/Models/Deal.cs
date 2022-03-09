using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToXML.Tests.Models
{
    [DebuggerDisplay("Deal Id - {DealId};\n Date - {DealDate};\nDiscount - {Discount};\nCount - {Count};\nCost - {Cost}")]
    class Deal
    {
        public Guid DealId { get; set; }

        public DateTime DealDate { get; set; }
        public virtual List<DealProduct> DealProducts { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
    }
}
