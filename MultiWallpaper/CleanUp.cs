﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiWallpaper
{
    public  class CleanUp
    {
        public CleanUp()
        { }

        private  string[] Paths = new string[5] { ".bmp", ".jpg", ".jpeg", ".png", ".jfif" };

        public  void RemoveSmall(string directory)
        {
            FileSystemInfo[] infos = new DirectoryInfo(directory).GetFileSystemInfos();
            var stuff = RemoveSmallFiles(infos);

            foreach (var item in stuff)
            {
                (new FileInfo(item)).Delete();
            }
        }

        private  List<string> RemoveSmallFiles(FileSystemInfo[] infos)
        {
            var stuff = new List<string>();
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i] is DirectoryInfo)
                {
                    stuff.AddRange(RemoveSmallFiles(((DirectoryInfo)infos[i]).GetFileSystemInfos()));
                }
                else if (infos[i] is FileInfo)
                {
                    if (Paths.Contains((((FileInfo)infos[i]).Extension).ToLower()))
                    {
                        try
                        {
                            using (var img = Image.FromFile(infos[i].FullName))
                            {
                                if ((img.Height >= img.Width && img.Height < 600) ||
                                    (img.Width > img.Height && img.Width < 960))
                                {
                                    //(infos[i] as FileInfo).MoveTo("C:\\Users\\James\\Desktop\\Remove");
                                    //i--;
                                    stuff.Add(infos[i].FullName);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            return stuff;
        }

        public void RemoveDuplicate(string directory)
        {
            FileSystemInfo[] infos = new DirectoryInfo(directory).GetFileSystemInfos();
            List<string> arrFiles = new List<string>();
            arrFiles = CountFiles(infos);

            string str = "";
            for (var i = 0; i < arrFiles.Count; i++)
            {
                var name = arrFiles[i];
                var file = new FileInfo(name);
                str = file.Name.Replace(file.Extension, "");

                for (var g = i + 1; g < arrFiles.Count; g++)
                {
                    var name2 = arrFiles[g];

                    var file2 = new FileInfo(name2);
                    if (file2.Name.Contains(str) && file2.Name.Length <= file.Name.Length + 7)
                    {
                        var check = false;
                        using (var img1 = Image.FromFile(file.FullName))
                        using (var img2 = Image.FromFile(file2.FullName))
                        {
                            var byteArr1 = ImageToByteArray(img1);
                            var byteArr2 = ImageToByteArray(img2);

                            if (CleanUp.ByteArrayCompare(byteArr1, byteArr2))  //byteArr1.SequenceEqual(byteArr2))
                            {
                                check = true;
                            }
                        }

                        if (check)
                        {
                            file2.Delete();
                            arrFiles.RemoveAt(g);
                            g--;
                        }
                    }
                }
            }
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);

        static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.  
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }

        private List<string> CountFiles(FileSystemInfo[] infos)
        {
            List<string> files = new List<string>();
            Array.Sort<FileSystemInfo>(infos, delegate (FileSystemInfo a, FileSystemInfo b)
            {
                return a.CreationTime.CompareTo(b.CreationTime);
            });
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i] is DirectoryInfo)
                {
                    files.AddRange(CountFiles(((DirectoryInfo)infos[i]).GetFileSystemInfos()));
                }
                else if (infos[i] is FileInfo)
                {
                    if (Paths.Contains((((FileInfo)infos[i]).Extension).ToLower()))
                    {
                        files.Add(infos[i].FullName);
                    }
                }
            }
            return files;
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public void SortImages()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            folder += "\\Pictures";

            FileSystemInfo[] infos = new DirectoryInfo(folder + "\\Unsorted").GetFileSystemInfos();
            //FileSystemInfo[] infos = new DirectoryInfo(folder).GetFileSystemInfos();
            var arrFiles = CountFiles(infos);

            foreach (var fileName in arrFiles)
            {
                var file = new FileInfo(fileName);
                var toCreate = file.LastWriteTime.Year + "-" + file.LastWriteTime.Month;

                var dir = folder + "\\" + toCreate;

                CreateDirectory(dir);

                try
                {
                    if (!File.Exists(dir + "\\" + file.Name))
                        file.MoveTo(dir + "\\" + file.Name);
                    else
                    {
                        file.MoveTo(dir + "\\" + file.Name, true);
                        /*for (var i = 1; i <= 8; i++)
                        {
                            var fileName2 = dir + "\\" + file.Name;
                            fileName2 = fileName2.Replace(file.Extension, "(" + i + ")" + file.Extension);
                            if (!File.Exists(fileName2))
                                file.MoveTo(fileName2);
                        }*/
                    }
                }
                catch
                {
                }
            }
        }

        public void SortImages2()
        {

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            folder += "\\..\\SkyDrive camera roll";

            FileSystemInfo[] infos = new DirectoryInfo(folder).GetFileSystemInfos();

            var arrFiles = CountFiles(infos);


            foreach (var fileName in arrFiles)
            {
                var file = new FileInfo(fileName);
                int toCreate1 = file.LastWriteTime.Year;
                int toCreate2 = file.LastWriteTime.Month;

                var dir1 = folder + "\\" + toCreate1;
                var dir2 = folder + "\\" + toCreate1 + "\\" + (toCreate2 < 10 ? "0" + toCreate2 : toCreate2);

                CreateDirectory(dir1);
                CreateDirectory(dir2);

                try
                {
                    if (Path.GetFullPath(dir2 + "\\" + file.Name) == file.FullName) { }
                    else if (!File.Exists(dir2 + "\\" + file.Name))
                        file.MoveTo(dir2 + "\\" + file.Name);
                    else
                    {
                        file.Delete();
                    }
                }
                catch
                {
                }
            }
        }

        public bool CreateDirectory(string dir)
        {
            if (Directory.Exists(dir))
                return true;
            try
            {
                Directory.CreateDirectory(dir);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
