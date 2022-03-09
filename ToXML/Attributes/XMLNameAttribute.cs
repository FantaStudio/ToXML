using System;

namespace ToXML.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class XMLNameAttribute : Attribute
    {
        public string Name { get; set; }

        public XMLNameAttribute(string name) { Name = name; }
    }
}
