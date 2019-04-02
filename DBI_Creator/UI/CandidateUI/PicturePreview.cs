using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DBI202_Creator.Utils;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class PicturePreview : Form
    {
        public PicturePreview(List<string> images)
        {
            InitializeComponent();
            Images = images;

            renderImages();
        }

        public List<string> Images { get; set; }

        private void renderImages()
        {
            tabControl.TabPages.Clear();
            for (var i = 0; i < Images.Count; i++)
            {
                var imgData = Images[i];
                var image = ImageUtils.Base64StringToImage(imgData);

                var pb = new PictureBox();
                pb.Dock = DockStyle.Fill;
                pb.Image = image;

                var tab = new TabPage("Image " + (i + 1));
                tab.AutoScroll = true;
                tab.Controls.Add(pb);

                tabControl.TabPages.Add(tab);
            }
        }

        private void renderTab(int i)
        {
            var imgData = Images[i];
            var image = ImageUtils.Base64StringToImage(imgData);

            var pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.Image = image;

            var tab = new TabPage("Image " + (i + 1));
            tab.AutoScroll = true;
            tab.Controls.Add(pb);

            tabControl.TabPages[i] = tab;
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            var selectedIndex = tabControl.SelectedIndex;
            if (selectedIndex != 0)
            {
                var temp = Images[selectedIndex - 1];
                Images[selectedIndex - 1] = Images[selectedIndex];
                Images[selectedIndex] = temp;

                //renderTab(selectedIndex - 1);
                //renderTab(selectedIndex);
                renderImages();
                tabControl.SelectedIndex = selectedIndex - 1;
            }
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            var selectedIndex = tabControl.SelectedIndex;
            if (selectedIndex != tabControl.TabPages.Count - 1)
            {
                var temp = Images[selectedIndex + 1];
                Images[selectedIndex + 1] = Images[selectedIndex];
                Images[selectedIndex] = temp;

                //renderTab(selectedIndex + 1);
                //renderTab(selectedIndex);
                renderImages();
                tabControl.SelectedIndex = selectedIndex + 1;
            }
        }
    }
}