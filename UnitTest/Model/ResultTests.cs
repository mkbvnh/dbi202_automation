using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using dbi_grading_module;
using DBI_Grading.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dbi_grading_module.Entity.Paper;
using DBI_Grading.Utils;
using UnitTest.UnitTestBase;

namespace UnitTest.Model
{
    [TestClass]
    public class ResultTests
    {
        private MockRepository mockRepository;

        private Mock<PaperSet> mockPaperSet;
        private Mock<Submission> mockSubmission;
        private PaperSet paperSet;
        private CompareUnitTest compare;

        [TestInitialize]
        public void TestInitialize()
        {
            Grading.SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "localhost",
                InitialCatalog = "master",
                UserID = "sa",
                Password = "123456"
            };
            string dataPath = ".\\DataUnitTest\\01_DBI202_PE_2019_Sample";
            Grading.RateStructure = 0.5;
            compare = new CompareUnitTest();
            Grading.TimeOutInSecond = 20;
            paperSet = SerializeUtils.DeserializeObject<PaperSet>(dataPath + "\\PaperSet.dat");
            paperSet.QuestionSet.DBScriptList = paperSet.DBScriptList;

            string[] sqlPaths = FileUtils.GetAllSql(dataPath);
            
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockPaperSet = this.mockRepository.Create<PaperSet>(paperSet);
            List<string> answers = new List<string>();
            foreach (string sqlPath in sqlPaths)
            {
                answers.Add(File.ReadAllText(sqlPath));
            }
            this.mockSubmission = this.mockRepository.Create<Submission>("StudentTest", "1", answers, null, "01_DBI202_PE_2019_Sample");
            mockSubmission.Object.PaperNo = "1";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private Result CreateResult()
        {
            return new Result(
                this.mockPaperSet.Object,
                this.mockSubmission.Object);
        }

        [TestMethod]
        public void SumOfPoint_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateResult();
            unitUnderTest.ListAnswers = mockSubmission.Object.ListAnswer;
            unitUnderTest.PaperNo = mockSubmission.Object.PaperNo;
            unitUnderTest.StudentId = mockSubmission.Object.StudentId;
            // Act
            unitUnderTest.GetPoint();
            var result = unitUnderTest.SumOfPoint();
            // Assert
            Assert.IsTrue(result == 10);
        }
    }
}
