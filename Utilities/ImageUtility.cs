using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.Mvc;

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

        public static void ProcessAndSaveImage(HttpFileCollectionBase files, int newMovieID, ModelStateDictionary modelState, MovieDataAccess access)
        {
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                if (file != null && file.ContentLength > 0)
                {
                    // Validate file type
                    string[] allowedFileTypes = { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedFileTypes.Contains(fileExtension))
                    {
                        throw new Exception("Only JPG, JPEG, PNG, and GIF files are allowed.");
                    }

                    // Convert the content length to gigabytes
                    double contentLengthGB = file.ContentLength / (1024.0 * 1024.0 * 1024.0);

                    // Check if the file exceeds the maximum size (2 GB)
                    if (contentLengthGB > 2)
                    {
                        throw new Exception("The uploaded file size cannot exceed 2 GB.");
                    }

                    try
                    {
                        // Validate image dimensions
                        using (Image img = Image.FromStream(file.InputStream))
                        {
                            int maxWidth = 1920;  // Maximum width allowed
                            int maxHeight = 1080; // Maximum height allowed

                            if (img.Width > maxWidth || img.Height > maxHeight)
                            {
                                throw new Exception("The uploaded image dimensions exceed the maximum allowed size.");
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        throw new Exception("Error processing the uploaded image: " + ex.Message);
                    }

                    // Process and save the image...
                    byte[] imageData;
                    file.InputStream.Position = 0;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }

                    System.Diagnostics.Debug.WriteLine($"Image data: {BitConverter.ToString(imageData)}");
                    System.Diagnostics.Debug.WriteLine($"Content length: {file.ContentLength}");
                    System.Diagnostics.Debug.WriteLine($"Input stream length: {file.InputStream.Length}");




                    string mimeType = file.ContentType;

                    access.UploadMovieImages(newMovieID, imageData,mimeType);


                }
            }
        }


    }
}