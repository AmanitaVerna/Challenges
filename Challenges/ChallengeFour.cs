using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class ChallengeFour {
		const int TrailLength = 8;
		const int MaxCellsToMutatePerFrame = 8;
		const int ChanceToChangeFallingChar = 8;

		private static ConsoleColor[] colors = { ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.DarkGreen};

		// The Challenge:
		// "Write a console app that displays randomly changing characters falling vertically downward with a trail behind them,
		// with new ones spawning at the top. This is a matrix "screensaver" type app, just does this until you close it.
		// For reference, see above [gif from The Matrix]. The closer to the above (ignoring the zooming or the matrix titlecard) the better,
		// but the minimum is the aforementioned description - alternative reference below [another gif, from a program that does this]."
		public static void Run() {
			// string fontName = ConsoleFont.GetFontName();
			// ConsoleFont.SetFont("NSimSun"); // this didn't seem to work for some reason, but after manually changing the font,
			// random katakana still didn't look as good as what was in the movie, so I went back to ASCII.
			
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.Unicode;
			int[] ys = new int[Console.WindowWidth];
			char[] yChs = new char[Console.WindowWidth];
			char[,] buffer = new char[Console.WindowWidth, Console.WindowHeight];
			for (int i = 0; i < ys.Length; i++) {
				ys[i] = -1;
				yChs[i] = ' ';
			}
			for (int x = 0; x < Console.WindowWidth; x++) {
				for (int y = 0; y < Console.WindowHeight; y++) {
					buffer[x, y] = ' ';
				}
			}
			Random rand = new Random();
			// This isn't ensuring that it runs at any particular framerate, currently. It runs as fast as it can, and is limited by the slowness of Console.Write.
			while (!Console.KeyAvailable) {
				int x = rand.Next(Console.WindowWidth);
				int y = ys[x];
				if (y < 0) {
					if (rand.Next(8)!=0) {
						continue;
					} else {
						yChs[x] = rand.RandChar();
					}
				}
				// change color of last drawn cell in this column
				if (y >= 0 && y < Console.WindowHeight) {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.SetCursorPosition(x, y);
					Console.Write(buffer[x, y]);
				}
				y++;
				if (rand.Next(ChanceToChangeFallingChar) == 0) {
					// Change the falling character to another random character
					yChs[x] = rand.RandChar();
				}
				// write to new cell
				if (y >= 0 && y < Console.WindowHeight) {
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.SetCursorPosition(x, y);
					buffer[x, y] = yChs[x];
					Console.Write(yChs[x]);
				}
				// Change color of cell TrailLength cells up from green to dark green
				int ym = y - TrailLength;
				if (ym >= 0 && ym < Console.WindowHeight) {
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.SetCursorPosition(x, ym);
					Console.Write(buffer[x, ym]);
				}
				// Change color of cell TrailLength*2 cells up from dark green to dark gray (that looked bad, so nevermind)
				ym = ym - TrailLength;
				if (ym >= 0 && ym < Console.WindowHeight) {
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.SetCursorPosition(x, ym);
					Console.Write(buffer[x, ym]);
				}
				// Erase cell TrailLength*3 cells up
				ym = ym - TrailLength;
				if (ym >= 0 && ym < Console.WindowHeight) {
					buffer[x, ym] = ' ';
					Console.SetCursorPosition(x, ym);
					Console.Write(buffer[x, ym]);
				}
				// If y is far enough down, reset column
				// Otherwise, update ys[x]
				if (y >= Console.WindowHeight + TrailLength*3) {
					ys[x] = -1;
					yChs[x] = ' ';
				} else {
					ys[x] = y;
				}

				// Change random cells
				for (int i=0; i < rand.Next(MaxCellsToMutatePerFrame); i++) {
					x = rand.Next(Console.WindowWidth);
					y = rand.Next(Console.WindowHeight);
					int yDist = ys[x] - y;
					int yc = 0;
					if (yDist < 0 || yDist >= TrailLength * 3) {
						continue;
					} else if (yDist > 0) {
						yc = 1 + yDist / TrailLength;
					}
					char ch = rand.RandChar();
					buffer[x, y] = ch;
					Console.ForegroundColor = colors[yc];
					Console.SetCursorPosition(x, y);
					Console.Write(buffer[x, y]);
				}
				
			}
			// ConsoleFont.SetFont(fontName);
			Console.CursorVisible = true;
			Console.ResetColor();
			Console.Clear();
		}
		public static char RandChar(this Random rand) {
			return (char)((int)'!' + rand.Next(64));
			// return (char)((int)'\u30a1' + rand.Next(89)); // katakana
		}
	}	
}
