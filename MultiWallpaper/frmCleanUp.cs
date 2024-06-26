﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            chkMove.Checked = true;
            chkMove2.Checked = true;
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
                stuff.SortImages2();
            }

            if (moveFiles)
            {
                stuff.SortImages();
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
