using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Challenges {
	public static class ChallengeFive {
		private static char[] ColorChars = { '@', '&', '%', '#', '(', '/', '*',  ',', '.', ' '};
		private static byte[] ColorBytes = { 30, 50, 70, 90, 110, 144, 175, 193, 221, 247};
		private static Dictionary<char, byte> ColorDictionary = new Dictionary<char, byte>(ColorChars.Length);

		// The Challenge:
		// "Convert this ASCII cow [cow.txt] to a BMP file using only built-in C# functionality."
		// ... microsoft seems to have moved Image into a NuGet package, so I'm not sure that counts anymore.
		// that's fine, bmp files are easy to write manually
		public static void Run() {
			for (int i = 0; i < ColorChars.Length; i++) {
				ColorDictionary[ColorChars[i]] = (byte)i;
			}
			// open cow.txt
			// read in lines
			List<string> lines;
			try {
				lines = new List<string>();
				using (StreamReader sr = new StreamReader("cow.txt")) {
					while (true) {
						string? line = sr.ReadLine();
						if (line != null) {
							lines.Add(line);
						} else {
							break;
						}
					}
				}
			} catch (Exception e) {
				Console.WriteLine("Error reading cow.txt: "+e.Message);
				return;
			}
			// convert runes in lines into colors and store into an image data buffer
			int bmpHeight = 2 * lines.Count();
			int bmpWidth = lines[0].Length;

			int paletteLength = ColorChars.Length * 4;
			int bmpStart = 54 + paletteLength;
			int stride = bmpWidth;
			int padding = 0;
			if ((bmpWidth & 3) != 0) {
				padding = 4 - (bmpWidth & 3);
				stride = bmpWidth + padding;
			}
			int bmpLength = stride * bmpHeight;
			int len = bmpStart + bmpLength;
			byte[] bmpData = new byte[len];
			bmpData[0] = (byte)'B';
			bmpData[1] = (byte)'M';
			StoreInt(bmpData, 2, len);
			for (int i = 6; i < 10; i++) {
				bmpData[i] = 0;
			}
			StoreInt(bmpData, 10, bmpStart);
			int dibHeaderSize = 40; // BITMAPINFOHEADER
			StoreInt(bmpData, 14, dibHeaderSize);
			StoreInt(bmpData, 18, bmpWidth);
			StoreInt(bmpData, 22, -bmpHeight);
			StoreShort(bmpData, 26, 1); // number of color planes, must be 1
			StoreShort(bmpData, 28, 8); // bits per pixel
			StoreInt(bmpData, 30, 0); // Compression method (BI_RGB, which means no compression. We could use RLE... Tempting. The space savings would be minimal, however.)
			StoreInt(bmpData, 34, 0); // bitmap image data size. Because we're using BI_RGB, we can just put in 0 here.
			StoreInt(bmpData, 38, bmpWidth); // horizontal resolution in pixels per meter, which, I'm just going to put in the number of pixels.
			StoreInt(bmpData, 42, bmpHeight); // vertical resolution in pixels per meter, which, I'm just going to put in the number of pixels.
			StoreInt(bmpData, 46, ColorChars.Length); // colors in color palette
			StoreInt(bmpData, 50, 0); // number of important colors. 0 for all of them. Also, we're not using 8 bpp screen resolutions anymore so this is pretty irrelevant.
			
			// Write the palette
			for (int i = 0; i < ColorChars.Length; i++) {
				// blue, green, red, 0x00
				bmpData[54 + i * 4] = ColorBytes[i];
				bmpData[54 + i * 4 + 1] = ColorBytes[i];
				bmpData[54 + i * 4 + 2] = ColorBytes[i];
				bmpData[54 + i * 4 + 3] = 0;
			}

			// Write the pixels
			for (int y = 0; y < bmpHeight; y++) {
				char[] line = lines[y >> 1].ToCharArray();
				// pixels
				for (int x = 0; x < bmpWidth; x++) {
					bmpData[bmpStart + y * stride + x] = ColorDictionary[line[x]]; // this will throw an exception if there's a character we overlooked in the ascii file
				}
				// padding
				for (int x = bmpWidth; x < stride; x++) {
					bmpData[bmpStart + y * stride + x] = 0;
				}
			}

			// Write the image data to a bmp file (cow.bmp)
			using (var w = File.Create("cow.bmp")) {
				w.Write(bmpData);
				Console.WriteLine("Wrote cow.bmp.");
			}			
		}

		public static void StoreShort(byte[] buffer, int idx, short num) {
			if (idx >= 0 && idx + 1 < buffer.Count()) {
				buffer[idx+1] = (byte)((num & 0xff00) >> 8);
				buffer[idx] = (byte)(num & 0x00ff);
			} else {
				throw new IndexOutOfRangeException("Index " + idx + " is out of range: Must be >= 0 and < " + (buffer.Count()-1) + " to fit in the specified buffer.");
			}
		}
		public static void StoreInt(byte[] buffer, int idx, int num) {
			if (idx >= 0 && idx + 3 < buffer.Count()) {
				buffer[idx+3] = (byte)((num & 0xff000000) >> 24);
				buffer[idx+2] = (byte)((num & 0x00ff0000) >> 16);
				buffer[idx+1] = (byte)((num & 0x0000ff00) >> 8);
				buffer[idx] = (byte)(num & 0x000000ff);
			} else {
				throw new IndexOutOfRangeException("Index " + idx + " is out of range: Must be >= 0 and < " + (buffer.Count()-3) + " to fit in the specified buffer.");
			}
		}
	}
}
