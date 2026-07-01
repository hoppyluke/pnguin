using System.IO.Compression;
using Pnguin.Chinstrap;
using Pnguin.Humboldt;

namespace Pnguin;

///<summary>
/// Provides methods for working with PNG image files.
///</summary>
public class Png
{
	public static readonly byte[] Signature = [137, 80, 78, 71, 13, 10, 26, 10];
	public const int SignatureLength = 8;

	///<summary>
	/// Writes a raster image to a PNG file at the destination stream.
	///</summary>
	public static async Task WriteAsync(Raster image, Stream destination, CancellationToken cancellationToken = default)
	{
		// write file type signature
		await destination.WriteAsync(Signature, cancellationToken).ConfigureAwait(false);
		
		// write IHDR
		var header = new ImageHeader(
			(uint)image.Width,
			(uint)image.Height,
			8,
			ColourType.RgbAlpha,
			CompressionMethod.Deflate,
			FilterMethod.Adaptive,
			InterlaceMethod.None
		);

		await destination.WriteAsync(header.ToBytes(), cancellationToken).ConfigureAwait(false);

		// write IDAT
		await WriteImageData(image, destination, cancellationToken).ConfigureAwait(false);

		// write IEND
		await destination.WriteAsync(new ImageEnd().ToBytes(), cancellationToken).ConfigureAwait(false);

		await destination.FlushAsync(cancellationToken).ConfigureAwait(false);
	}

	private static async Task WriteImageData(Raster image, Stream destination, CancellationToken cancellationToken)
	{
		var filteredData = Filter(image, cancellationToken);
		var compressed = await CompressAsync(filteredData, cancellationToken).ConfigureAwait(false);
		var idat = new ImageData(compressed);
		await destination.WriteAsync(idat.ToBytes(), cancellationToken).ConfigureAwait(false);
	}

	private static List<byte[]> Scanlines(Raster image, CancellationToken cancellationToken)
	{
		var lines = new List<byte[]>(image.Height);

		for (var y = 0; y < image.Height; y++)
		{
			cancellationToken.ThrowIfCancellationRequested();
			
			var scanline = new byte[image.Width * 4];
			var pointer = 0;
			
			for (var x = 0; x < image.Width; x++)
			{
				var pixel = image[x, y];
				scanline[pointer++] = pixel.R;
				scanline[pointer++] = pixel.G;
				scanline[pointer++] = pixel.B;
				scanline[pointer++] = pixel.A;
			}

			lines.Add(scanline);
		}

		return lines;
	}

	private static byte[] Filter(Raster image, CancellationToken cancellationToken)
	{
		var scanlines = Scanlines(image, cancellationToken);

		// scan lines are 1 byte per channel (RGBA) + filter method byte header
		var filteredLineLength = (image.Width * 4) + 1;
		
		var buffer = new byte[filteredLineLength  * image.Height];
		var pointer = 0;

		foreach(var scanline in scanlines)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var filter = FilterSelector.Select(scanline);
			var filtered = filter.Filter(scanline);

			Array.Copy(filtered, 0, buffer, pointer, filteredLineLength);

			pointer += filteredLineLength;
		}

		return buffer;
	}

	private static async Task<byte[]> CompressAsync(byte[] data, CancellationToken cancellationToken)
	{
        using var dataStream = new MemoryStream();
        await using (var compressionStream = new ZLibStream(dataStream, CompressionLevel.Optimal))
            await compressionStream.WriteAsync(data, cancellationToken).ConfigureAwait(false);
        
        return dataStream.ToArray();
    }
}
