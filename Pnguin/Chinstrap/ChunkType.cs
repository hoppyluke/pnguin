namespace Pnguin.Chinstrap;

internal readonly struct ChunkType
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

	public byte[] ToBytes() => _bytes;
	
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
