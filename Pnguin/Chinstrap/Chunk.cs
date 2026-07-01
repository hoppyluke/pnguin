using System.IO.Hashing;

namespace Pnguin.Chinstrap;

///<summary>
/// A chunk of a PNG file.
///</summary>
internal abstract class Chunk
{
	///<summary>
	/// Maximum length of a chunk according to PNG spec (2^31 - 1).
	///</summary>
	public const uint MaxLength = int.MaxValue;

	///<summary>
	/// Length of this chunk's data field.
	///</summary>
    public uint Length => (uint)Data.Length;

	///<summary>
	/// Total size of this chunk, in bytes.
	///</summary>
	public long TotalLength => Data.Length + 12;

	///<summary>
	/// Chunk type code.
	///</summary>
    public abstract ChunkType Type { get; }

	private readonly Lazy<byte[]> _data;

	///<summary>
	/// Bytes for this chunk (if any, may be zero length).
	///</summary> 
    public byte[] Data => _data.Value;

	private readonly Lazy<byte[]> _crc;
	
	///<summary>
	/// Four-byte Cyclic Redundancy Check over chunk type code and data.
	///</summary>
	public byte[] Crc => _crc.Value;

	public Chunk()
	{
		_data = new(PackData);
		_crc = new(CalculateCrc);
	}

	///<summary>
	/// Packs the chunk data into a byte array.
	///</summary>
	protected abstract byte[] PackData();
	
    private byte[] CalculateCrc() => ByteWriter.Write(Crc32.HashToUInt32([.. Type.ToBytes(), .. Data]));
	
	///<summary>
	/// Converts the entire chunk including header and CRC to a bytes.
	///</summary>
	public byte[] ToBytes()
	{
		// length (4 bytes) + type (4) + data + CRC (4)
		var totalLength = Data.Length + 12;
		var buffer = new byte[totalLength];

		ByteWriter.Write(Length, buffer, 0);
		Array.Copy(Type.ToBytes(), 0, buffer, 4, 4);
		Array.Copy(Data, 0, buffer, 8, Data.Length);
		Array.Copy(Crc, 0, buffer, 8 + Data.Length, 4);

		return buffer;
	}
}
