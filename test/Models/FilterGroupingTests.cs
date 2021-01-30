using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using ParcelApp.Models;

namespace ParcelApp.Tests.Models
{
    [TestFixture]
    public class FilterGroupingTests
    {
        private readonly XDocument MultipleParcelsOneRecipients;
        private readonly XDocument MultipleParcelsTwoRecipients;

        public FilterGroupingTests()
        {
            MultipleParcelsOneRecipients = XDocument.Parse(@"<document>
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
                                                      <Value>1.0</Value>
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

            MultipleParcelsTwoRecipients = XDocument.Parse(@"<document>
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
                                                          <Name>Soner Colen</Name>
                                                          <Address>
                                                             <Street>Meester Willemstraat</Street>
                                                             <HouseNumber>111</HouseNumber>
                                                             <PostalCode>3036MN</PostalCode>
                                                             <City>Rotterdam</City>
                                                          </Address>
                                                       </Receipient>
                                                       <Weight>2.0</Weight>
                                                       <Value>0.0</Value>
                                                    </Parcel>
                                              </document>");
        }

        [Test]
        public void GetDeliveries_FilterGrouping_MultipleParcelsOneRecipients_ReturnsSizeOneList()
        {
            // Arrange
            var selector = new FilterGrouping(MultipleParcelsOneRecipients);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 1);
            Assert.AreEqual(selectedItems[0].weight, 105);
            Assert.AreEqual(selectedItems[0].value, 1501);
        }

        [Test]
        public void GetDeliveries_LightWeightsSelector_ultipleParcelsTwoRecipients_ReturnsSizeTwoList()
        {
            // Arrange
            var selector = new FilterGrouping(MultipleParcelsTwoRecipients);
            // Act
            var selectedItems = selector.GetDeliveries().ToList();
            // Assert
            Assert.AreEqual(selectedItems.Count(), 2);
        }
    }
}