using System.Collections.Generic;

namespace ParcelApp.Models
{
    /// <summary>
    /// IFilter interface, common to all filters
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// All filters should produce an enumerable of parcel deliveries
        /// </summary>
        /// <returns>An enumerable of parcel deliveries, with the recipient, the weight of the delivery and its value</returns>
        IEnumerable<(string recipient, double weight, double value)> GetDeliveries();
    }
}