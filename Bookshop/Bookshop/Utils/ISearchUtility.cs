using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop.Utils
{
    public interface ISearchUtility
    {
        bool IsInsensitiveString(string value, string filter);
    }
}