using System.Collections;

namespace Pnguin;

public class Raster : IEnumerable<TrueColour>
{
	private readonly TrueColour[] _pixels;

    public int Width { get; }
    public int Height { get; }

	public TrueColour this[int x, int y]
	{
		get
		{
			if (x < 0 || x >= Width)
				throw new IndexOutOfRangeException();

			if (y < 0 || y >= Height)
				throw new IndexOutOfRangeException();

			return _pixels[Index(x, y)];
		}

		set
		{
			if (x < 0 || x >= Width)
				throw new IndexOutOfRangeException();

			if (y < 0 || y >= Height)
				throw new IndexOutOfRangeException();

			_pixels[Index(x, y)] = value;
		}
	}

	public Raster(int width, int height)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(width, 1, nameof(width));
		ArgumentOutOfRangeException.ThrowIfLessThan(height, 1, nameof(height));
		
		Width = width;
		Height = height;

		_pixels = new TrueColour[width * height];
	}

	public IEnumerator<TrueColour> GetEnumerator() => ((IEnumerable<TrueColour>)_pixels).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => _pixels.GetEnumerator();

	public void Fill(TrueColour colour) => Array.Fill(_pixels, colour);
		
	private int Index(int x, int y) => (y * Width) + x;
	
}
