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

//and save it in documnt
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

