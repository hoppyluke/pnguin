namespace Pnguin.Chinstrap;

///<summary>
/// IDAT data chunk containing compressed and filtered image data.
///</summary>
internal class ImageData(byte[] compressedData) : Chunk
{
	private static readonly ChunkType HeaderType = ChunkType.Parse("IDAT");
	
	private readonly byte[] _data = compressedData;

	public override ChunkType Type => HeaderType;

	protected override byte[] PackData() => _data;
}
