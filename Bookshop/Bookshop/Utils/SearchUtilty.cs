using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop.Utils
{
    public class SearchUtilty : ISearchUtility
    {
        public bool IsInsensitiveString(string value, string filter)
        {
            return value.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) != -1;
        }
    }
}