using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using dbi_grading_module.Configuration;

namespace UnitTest.Configuration
{
    [TestClass]
    public class DatabaseConfigTests
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
        public void GenerateDatabase_StateUnderTest_EmptyScript()
        {
            // Arrange
            string dbSolutionName = "";
            string dbAnswerName = "";
            string dbScript = "";
            Exception e = null;

            // Act
            try
            {
                DatabaseConfig.GenerateDatabase(
                    dbSolutionName,
                    dbAnswerName,
                    dbScript);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            // Assert
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void GenerateDatabase_StateUnderTest_Error()
        {
            // Arrange
            string dbSolutionName = "";
            string dbAnswerName = "";
            string dbScript = "create";
            Exception e = null;
            // Act
            try
            {
                DatabaseConfig.GenerateDatabase(
                    dbSolutionName,
                    dbAnswerName,
                    dbScript);
            }
            catch (Exception exception)
            {
                e = exception;
            }

            // Assert
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void PrepareSpCompareDatabase_StateUnderTest_ExpectedBehavior()
        {

            // Act
            var result = DatabaseConfig.PrepareSpCompareDatabase();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CheckConnection_StateUnderTest_ExpectedBehavior()
        {
            string dataSource = "localhost";
            string userId = "sa";
            string password = "123456";
            string initialCatalog = "master";

            // Act
            var result = DatabaseConfig.CheckConnection(
                dataSource,
                userId,
                password,
                initialCatalog);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExecuteSingleQuery_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string query = "exec sp_help";
            string catalog = "master";

            // Act
            var result = DatabaseConfig.ExecuteSingleQuery(
                query,
                catalog);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DropAllDatabaseCreated_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string afterTime = DatabaseConfig.ExecuteScalarQuery("select GetDate()").ToString();
            DatabaseConfig.ExecuteSingleQuery("create database test", "master");
            // Act
            var result = DatabaseConfig.DropAllDatabaseCreated(
                afterTime);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExecuteScalarQuery_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string query = "select GetDate()";

            // Act
            var result = DatabaseConfig.ExecuteScalarQuery(
                query);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
