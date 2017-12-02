using System.Web;

namespace Bookshop.Utils
{
    public interface IImageFileUtility
    {
        string SaveImageFileInPath(HttpPostedFileBase file, string imageFolderUrl);
        void DeleteImageFromPath(string path, string defaultImagePath);
    }
}