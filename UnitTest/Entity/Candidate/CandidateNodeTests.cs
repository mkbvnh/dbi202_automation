using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using dbi_grading_module.Entity.Candidate;

namespace UnitTest.Entity.Candidate
{
    [TestClass]
    public class CandidateNodeTests
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

        private CandidateNode CreateCandidateNode()
        {
            return new CandidateNode();
        }

        [TestMethod]
        public void AddPath_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateCandidateNode();
            CandidateNode node = new CandidateNode()
            {
                Candidate = new dbi_grading_module.Entity.Candidate.Candidate(),
                Children = new List<CandidateNode>(),
                Paths = new List<List<CandidateNode>>()
            };
            List<CandidateNode> candidatesPath = new List<CandidateNode>();

            // Act
            unitUnderTest.AddPath(
                node,
                candidatesPath);

            // Assert
            Assert.IsNotNull(unitUnderTest);
        }
    }
}
