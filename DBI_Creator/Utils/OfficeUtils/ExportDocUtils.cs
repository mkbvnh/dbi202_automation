﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Entity.Paper;
using Microsoft.Office.Interop.Word;

namespace DBI202_Creator.Utils.OfficeUtils
{
    [ExcludeFromCodeCoverage]
    internal static class ExportDocUtils
    {
        /// <summary>
        ///     Export Doc in path
        /// </summary>
        /// <param name="firstPagePath"></param>
        /// <param name="paper"></param>
        /// <param name="path">Save to location</param>
        /// <returns></returns>
        public static bool ExportDoc(string firstPagePath, Paper paper, string path)
        {
            Application wordApp = null;
            try
            {
                //Start new Word Application
                wordApp = new Application
                {
                    Visible = false
                };
                object missing = Missing.Value;
                Document doc;

                //Merge First Page
                if (!string.IsNullOrEmpty(firstPagePath))
                {
                    doc = wordApp.Documents.Add(firstPagePath);
                    doc.Words.Last.InsertBreak(WdBreakType.wdPageBreak);
                }
                else
                {
                    doc = wordApp.Documents.Add(missing);
                }

                //Settings Page
                DocUtils.SettingsPage(doc);

                //Setings Header and Footer of the page
                DocUtils.SettingsHeaderAndFooter(paper, doc);

                //Insert QuestionRequirement of the Exam
                for (var i = 0; i < paper.CandidateSet.Count; i++)
                    AppendTestQuestion(paper.CandidateSet.ElementAt(i), doc, i + 1, ref missing);

                //Saving file
                DocUtils.SavingDocFile(doc, path, paper);
            }
            finally
            {
                wordApp?.Application.Quit(false);
            }
            return true;
        }

        /// <summary>
        ///     Append QuestionRequirement of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="doc"></param>
        /// <param name="questionNumber"></param>
        /// <param name="missing"></param>
        private static void AppendTestQuestion(Candidate q, _Document doc, int questionNumber, ref object missing)
        {
            var paraQuestionNumber = doc.Content.Paragraphs.Add(ref missing);
            var question = "Question " + questionNumber + ":";
            paraQuestionNumber.Range.Text = question;
            var questionNumberRange = doc.Range(paraQuestionNumber.Range.Start,
                paraQuestionNumber.Range.Start + question.Length);
            questionNumberRange.Font.Bold = 1;
            questionNumberRange.Font.Underline = WdUnderline.wdUnderlineSingle;

            paraQuestionNumber.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraQuestionNumber.Range.Font.Name = "Arial";

            paraQuestionNumber.Range.InsertParagraphAfter();

            var paraContent = doc.Content.Paragraphs.Add(ref missing);
            paraContent.Range.Font.Bold = 0;
            paraContent.Range.Font.Underline = WdUnderline.wdUnderlineNone;
            paraContent.Range.Text = q.QuestionRequirement;
            paraContent.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraContent.Range.InsertParagraphAfter();

            var images = q.Illustration;
            var i = 0;
            var imageName = AppDomain.CurrentDomain.BaseDirectory + @"/" + q.CandidateId + ".bmp";
            foreach (var image in images)
                if (ImageUtils.Base64StringToImage(image) != null)
                {
                    var img = ImageUtils.Base64StringToImage(image);
                    Image tempImg = new Bitmap(img);
                    tempImg.Save(imageName);
                    var paraImage = doc.Content.Paragraphs.Add(ref missing);
                    paraImage.Range.InlineShapes.AddPicture(imageName);
                    paraImage.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    var paraImageDescription = doc.Content.Paragraphs.Add(ref missing);
                    paraImageDescription.Range.Text = "Picture " + questionNumber + "." + ++i + "";
                    paraImageDescription.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraImageDescription.Range.InsertParagraphAfter();
                }

            if (File.Exists(imageName)) File.Delete(imageName);
        }
    }
}