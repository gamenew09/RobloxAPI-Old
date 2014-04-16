using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobloxAPI
{
    public class SearchQuery
    {

        public Dictionary<string, string> Arguments
        {
            get;
            private set;
        }

        public SearchQuery() { Arguments = new Dictionary<string, string>(); }

        public override string ToString()
        {
            string s = "";
            int i = 0;
            foreach (string _ in Arguments.Keys)
            {
                if (i != s.Length - 1)
                {
                    s += Arguments.Keys.ToArray()[i] + "=" + Arguments.Values.ToArray()[i] + "&";
                }
                else
                {
                    s += Arguments.Keys.ToArray()[i] + "=" + Arguments.Values.ToArray()[i];
                }
                i++;
            }
            if (s.ToCharArray().Last() == '&')
            {
                s.Remove(s.ToCharArray().Count() - 1, 1);
            }
            return s;
        }

    }
}
