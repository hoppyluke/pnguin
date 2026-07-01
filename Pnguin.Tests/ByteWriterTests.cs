namespace Pnguin.Tests;

public class ByteWriterTests
{
	[Fact]
	public void WriteValidatesOffset()
	{
		var a = new byte[4];
		uint v = 123;

		Assert.Throws<IndexOutOfRangeException>(() => ByteWriter.Write(v, a, 2));
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(uint.MaxValue)]
	[InlineData(1234567890)]
	public void WriteUsesNetworkByteOrder(uint value)
	{
		var a = new byte[4];

		ByteWriter.Write(value, a, 0);

		var b = BitConverter.GetBytes(value);
		var msb = BitConverter.IsLittleEndian ? b[3] : b[0];

		Assert.Equal(msb, a[0]);
	}

	[Fact]
	public void WriteUsesOffset()
	{
		var a = new byte[6];

		ByteWriter.Write(uint.MaxValue, a, 1);

		Assert.Equal(0, a[0]);
		Assert.Equal(255, a[1]);
		Assert.Equal(255, a[2]);
		Assert.Equal(255, a[3]);
		Assert.Equal(255, a[4]);
		Assert.Equal(0, a[5]);
		
	}

	[Fact]
	public void WritePackedBytesValidatesOffset()
	{
		var a = new byte[4];
		var  v = new PackedBytes(1, 2, 3, 4);

		Assert.Throws<IndexOutOfRangeException>(() => ByteWriter.Write(v, a, 1));
	}

	[Fact]
	public void WritePackedBytes()
	{
		var a = new byte[8];
		var v = new PackedBytes(1, 2, 3, 4);

		ByteWriter.Write(v, a, 2);

		Assert.Equal(1, a[2]);
		Assert.Equal(2, a[3]);
		Assert.Equal(3, a[4]);
		Assert.Equal(4, a[5]);
	}
}
