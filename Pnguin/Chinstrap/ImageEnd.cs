namespace Pnguin.Chinstrap;

///<summary>
/// IEND final chunk of a PNG image.
///</summary>
internal class ImageEnd : Chunk
{
	private static readonly ChunkType HeaderType = ChunkType.Parse("IEND");

	public override ChunkType Type => HeaderType;

	protected override byte[] PackData() => [];
}
