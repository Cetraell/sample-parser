using HtmlAgilityPack;
using System;

namespace SampleParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.kellysubaru.com/used-inventory/index.htm";

            var carNumber = 2;

            var vinSelector = $"(//dl[@class='vin']/dd)[{carNumber}]";
            var priceSelector = $"(//span[contains(@class, 'internetPrice')] /span[@class = 'value'])[{carNumber}]";
            var photoSelector = $"(//img[contains(@class,'photo')])[{carNumber}]";

            try
            {

                var web = new HtmlWeb();
                var doc = web.Load(url);

                var vin = TryGetNodeInnerText(doc, vinSelector);
                var price = TryGetNodeInnerText(doc, priceSelector);
                var photo = TryGetNodeAttributeValue(doc, photoSelector, "src");

                Console.WriteLine($"VIN: {vin}");
                Console.WriteLine($"Price: {price}");
                Console.WriteLine($"Photo: {photo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static string TryGetNodeInnerText(HtmlDocument doc, string xPath)
        {
            return doc.DocumentNode.SelectSingleNode(xPath) is { } selectedNode
                ? selectedNode.InnerText
                : "not found";
        }

        public static string TryGetNodeAttributeValue(HtmlDocument doc, string xPath, string attributeName)
        {
            return doc.DocumentNode.SelectSingleNode(xPath) is { } selectedNode
                ? selectedNode.GetAttributeValue(attributeName, "not found")
                : "not found";
        }
    }
}