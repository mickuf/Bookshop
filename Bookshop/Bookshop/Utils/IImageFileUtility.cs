using System.Web;

namespace Bookshop.Utils
{
    public interface IImageFileUtility
    {
        string SaveImageFileInPath(HttpPostedFileBase file);
        void DeleteImageFromPath(string path);
    }
}