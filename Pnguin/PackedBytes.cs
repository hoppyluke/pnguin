namespace Pnguin;

///<summary>
/// 4 bytes packed into a single <see cref="uint" /> value.
///</summary>
internal readonly struct PackedBytes
{
	private static readonly int[] ShiftCounts = [24, 16, 8, 0];

	private readonly uint _value;

	public byte this[int index]
	{
		get
		{
			if (index < 0 || index > 3)
				throw new IndexOutOfRangeException("Index must be non-negative and less than 4");
				
			return (byte)((_value >> ShiftCounts[index]) & 255);
		}
	}

	public PackedBytes(byte[] bytes)
	{
		if (bytes.Length != 4) throw new ArgumentException("Input must be a 4 byte array");

		_value = ((uint)bytes[0] << 24) | ((uint)bytes[1] << 16) | ((uint)bytes[2] << 8) | bytes[3];
	}

	public PackedBytes(byte b0, byte b1, byte b2, byte b3)
	{
		_value = ((uint)b0 << 24) | ((uint)b1 << 16) | ((uint)b2 << 8) | b3;
	}

	public override string ToString() => _value.ToString("X8");
}
