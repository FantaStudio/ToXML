using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using ToXML.Attributes;


namespace ToXML
{

    public static class XMLConverter
    {
        private static string GetXMLName(MemberInfo info)
        {
            return CustomAttributeExtensions
                .GetCustomAttribute<XMLNameAttribute>(info)?
                .Name;
        }

        private static bool IsParsable(MemberInfo info)
        {
            var parsableAttribute = CustomAttributeExtensions.GetCustomAttribute<XMLParsableAttribute>(info);
            return parsableAttribute == null || parsableAttribute.Parsable;
        }

        /// <summary>
        /// Converts object to XML object
        /// properties represented as XElement, not XAttribute
        /// </summary>
        /// <param name="objectToParse"></param>
        /// <returns>XElement from passed object, null if convert is unsuccessfull </returns>
        public static XElement ObjectToXML(object objectToParse)
        {
            Type currentType = objectToParse.GetType();

            if (!IsParsable(currentType))
            {
                return null;
            }

            string parsedElementName = GetXMLName(currentType);
            XElement parsedElement = new XElement(parsedElementName ?? currentType.Name);

            PropertyInfo[] properties = currentType.GetProperties();
            if (properties.Length < 1)
            {
                return parsedElement;
            }

            foreach (PropertyInfo property in properties)
            {
                if(!IsParsable(property))
                {
                    continue;
                }

                object propValue = property.GetValue(objectToParse);

                if (propValue == null)
                {
                    continue;
                }

                // Создаём свойство как XML Element
                string xmlPropName = GetXMLName(property);
                XElement xmlProp = new XElement(xmlPropName ?? property.Name);

                // Если свойство - строка
                if(property.PropertyType == typeof(string))
                {
                    xmlProp.Value = propValue.ToString();
                    parsedElement.Add(xmlProp);
                    continue;
                }

                //Если свойство это перечисление
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    IEnumerable<object> childList = ((IEnumerable)propValue).Cast<object>();
                    // Парсим это перечисление
                    foreach (object childObect in childList)
                    {
                        XElement childXml = ObjectToXML(childObect);
                        if(childXml != null)
                        {
                            xmlProp.Add(childXml);
                        }
                    }
                }
                else
                {
                    //Если тип свойства - класс
                    if (propValue.GetType().IsClass)
                    {
                        XElement childXml = ObjectToXML(propValue);
                        if (childXml != null)
                        {
                            xmlProp.Add(childXml);
                        }
                    }
                    // Просто записываем свойство как строку
                    else
                    {
                        xmlProp.Value = propValue.ToString();
                    }
                }

                // Добавляем свойство в основной элемент
                parsedElement.Add(xmlProp);
            }
            return parsedElement;
        }

        /// <summary>
        /// Converts XML Object to specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToParse"></param>
        /// <returns></returns>
        public static T XMLToObject<T>(XElement objectToParse)
        {
            var curType = typeof(T);

            // Создаём экземпляр класса, в который нужно конвертировать
            T parsedElement = Activator.CreateInstance<T>();

            var properties = curType.GetProperties();
            if (properties.Length < 1)
            {
                return parsedElement;
            }

            foreach (var property in properties)
            {
                // Получаем свойство как XML Element
                var propValue = objectToParse.Element(property.Name);
                if (propValue == null || propValue.Value == null)
                {
                    continue;
                }

                if(typeof(IConvertible).IsAssignableFrom(property.PropertyType))
                {
                    property.SetValue(parsedElement,
                        Convert.ChangeType(propValue.Value, property.PropertyType, CultureInfo.CurrentCulture), 
                        null);
                }
                else if(property.PropertyType == typeof(Guid))
                {
                    if(Guid.TryParse(propValue.Value, out Guid guid))
                    {
                        property.SetValue(parsedElement, guid);
                    }
                }
            }
            return parsedElement;
        }

        public static XElement ToXML(this object obectToParse)
            => ObjectToXML(obectToParse);

        public static T FromXML<T>(this XElement objectToParse)
            => XMLToObject<T>(objectToParse);
    }
}
