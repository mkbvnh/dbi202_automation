using System.Collections.Generic;
using System.Data.SqlClient;
using dbi_grading_module;
using dbi_grading_module.Entity.Candidate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTest.Properties;
using UnitTest.UnitTestBase;

namespace UnitTest
{
    [TestClass]
    public class GradingTests
    {
        private CompareUnitTest _compare;
        private MockRepository _mockRepository;
        private readonly string _dbScript = Resources.dbScript;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            Grading.SqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "master",
                UserID = "sa",
                Password = "123456"
            };
            Grading.RateStructure = 0.5;
            _compare = new CompareUnitTest();
            Grading.TimeOutInSecond = 1000;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockRepository.VerifyAll();
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
            var a = _compare.CompareDictionary(expectedRes, actualRes);
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
            var check = _compare.CompareDictionary(expectedRes, actualRes);
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
            var check = _compare.CompareDictionary(expectedRes, actualRes);
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
                Point = 1,
                QuestionRequirement = "",
                Illustration = new List<string>(),
                QuestionId = "1"
            };
            candidate.Equals(candidate);
            var checkIllustration = candidate.Illustration;
            var studentId = "test";
            var answer =
                "create table Students(\nStuI int primary key,\nName nvarchar(30),\nDateOfBirth nvarchar(30)\n)\n\ncreate table Dom(\nDomID int primary key,\nName nvarchar(50)\n)\n\ncreate table Rooms(\nDomID int references Dom(DomID),\nRoomNumber int,\nCapacity int,\nFloor int,\nprimary key(RoomNumber, DomID)\n)\n\ncreate table Live(\t\nDomID int,\nRoomNumber int,\nStuI int references Students(StuI),\nStartDate Date,\nEndDate Date,\nprimary key (DomID, RoomNumber, StuI, StartDate),\nforeign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)\n)\n";
            var questionOrder = 1;

            // Act
            var actualRes = Grading.SchemaType(
                candidate,
                studentId,
                answer,
                questionOrder)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select AVAIL_BALANCE, ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID as abc from ACCOUNT";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed_WithGoSyntax()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select AVAIL_BALANCE, ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID as abc from ACCOUNT GO";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT where account_id = 1";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataError()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select * from department";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataError_Query()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select AVAIL_BALANCE, abc, OPEN_BRANCH_ID as abc from ACCOUNT";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed_SortPassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account order by OPEN_BRANCH_ID",
                Point = 1,
                TestQuery = "select AVAIL_BALANCE, ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT order by OPEN_BRANCH_ID",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = true,
            };
            string studentId = "Test";
            string answer = "select AVAIL_BALANCE, ACCOUNT_ID, OPEN_DATE, CUST_ID, OPEN_BRANCH_ID, OPEN_DATE from ACCOUNT order by OPEN_BRANCH_ID";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }


        [TestMethod]
        public void SelectType_CompareDataPassed_SortFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select * from account order by OPEN_BRANCH_ID",
                Point = 1,
                TestQuery = "select AVAIL_BALANCE, ACCOUNT_ID, CUST_ID, OPEN_BRANCH_ID from ACCOUNT order by OPEN_BRANCH_ID",
                CheckColumnName = false,
                CheckDistinct = false,
                RequireSort = true,
            };
            string studentId = "Test";
            string answer = "select AVAIL_BALANCE, ACCOUNT_ID, OPEN_DATE, CUST_ID, OPEN_BRANCH_ID, OPEN_DATE from ACCOUNT";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0,5";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed_DistinctFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select distinct OPEN_BRANCH_ID from account",
                Point = 1,
                TestQuery = "select distinct OPEN_BRANCH_ID from account",
                CheckColumnName = false,
                CheckDistinct = true,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select OPEN_BRANCH_ID from account";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0,5";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed_DistinctPassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select distinct OPEN_BRANCH_ID, CUST_ID, account.OPEN_DATE from account",
                Point = 1,
                TestQuery = "select distinct OPEN_BRANCH_ID, CUST_ID, account.OPEN_DATE from account",
                CheckColumnName = false,
                CheckDistinct = true,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select distinct OPEN_BRANCH_ID, CUST_ID, account.OPEN_DATE from account";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed_ColumnNamePassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select ACCOUNT_ID, AVAIL_BALANCE, OPEN_EMP_ID ,OPEN_BRANCH_ID, CUST_ID as ID from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, AVAIL_BALANCE, OPEN_BRANCH_ID, CUST_ID as ID from account",
                CheckColumnName = true,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select ACCOUNT_ID, AVAIL_BALANCE, CUST_ID as ID, OPEN_BRANCH_ID from account";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void SelectType_CompareDataPassed_ColumnNameFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Select,
                Solution = "select ACCOUNT_ID, AVAIL_BALANCE, OPEN_EMP_ID ,OPEN_BRANCH_ID, CUST_ID as ID from account",
                Point = 1,
                TestQuery = "select ACCOUNT_ID, AVAIL_BALANCE, OPEN_BRANCH_ID, CUST_ID as ID from account",
                CheckColumnName = true,
                CheckDistinct = false,
                RequireSort = false,
            };
            string studentId = "Test";
            string answer = "select ACCOUNT_ID, AVAIL_BALANCE, CUST_ID as ID, OPEN_BRANCH_ID as abc from account";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.SelectType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0,5";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void TriggerType_CompareDataPassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Trigger,
                Solution = "create trigger Trigger_Delete_Account\non ACCOUNT\ninstead of delete\nas\nbegin\n\tselect FIRST_NAME, LAST_NAME, ACCOUNT_ID, AVAIL_BALANCE\n\tfrom deleted, CUSTOMER\n\twhere deleted.CUST_ID = CUSTOMER.CUST_ID\nend\n",
                Point = 1,
                TestQuery = "/*0.5*/\n/*Run example in question*/\ndelete from ACCOUNT where ACCOUNT_ID in (1,2)\nselect * from ACCOUNT\n\n/*0.5*/\n/*Run another example*/\ndelete from ACCOUNT where ACCOUNT_ID in (7,9)\nselect * from ACCOUNT\n",
            };
            string studentId = "Test";
            string answer = "create trigger Trigger_Delete_Account\non ACCOUNT\ninstead of delete\nas\nbegin\n\tselect FIRST_NAME, LAST_NAME, ACCOUNT_ID, AVAIL_BALANCE\n\tfrom deleted, CUSTOMER\n\twhere deleted.CUST_ID = CUSTOMER.CUST_ID\nend\n";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void TriggerType_CompareDataError_QuerySolution()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Trigger,
                Solution = "create trigsdfdsfger Trigger_Delete_Account\non ACCOUNT\ninstead of delete\nas\nbegin\n\tselect FIRST_NAME, LAST_NAME, ACCOUNT_ID, AVAIL_BALANCE\n\tfrom deleted, CUSTOMER\n\twhere deleted.CUST_ID = CUSTOMER.CUST_ID\nend\n",
                Point = 1,
                TestQuery = "/*0.5*/\n/*Run example in question*/\ndelete from ACCOUNT where ACCOUNT_ID in (1,2)\nselect * from ACCOUNT\n\n/*0.5*/\n/*Run another example*/\ndelete from ACCOUNT where ACCOUNT_ID in (7,9)\nselect * from ACCOUNT\n",
            };
            string studentId = "Test";
            string answer = "create trigger Trigger_Delete_Account\non ACCOUNT\ninstead of delete\nas\nbegin\n\tselect FIRST_NAME, LAST_NAME, ACCOUNT_ID, AVAIL_BALANCE\n\tfrom deleted, CUSTOMER\n\twhere deleted.CUST_ID = CUSTOMER.CUST_ID\nend\n";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void TriggerType_CompareDataError_TimeOut()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Trigger,
                Solution = "create trigger Trigger_Delete_Account\non ACCOUNT\ninstead of delete\nas\nbegin\n\tselect FIRST_NAME, LAST_NAME, ACCOUNT_ID, AVAIL_BALANCE\n\tfrom deleted, CUSTOMER\n\twhere deleted.CUST_ID = CUSTOMER.CUST_ID\nend\n",
                Point = 1,
                TestQuery = "/*0.5*/\n/*Run example in question*/\ndelete from ACCOUNT where ACCOUNT_ID in (1,2)\nselect * from ACCOUNT\n\n/*0.5*/\n/*Run another example*/\ndelete from ACCOUNT where ACCOUNT_ID in (7,9)\nselect * from ACCOUNT\n",
            };
            string studentId = "Test";
            string answer = "WAITFOR DELAY '02:00' \ncreate trigger Trigger_Delete_Account\non ACCOUNT\ninstead of delete\nas\nbegin\n\tselect FIRST_NAME, LAST_NAME, ACCOUNT_ID, AVAIL_BALANCE\n\tfrom deleted, CUSTOMER\n\twhere deleted.CUST_ID = CUSTOMER.CUST_ID\nend\n";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void ProcedureType_CompareDataPassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Procedure,
                Solution = "create proc Display_EmployeesDepartment @dept_Id int\nas\nbegin\n\tselect DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees \n\tfrom DEPARTMENT left join EMPLOYEE\n\ton DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID\n\twhere DEPARTMENT.DEPT_ID = @dept_Id\n\tgroup by DEPARTMENT.NAME\nend\n",
                Point = 1,
                TestQuery = "/* 0.5 */\n/* Test example in question */\nexecute Display_EmployeesDepartment 4\n\n/* 0.5 */\n/* Test another question */\nexecute Display_EmployeesDepartment 1",
            };
            string studentId = "Test";
            string answer =
                "create proc Display_EmployeesDepartment @dept_Id int\nas\nbegin\n\tselect DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees \n\tfrom DEPARTMENT left join EMPLOYEE\n\ton DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID\n\twhere DEPARTMENT.DEPT_ID = @dept_Id\n\tgroup by DEPARTMENT.NAME\nend\n";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void ProcedureType_CompareDataPassed_WithGoSyntax()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Procedure,
                Solution = "create proc Display_EmployeesDepartment @dept_Id int\nas\nbegin\n\tselect DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees \n\tfrom DEPARTMENT left join EMPLOYEE\n\ton DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID\n\twhere DEPARTMENT.DEPT_ID = @dept_Id\n\tgroup by DEPARTMENT.NAME\nend\n",
                Point = 1,
                TestQuery = "/* 0.5 */\n/* Test example in question */\nexecute Display_EmployeesDepartment 4\n\n/* 0.5 */\n/* Test another question */\nexecute Display_EmployeesDepartment 1",
            };
            string studentId = "Test";
            string answer =
                "GO\ncreate proc Display_EmployeesDepartment @dept_Id int\nas\nbegin\n\tselect DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees \n\tfrom DEPARTMENT left join EMPLOYEE\n\ton DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID\n\twhere DEPARTMENT.DEPT_ID = @dept_Id\n\tgroup by DEPARTMENT.NAME\nend\nGO";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void ProcedureType_CompareDataPassed_FailNumberOfTable()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.Procedure,
                Solution = "create proc Display_EmployeesDepartment @dept_Id int\nas\nbegin\n\tselect DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees \n\tfrom DEPARTMENT left join EMPLOYEE\n\ton DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID\n\twhere DEPARTMENT.DEPT_ID = @dept_Id\n\tgroup by DEPARTMENT.NAME\nend\n",
                Point = 1,
                TestQuery = "/* 0.5 */\n/* Test example in question */\nexecute Display_EmployeesDepartment 4\n\n/* 0.5 */\n/* Test another question */\nexecute Display_EmployeesDepartment 1",
            };
            string studentId = "Test";
            string answer =
                "create proc Display_EmployeesDepartment @dept_Id int\nas\nbegin\n\tselect DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees \n\tfrom DEPARTMENT left join EMPLOYEE\n\ton DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID\n\twhere DEPARTMENT.DEPT_ID = @dept_Id\n\tgroup by DEPARTMENT.NAME\n    select * from account\nend\n";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0,5";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void DmlType_CompareDataPassed()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.DML,
                Solution = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'",
                Point = 1,
                TestQuery = "/*0,5*/\n/*Verify Branch table*/\nselect * from Branch\n/*0,5*/\n/*Verify Account table*/\nselect * from Account",
            };
            string studentId = "Test";
            string answer = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "1";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void DmlType_CompareDataFail_FirstTcFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.DML,
                Solution = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'",
                Point = 1,
                TestQuery = "/*0,5*/\n/*Verify Branch table*/\nselect * from Branch\n/*0,5*/\n/*Verify Account table*/\nselect * from Account",
            };
            string studentId = "Test";
            string answer = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('5','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0,5";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void DmlType_CompareDataFail_SecondTcFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.DML,
                Solution = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'",
                Point = 1,
                TestQuery = "/*0,5*/\n/*Verify Branch table*/\nselect * from Branch\n/*0,5*/\n/*Verify Account table*/\nselect * from Account",
            };
            string studentId = "Test";
            string answer = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '2'";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0,5";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }

        [TestMethod]
        public void DmlType_CompareDataError_FirstTcFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                CandidateId = "",
                QuestionId = "",
                QuestionType = Candidate.QuestionTypes.DML,
                Solution = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'",
                Point = 1,
                TestQuery = "/*0,5*/\n/*Verify Branch table*/\nselect * from Branch\n/*0,5*/\n/*Verify Account table*/\nselect * from Account",
            };
            string studentId = "Test";
            string answer = "INSERT INTO [dbo].[BRANCH]([ADDRESSdfd],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('5','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'";
            int questionOrder = 0;
            if (string.IsNullOrEmpty(candidate.CandidateId) && string.IsNullOrEmpty(candidate.QuestionId))
            {
                // Act
                var actualRes = Grading.DmlSpTriggerType(
                    candidate,
                    studentId,
                    answer,
                    questionOrder,
                    _dbScript)["Point"];
                var expectedRes = "0";
                // Assert
                Assert.IsTrue(expectedRes.Equals(actualRes));
            }
        }

        [TestMethod]
        public void DmlType_CompareDataError_SecondTcFail()
        {
            // Arrange
            Candidate candidate = new Candidate()
            {
                QuestionType = Candidate.QuestionTypes.DML,
                Solution = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n     VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_ID = '1'",
                Point = 1,
                TestQuery = "/*0,5*/\n/*Verify Branch table*/\nselect * from Branch\n/*0,5*/\n/*Verify Account table*/\nselect * from Account",
            };
            string studentId = "Test";
            string answer = "INSERT INTO [dbo].[BRANCH]([ADDRESS],[CITY],[NAME],[STATE],[ZIP_CODE])\n    VALUES('1','1','1','1','1')\n\nDELETE FROM [dbo].[ACCOUNT]\n      WHERE ACCOUNT_fdfdID = '2'";
            int questionOrder = 0;

            // Act
            var actualRes = Grading.DmlSpTriggerType(
                candidate,
                studentId,
                answer,
                questionOrder,
                _dbScript)["Point"];
            var expectedRes = "0";
            // Assert
            Assert.IsTrue(expectedRes.Equals(actualRes));
        }
























    }
}