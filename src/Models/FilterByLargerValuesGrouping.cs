using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ParcelApp.Models
{
    public class FilterByLargerValuesGrouping : IFilter
    {
        // the filter which groups the parcels by recipient (the component to be decorated)
        private readonly FilterGrouping filterGrouping;

        public FilterByLargerValuesGrouping(XDocument document, double value)
        {
            filterGrouping = new FilterGrouping(document);
            Value = value;
        }

        /// <summary>
        /// The value used for filtering 
        /// </summary>
        private double Value { get; }

        public IEnumerable<(string recipient, double weight, double value)> GetDeliveries()
        {
            // first group by recipient
            var groupByReceipient = filterGrouping.GetDeliveries();

            // then apply the department selection by larger value
            var departmentSelection = groupByReceipient?.Where(item => item.value >= Value);

            return departmentSelection;
        }
    }
}