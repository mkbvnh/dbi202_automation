using DBI202_Creator.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity.Question;

namespace UnitTest.UI
{
    [TestClass]
    public class QuestionPanelTests
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

        private QuestionPanel CreateQuestionPanel()
        {
            return new QuestionPanel(new Question(), (Question q, System.Windows.Forms.TabPage tab) => { return true; });
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateQuestionPanel();

            // Act
            unitUnderTest.AddCandidateBtn_Click(null, null);
            unitUnderTest.HandleDeleteCandidate(new dbi_grading_module.Entity.Candidate.Candidate(), new System.Windows.Forms.TabPage());
            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
