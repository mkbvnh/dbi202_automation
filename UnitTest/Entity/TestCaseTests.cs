using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity;

namespace UnitTest.Entity
{
    [TestClass]
    public class TestCaseTests
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

        private TestCase CreateTestCase()
        {
            return new TestCase()
            {
                Description = "Test",
                RatePoint = 0.5,
                TestQuery = "select"
            };
        }

        [TestMethod]
        public void ToString_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateTestCase();

            // Act
            var result = unitUnderTest.ToString();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
