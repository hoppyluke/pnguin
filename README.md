# Pnguin

**Pnguin** is a cross-platform library for PNG image creation.

# Quickstart

```cs
var image = new Raster(10, 10);
var colour = new TrueColour(255, 128, 0, 128);
image[0, 0] = colour;

await using var stream = new FileStream("hello.png", FileMode.Create);

await Png.WriteAsync(image, stream);
```

# Core types

`Raster` defines the image format understood by Pnguin. This represents a bitmap image made of True Color with alpha (RGBA) pixels. The `TrueColour` type uses 1 byte per channel.

Convert a `Raster` to a PNG image by writing it to a stream with `Png.WriteAsync()`.
