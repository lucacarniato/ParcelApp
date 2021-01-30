using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ParcelApp.Models
{
    public class FilterByWeightRangeNoGrouping : IFilter
    {
        private readonly XDocument Document;

        public FilterByWeightRangeNoGrouping(XDocument document, double fromWeight, double toWeight)
        {
            Document = document;
            FromWeight = fromWeight;
            ToWeight = toWeight;
        }

        /// <summary>
        /// The lower value used for filtering
        /// </summary>
        private double FromWeight { get; }

        /// <summary>
        /// The upper value used for filtering
        /// </summary>
        private double ToWeight { get; }

        public IEnumerable<(string recipient, double weight, double value)> GetDeliveries()
        {
            // Do not group by recipient, just convert each parcel in a recipient (a tuple of strings), a weight (a double) and the value (another double)
            var notGroupedParcels = Document?.Descendants("Parcel").Select(item => (
                recipient: string.Join(" ",
                    item.Element("Receipient")?.Element("Name")?.Value,
                    item.Element("Receipient")?.Element("Address")?.Element("Street")?.Value,
                    item.Element("Receipient")?.Element("Address")?.Element("HouseNumber")?.Value,
                    item.Element("Receipient")?.Element("Address")?.Element("PostalCode")?.Value,
                    item.Element("Receipient")?.Element("Address")?.Element("City")?.Value),
                weight: Convert.ToDouble(item.Element("Weight")?.Value),
                value: Convert.ToDouble(item.Element("Value")?.Value)));

            var departmentSelection = notGroupedParcels?.Where(item => item.weight >= FromWeight &&
                                                                       item.weight < ToWeight);
            return departmentSelection;
        }
    }
}