using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace MultiWallpaper
{
    class Window: ApplicationContext
    {
        public Window()
        {
            Storage store = new Storage();

            string[] arrFolders = new string[0];
            //if (store.Load() == true)
            if (store.LoadData() == true)
                arrFolders = store.Folders.Split(',');

            if (arrFolders.Length > 0)
            {
                if (arrFolders[arrFolders.Length - 1].Length == 0)
                    arrFolders = arrFolders.Take(arrFolders.Length - 1).ToArray();

                    directory = new MultiWallpaper.Directories(arrFolders);
            }

            InitializeStrip();
            InitializeContext();

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

            ToolStripMenuItem Test = new ToolStripMenuItem();
            Test.Text = "View Current Images";

            if (directory != null)
            {
                for (int i = 0, len = directory.ScreenCount; i < len; i++)
                {
                    ToolStripMenuItem images = new ToolStripMenuItem();
                    images.Text = $"Image {i + 1}";
                    images.Click += OpenImage_Click;
                    Test.DropDownItems.Add(images);
                }
            }

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
            menu.Items.Add(Test);
            menu.Items.Add(CleanUp);
            menu.Items.Add(Change);
            menu.Items.Add(Exit);
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            var index = int.Parse(item.Text.Replace("Image ", ""));

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = directory.ImagesSetToScreens[index - 1]
            };
            Process.Start(startInfo);

            //Process.Start(directory.ImagesSetToScreens[index - 1]);
        }

        private void CleanUp_Click(object sender, EventArgs e)
        {
            frmCleanUp clean = new frmCleanUp();

            if (!clean.ClosingForm)
            {
                _ = clean.ShowDialog();
            }

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
