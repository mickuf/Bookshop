using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshop.Utils
{
    public class ImageFileUtility : IImageFileUtility
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string SaveImageFileInPath(HttpPostedFileBase file, string imageFolderUrl)
        {
            string imagePath;

            try
            {
                if (file != null && file.ContentLength > 0 && imageFolderUrl != null)
                {
                    Log.Debug("Image file exist");

                    string fileName = Path.GetFileName(file.FileName);
                    string guid = Guid.NewGuid().ToString();
                    string wholeImagePath = Path.Combine(HttpContext.Current.Server.MapPath(imageFolderUrl), guid + fileName);
                    file.SaveAs(wholeImagePath);

                    Log.DebugFormat("Image file saved in: {0}", wholeImagePath);

                    imagePath = imageFolderUrl + guid + fileName;

                    return imagePath;
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Save image file failed!", ex);
            }

            Log.Debug("Image file not exist, used default image file");

            imagePath = imageFolderUrl + "default.png";
            return imagePath;
        }

        public void DeleteImageFromPath(string path, string defaultImagePath)
        {
            try
            {
                string pathToDelete = Path.Combine(HttpContext.Current.Server.MapPath(path));

                Log.DebugFormat("Image file path to delete: {0}", pathToDelete);

                if (File.Exists(pathToDelete) && !pathToDelete.Contains(defaultImagePath))
                {
                    File.Delete(pathToDelete);
                    Log.Debug("Image file deleted");
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Delete image file failed!", ex);
            }

        }
    }
}