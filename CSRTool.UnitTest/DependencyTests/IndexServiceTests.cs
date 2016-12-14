using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using CSRTool.Dependencies.ApplicationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CSRTool.UnitTest.DependencyTests
{
    [TestClass]
    public class IndexServiceTests
    {
        [TestMethod]
        public void GetIndexes_Should_Return_A_List_Of_Indexes()
        {
            //Arrange
            var indexRepositoryMock = Substitute.For<IIndexRepository>();
            indexRepositoryMock.GetIndexes().Returns(x => MockReturnIndexList());

            var sut = new IndexService(indexRepositoryMock);


            //Act
            var result = sut.GetIndexes();

            //Assert
            indexRepositoryMock.Received().GetIndexes();
            Assert.AreEqual(MockReturnIndexList().Count, result.Count);
        }

        private List<Index> MockReturnIndexList()
        {
            var mockReturn = new List<Index>
            {
                new Index(),
                new Index(),
                new Index()
            };

            return mockReturn;
        }
    }
}