using DBI202_Creator.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest.UI
{
    [TestClass]
    public class CreatorFormTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private CreatorForm CreateCreatorForm()
        {
            return new CreatorForm();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateCreatorForm();

            // Act

            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
