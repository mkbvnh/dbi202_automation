using DBI_Grading.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest.UI
{
    [TestClass]
    public class ImportFormTests
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

        private ImportForm CreateImportForm()
        {
            return new ImportForm();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateImportForm();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
