using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSanitizer.BL
{
    public class SanitizedFileContentProvider : ISanitizedFileContentProvider
    {
        private readonly Dictionary<string, Func<string, string>> sanitizers = new()
        {
            {".abc", SanitizeAbc },
            {".efg", SanitizeEfg }
        };


        public string GetSanitizedFileContent(Stream input, string fileExtention)
        {
            return sanitizers[fileExtention](new StreamReader(input).ReadToEnd());
        }

        private static string SanitizeAbc(string arg)
        {
            if (!IsValidFile(arg, "123", "789", "A.C"))
                throw new Exception("Could not parse file");

            return Regex.Replace(arg, "A[^1-9]C", "A255C");
        }
        private static string SanitizeEfg(string arg)
        {
            if (!IsValidFile(arg, "123", "789", "E.G"))
                throw new Exception("Could not parse file");

            return Regex.Replace(arg, "E[^a-z]G", "E255G");
        }

        private static bool IsValidFile(string arg, string prefix, string postfix, string repeatingPattern)
        {
            return Regex.IsMatch(arg, $@"^{prefix}\s*({repeatingPattern}\s*)*\s*{postfix}$");
        }
    }
}
