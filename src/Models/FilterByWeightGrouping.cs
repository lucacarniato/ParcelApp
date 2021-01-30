using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ParcelApp.Models
{
    public class FilterByWeightGrouping : IFilter
    {
        // the filter which groups the parcels by recipient (the component to be decorated)
        private readonly FilterGrouping _filterGrouping;

        public FilterByWeightGrouping(XDocument document, double fromWeight, double toWeight)
        {
            _filterGrouping = new FilterGrouping(document);
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
            // first group by recipient
            var groupByReceipient = _filterGrouping.GetDeliveries();

            // then apply the department selection by weight
            var departmentSelection = groupByReceipient?.Where(item => item.weight >= FromWeight &&
                                                                       item.weight < ToWeight);
            return departmentSelection;
        }
    }
}