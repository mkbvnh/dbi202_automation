using System.Collections.Generic;
using dbi_grading_module.Entity.Paper;
using DBI202_Creator.UI.ExportUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.Commons;
using UnitTest.UnitTestBase;

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
            var dataPath = ".\\DataUnitTest\\01_DBI202_PE_2019_Sample";
            Constants.PaperSet = SerializeUtils.DeserializeObject<PaperSet>(dataPath + "\\PaperSet.dat");

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
            unitUnderTest.browseBtn_Click(null, null);
            unitUnderTest.newBtn_Click(null, null);

            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
