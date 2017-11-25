using System;

namespace Bookshop.Utils
{
    public class SearchUtilty : ISearchUtility
    {
        public bool IsInsensitiveString(string value, string filter)
        {
            if (String.IsNullOrEmpty(value) || String.IsNullOrEmpty(filter))
            {
                return false;
            }
            else
            {
                return value.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) != -1;
            }
        }
    }
}