using System;
using System.Drawing;
using System.Windows.Forms;
using dbi_grading_module.Entity;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Utils;
using DBI202_Creator.Commons;
using DBI202_Creator.Utils;
using System.Diagnostics.CodeAnalysis;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class CandidatePanel : UserControl
    {
        private readonly HandleDelete _handleDelete;

        public CandidatePanel(Candidate candidate, Func<Candidate, TabPage, bool> handleDelete)
        {
            InitializeComponent();
            Candidate = candidate;
            OnCreate();
            _handleDelete = new HandleDelete(handleDelete);
        }

        public Candidate Candidate { get; set; }

        // Bind Candidate data to controls.
        private void OnCreate()
        {
            questionTypeComboBox.DataSource = new BindingSource(Constants.QuestionTypes(), null);
            questionTypeComboBox.DisplayMember = "Key";
            questionTypeComboBox.ValueMember = "Value";

            questionTypeComboBox.DataBindings.Add("SelectedValue", Candidate, "QuestionType", true,
                DataSourceUpdateMode.OnPropertyChanged);
            contentTxt.DataBindings.Add("Text", Candidate, "QuestionRequirement", true,
                DataSourceUpdateMode.OnPropertyChanged);

            solutionTxt.DataBindings.Add("Text", Candidate, "Solution", true, DataSourceUpdateMode.OnPropertyChanged);
            testQueryTxt.DataBindings.Add("Text", Candidate, "TestQuery", true, DataSourceUpdateMode.OnPropertyChanged);

            requireSortCheckBox.DataBindings.Add("Checked", Candidate, "RequireSort", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkColumnNameCheckbox.DataBindings.Add("Checked", Candidate, "CheckColumnName", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkDistinctCheckbox.DataBindings.Add("Checked", Candidate, "CheckDistinct", true,
                DataSourceUpdateMode.OnPropertyChanged);

            // Images.
            if (Candidate.Illustration.Count != 0)
            {
                imgPreview.Text = @"Preview";
                imgPreview.Visible = true;
            }

            // Trigger questionTypeComboBox SelectedValueChanged event
            QuestionTypeComboBox_SelectedValueChanged(questionTypeComboBox, null);
        }

        [ExcludeFromCodeCoverage]
        // Browse Images.
        private void BrowseImgBtn_Click(object sender, EventArgs e)
        {
            Candidate.Illustration.Clear();
            browseImgDialog.InitialDirectory = "C:\\";
            browseImgDialog.Filter = @"Image Files|*.jpg;*.jpeg;*.png;";
            browseImgDialog.Multiselect = true;
            if (browseImgDialog.ShowDialog() == DialogResult.OK)
                foreach (var fileName in browseImgDialog.FileNames)
                {
                    // Get the path of specified file
                    var filePath = fileName;

                    var img = Image.FromFile(filePath);
                    if (img.Width > Constants.Size.IMAGE_WIDTH)
                        img = ImageUtils.ResizeImage(img, Constants.Size.IMAGE_WIDTH);

                    var base64Data = ImageUtils.ImageToBase64(img);

                    Candidate.Illustration.Add(base64Data);
                    imgPreview.Text = @"Preview";
                    var tt = new ToolTip();
                    tt.SetToolTip(imgPreview, "Click to preview");
                }
        }

        // Preview Images.
        [ExcludeFromCodeCoverage]
        protected virtual void ImgPreview_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var preview = new PicturePreview(Candidate.Illustration) {Visible = true};
            preview.Show();
        }

        // Delete current Candidate from Question
        // Close current Tab.
        [ExcludeFromCodeCoverage]
        public void DeleteCandidateBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to remove candidate?", "Remove Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            if (result == DialogResult.Yes)
            {
                _handleDelete(Candidate, (TabPage)Parent);
            }
        }

        private void QuestionTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (questionTypeComboBox.SelectedValue)
            {
                case Candidate.QuestionTypes.Select:
                    SelectState();
                    break;
                case Candidate.QuestionTypes.Procedure:
                    ProcedureState();
                    break;
                case Candidate.QuestionTypes.Trigger:
                    TriggerState();
                    break;
                case Candidate.QuestionTypes.Schema:
                    SchemaState();
                    break;
                case Candidate.QuestionTypes.DML:
                    DmlState();
                    break;
                default:
                    return;
            }
        }

        private void SelectState()
        {
            testQueryTxt.Enabled = true;

            requireSortCheckBox.Visible = true;

            requireSortCheckBox.Checked = false;

            checkColumnNameCheckbox.Visible = true;

            checkColumnNameCheckbox.Checked = false;

            checkDistinctCheckbox.Visible = true;

            checkDistinctCheckbox.Checked = false;
        }

        public void ProcedureState()
        {
            testQueryTxt.Enabled = true;

            requireSortCheckBox.Visible = false;

            requireSortCheckBox.Checked = false;

            checkColumnNameCheckbox.Visible = false;

            checkColumnNameCheckbox.Checked = false;

            checkDistinctCheckbox.Visible = false;

            checkDistinctCheckbox.Checked = false;
        }

        public void TriggerState()
        {
            testQueryTxt.Enabled = true;

            requireSortCheckBox.Visible = false;

            requireSortCheckBox.Checked = false;

            checkColumnNameCheckbox.Visible = false;

            checkColumnNameCheckbox.Checked = false;

            checkDistinctCheckbox.Visible = false;

            checkDistinctCheckbox.Checked = false;
        }

        public void DmlState()
        {
            testQueryTxt.Enabled = true;

            requireSortCheckBox.Visible = false;

            requireSortCheckBox.Checked = false;

            checkColumnNameCheckbox.Visible = false;

            checkColumnNameCheckbox.Checked = false;

            checkDistinctCheckbox.Visible = false;

            checkDistinctCheckbox.Checked = false;
        }

        public void SchemaState()
        {
            testQueryTxt.Enabled = false;

            requireSortCheckBox.Visible = false;

            requireSortCheckBox.Checked = false;

            checkColumnNameCheckbox.Visible = false;

            checkColumnNameCheckbox.Checked = false;

            checkDistinctCheckbox.Visible = false;

            checkDistinctCheckbox.Checked = false;
        }

        public void CandidatePanel_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill; //Fill Usercontrol within the his parent layout
        }

        public void InsertTcBtn_Click(object sender, EventArgs e)
        {
            var testCase = new TestCase();

            var tcDialog = new TestCaseDialog(testCase, tc =>
            {
                testQueryTxt.AppendText(tc.ToString());
                return true;
            }) {Visible = true};

            tcDialog.Show();
        }

        public void ValidateTcBtn_Click(object sender, EventArgs e)
        {
            var testCases = StringUtils.GetTestCases(testQueryTxt.Text, Candidate);
            var mess = "";
            var countTc = 0;
            double rate = 0;
            foreach (var testCase in testCases)
            {
                mess += "TC " + ++countTc + ":" + "[" + testCase.RatePoint + "] - " + testCase.Description + "\n" +
                        testCase.TestQuery + "\n";
                rate += testCase.RatePoint;
            }

            if (rate < 1)
                mess += "WARNING: Total RatePoint is " + rate;
            MessageBox.Show(this, mess);
        }

        private delegate bool HandleDelete(Candidate c, TabPage tp);
    }
}