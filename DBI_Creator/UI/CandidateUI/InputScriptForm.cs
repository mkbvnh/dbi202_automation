using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class InputScriptForm : Form
    {
        private readonly HandleClose handleClose;

        public InputScriptForm()
        {
            InitializeComponent();
        }

        public InputScriptForm(Func<List<string>, bool> _handleClose, List<string> scriptList)
        {
            InitializeComponent();
            handleClose = new HandleClose(_handleClose);

            for (var i = 0; i < tabControl.TabPages.Count; i++)
            {
                var box = new RichTextBox();
                box.Name = "scriptTextBox";
                box.Dock = DockStyle.Fill;

                tabControl.TabPages[i].Controls.Add(box);
            }

            for (var i = 0; i < scriptList.Count && i < tabControl.TabPages.Count; i++)
                tabControl.TabPages[i].Controls["scriptTextBox"].Text = scriptList[i];
        }

        private void InputScriptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var scriptList = new List<string>();
            foreach (TabPage tab in tabControl.TabPages)
            {
                var script = ((RichTextBox) tab.Controls["scriptTextBox"]).Text;
                scriptList.Add(script);
            }
            handleClose(scriptList);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private delegate bool HandleClose(List<string> scripts);
    }
}