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
            unitUnderTest.Question = new Question();
            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
