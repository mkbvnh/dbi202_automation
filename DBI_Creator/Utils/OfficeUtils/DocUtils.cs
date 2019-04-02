using System;
using DBI202_Creator.Entities.Paper;
using Microsoft.Office.Interop.Word;

namespace DBI202_Creator.Utils.OfficeUtils
{
    internal static class DocUtils
    {
        /// <summary>
        ///     Setting for doc file
        /// </summary>
        /// <param name="document"></param>
        public static void SettingsPage(Document document)
        {
            try
            {
                foreach (Section section in document.Sections)
                    section.PageSetup.PaperSize = WdPaperSize.wdPaperA4;

                //1 inch = 72 points

                document.PageSetup.BottomMargin = 72;
                document.PageSetup.TopMargin = 72;
                document.PageSetup.LeftMargin = 72;
                document.PageSetup.RightMargin = 72;

                document.PageSetup.FooterDistance = 36;
                document.PageSetup.HeaderDistance = 36;

                document.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///     Saving file
        /// </summary>
        /// <param name="doc">Document want to save</param>
        /// <param name="path"></param>
        /// <param name="ei">ExamForDoc</param>
        public static void SavingDocFile(Document doc, string path, Paper exam)
        {
            try
            {
                doc.SaveAs(path + @"\" + exam.PaperNo, WdSaveFormat.wdFormatDocumentDefault);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///     Setting header and footer
        /// </summary>
        /// <param name="paper"></param>
        public static void SettingsHeaderAndFooter(Paper paper, Document doc)
        {
            try
            {
                foreach (Section wordSection in doc.Sections)
                {
                    var headerRange = wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Collapse(WdCollapseDirection.wdCollapseEnd);
                    if (paper != null)
                    {
                        var p1 = headerRange.Paragraphs.Add();
                        p1.Range.Text = "             Paper No: " + paper.PaperNo;
                    }

                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldNumPages);

                    var p4 = headerRange.Paragraphs.Add();
                    p4.Range.Text = " of ";
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}