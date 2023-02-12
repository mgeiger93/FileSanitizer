using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSanitizer.BL
{
    public class SanitizedFileSreamProvider : ISanitizedFileSreamProvider
    {
        private readonly Dictionary<string, Func<Stream, Stream>> streams = new()
        {
            {".abc", input => new SanitizedAbcFileStream(input) }
        };

        public Stream GetSanitizedStream(Stream stream, string fileExtention)
        {
            return streams[fileExtention](stream);
        }
    }
}
