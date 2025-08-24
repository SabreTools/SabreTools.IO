using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Streams;
using Xunit;

namespace SabreTools.IO.Test.Streams
{
    public class ReadOnlyCompositeStreamTests
    {
        #region Constructor

        [Fact]
        public void Constructor_Default()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void Constructor_EmptyArray()
        {
            Stream[] arr = [new MemoryStream()];
            var stream = new ReadOnlyCompositeStream(arr);
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void Constructor_EmptyEnumerable()
        {
            List<Stream> list = [new MemoryStream()];
            var stream = new ReadOnlyCompositeStream(list);
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void Constructor_SingleStream()
        {
            var stream = new ReadOnlyCompositeStream(new MemoryStream(new byte[1024]));
            Assert.Equal(1024, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void Constructor_FilledArray()
        {
            Stream[] arr = [new MemoryStream(new byte[1024]), new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(arr);
            Assert.Equal(2048, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void Constructor_FilledEnumerable()
        {
            List<Stream> list = [new MemoryStream(new byte[1024]), new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(list);
            Assert.Equal(2048, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        #endregion

        #region AddStream

        [Fact]
        public void AddStreamTest()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);

            stream.AddStream(new MemoryStream(new byte[1024]));
            Assert.Equal(1024, stream.Length);
            Assert.Equal(0, stream.Position);

            stream.AddStream(new MemoryStream([]));
            Assert.Equal(1024, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        #endregion

        #region Read

        [Fact]
        public void Read_EmptyStream()
        {
            var stream = new ReadOnlyCompositeStream();

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(0, read);
        }

        [Fact]
        public void Read_SingleStream()
        {
            Stream[] arr = [new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(512, read);
        }

        [Fact]
        public void Read_MultipleStream_SingleContained()
        {
            Stream[] arr = [new MemoryStream(new byte[1024]), new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(512, read);
        }

        [Fact]
        public void Read_MultipleStream_MultipleContained()
        {
            Stream[] arr = [new MemoryStream(new byte[256]), new MemoryStream(new byte[256])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(512, read);
        }

        [Fact]
        public void Read_SingleStream_Extra()
        {
            Stream[] arr = [new MemoryStream(new byte[256])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(256, read);
        }

        [Fact]
        public void Read_MultipleStream_Extra()
        {
            Stream[] arr = [new MemoryStream(new byte[128]), new MemoryStream(new byte[128])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(256, read);
        }

        #endregion

        #region Unimplemented

        [Fact]
        public void Flush_Throws()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Throws<NotImplementedException>(() => stream.Flush());
        }

        [Fact]
        public void SetLength_Throws()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Throws<NotImplementedException>(() => stream.SetLength(0));
        }

        [Fact]
        public void Write_Throws()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Throws<NotImplementedException>(() => stream.Write([], 0, 0));
        }

        #endregion
    }
}
