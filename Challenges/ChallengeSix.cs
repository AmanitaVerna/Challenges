using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class ChallengeSix {
		// The Challenge:
		// "Take input of two positive integers from the user, loop between them and do 3x + 1 to each,
		// if it is odd +1, if it is even, /2 until odd again, count the steps and continue until the number is 1
		// and print the original number and number of steps.
		// If this sounds too easy, multithread it. If this sounds too hard, try just doing it for one entered value."
		public static void Run() {
			List<uint> numbers = new List<uint>();
			Console.WriteLine("Enter a positive integer, or press Enter without entering anything if you are done entering numbers: ");
			string? line = null;
			bool enteringNumbers = true;
			while (enteringNumbers) {
				line = Console.ReadLine();
				if (line != null) {
					if (line.Equals("", StringComparison.OrdinalIgnoreCase)) {
						enteringNumbers = false;
					} else {
						uint num = 0;
						try {
							num = Convert.ToUInt32(line);
						} catch (Exception) {
							Console.WriteLine("Try again with an actual positive number?");
							continue;
						}
						numbers.Add(num);
					}
				}
			}
			Console.WriteLine("Calculating...");
			Parallel.ForEach(numbers, num => Six(num));
		}
		public static void Six(uint num) {
			uint orig = 3 * num + 1;
			uint x = orig;
			int steps = 0;
			while (x != 1) {
				if ((x & 1)==1) {
					// if it's odd, add one
					x += 1;
				} else {
					// if it's even, divide by two
					x /= 2;
				}
				steps++;
			}
			Console.WriteLine("The number " + num + ", once transformed into " + orig + ", took " + steps + " steps to become 1.");
		}
	}
}
