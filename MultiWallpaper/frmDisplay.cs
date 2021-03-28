using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MultiWallpaper
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            m_Store = new Storage();

            string strFolders = "";

            if (m_Store.Load() == true)
                strFolders = m_Store.Folders;

            m_arrFolders = new List<string>();

            if (strFolders.Length > 0)
            {
                m_arrFolders.AddRange(strFolders.Split(','));
                for (int i = 0; i < m_arrFolders.Count; i++)
                {
                    this.lstFolders.Items.Add(m_arrFolders[i]);
                }
            }
        }

        private List<string> m_arrFolders;
        private Storage m_Store;

        public string[] getFolders
        {
            get { return m_arrFolders.ToArray(); }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fbdBrowse.ShowDialog())
            {
                this.m_arrFolders.Add(fbdBrowse.SelectedPath);
                this.lstFolders.Items.Add(fbdBrowse.SelectedPath);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            m_Store.Folders = String.Join(", ", m_arrFolders.ToArray());
            m_Store.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RemoveSelected(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(lstFolders);
            selectedItems = lstFolders.SelectedItems;

            if (lstFolders.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                    lstFolders.Items.Remove(selectedItems[i]);
            }
        }
    }
}
