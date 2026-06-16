namespace Pnguin.Tests;

public class PackedBytesTests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(4)]
	public void IndexIsValidated(int index)
	{
		var b = new PackedBytes(0, 1, 2, 3);

		Assert.Throws<IndexOutOfRangeException>(() => b[index]);
	}

	[Theory]
	[InlineData(0, 0, 0, 0)]
	[InlineData(1, 2, 3, 4)]
	[InlineData(255, 255, 255, 255)]
	public void RoundTrip(byte b0, byte b1, byte b2, byte b3)
	{
		var b = new PackedBytes(b0, b1, b2, b3);

		Assert.Equal(b0, b[0]);
		Assert.Equal(b1, b[1]);
		Assert.Equal(b2, b[2]);
		Assert.Equal(b3, b[3]);
	}

	[Theory]
	[InlineData(0, 255, 128, 1)]
	public void ConstructorsAreEquivalent(byte b0, byte b1, byte b2, byte b3)
	{
		var x = new PackedBytes(b0, b1, b2, b3);
		var y = new PackedBytes([b0, b1, b2, b3]);

		Assert.Equal(x, y);
	}
}
