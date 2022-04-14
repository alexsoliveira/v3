using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Templates.Interfaces;

namespace TGS.Cartorio.Application.Templates
{
    public class TemplateReader : ITemplateReader
    {
        public async Task<string> Read(string path, Dictionary<string, string> replaces)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    return string.Empty;

                string template = null;
                using (var sr = new StreamReader(path))
                {
                    template = await sr.ReadToEndAsync();
                }

                if (string.IsNullOrEmpty(template))
                    return string.Empty;

                foreach (var replace in replaces)
                    template = template.Replace(replace.Key, replace.Value);

                return template;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string,string> CreateReplaceDictionary(KeyValuePair<string,string>[] keyValuePairs)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                foreach (var keyValuePair in keyValuePairs)
                    dic.Add(keyValuePair.Key, keyValuePair.Value);

                return dic;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static KeyValuePair<string, string> CreateKeyValue(string key, string value)
        {
            try
            {
                return new KeyValuePair<string, string>(key, value);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
