using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ParcelApp.Models
{
    internal sealed class Model
    {
        /// <summary>
        /// The current department
        /// </summary>
        public Departments Department { get; set; }

        /// <summary>
        /// A current document, set after the read method
        /// </summary>
        private XDocument Document { get; set; }

        /// <summary>
        /// The method to read the XML Document
        /// </summary>
        /// <param name="path"></param>
        public void ReadXmlParcels(string path)
        {
            Document = XDocument.Load(path);
        }
        /// <summary>
        /// A factory method to generate different parcel filters
        /// </summary>
        /// <returns></returns>
        private IFilter MakeFilter()
        {
            if (Department == Departments.Mail) return new FilterByWeightGrouping(Document, 0.0, 1.0);

            if (Department == Departments.Regular) return new FilterByWeightGrouping(Document, 1.0, 10.0);

            if (Department == Departments.Heavy) return new FilterByWeightGrouping(Document, 10.0, double.MaxValue);

            if (Department == Departments.Insurance) return new FilterByLargerValuesGrouping(Document, 1000.0);

            if (Department == Departments.Mail_not_grouped_by_receipient)
                return new FilterByWeightRangeNoGrouping(Document, 0.0, 1.0);

            return null;
        }

        /// <summary>
        /// Gets the filtered deliveries. The final result passed to the view model is converted to a list of strings
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDeliveries()
        {
            var filter = MakeFilter();

            var deliveries = filter?.GetDeliveries();

            var deliveriesList = deliveries?.Select(item => string.Join(" ", item.recipient,
                "Weight", Convert.ToString(item.weight, CultureInfo.InvariantCulture),
                "Value", Convert.ToString(item.value, CultureInfo.InvariantCulture)));

            return deliveriesList?.ToList();
        }
    }
}