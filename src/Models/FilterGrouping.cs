using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ParcelApp.Models
{
    public class FilterGrouping : IFilter
    {
        private readonly XDocument Document;

        public FilterGrouping(XDocument document)
        {
            Document = document;
        }

        public IEnumerable<(string recipient, double weight, double value)> GetDeliveries()
        {
            // group deliveries by recipient, because document might be delivered to the same person and address.
            // It's a waste sending two deliveries. Assume that the cost of insuring a grouped value is less than the cost of sending multiple deliveries.
            var groupedByReceipient = Document?.Descendants("Parcel").GroupBy(item =>
                new
                {
                    Name = item.Element("Receipient")?.Element("Name")?.Value,
                    Street = item.Element("Receipient")?.Element("Address")?.Element("Street")?.Value,
                    HouseNumber = item.Element("Receipient")?.Element("Address")?.Element("HouseNumber")?.Value,
                    PostalCode = item.Element("Receipient")?.Element("Address")?.Element("PostalCode")?.Value,
                    City = item.Element("Receipient")?.Element("Address")?.Element("City")?.Value
                }).Select(group => (
                recipient: string.Join(" ", group.Key.Name, group.Key.Street, group.Key.HouseNumber,
                    group.Key.PostalCode, group.Key.City),
                weight: group.Sum(e => Convert.ToDouble(e.Element("Weight")?.Value)),
                value: group.Sum(e => Convert.ToDouble(e.Element("Value")?.Value))));
            return groupedByReceipient;
        }
    }
}