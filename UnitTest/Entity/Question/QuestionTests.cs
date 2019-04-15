using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using dbi_grading_module.Entity.Candidate;

namespace UnitTest.Entity.Question
{
    [TestClass]
    public class QuestionTests
    {
        private MockRepository mockRepository;

        private Mock<List<dbi_grading_module.Entity.Candidate.Candidate>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockList = this.mockRepository.Create<List<dbi_grading_module.Entity.Candidate.Candidate>>();
            mockList.Object.Add(new dbi_grading_module.Entity.Candidate.Candidate()
            {
                Point = 0
            });
            double point = mockList.Object.First().Point;
            var questions = mockList.Object;
            dbi_grading_module.Entity.Question.Question q = new dbi_grading_module.Entity.Question.Question();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private dbi_grading_module.Entity.Question.Question CreateQuestion()
        {
            return new dbi_grading_module.Entity.Question.Question(
                "",
                0,
                this.mockList.Object);
        }
    }
}
