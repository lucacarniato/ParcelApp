using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using ParcelApp.Models;

namespace ParcelApp.Tests.Models
{
    [TestFixture]
    public class FilterByWeightGroupingTests
    {
        private readonly XDocument singleLightParcel;
        private readonly XDocument singleMediumWeightParcel;
        private readonly XDocument singleHeavyParcel;
        private readonly XDocument multipleParcels;

        public FilterByWeightGroupingTests()
        {
            singleLightParcel = XDocument.Parse(@"<document>
                                                     <Parcel>
                                                        <Receipient>
                                                          <Name>Vinny Gankema</Name>
                                                          <Address>
                                                             <Street>Marijkestraat</Street>
                                                             <HouseNumber>28</HouseNumber>
                                                             <PostalCode>4744AT</PostalCode>
                                                             <City>Bosschenhoofd</City>
                                                          </Address>
                                                        </Receipient>
                                                        <Weight>0.02</Weight>
                                                        <Value>0.0</Value>
                                                     </Parcel>
                                                  </document>");

            singleMediumWeightParcel = XDocument.Parse(@"<document>
                                                     <Parcel>
                                                        <Receipient>
                                                          <Name>Vinny Gankema</Name>
                                                          <Address>
                                                             <Street>Marijkestraat</Street>
                                                             <HouseNumber>28</HouseNumber>
                                                             <PostalCode>4744AT</PostalCode>
                                                             <City>Bosschenhoofd</City>
                                                          </Address>
                                                        </Receipient>
                                                        <Weight>5.0</Weight>
                                                        <Value>100.0</Value>
                                                     </Parcel>
                                                  </document>");

            singleHeavyParcel = XDocument.Parse(@"<document>
                                                     <Parcel>
                                                        <Receipient>
                                                          <Name>Vinny Gankema</Name>
                                                          <Address>
                                                             <Street>Marijkestraat</Street>
                                                             <HouseNumber>28</HouseNumber>
                                                             <PostalCode>4744AT</PostalCode>
                                                             <City>Bosschenhoofd</City>
                                                          </Address>
                                                        </Receipient>
                                                        <Weight>100.0</Weight>
                                                        <Value>1500.0</Value>
                                                     </Parcel>
                                                  </document>");


            multipleParcels = XDocument.Parse(@"<document>
                                                   <Parcel>
                                                      <Receipient>
                                                         <Name>Vinny Gankema</Name>
                                                         <Address>
                                                            <Street>Marijkestraat</Street>
                                                            <HouseNumber>28</HouseNumber>
                                                            <PostalCode>4744AT</PostalCode>
                                                            <City>Bosschenhoofd</City>
                                                         </Address>
                                                      </Receipient>
                                                      <Weight>15.0</Weight>
                                                      <Value>0.0</Value>
                                                   </Parcel>
                                                    <Parcel>
                                                       <Receipient>
                                                          <Name>Vinny Gankema</Name>
                                                          <Address>
                                                             <Street>Marijkestraat</Street>
                                                             <HouseNumber>28</HouseNumber>
                                                             <PostalCode>4744AT</PostalCode>
                                                             <City>Bosschenhoofd</City>
                                                          </Address>
                                                       </Receipient>
                                                       <Weight>90.0</Weight>
                                                       <Value>1500.0</Value>
                                                    </Parcel>
                                              </document>");
        }

        [Test]
        public void GetDeliveries_LightWeightsSelector_LightParcel_ReturnsSizeOneList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleLightParcel, 0, 1);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].weight, 0.02);
            Assert.AreEqual(selectedItems[0].value, 0.0);
        }

        [Test]
        public void GetDeliveries_LightWeightsSelector_MediumWeightParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleMediumWeightParcel, 0, 1);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_LightWeightsSelector_HeavyParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleHeavyParcel, 0, 1);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_MediumWeightsSelector_LightParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleLightParcel, 1, 10);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_MediumWeightsSelector_MediumWeightParcel_ReturnsSizeOneList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleMediumWeightParcel, 1, 10);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].weight, 5.0);
            Assert.AreEqual(selectedItems[0].value, 100.0);
        }

        [Test]
        public void GetDeliveries_MediumWeightsSelector_HeavyParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleHeavyParcel, 1, 10);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_HeavyWeightsSelector_LightParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleLightParcel, 10, double.MaxValue);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_HeavyWeightsSelector_MediumWeightParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleMediumWeightParcel, 10, double.MaxValue);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_HeavyWeightsSelector_HeavyParcel_ReturnsSizeOneList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(singleHeavyParcel, 10, double.MaxValue);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].weight, 100.0);
            Assert.AreEqual(selectedItems[0].value, 1500.0);
        }

        [Test]
        public void GetDeliveries_LightWeightsSelector_MultipleParcels_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(multipleParcels, 0, 1);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert, the two parcels are summed up in one heavy delivery
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_MediumWeightsSelector_MultipleParcels_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(multipleParcels, 1, 10);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert, the two parcels are summed up in one heavy delivery
            Assert.AreEqual(selectedItems.Count(), 0);
        }


        [Test]
        public void GetDeliveries_HeavyWeightsSelector_MultipleParcels_ReturnsSizeOneList()
        {
            // Arrange
            var selector = new FilterByWeightGrouping(multipleParcels, 10, double.MaxValue);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].weight, 105);
            Assert.AreEqual(selectedItems[0].value, 1500);
        }
    }
}