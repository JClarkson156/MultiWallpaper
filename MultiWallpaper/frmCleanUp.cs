using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiWallpaper
{
    public partial class frmCleanUp : Form
    {
        public string strDirectory;

        public bool removeSmall = false;
        public bool removeDuplicate = false;
        public bool moveFiles = false;
        public bool moveFiles2 = false;

        private DateTime _prevDate;

        public bool ClosingForm { get; set; } = false;

        public frmCleanUp()
        {
            InitializeComponent();

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    strDirectory = fbd.SelectedPath;
                else
                    ClosingForm = true;
            }

            LoadData();

            chkMove.Checked = true;
            chkMove2.Checked = true;
        }

        public void LoadData()
        {
            var systemPath = System.Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
            );
            var complete = Path.Combine(systemPath, "files3.txt");
            DateTime prevDate = DateTime.Today;
            try
            {
                using (StreamReader iso = new StreamReader(complete))
                {
                    string dateString = iso.ReadToEnd();
                    DateTime.TryParse(dateString.Trim(), out prevDate);
                }
            }
            catch
            {
            }
            finally
            {
                _prevDate = prevDate;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var stuff = new CleanUp();
            if (removeSmall)
            {
                stuff.RemoveSmall(strDirectory);
            }

            if (removeDuplicate)
            {
                stuff.RemoveDuplicate(strDirectory);
            }

            if (moveFiles2)
            {
                stuff.SortImages2(_prevDate);
            }

            if (moveFiles)
            {
                stuff.SortImages(_prevDate);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void chkRemoveSmall_CheckedChanged(object sender, EventArgs e)
        {
            removeSmall = !removeSmall;
        }

        private void chkRemoveDuplicate_CheckedChanged(object sender, EventArgs e)
        {
            removeDuplicate = !removeDuplicate;
        }

        private void chkMove_CheckedChanged(object sender, EventArgs e)
        {
            moveFiles = !moveFiles;
        }

        private void chkMove2_CheckedChanged(object sender, EventArgs e)
        {
            moveFiles2 = !moveFiles2;
        }
    }
}
