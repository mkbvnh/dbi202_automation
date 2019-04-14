using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Utils;

namespace UnitTest.Utils
{
    [TestClass]
    public class StringUtilsTests
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


        [TestMethod]
        public void ReplaceByLine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string input = "select * from test GO";
            string oldValue = "go";
            string newValue = "\n";

            // Act
            var result = StringUtils.ReplaceByLine(
                input,
                oldValue,
                newValue);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FormatSqlCode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string query = "select * from test";

            // Act
            var result = StringUtils.FormatSqlCode(
                query);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTestCases_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string input = "";
            Candidate candidate = new Candidate()
            {
                Point = 1,
            };

            // Act
            var result = StringUtils.GetTestCases(
                input,
                candidate);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
