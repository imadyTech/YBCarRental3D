using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_DataBasis
    {
        public int Id = -1;
        public char persistentSeparator = ';';

        protected string serializedString;
        protected Dictionary<string, string> stringPairsMap;

        public YB_DataBasis()
        {
            stringPairsMap = new Dictionary<string, string>();
        }


        public bool HasValue(string key)
        {
            return stringPairsMap.ContainsKey(key);
        }
        public string FindValue(string key)
        {
            try
            {
                return stringPairsMap[key];

            }
            catch
            {
                return string.Empty;
            }
        }

        public Dictionary<string, string> SplitLine(string line)
        {
            var list = line.Split(this.persistentSeparator);
            foreach (var phrase in list)
            {
                if (!string.IsNullOrEmpty(phrase))
                {
                    int colonIndex = phrase.IndexOf(':');
                    var key = phrase.Substring(0, colonIndex);
                    var value = phrase.Substring(colonIndex + 1);
                    stringPairsMap.Add(key, value);
                }
            }
            return stringPairsMap;
        }
    }
}
