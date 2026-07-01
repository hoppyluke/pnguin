namespace Pnguin.Rockhopper;

using Pnguin;

public class Simple
{
    [Fact]
    public async Task RedSquare()
    {
        var image = new Raster(100, 100);
        var red = new TrueColour(255, 0, 0);
        image.Fill(red);

        await WriteImage(image, "RedSquare.png");
    }

    [Fact]
    public async Task Penguin()
    {
        var image = new Raster(256, 128);
        image.Fill(TrueColour.Transparent);

        var black = new TrueColour(0, 0, 0, 255);
        var white = new TrueColour(255, 255, 255, 255);
        var orange = new TrueColour(255, 140, 0, 255);

        // Rough pixel art of a penguin: 'B' body outline, 'W' belly, 'O' beak/feet, '.' left transparent.
        string[] pixelArt =
        [
            ".....BBBBB.....",
            "....BBBBBBB....",
            "...BBBBBBBBB...",
            "...BBBBBBBBB...",
            "..BBBBBBBBBBB..",
            "..BBWWWWWWWBB..",
            ".BBBWEWWWEWBBB.",
            ".BBBWWWWWWWBBB.",
            ".BBBWWOOOWWBBB.",
            ".BBBWWWWWWWBBB.",
            ".BBBWWWWWWWBBB.",
            "..BBWWWWWWWBB..",
            "..BBBBBBBBBBB..",
            "..BBBBBBBBBBB..",
            "...BB.....BB...",
            "...BB.....BB...",
        ];

        const int scale = 6;
        var offsetX = (image.Width - pixelArt[0].Length * scale) / 2;
        var offsetY = (image.Height - pixelArt.Length * scale) / 2;

        for (var row = 0; row < pixelArt.Length; row++)
        {
            for (var col = 0; col < pixelArt[row].Length; col++)
            {
                var colour = pixelArt[row][col] switch
                {
                    'B' => black,
                    'W' => white,
                    'O' => orange,
                    'E' => black,
                    _ => (TrueColour?)null,
                };

                if (colour is null)
                    continue;

                for (var dy = 0; dy < scale; dy++)
                    for (var dx = 0; dx < scale; dx++)
                        image[offsetX + (col * scale) + dx, offsetY + (row * scale) + dy] = colour.Value;
            }
        }

        await WriteImage(image, "penguin.png");
    }

    private static async Task WriteImage(Raster image, string path)
    {
        const string dir = "images";
        Directory.CreateDirectory(dir);
        await using var s = new FileStream($"{dir}/{path}", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        await Png.WriteAsync(image, s);
    }
}
