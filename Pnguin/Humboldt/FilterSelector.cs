namespace Pnguin.Humboldt;

internal static class FilterSelector
{
	///<summary>
	/// Selects the filter algorithm to apply to a scanline.
	///</summary>
	public static FilterAlgorithm Select(byte[] scanline) => new NoneFilter();
}
