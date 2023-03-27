using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class ChallengeOne {
		// The Challenge:
		// "store a 10x10 matrix in a 1D array of float,
		// generate random values for all of these,
		// print the grid out in a readable format,
		// and then transform it so that it is flipped vertically
		// (bottom left is now top left, bottom right is now top right) and print it again."
		public static void Run() {
			float[] grid = new float[100];
			Random rand = new Random();
			for (int i = 0; i < grid.Length; i++) {
				grid[i] = rand.NextSingle();
			}
			PrintGrid(grid);
			// Flip grid vertically
			float[] bar = new float[10];
			for (int y = 0; y < 4; y++) {
				Array.Copy(grid, y*10, bar, 0, 10);
				Array.Copy(grid, (9 - y) * 10, grid, y * 10, 10);
				Array.Copy(bar, 0, grid, (9 - y) * 10, 10);
			}
			Console.WriteLine();
			Console.WriteLine("Flipped vertically:");
			Console.WriteLine();
			PrintGrid(grid);
		}
		public static void PrintGrid(float[] grid) {
			int i = 0;
			for (int y = 0; y < 10; y++) {
				for (int x = 0; x < 10; x++) {
					Console.Write("{0,6:N4} ", grid[i]);
					i++;
				}
				Console.Write("\n");
			}
		}
	}
}
