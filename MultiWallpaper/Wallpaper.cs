/*********************************** Module Header ***********************************\
Module Name:  Wallpaper.cs
Project:      CSSetDesktopWallpaper
Copyright (c) Microsoft Corporation.

The file defines a wallpaper helper class.

    Wallpaper.SetDesktopWallpaper(string path, WallpaperStyle style)

This is the key method that sets the desktop wallpaper. The method body is composed of 
configuring the wallpaper style in the registry and setting the wallpaper with 
SystemParametersInfo.

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\*************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Windows.Forms;


namespace MultiWallpaper
{
    public static class Wallpaper
    {
        /// <summary>
        /// Determine if .jpg files are supported as wallpaper in the current 
        /// operating system. The .jpg wallpapers are not supported before 
        /// Windows Vista.
        /// </summary>
        public static bool SupportJpgAsWallpaper
        {
            get
            {
                return (Environment.OSVersion.Version >= new Version(6, 0));
            }
        }


        /// <summary>
        /// Determine if the fit and fill wallpaper styles are supported in 
        /// the current operating system. The styles are not supported before 
        /// Windows 7.
        /// </summary>
        public static bool SupportFitFillWallpaperStyles
        {
            get
            {
                return (Environment.OSVersion.Version >= new Version(6, 1));
            }
        }


        /// <summary>
        /// Set the desktop wallpaper.
        /// </summary>
        /// <param name="path">Path of the wallpaper</param>
        /// <param name="style">Wallpaper style</param>
        public static void SetDesktopWallpaper(string[] arrScreens)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            key.SetValue(@"WallpaperStyle", "0");
            key.SetValue(@"TileWallpaper", "1");
            key.Close();

            var ImageSize = SystemInformation.VirtualScreen;
            var arrPhysScreens = Screen.AllScreens;

            var StartX = ImageSize.X;
            var StartY = ImageSize.Y;

            var store = new Storage();

            Image test = null;

            Bitmap resultImage = new Bitmap(ImageSize.Width, ImageSize.Height, PixelFormat.Format24bppRgb);
            Graphics grp = Graphics.FromImage(resultImage);
            grp.CompositingMode = CompositingMode.SourceCopy;
            grp.CompositingQuality = CompositingQuality.HighQuality;
            grp.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grp.SmoothingMode = SmoothingMode.HighQuality;
            grp.PixelOffsetMode = PixelOffsetMode.HighQuality;
            
            grp.FillRectangle(Brushes.Black, 0, 0, ImageSize.Width, ImageSize.Height);

            for (int i = 0; i < arrPhysScreens.Length; i++)
            {
                test = Image.FromFile(arrScreens[i], true);

                float Ratio = (float)test.Width / (float)test.Height;
                float Ratio2 = (float)arrPhysScreens[i].Bounds.Width / (float)arrPhysScreens[i].Bounds.Height;

                if (Ratio2 >= Ratio)
                {
                    if (arrPhysScreens[i].Bounds.Height != test.Height)
                    {
                        float newWidth = arrPhysScreens[i].Bounds.Height * Ratio;

                        //store.SaveLog(Ratio, Ratio2, test.Width, test.Height, arrPhysScreens[i].Bounds.Width, arrPhysScreens[i].Bounds.Height, newWidth, arrPhysScreens[i].Bounds.Height);
                        grp.DrawImage(test, -StartX + arrPhysScreens[i].Bounds.X + (arrPhysScreens[i].Bounds.Width / 2) - (newWidth / 2), -StartY + arrPhysScreens[i].Bounds.Y, newWidth, arrPhysScreens[i].Bounds.Height);
                    }
                    else
                    {
                        //store.SaveLog(Ratio, Ratio2, test.Width, test.Height, arrPhysScreens[i].Bounds.Width, arrPhysScreens[i].Bounds.Height, 0, 0);
                        grp.DrawImage(test, -StartX + arrPhysScreens[i].Bounds.X + (arrPhysScreens[i].Bounds.Width / 2) - (test.Width / 2), -StartY + arrPhysScreens[i].Bounds.Y, test.Width, test.Height);
                    }
                }
                else
                {
                    if (arrPhysScreens[i].Bounds.Width != test.Width)
                    {
                        float newHeight = arrPhysScreens[i].Bounds.Width / Ratio;

                        //store.SaveLog(Ratio, Ratio2, test.Width, test.Height, arrPhysScreens[i].Bounds.Width, arrPhysScreens[i].Bounds.Height, arrPhysScreens[i].Bounds.Width, newHeight);
                        grp.DrawImage(test, -StartX + arrPhysScreens[i].Bounds.X, -StartY + arrPhysScreens[i].Bounds.Y + (arrPhysScreens[i].Bounds.Height / 2) - (newHeight / 2), arrPhysScreens[i].Bounds.Width, newHeight);
                    }
                    else
                    {
                        //store.SaveLog(Ratio, Ratio2, test.Width, test.Height, arrPhysScreens[i].Bounds.Width, arrPhysScreens[i].Bounds.Height, 0, 0);
                        grp.DrawImage(test, -StartX + arrPhysScreens[i].Bounds.X, -StartY + arrPhysScreens[i].Bounds.Y + (arrPhysScreens[i].Bounds.Height / 2) - (test.Height /2), test.Width, test.Height);
                    }
                }
            }
            
            string path = String.Format(@"{0}\Microsoft\Windows\Themes\{1}.bmp", 
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "test");
            resultImage.Save(path, ImageFormat.Bmp);

            grp.Dispose();
            if(test != null)
                test.Dispose();
            resultImage.Dispose();

            // Set the desktop wallpapaer by calling the Win32 API SystemParametersInfo 
            // with the SPI_SETDESKWALLPAPER desktop parameter. The changes should 
            // persist, and also be immediately visible.
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE))
            {
                throw new Win32Exception();
            }
        }

        private const uint SPI_SETDESKWALLPAPER = 20;
        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam,
            string pvParam, uint fWinIni);
    }
}