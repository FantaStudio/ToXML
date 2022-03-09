using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ToXML.Tests.Models;
using Xunit;

namespace ToXML.Tests
{
    public class ConverterTest
    {
        [Fact]
        public void ConvertToXMLTest()
        {
            Guid[] guids = {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            Deal deal1 = new Deal
            {
                DealId = guids[0],
                DealDate = DateTime.Now,
                Cost = 1000,
                Count = 2
            };

            Deal deal2 = new Deal
            {
                DealId = guids[1],
                DealDate = DateTime.Now,
                Cost = 300,
                Count = 1
            };

            Product product1 = new Product
            {
                ProductId = guids[2],
                ProductName = "Bread",
                RetailPrice = 150,
                TradePrice = 100
            };

            Product product2 = new Product
            {
                ProductId = guids[3],
                ProductName = "Toster",
                RetailPrice = 500,
                TradePrice = 400
            };

            DealProduct dealProduct1 = new DealProduct
            {
                DealProductId = guids[4],
                Count = 2,
                Product = product1,
                Deal = deal1
            };

            DealProduct dealProduct2 = new DealProduct
            {
                DealProductId = guids[5],
                Count = 1,
                Product = product2,
                Deal = deal2
            };


            XElement product1XML = product1.ToXML();
            XElement product2XML = product2.ToXML();

            XElement deal1XML = dealProduct1.ToXML();
            XElement deal2XML = dealProduct2.ToXML();

            XElement dealProduct1XML = dealProduct1.ToXML();
            XElement dealProduct2XML = dealProduct2.ToXML();

            Assert.NotNull(product1XML);
            Assert.NotNull(product2XML);

            Assert.NotNull(deal1XML);
            Assert.NotNull(deal2XML);

            Assert.NotNull(dealProduct1XML);
            Assert.NotNull(dealProduct2XML);

            Assert.NotNull(product1XML.Elements("ProductTitle").FirstOrDefault(p => p.Value == product1.ProductName));
            Assert.NotNull(product2XML.Elements("ProductTitle").FirstOrDefault(p => p.Value == product2.ProductName));

            Assert.Equal(4, product1XML.Elements().Count());
            Assert.Equal(4, product2XML.Elements().Count());

            Assert.True(dealProduct1XML.Elements("Product").Any()
                && dealProduct1XML.Elements("Deal").Any());

            Assert.True(dealProduct2XML.Elements("Product").Any()
                && dealProduct2XML.Elements("Deal").Any());
        }

        [Fact]
        public void ConvertFromXMLToObjectTest()
        {
            XElement deal = new XElement("Deal");

            Guid id = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;
            int cost = 1000;
            int count = 2;

            deal.Add(new XElement("DealId")
            {
                Value = id.ToString()
            });

            deal.Add(new XElement("DealDate")
            {
                Value = dateTime.ToString()
            });

            deal.Add(new XElement("Cost")
            {
                Value = cost.ToString()
            });

            deal.Add(new XElement("Count")
            {
                Value = count.ToString()
            });

            Deal dealFromXMl = deal.FromXML<Deal>();

            Assert.Equal(dateTime.ToString(), dealFromXMl.DealDate.ToString());
            Assert.Equal(id, dealFromXMl.DealId);
           
            Assert.Equal(cost, dealFromXMl.Cost);
            Assert.Equal(count, dealFromXMl.Count);
        }
    }
}
