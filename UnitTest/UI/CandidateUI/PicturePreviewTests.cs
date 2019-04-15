using DBI202_Creator.UI.CandidateUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTest.UI.CandidateUI
{
    [TestClass]
    public class PicturePreviewTests
    {
        private MockRepository mockRepository;

        private Mock<List<string>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockList = this.mockRepository.Create<List<string>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private PicturePreview CreatePicturePreview()
        {
            return new PicturePreview(
                this.mockList.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreatePicturePreview();

            // Act
            //unitUnderTest.LeftBtn_Click(null, null);
            //unitUnderTest.rightBtn_Click(null, null);
            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
