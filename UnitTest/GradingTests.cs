using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using dbi_grading_module;
using dbi_grading_module.Entity.Candidate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTest.UnitTestBase;

namespace UnitTest
{
    [TestClass]
    public class GradingTests
    {
        private CompareUnitTest compare;
        private MockRepository mockRepository;


        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            Grading.SqlConnectionStringBuilder = new SqlConnectionStringBuilder
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
            mockRepository.VerifyAll();
        }


        //Test SchemaType
        [TestMethod]
        public void SchemaType_Passed()
        {
            // Arrange
            var candidate = new Candidate
            {
                QuestionType = Candidate.QuestionTypes.Schema,
                Solution =
                    "create table Departments(\nDeptID int primary key,\nName nvarchar(50)\n)\n\ncreate table Projects(\nProID int primary key,\nName nvarchar(30),\nDeptID int references Departments(DeptID)\n)\n\ncreate table Employees(\nEmpID int primary key,\nName nvarchar(30),\nAddress nvarchar(50),\nDeptID int references Departments(DeptID)\n)\n\ncreate table Work(\nProID int references Projects(ProID),\nEmpID int references Employees(EmpID),\nHours int,\nprimary key(ProID,EmpID)\n)\n",
                Point = 1
            };
            var studentId = "test";
            var answer =
                "create table Departments(\nDeptID int primary key,\nName nvarchar(50)\n)\n\ncreate table Projects(\nProID int primary key,\nName nvarchar(30),\nDeptID int references Departments(DeptID)\n)\n\ncreate table Employees(\nEmpID int primary key,\nName nvarchar(30),\nAddress nvarchar(50),\nDeptID int references Departments(DeptID)\n)\n\ncreate table Work(\nProID int references Projects(ProID),\nEmpID int references Employees(EmpID),\nHours int,\nprimary key(ProID,EmpID)\n)\n";
            var questionOrder = 1;

            // Act
            var expectedRes = Grading.SchemaType(
                candidate,
                studentId,
                answer,
                questionOrder);
            var actualRes = new Dictionary<string, string>
            {
                {"Point", "1"},
                {"Comment", "Total Point: 1/1 - Passed\nCount Tables in database: Same\n"}
            };
            var a = compare.CompareDictionary(expectedRes, actualRes);
            // Assert
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void SchemaType_Fail()
        {
            // Arrange
            var candidate = new Candidate
            {
                QuestionType = Candidate.QuestionTypes.Schema,
                Solution = "create table Students(\nStuID int primary key,\nName nvarchar(30),\nDateOfBirth date\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuID int references Students(StuID),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuID, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n",
                Point = 1
            };
            var studentId = "test";
            var answer =
                "create table Students(\nStuI int primary key,\nName nvarchar(30),\nDateOfBirth nvarchar(30)\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuI int references Students(StuI),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuI, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n";
            var questionOrder = 1;

            // Act
            var actualRes = Grading.SchemaType(
                candidate,
                studentId,
                answer,
                questionOrder);
            var expectedRes = new Dictionary<string, string>
            {
                {"Point", "0,68"},
                {"Comment", "Total Point: 0,68/1,  - Errors details:\nCount Tables in database: Same\n\n- Structure - [Point: 0,41/0,5] - [Comparison: 23/28]:\n+ Definition has 1 error(s) (-1 comparison each error):\n1. Required: Students (DateOfBirth) => date, NULL\n    Answer  : Students (DateOfBirth => nvarchar(30), NULL\n+ Column(s) missing has 2 errors(s) (-2 comparison each error):\n1. StuID (Live)\n2. StuID (Students)\n\n- Constraint - [Point: 0,27/0,5] - [Comparison: 6/11]:\n+ References check  (-1 comparison each error):\n1. Missing StuID (Students) - StuID (Live)\n+ Missing Primary Key  (-1 comparison each error): \n1. StuID (Students) \n2. StuID (Live) \n+ Redundant Primary Key (-1 comparison each error): \n1. StuI (Students) \n2. StuI (Live) \n"}
            };
            var check = compare.CompareDictionary(expectedRes, actualRes);
            // Assert
            Assert.IsTrue(check);
        }

        //Keep grading and save the sql error
        [TestMethod]
        public void SchemaType_ErrorStudentQuery()
        {
            // Arrange
            var candidate = new Candidate
            {
                QuestionType = Candidate.QuestionTypes.Schema,
                Solution = "create table Students(\nStuID int primary key,\nName nvarchar(30),\nDateOfBirth date\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuID int references Students(StuID),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuID, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n",
                Point = 1
            };
            var studentId = "test";
            var answer =
                "create tadfble Students(\nStuI int primary key,\nName nvarchar(30),\nDateOfBirth nvarchar(30)\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuI int references Students(StuI),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuI, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n";
            var questionOrder = 1;

            // Act
            var actualRes = Grading.SchemaType(
                candidate,
                studentId,
                answer,
                questionOrder);
            var expectedRes = new Dictionary<string, string>
            {
                {"Point", "0"},
                {"Comment", "Answer query error: Unknown object type 'tadfble' used in a CREATE, DROP, or ALTER statement.\n"}
            };
            var check = compare.CompareDictionary(expectedRes, actualRes);
            // Assert
            Assert.IsTrue(check);
        }
        /// <summary>
        /// It will throw Exception
        /// </summary>
        [TestMethod]
        public void SchemaType_ErrorTeacherQuery()
        {
            // Arrange
            var candidate = new Candidate
            {
                QuestionType = Candidate.QuestionTypes.Schema,
                Solution = "create tadsfble Students(\nStuID int primary key,\nName nvarchar(30),\nDateOfBirth date\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuID int references Students(StuID),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuID, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n",
                Point = 1
            };
            var studentId = "test";
            var answer =
                "create table Students(\nStuI int primary key,\nName nvarchar(30),\nDateOfBirth nvarchar(30)\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuI int references Students(StuI),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuI, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n";
            var questionOrder = 1;

            // Act

            Exception expectedRes = null;
            try
            {
                var actualRes = Grading.SchemaType(
                    candidate,
                    studentId,
                    answer,
                    questionOrder);
            }
            catch (Exception e)
            {
                expectedRes = e;
            }
            // Assert
            Assert.IsNotNull(expectedRes);
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