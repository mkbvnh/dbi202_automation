using DBI202_Creator.UI.CandidateUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity;

namespace UnitTest.UI.CandidateUI
{
    [TestClass]
    public class TestCaseDialogTests
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        private TestCaseDialog CreateTestCaseDialog()
        {
            TestCase tc = new TestCase();
            tc.RatePoint = 1;
            tc.Description = "something";
            tc.TestQuery = "select * from something";
            return new TestCaseDialog(tc, (TestCase t) => true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateTestCaseDialog();

            // Act
            unitUnderTest.insertBtn_Click(null, null);
            unitUnderTest.cancelBtn_Click(null, null);
            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
