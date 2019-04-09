using DBI202_Creator.UI.ExportUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity.Question;

namespace UnitTest.UI.ExportUI
{
    [TestClass]
    public class ExportConfirmTests
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

        private ExportConfirm CreateExportConfirm()
        {
            return new ExportConfirm(
                this.mockQuestionSet.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateExportConfirm();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
