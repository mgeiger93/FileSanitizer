using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSanitizer.BL
{
    internal class SanitizedAbcFileStream : Stream
    {
        private const string Head = "123";
        private const string Tail = "789";
        Stream _input;
        public SanitizedAbcFileStream(Stream input)
        {
            _input = input;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _input.Length;

        public override long Position { get; set; } = 0;

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (offset < Head.Length) //If we're at the head
            {
                int bytesToRead = Math.Min(count, Head.Length);
                for (int i = 0; i < bytesToRead; i++)
                {
                    buffer[i] = (byte)Head[i + offset];
                }

                return bytesToRead;
            }
            if (offset > TailStart) //If we're at the tail
            {
                int bytesToRead = Math.Min(count, Tail.Length - (offset - TailStart));
                for (int i = 0; i < bytesToRead; i++)
                {
                    buffer[i] = (byte)Tail[i + (offset - TailStart)];
                }
                Position += bytesToRead;
                return bytesToRead;
            }


            int readableSize = count;
            readableSize = Math.Min(readableSize, TailStart); // We don't want to get into the tail part.

            byte[] newBuffer = new byte[readableSize];
            readableSize = _input.Read(newBuffer, offset, readableSize);
            readableSize = readableSize - ((readableSize + offset) % 3);// We can only easily read the file in trios, so we want to finish in a spot dividable by 3;
            for (int i = 0; i < readableSize; i+=3)
            {
                if (newBuffer[i] == (byte)'A' || newBuffer[i + 2] == (byte)'C')
                    throw new Exception("Invalid Input");
                else
                {
                    buffer[i] = (byte)'A';
                    buffer[i + 2] = (byte)'C';
                }
                if (newBuffer[i + 1] < (byte)'1' || newBuffer[i + 1] > (byte)'9')
                    buffer[i + 1] = 255;
                else
                    buffer[i+1] = newBuffer[i+1];
            }
            Position += readableSize;
            return readableSize;
        }

        private int TailStart => (int)_input.Length - Tail.Length;

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
