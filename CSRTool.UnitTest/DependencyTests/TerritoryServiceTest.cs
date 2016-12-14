using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using CSRTool.Dependencies.ApplicationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CSRTool.UnitTest.DependencyTests
{
    [TestClass]
    public class TerritoryServiceTest
    {
        [TestMethod]
        public void GetTerritories_Should_Return_A_List_Of_Territories()
        {

            //Arrange
            var territoryRepositoryMock = Substitute.For<ITerritoryRepository>();
            territoryRepositoryMock.GetTerritories().Returns(x => MockReturnTerritoryList());

            var sut = new TerritoryService(territoryRepositoryMock);


            //Act
            var result = sut.GetTerritories();

            //Assert
            territoryRepositoryMock.Received().GetTerritories();
            Assert.AreEqual(MockReturnTerritoryList().Count, result.Count);
        }

        private List<Territory> MockReturnTerritoryList()
        {
            var mockReturn = new List<Territory>
            {
                new Territory(),
                new Territory(),
                new Territory()
            };

            return mockReturn;
        }
    }
}
