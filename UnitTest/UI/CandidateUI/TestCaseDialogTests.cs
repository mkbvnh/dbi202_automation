using DBI202_Creator.UI.CandidateUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity;

namespace UnitTest.UI.CandidateUI
{
    [TestClass]
    public class TestCaseDialogTests
    {
        private MockRepository mockRepository;

        private Mock<TestCase> mockTestCase;
        private Mock<TestCaseDialog.HandleInsert> mockHandleInsert;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockTestCase = this.mockRepository.Create<TestCase>();
            this.mockHandleInsert = this.mockRepository.Create<TestCaseDialog.HandleInsert>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private TestCaseDialog CreateTestCaseDialog()
        {
            return new TestCaseDialog(
                this.mockTestCase.Object,
                this.mockHandleInsert.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateTestCaseDialog();

            // Act

            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
