using DBI202_Creator.UI.CandidateUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity.Candidate;

namespace UnitTest.UI.CandidateUI
{
    [TestClass]
    public class CandidatePanelTests
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

        private CandidatePanel CreateCandidatePanel()
        {
            return new CandidatePanel(new Candidate(), (Candidate c, System.Windows.Forms.TabPage tab) => true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateCandidatePanel();

            // Act
            unitUnderTest.ProcedureState();
            unitUnderTest.TriggerState();
            unitUnderTest.DmlState();
            unitUnderTest.SchemaState();

            unitUnderTest.CandidatePanel_Load(null, null);
            unitUnderTest.InsertTcBtn_Click(null, null);
            unitUnderTest.ValidateTcBtn_Click(null, null);
            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
