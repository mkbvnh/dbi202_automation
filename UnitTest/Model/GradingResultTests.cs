using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using dbi_grading_module;
using dbi_grading_module.Entity.Paper;
using DBI_Grading.Model;
using DBI_Grading.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTest.UnitTestBase;

namespace UnitTest.Model
{
    [TestClass]
    public class GradingResultTests
    {
        private CompareUnitTest compare;

        private Mock<PaperSet> mockPaperSet;
        private MockRepository mockRepository;
        private Mock<Submission> mockSubmission;
        private PaperSet paperSet;

        [TestInitialize]
        public void TestInitialize()
        {
            Grading.SqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "master",
                UserID = "sa",
                Password = "123456"
            };
            var dataPath = ".\\DataUnitTest\\01_DBI202_PE_2019_Sample";
            Grading.RateStructure = 0.5;
            compare = new CompareUnitTest();
            Grading.TimeOutInSecond = 20;
            paperSet = SerializeUtils.DeserializeObject<PaperSet>(dataPath + "\\PaperSet.dat");
            paperSet.QuestionSet.DBScriptList = paperSet.DBScriptList;

            var sqlPaths = FileUtils.GetAllSql(dataPath);

            mockRepository = new MockRepository(MockBehavior.Strict);

            mockPaperSet = mockRepository.Create<PaperSet>(paperSet);
            var answers = new List<string>();
            foreach (var sqlPath in sqlPaths) answers.Add(File.ReadAllText(sqlPath));
            mockSubmission =
                mockRepository.Create<Submission>("StudentTest", "1", answers, null, "01_DBI202_PE_2019_Sample");
            mockSubmission.Object.PaperNo = "1";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mockRepository.VerifyAll();
        }

        private Result CreateResult()
        {
            return new Result(
                mockPaperSet.Object,
                mockSubmission.Object);
        }

        //Max Point
        [TestMethod]
        public void SumOfPoint_StateUnderTest_Passed()
        {
            // Arrange
            var unitUnderTest = CreateResult();
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