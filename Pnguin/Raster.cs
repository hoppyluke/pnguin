using System.Collections;

namespace Pnguin;

///<summary>
/// Represents an image as a rectangular array of RGBA pixels.
///</summary>
public class Raster : IEnumerable<TrueColour>
{
	private readonly TrueColour[] _pixels;

    public int Width { get; }
    public int Height { get; }

	///<summary>
	/// Gets or sets the pixel value at the specified coordinates.
	/// (0,0) is the top left pixel of the image.
	///</summary>
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

	///<summary>
	/// Creates a new raster image with the specified dimensions.
	///</summary>
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

	///<summary>
	/// Fills the entire image with the specified colour.
	///</summary>
	public void Fill(TrueColour colour) => Array.Fill(_pixels, colour);

	///<summary>
	/// Calculates the index into the internal 1D array of the specified coordinates.
	///</summary>	
	private int Index(int x, int y) => (y * Width) + x;
	
}
