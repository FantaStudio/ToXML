using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ToXML.Attributes;
using ToXML.Tests.Models;
using Xunit;

namespace ToXML.Tests
{
    public class AttributesTest
    {
        [Fact]
        public void NameAttributeTest()
        {
            Product testProduct = new Product() { ProductName = "test title" };
            XElement testProductXML = testProduct.ToXML();
            
            Assert.NotNull(testProductXML);
            Assert.Equal("Product", testProductXML.Name);
            Assert.True(testProductXML.Elements().Where(x => x.Name == "ProductTitle").Any());
        }

        [Fact]
        public void ParsableAttributeTest()
        {
            Product testProduct = new Product() { ProductName = "test title", Description = "it is description" };
            XElement testProductXML = testProduct.ToXML();

            Assert.NotNull(testProductXML);
            Assert.False(testProductXML.Elements().Where(x => x.Name == "Description").Any());
        }
    }
}
