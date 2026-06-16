namespace Pnguin.Tests;

public class RasterTests
{
	[Theory]
	[InlineData(0, 1)]
	[InlineData(3, -1)]
	[InlineData(-1, 0)]
	[InlineData(0, 0)]
	public void ConstructorValidatesSize(int w, int h)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new Raster(w, h));
	}

	[Theory]
	[InlineData(-1, 1)]
	[InlineData(10, 1)]
	[InlineData(1, -2)]
	[InlineData(1, 10)]
	[InlineData(11, 11)]
	public void GetIndexIsValidated(int x, int y)
	{
		var r = new Raster(10, 10);

		Assert.Throws<IndexOutOfRangeException>(() => r[x, y]);
	}

	[Theory]
	[InlineData(-1, 1)]
	[InlineData(10, 1)]
	[InlineData(1, -2)]
	[InlineData(1, 10)]
	[InlineData(11, 11)]
	public void SetIndexIsValidated(int x, int y)
	{
		var r = new Raster(10, 10);

		Assert.Throws<IndexOutOfRangeException>(() => r[x, y] = new TrueColour(1, 2, 3, 255));
	}

	[Fact]
	public void SinglePixelIsAccessible()
	{
		var p = new TrueColour(0, 1, 2, 3);
		var r = new Raster(1, 1);

		r[0, 0] = p;

		var result = r[0, 0];

		Assert.Equal(p.ToString(), result.ToString());
	}

	[Theory]
	[InlineData(4, 0)]
	public void PixelsAreAcessible(int x, int y)
	{
		var r = new Raster(10, 5);
		r[x, y] = new TrueColour(0, 0, 0);

		Assert.Equal(TrueColour.Opaque, r[x, y].A);
	}

	[Fact]
	public void FillUpdatesAllPixels()
	{
		var fillColour = new TrueColour(255, 128, 0);
		var r = new Raster(5, 5);

		r.Fill(fillColour);

		Assert.All(r, p => Assert.Equal(fillColour, p));
	}
}
