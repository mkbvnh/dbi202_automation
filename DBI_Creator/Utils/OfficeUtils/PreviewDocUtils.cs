using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using DBI202_Creator.Entities.Candidate;
using DBI202_Creator.Entities.Question;
using Microsoft.Office.Interop.Word;

namespace DBI202_Creator.Utils.OfficeUtils
{
    internal class PreviewDocUtils
    {
        public static void PreviewCandidatePackage(QuestionSet questionSet)
        {
            Application wordApp = null;
            try
            {
                wordApp = new Application();
                //Init a Document in Word application
                wordApp.Visible = true;
                object missing = Missing.Value;
                var doc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                //Settings Page
                DocUtils.SettingsPage(doc);

                //Insert Header and Footer of the page
                DocUtils.SettingsHeaderAndFooter(null, doc);

                //Insert QuestionRequirement of the Exam
                for (var i = 0; i < questionSet.QuestionList.Count; i++)
                {
                    var section = i == 0 ? doc.Sections.First : doc.Sections.Add();
                    for (var j = 0; j < questionSet.QuestionList.ElementAt(i).Candidates.Count; j++)
                        AppendSection(questionSet.QuestionList.ElementAt(i).Candidates.ElementAt(j), section, i + 1,
                            j + 1, ref missing);
                }
            }
            catch (Exception e)
            {
                wordApp?.Application.Quit(false);
                throw e;
            }
        }

        /// <summary>
        ///     Append QuestionRequirement of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="section"></param>
        private static void AppendSection(Candidate q, Section section, int questionNumber, int candidateNumber,
            ref object missing)
        {
            //Insert Title of question
            var paraTitle = section.Range.Paragraphs.Add(ref missing);
            paraTitle.Range.Text = "Question " + questionNumber + "." + candidateNumber + ":";
            paraTitle.Range.Font.Name = "Arial";
            paraTitle.Range.Font.Bold = 1;
            paraTitle.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
            paraTitle.Range.ParagraphFormat.LeftIndent = 0;
            paraTitle.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraTitle.Range.InsertParagraphAfter();

            //Insert question requirement
            if (!string.IsNullOrEmpty(q.QuestionRequirement))
            {
                var paraRequirement = section.Range.Paragraphs.Add(ref missing);
                q.QuestionRequirement = q.QuestionRequirement.Trim();
                //if (!q.QuestionRequirement.EndsWith(".")) q.QuestionRequirement = string.Concat(q.QuestionRequirement, ".");
                paraRequirement.Range.Text = q.QuestionRequirement;
                paraRequirement.Range.Font.Name = "Arial";
                paraRequirement.Range.Font.Bold = 0;
                paraRequirement.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                paraRequirement.Range.Font.Italic = 0;
                paraRequirement.Range.ParagraphFormat.LeftIndent = 0;
                paraRequirement.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                paraRequirement.Range.InsertParagraphAfter();
            }

            //Insert illustration images
            var images = q.Illustration;
            var i = 0;
            foreach (var image in images)
                if (ImageUtils.Base64StringToImage(image) != null)
                {
                    var img = ImageUtils.Base64StringToImage(image);
                    Image tempImg = new Bitmap(img);
                    var imageName = AppDomain.CurrentDomain.BaseDirectory + @"/tmpImg.bmp";
                    tempImg.Save(imageName);
                    var paraImage = section.Range.Paragraphs.Add(ref missing);
                    paraImage.Range.InlineShapes.AddPicture(imageName);
                    paraImage.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraImage.Range.ParagraphFormat.LeftIndent = 0;

                    var paraImageDescription = section.Range.Paragraphs.Add(ref missing);
                    paraImageDescription.Range.Text = "Picture " + questionNumber + "." + ++i + "";
                    paraImageDescription.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraImageDescription.Range.Font.Bold = 0;
                    paraImageDescription.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                    paraImageDescription.Range.ParagraphFormat.LeftIndent = 0;
                    paraImageDescription.Range.InsertParagraphAfter();
                }

            //Insert Solution query
            InsertBlock("Solution: ", q.Solution, true, section, ref missing);

            //Insert Test query
            InsertBlock("Test query: ", q.TestQuery, true, section, ref missing);
        }

        private static void InsertBlock(string title, string content, bool isQuery, Section section, ref object missing)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var paraTestQueryTitle = section.Range.Paragraphs.Add(ref missing);
                paraTestQueryTitle.Range.Text = title;
                paraTestQueryTitle.Range.Font.Name = "Arial";
                paraTestQueryTitle.Range.Font.Bold = 0;
                paraTestQueryTitle.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                paraTestQueryTitle.Range.Font.Italic = 1;
                paraTestQueryTitle.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                paraTestQueryTitle.Range.ParagraphFormat.LeftIndent = 0;
                paraTestQueryTitle.Range.InsertParagraphAfter();

                content = content.Trim();
                var paraTestQueryContent = section.Range.Paragraphs.Add(ref missing);
                if (isQuery)
                    content = SqlUtils.FormatSqlCode(content);
                paraTestQueryContent.Range.Font.Name = "Arial";
                paraTestQueryContent.Range.Font.Bold = 0;
                paraTestQueryContent.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                paraTestQueryContent.Range.Font.Italic = 0;
                paraTestQueryContent.Range.ParagraphFormat.LeftIndent = 36;
                paraTestQueryContent.Range.Text = content;
            }
        }
    }
}