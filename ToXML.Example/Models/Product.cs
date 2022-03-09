using System;
using System.Collections.Generic;
using System.Text;

namespace ToXML.Example.Models
{
    class Product
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
