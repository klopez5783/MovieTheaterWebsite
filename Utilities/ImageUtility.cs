using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace MovieTheater.Utilities
{
    public class ImageUtility
    {
        public static string GetImageType(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                using (var image = Image.FromStream(ms))
                {
                    return image.RawFormat.ToString();
                }
            }
        }
    }
}