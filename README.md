# ToXML
Small extension that converts objects to xml and vice versa

## What is it for
Imagine what we need receive c# object as xml element

For example we have

```c#
class Product
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Discount { get; set; }

    public DateTime AddedDate { get; set; }
}
```
And it's instance
```c#
Product testProduct = new Product
{
    Title = "Milk",
    Description = "Tasty Tasty milk",
    Price = 10,
    Discount = 5,
    AddedDate = DateTime.Now
};
```

**ToXML** prodives special class called "XMLConverter"

We can use it's **ObjectToXML** method, that allows convert object to xml
```c#
XElement xmlProduct = XMLConverter.ObjectToXML(product);

//and save it in document
strin path = "your path";
XDocument document = new XDocument();
document.Add(xmlProduct);
document.Save(path);
```
![image](https://user-images.githubusercontent.com/33997732/157474735-02b0ee1e-8a43-4dd2-a3fb-eb621702774c.png)

**ToXML** provides object extension for more convenient, so we can do the same in other way
```c#
XElement xmlProduct = product.ToXML():
```

## Some settings
**ToXML** also has few attributes to define how pase object
+ XMLName - allows to set your name of class/property
+ XMLParsable - allows to enable/disable class/property converter parse

For example for that

```c# 
class Product
{
    [XMLName("Name")]
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
    
    [XMLParsable(false)]
    public int Discount { get; set; }

    public DateTime AddedDate { get; set; }
}
```

We receive following result

![image](https://user-images.githubusercontent.com/33997732/157717907-7bcc886b-b0fe-411a-959b-f9f23cb91c33.png)

How we can see, "Title" was renamed as "Name" and "Discount" was ignored by converter


