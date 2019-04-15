using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class InputScriptForm : Form
    {
        private readonly HandleClose _handleClose;

        public InputScriptForm(Func<List<string>, bool> handleClose, List<string> scriptList)
        {
            InitializeComponent();
            _handleClose = new HandleClose(handleClose);

            for (var i = 0; i < tabControl.TabPages.Count; i++)
            {
                var box = new RichTextBox
                {
                    Name = "scriptTextBox",
                    Dock = DockStyle.Fill
                };

                tabControl.TabPages[i].Controls.Add(box);
            }

            for (var i = 0; i < scriptList.Count && i < tabControl.TabPages.Count; i++)
                tabControl.TabPages[i].Controls["scriptTextBox"].Text = scriptList[i];
        }

        
        public void InputScriptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var scriptList = new List<string>();
            for (var index = 0; index < tabControl.TabPages.Count; index++)
            {
                var tab = tabControl.TabPages[index];
                var script = ((RichTextBox) tab.Controls["scriptTextBox"]).Text;
                scriptList.Add(script);
            }

            _handleClose(scriptList);
        }

        public void SaveBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private delegate bool HandleClose(List<string> scripts);
    }
}