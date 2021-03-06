﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using dbi_grading_module.Entity.Paper;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.Utils;
using DBI202_Creator.Utils.OfficeUtils;

namespace DBI202_Creator.Model
{
    [ExcludeFromCodeCoverage]
    public class PaperModel
    {
        public string Path { get; set; }
        public ShufflePaperModel Spm { get; set; }
        public string FirstPagePath { get; set; }

        public void CreatePaperDat()
        {
            //Remove Illustration in PaperSet
            var paperSet = Spm.PaperSet.CloneObjectSerializable<PaperSet>();

            //Saving Question Set
            var tmpQuestionSet = paperSet.QuestionSet.CloneObjectSerializable<QuestionSet>();

            foreach (var paper in paperSet.Papers)
                foreach (var candidate in paper.CandidateSet)
                    candidate.Illustration = new List<string>();
            //Adding Illustration into QuestionSet
            paperSet.QuestionSet = tmpQuestionSet;

            //Remove Duplicated database
            paperSet.QuestionSet.DBScriptList = new List<string>();

            //Create PaperSet
            SerializeUtils.SerializeObject(paperSet, Path + @"\PaperSet.dat");
        }

        public void CreateAll()
        {
            //Count PaperNo
            var countPaperNo = 0;
            //Write Paper Image
            try
            {
                foreach (var paper in Spm.PaperSet.Papers)
                {
                    var paperPath = FileUtils.CreateNewDirectory(Path, (++countPaperNo).ToString("D2"));

                    //Write DbScript
                    var givenPath = FileUtils.CreateNewDirectory(paperPath, "Given");

                    File.WriteAllText(givenPath + @"\DBscript" + ".sql", Spm.PaperSet.DBScriptList[0]);

                    //Create word file
                    ExportDocUtils.ExportDoc(FirstPagePath, paper, paperPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static int MaxNumberOfTests(List<Question> questionsBank)
        {
            var count = 1;
            foreach (var question in questionsBank)
            {
                if (question == null || question.Candidates.Count == 0)
                    continue;
                count *= question.Candidates.Count;
            }

            if (count < 1) count = 1;
            return count;
        }
    }
}