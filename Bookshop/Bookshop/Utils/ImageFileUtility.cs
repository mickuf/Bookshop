using System;
using System.IO;
using System.Web;

namespace Bookshop.Utils
{
    public class ImageFileUtility : IImageFileUtility
    {
        private const string DefaultImagePath = "\\Content\\images\\default.png";
        private const string ImageFolderUrl = "~/Content/images/";
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string SaveImageFileInPath(HttpPostedFileBase file)
        {
            string imagePath;

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    Log.Debug("File exist");

                    string fileExtension = Path.GetExtension(file.FileName);

                    if ((fileExtension.Contains("png") || fileExtension.Contains("jpg") || fileExtension.Contains("jpeg")))
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string guid = Guid.NewGuid().ToString();
                        string wholeImagePath = Path.Combine(HttpContext.Current.Server.MapPath(ImageFolderUrl), guid + fileName);
                        file.SaveAs(wholeImagePath);

                        Log.DebugFormat("Image file saved in: {0}", wholeImagePath);

                        imagePath = ImageFolderUrl + guid + fileName;

                        return imagePath;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Save image file failed!", ex);
            }

            Log.Debug("Image file not exist, used default image file");

            imagePath = ImageFolderUrl + "default.png";
            return imagePath;
        }

        public void DeleteImageFromPath(string path)
        {
            try
            {
                string pathToDelete = Path.Combine(HttpContext.Current.Server.MapPath(path));

                Log.DebugFormat("Image file path to delete: {0}", pathToDelete);

                if (File.Exists(pathToDelete) && !pathToDelete.Contains(DefaultImagePath))
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