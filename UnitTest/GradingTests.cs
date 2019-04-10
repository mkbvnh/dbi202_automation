using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using dbi_grading_module;
using dbi_grading_module.Entity.Candidate;
using UnitTest.UnitTestBase;

namespace UnitTest
{
    [TestClass]
    public class GradingTests
    {
        private MockRepository mockRepository;
        private CompareUnitTest compare;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            Grading.SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "localhost",
                InitialCatalog = "master",
                UserID = "sa",
                Password = "123456"
            };
            Grading.RateStructure = 0.5;
            compare = new CompareUnitTest();
            Grading.TimeOutInSecond = 20;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void SchemaType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Schema,
                Solution = "create table Departments(\r\nDeptID int primary key,\r\nName nvarchar(50)\r\n)\r\n\r\ncreate table Projects(\r\nProID int primary key,\r\nName nvarchar(30),\r\nDeptID int references Departments(DeptID)\r\n)\r\n\r\ncreate table Employees(\r\nEmpID int primary key,\r\nName nvarchar(30),\r\nAddress nvarchar(50),\r\nDeptID int references Departments(DeptID)\r\n)\r\n\r\ncreate table Work(\r\nProID int references Projects(ProID),\r\nEmpID int references Employees(EmpID),\r\nHours int,\r\nprimary key(ProID,EmpID)\r\n)\r\n",
                Point = 1
            };
            string studentId = "test";
            string answer = "create table Departments(\r\nDeptID int primary key,\r\nName nvarchar(50)\r\n)\r\n\r\ncreate table Projects(\r\nProID int primary key,\r\nName nvarchar(30),\r\nDeptID int references Departments(DeptID)\r\n)\r\n\r\ncreate table Employees(\r\nEmpID int primary key,\r\nName nvarchar(30),\r\nAddress nvarchar(50),\r\nDeptID int references Departments(DeptID)\r\n)\r\n\r\ncreate table Work(\r\nProID int references Projects(ProID),\r\nEmpID int references Employees(EmpID),\r\nHours int,\r\nprimary key(ProID,EmpID)\r\n)\r\n";
            int questionOrder = 1;

            // Act
            var expectedRes = Grading.SchemaType(
                candidate,
                studentId,
                answer,
                questionOrder);
            Dictionary<string, string> actualRes = new Dictionary<string, string>
            {
                {"Point", "1"},
                {"Comment", "Total Point: 1/1 - Passed\nCount Tables in database: Same\n"}
            };
            expectedRes["Point"] = expectedRes["Point"];
            bool a = compare.CompareDictionary<string, string>(expectedRes, actualRes);
            // Assert
            Assert.IsTrue(a);
        }

        //[TestMethod]
        //public void SelectType_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    Candidate candidate = TODO;
        //    string studentId = TODO;
        //    string answer = TODO;
        //    int questionOrder = TODO;
        //    string dbScript = TODO;

        //    // Act
        //    var result = Grading.SelectType(
        //        candidate,
        //        studentId,
        //        answer,
        //        questionOrder,
        //        dbScript);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void DmlSpTriggerType_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    Candidate candidate = TODO;
        //    string studentId = TODO;
        //    string answer = TODO;
        //    int questionOrder = TODO;
        //    string dbScript = TODO;

        //    // Act
        //    var result = Grading.DmlSpTriggerType(
        //        candidate,
        //        studentId,
        //        answer,
        //        questionOrder,
        //        dbScript);

        //    // Assert
        //    Assert.Fail();
        //}
    }
}
