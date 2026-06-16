namespace Pnguin;

public class Png
{
	public static readonly byte[] Signature = [137, 80, 78, 71, 13, 10, 26, 10];
	public const int SignatureLength = 8;

	private readonly List<Chunk> _chunks = [];

	public IEnumerable<Chunk> Chunks => _chunks;
}

public enum ChunkPriority : byte
{
	Critical = 0,
	Ancillary = 1
}

public struct Chunk
{
	public const uint MaxLength = int.MaxValue;

	public uint Length { get; private set; }

	public ChunkType Type { get; init; }

	public byte[] Data { get; init; }

	public byte[] Crc { get; init; }

	public Chunk(ChunkType type, byte[] data)
	{
		Length = (uint)data.Length;
		Type = type;
		Data = data;
		Crc = []; // TODO calculate CRC
	}
}

public readonly struct ChunkType
{
	private readonly byte[] _bytes;

	private ChunkType(byte[] bytes)
	{
		if (bytes.Length != 4)
			throw new ArgumentException("Chunk Type must be 4 bytes");

		foreach (var b in bytes)
			if (!char.IsAsciiLetter((char)b))
				throw new ArgumentOutOfRangeException(nameof(bytes), "Chunk bytes must be ASCII letters");

		_bytes = bytes;
	}
	
	public static ChunkType Parse(string s)
	{
		if (s.Length != 4)
			throw new ArgumentException("Chunk Type must have 4 letters");

		foreach (var c in s)
			if (!char.IsAsciiLetter(c))
				throw new ArgumentOutOfRangeException(nameof(s), "Chunk bytes must be ASCII letters");

		return new ChunkType([(byte)s[0], (byte)s[1], (byte)s[2], (byte)s[3]]);
	}

	
}
