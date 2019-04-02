using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBI_Exam_Creator_Tool.Entities;
using DBI_Exam_Creator_Tool.Commons;

namespace DBI_Exam_Creator_Tool.UI
{
    public partial class RequirementForm : Form
    {
        public Requirement Requirement { get; set; }

        private Candidate.QuestionTypes QuestionType = Candidate.QuestionTypes.Select;
        private bool isNewReq = false;

        private delegate bool HandleDispose(Requirement req, bool isNewReq, bool saved);
        private HandleDispose handleDispose;

        public RequirementForm()
        {
            InitializeComponent();
        }

        public RequirementForm(Requirement requirement, Candidate.QuestionTypes questionType, bool _isNewReq, Func<Requirement, bool, bool, bool> _handleDispose)
        {
            InitializeComponent();
            Requirement = copyRequirement(requirement);
            this.QuestionType = questionType;
            this.isNewReq = _isNewReq;
            this.handleDispose = new HandleDispose(_handleDispose);

            // Default state.
            requireSortCheckBox.Enabled = true;
            checkEffectQueryTxt.Enabled = false;

            // Data Bindings.
            Dictionary<string, Requirement.RequirementTypes> dict = Constants.GetRequirementTypes(QuestionType);
            typeComboBox.DataSource = new BindingSource(dict, null);
            typeComboBox.DisplayMember = "Key";
            typeComboBox.ValueMember = "Value";
            typeComboBox.DataBindings.Add("SelectedValue", Requirement, "Type");

            requireSortCheckBox.DataBindings.Add("Checked", Requirement, "RequireSort");

            checkEffectQueryTxt.DataBindings.Add("Text", Requirement, "CheckEffectQuery");

            // Trigger comboBox SelectedvalueChanged event
            typeComboBox_SelectedValueChanged(typeComboBox, null);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirm Save", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Save.
                handleDispose(Requirement, isNewReq, true);
                this.Dispose();
            }
            else
            {
                // Do nothing.
            }
        }

        private Requirement copyRequirement(Requirement req)
        {
            return new Requirement(req.RequirementId, req.CandidateId, req.Type, req.RequireSort, req.CheckEffectQuery);
        }

        private void RequirementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not save changes.
            handleDispose(Requirement, isNewReq, false);
        }
        
        private void typeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (typeComboBox.SelectedValue)
            {
                case (Requirement.RequirementTypes.ResultSet):
                    requireSortCheckBox.Enabled = true;
                    checkEffectQueryTxt.Enabled = false;
                    break;
                case (Requirement.RequirementTypes.Effect):
                    requireSortCheckBox.Enabled = false;
                    checkEffectQueryTxt.Enabled = true;
                    break;
                case (Requirement.RequirementTypes.Parameter):
                    requireSortCheckBox.Enabled = false;
                    checkEffectQueryTxt.Enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
