using System.Runtime.InteropServices;

namespace Pnguin;

internal static class ByteWriter
{
	///<summary>
	/// Converts an unsigned integer to a byte array in network byte order.
	///</summary>
	public static byte[] Write(uint value)
	{
		var a = new byte[4];
		Write(value, a, 0);
		return a;
	}
	
	///<summary>
	/// Writes a value to an array in network byte order.
	///</summary>
	public static void Write(uint value, byte[] array, int offset)
	{
		if (offset > array.Length - 4)
			throw new IndexOutOfRangeException("Write would exceed array bounds");

		var bytes = BitConverter.GetBytes(value);
		if (BitConverter.IsLittleEndian)
			Array.Reverse(bytes);

		Array.Copy(bytes, 0, array, offset, 4);
	}

	///<summary>
	/// Writes packed bytes to an array.
	///</summary>
	public static void Write(PackedBytes value, byte[] array, int offset)
	{
		if (offset > array.Length - 4)
			throw new IndexOutOfRangeException("Write would exceed array bounds");

		array[offset] = value[0];
		array[offset + 1] = value[1];
		array[offset + 2] = value[2];
		array[offset + 3] = value[3];
	}
}
