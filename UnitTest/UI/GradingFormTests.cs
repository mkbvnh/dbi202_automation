using DBI_Grading.Model;
using DBI_Grading.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using dbi_grading_module.Entity.Paper;

namespace UnitTest.UI
{
    [TestClass]
    public class GradingFormTests
    {
        private MockRepository mockRepository;

        private Mock<List<Submission>> mockList;
        private Mock<PaperSet> mockPaperSet;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockList = this.mockRepository.Create<List<Submission>>();
            this.mockPaperSet = this.mockRepository.Create<PaperSet>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private GradingForm CreateGradingForm()
        {
            return new GradingForm(
                this.mockList.Object,
                this.mockPaperSet.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateGradingForm();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
