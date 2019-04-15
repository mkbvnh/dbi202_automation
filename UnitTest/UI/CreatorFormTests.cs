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
            unitUnderTest.AddQuestionBtn_Click(null, null);
            unitUnderTest.AddQuestionToolStripMenuItem_Click(null, null);

            unitUnderTest.PreviewBtn_Click(null, null);
            unitUnderTest.RemoveQuestionBtn_Click(null, null);

            //unitUnderTest.OpenToolStripMenuItem_Click(null, null);
            //unitUnderTest.SaveToolStripMenuItem_Click(null, null);
            //unitUnderTest.ImportPaperSetToolStripMenuItem_Click(null, null);
            //unitUnderTest.ExportPaperSetToolStripMenuItem_Click(null, null);

            unitUnderTest.ScriptBtn_Click(null, null);
            unitUnderTest.VerifySolutionBtn_Click(null, null);

            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
