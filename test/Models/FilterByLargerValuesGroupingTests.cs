using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using ParcelApp.Models;

namespace ParcelApp.Tests.Models
{
    [TestFixture]
    public class FilterByLargerValuesGroupingTests
    {
        private readonly XDocument LowValueParcel;
        private readonly XDocument HighValueParcel;
        private readonly XDocument MultipleParcels;

        public FilterByLargerValuesGroupingTests()
        {
            LowValueParcel = XDocument.Parse(@"<document>
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
                                                        <Weight>0.5</Weight>
                                                        <Value>5.0</Value>
                                                     </Parcel>
                                                  </document>");

            HighValueParcel = XDocument.Parse(@"<document>
                                                     <Parcel>
                                                        <Receipient>
                                                          <Name>Soner Colen</Name>
                                                          <Address>
                                                             <Street>Meester Willemstraat</Street>
                                                             <HouseNumber>111</HouseNumber>
                                                             <PostalCode>3036MN</PostalCode>
                                                             <City>Rotterdam</City>
                                                          </Address>
                                                        </Receipient>
                                                        <Weight>5.0</Weight>
                                                        <Value>5000.0</Value>
                                                     </Parcel>
                                                  </document>");


            MultipleParcels = XDocument.Parse(@"<document>
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
                                                      <Value>1500.0</Value>
                                                   </Parcel>
                                                   <Parcel>
                                                      <Receipient>
                                                        <Name>Soner Colen</Name>
                                                        <Address>
                                                           <Street>Meester Willemstraat</Street>
                                                           <HouseNumber>111</HouseNumber>
                                                           <PostalCode>3036MN</PostalCode>
                                                           <City>Rotterdam</City>
                                                        </Address>
                                                      </Receipient>
                                                      <Weight>5.0</Weight>
                                                      <Value>5000.0</Value>
                                                   </Parcel>
                                              </document>");
        }


        [Test]
        public void GetDeliveries_HighValueSelector_LowValueParcel_ReturnsEmptyList()
        {
            // Arrange
            var selector = new FilterByLargerValuesGrouping(LowValueParcel, 1000.0);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 0);
        }

        [Test]
        public void GetDeliveries_HighValueSelector_HighValueParcel_SizeOneList()
        {
            // Arrange
            var selector = new FilterByLargerValuesGrouping(HighValueParcel, 1000.0);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].weight, 5.0);
            Assert.AreEqual(selectedItems[0].value, 5000.0);
        }

        [Test]
        public void GetDeliveries_HighValueSelector_MultipleParcels_SizeTwoList()
        {
            // Arrange
            var selector = new FilterByLargerValuesGrouping(MultipleParcels, 1000.0);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 2);
            Assert.AreEqual(selectedItems[0].value, 1500.0);
            Assert.AreEqual(selectedItems[1].value, 5000.0);
        }

        [Test]
        public void GetDeliveries_VeryHighValueSelector_MultipleParcels_SizeTwoList()
        {
            // Arrange
            var selector = new FilterByLargerValuesGrouping(MultipleParcels, 2000.0);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].value, 5000.0);
        }
    }
}