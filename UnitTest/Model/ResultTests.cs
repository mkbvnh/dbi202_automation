using System;
using System.Data.SqlClient;
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
            string dataPath = ".\\DataUnitTest\\01_DBI202_PE_2019_Sample\\PaperSet.dat";
            Grading.RateStructure = 0.5;
            compare = new CompareUnitTest();
            Grading.TimeOutInSecond = 20;
            paperSet = SerializeUtils.DeserializeObject<PaperSet>(dataPath + "\\PaperSet.dat");
            paperSet.QuestionSet.DBScriptList = paperSet.DBScriptList;

            string[] sqlPaths = FileUtils.GetAllSql(dataPath);

            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockPaperSet = this.mockRepository.Create<PaperSet>();
            this.mockSubmission = this.mockRepository.Create<Submission>(paperSet, "PE_Unit_Test");
            

            mockSubmission.Object.ListAnswer = ;
            mockSubmission.Object.PaperNo = "1";
            mockSubmission.Object.StudentId = "StudentTest";
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

            // Act
            var result = unitUnderTest.SumOfPoint();
            Console.WriteLine(result);
            // Assert
            Assert.Fail();
        }
    }
}
