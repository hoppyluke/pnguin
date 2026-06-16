namespace Pnguin;

///<summary>
/// True colour value with transparency.
/// Bit depth of 8 bits per sample (32 bits overall).
///</summary>
public readonly struct TrueColour(byte r, byte g, byte b, byte a)
{
	public const byte Transparent = 0;
	public const byte Opaque = 255;

	private readonly PackedBytes _value = new PackedBytes(r, g, b, a);

	public byte R => _value[0];
	public byte G => _value[1];
	public byte B => _value[2];
	public byte A => _value[3];

	public TrueColour(byte r, byte g, byte b) : this(r, g, b, 255) {}
}
