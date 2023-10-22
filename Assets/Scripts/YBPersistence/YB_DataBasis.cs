using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_DataBasis
    {
        [JsonProperty]
        public int Id = -1;
        [JsonIgnore]
        public char persistentSeparator = ';';

        [JsonIgnore]
        protected string serializedString;
        [JsonIgnore]
        protected Dictionary<string, string> stringPairsMap;

        public YB_DataBasis()
        {
            stringPairsMap = new Dictionary<string, string>();
        }


        public bool HasValue(string key)
        {
            return stringPairsMap.ContainsKey(key);
        }
        /// <summary>
        /// get value from stringPairsMap (if data didn't go over a SplitLine process then will cause to an error)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
        public string FindFieldValue(string key)
        {
            try
            {
                var type = this.GetType();
                var field = type.GetField(key);
                return field.GetValue(this).ToString();
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
