using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Wallpaper_Manager
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        UInt32 SPI_SETWALL = 0x14;
        UInt32 SPIF_UPDATE = 0x01;
        UInt32 SPIF_SWEDWINI = 0x2;

        OpenFileDialog op = new OpenFileDialog();
        string filepath;
        string standardPath = "D:\\";

        public Form1()
        {
            InitializeComponent();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                webBrowser.Url = new Uri(standardPath);
            }
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(op.ShowDialog() == DialogResult.OK)
            {
                filepath = op.FileName;
                Display.Image = Image.FromFile(filepath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SystemParametersInfo(SPI_SETWALL, 0, filepath, SPIF_UPDATE | SPIF_SWEDWINI);
        }


        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Pfad Wählen" }) 
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    webBrowser.Url = new Uri(fbd.SelectedPath);
                    txtPath.Text = fbd.SelectedPath;
                }
            };
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
                webBrowser.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoForward)
                webBrowser.GoForward();
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {                

            if (webBrowser.CanSelect)
                webBrowser.Select();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {

                webBrowser.Url = new Uri(txtPath.Text);
                fbd.SelectedPath = txtPath.Text;

            }
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {

                    webBrowser.Url = new Uri(txtPath.Text);
                    fbd.SelectedPath = txtPath.Text;

                }
            }
        }

        private void webBrowser_ControlAdded(object sender, ControlEventArgs e)
        {
            btnSelect.BackColor = Color.Aqua;
        }

        void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.Body.AttachEventHandler("ondblclick", Document_DoubleClick);
        }

        void Document_DoubleClick(object sender, EventArgs e) {
            btnSelect.BackColor = Color.Green;
        }

    }
}
