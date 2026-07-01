namespace Pnguin.Chinstrap;

internal enum ColourType : byte
{
	Grayscale = 0,
	Rgb = 2,
	Palette = 3,
	GrayscaleAlpha = 4,
	RgbAlpha = 6
}

internal enum CompressionMethod : byte
{
	Deflate = 0
}

internal enum FilterMethod : byte
{
	Adaptive = 0
}

internal enum InterlaceMethod : byte
{
	None = 0,
	Adam7 = 1
}

///<summary>
/// IHDR image header chunk.
///</summary>
internal class ImageHeader(
    uint width,
    uint height,
    byte bitDepth,
    ColourType colourType,
    CompressionMethod compressionMethod,
    FilterMethod filterMethod,
    InterlaceMethod interlaceMethod) : Chunk
{
	private static readonly ChunkType HeaderType = ChunkType.Parse("IHDR");

    public uint Width { get; } = width;
    public uint Height { get; } = height;
    public byte BitDepth { get; } = bitDepth;
    public ColourType ColourType { get; } = colourType;
    public CompressionMethod CompressionMethod { get; } = compressionMethod;
    public FilterMethod FilterMethod { get; } = filterMethod;
    public InterlaceMethod InterlaceMethod { get; } = interlaceMethod;

    public override ChunkType Type => HeaderType;

    protected override byte[] PackData()
	{
		var data = new byte[13];

		ByteWriter.Write(Width, data, 0);
		ByteWriter.Write(Height, data, 4);
		data[8] = BitDepth;
		data[9] = (byte)ColourType;
		data[10] = (byte)CompressionMethod;
		data[11] = (byte)FilterMethod;
		data[12] = (byte)InterlaceMethod;

		return data;
	}
}
