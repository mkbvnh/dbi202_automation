using DBI202_Creator.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity.Question;

namespace UnitTest.UI
{
    [TestClass]
    public class VerifyFormTests
    {
        private MockRepository mockRepository;

        private Mock<QuestionSet> mockQuestionSet;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockQuestionSet = this.mockRepository.Create<QuestionSet>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private VerifyForm CreateVerifyForm()
        {
            return new VerifyForm(
                this.mockQuestionSet.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateVerifyForm();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
