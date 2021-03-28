using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MultiWallpaper
{
    class Window: ApplicationContext
    {
        public Window()
        {
            InitializeStrip();
            InitializeContext();

            Storage store = new Storage();

            string[] arrFolders = new string[0];
            if (store.Load() == true)
                arrFolders = store.Folders.Split(',');

            if (arrFolders[arrFolders.Length - 1].Length == 0)
                arrFolders = arrFolders.Take(arrFolders.Length - 1).ToArray();

            if(arrFolders.Length > 0)
                directory = new MultiWallpaper.Directories(arrFolders);

            store = null;        
        }

        private ContextMenuStrip menu;
        private Directories directory = null;

        private void InitializeContext()
        {
            Container components = new System.ComponentModel.Container();
            NotifyIcon notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = menu,
                Icon = MultiWallpaper.Properties.Resources.Icon,
                Text = "Wallpaper Tool",
                Visible = true
            };
        }

        private void InitializeStrip()
        {
            menu = new ContextMenuStrip();

            ToolStripMenuItem SetFolder = new ToolStripMenuItem();
            SetFolder.Text = "Set Folder";
            SetFolder.Click += SetFolder_Click;

            ToolStripMenuItem Change = new ToolStripMenuItem();
            Change.Text = "Change";
            Change.Click += Change_Click;

            ToolStripMenuItem CleanUp = new ToolStripMenuItem();
            CleanUp.Text = "Clean up";
            CleanUp.Click += CleanUp_Click;

            ToolStripMenuItem Exit = new ToolStripMenuItem();
            Exit.Text = "Exit";
            Exit.Click += Exit_Click;

            menu.Items.Add(SetFolder);
            menu.Items.Add(CleanUp);
            menu.Items.Add(Change);
            menu.Items.Add(Exit);
        }

        private void CleanUp_Click(object sender, EventArgs e)
        {
            frmCleanUp clean = new frmCleanUp();

            DialogResult dr = clean.ShowDialog();

            clean.Dispose();
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if(directory != null)
                directory.Change();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.ExitThread();
            this.ExitThreadCore();
        }

        private void SetFolder_Click(object sender, EventArgs e)
        {
            frmMain main = new frmMain();

            DialogResult dr = main.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (directory == null)
                {
                    directory = new Directories(main.getFolders);
                }
                else
                {
                    directory.Reset(main.getFolders);
                }
            }

            main.Dispose();
        }
    }
}
