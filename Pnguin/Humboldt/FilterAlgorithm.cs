namespace Pnguin.Humboldt;

internal enum FilterType : byte
{
	None = 0,
	Sub = 1,
	Up = 2,
	Average = 3,
	Paeth = 4
}

internal abstract class FilterAlgorithm
{
	public abstract FilterType Type { get; }

	///<summary>
	/// Filters a scanline and prepends the filter type.
	///</summary>
	public abstract byte[] Filter(byte[] scanline);
}

internal class NoneFilter : FilterAlgorithm
{
	public override FilterType Type => FilterType.None;

	public override byte[] Filter(byte[] scanline) => [(byte)FilterType.None, .. scanline];
}
