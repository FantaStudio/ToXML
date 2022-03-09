using System;

namespace ToXML.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class XMLParsableAttribute : Attribute
    {
        public bool Parsable { get; set; } = true;
        public XMLParsableAttribute() { }
        public XMLParsableAttribute(bool parsable)
            => Parsable = parsable;
    }
}
