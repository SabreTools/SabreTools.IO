using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Streams;
using Xunit;

namespace SabreTools.IO.Test.Streams
{
    public class ReadOnlyCompositeStreamTests
    {
        [Fact]
        public void DefaultConstructorTest()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void EmptyArrayConstructorTest()
        {
            Stream[] arr = [new MemoryStream()];
            var stream = new ReadOnlyCompositeStream(arr);
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void EmptyEnumerableConstructorTest()
        {
            // Empty enumerable constructor
            List<Stream> list = [new MemoryStream()];
            var stream = new ReadOnlyCompositeStream(list);
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void SingleStreamConstructorTest()
        {
            var stream = new ReadOnlyCompositeStream(new MemoryStream(new byte[1024]));
            Assert.Equal(1024, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void FilledArrayConstructorTest()
        {
            Stream[] arr = [new MemoryStream(new byte[1024]), new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(arr);
            Assert.Equal(2048, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void FilledEnumerableConstructorTest()
        {
            List<Stream> list = [new MemoryStream(new byte[1024]), new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(list);
            Assert.Equal(2048, stream.Length);
            Assert.Equal(0, stream.Position);
        }
    
        [Fact]
        public void AddStreamTest()
        {
            var stream = new ReadOnlyCompositeStream();
            Assert.Equal(0, stream.Length);
            Assert.Equal(0, stream.Position);

            stream.AddStream(new MemoryStream(new byte[1024]));
            Assert.Equal(1024, stream.Length);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void EmptyStreamReadTest()
        {
            var stream = new ReadOnlyCompositeStream();

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(0, read);
        }

        [Fact]
        public void SingleStreamReadTest()
        {
            Stream[] arr = [new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(512, read);
        }

        [Fact]
        public void MultipleStreamSingleContainedReadTest()
        {
            Stream[] arr = [new MemoryStream(new byte[1024]), new MemoryStream(new byte[1024])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(512, read);
        }

        [Fact]
        public void MultipleStreamMultipleContainedReadTest()
        {
            Stream[] arr = [new MemoryStream(new byte[256]), new MemoryStream(new byte[256])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(512, read);
        }

        [Fact]
        public void SingleStreamExtraReadTest()
        {
            Stream[] arr = [new MemoryStream(new byte[256])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(256, read);
        }

        [Fact]
        public void MultipleStreamExtraReadTest()
        {
            Stream[] arr = [new MemoryStream(new byte[128]), new MemoryStream(new byte[128])];
            var stream = new ReadOnlyCompositeStream(arr);

            byte[] buf = new byte[512];
            int read = stream.Read(buf, 0, 512);

            Assert.Equal(256, read);
        }
    }
}