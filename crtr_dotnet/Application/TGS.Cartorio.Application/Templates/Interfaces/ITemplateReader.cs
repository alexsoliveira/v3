using System.Collections.Generic;
using System.Threading.Tasks;

namespace TGS.Cartorio.Application.Templates.Interfaces
{
    public interface ITemplateReader
    {
        Task<string> Read(string path, Dictionary<string, string> replaces);
        Dictionary<string, string> CreateReplaceDictionary(KeyValuePair<string, string>[] keyValuePairs);
    }
}
