using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Challenges {
	// The Challenge:
	// "Write a method that recursively calls itself to calculate the factorial of an input integer and outputs the result as an integer."
	public static class ChallengeThree {
		public static void Run() {
			Console.WriteLine("What integer do you want to calculate the factorial of? [Enter an integer, or q to cancel]");
			string? line = null;
			do {
				line = Console.ReadLine();
				if (line != null) {
					if (line.Equals("q", StringComparison.OrdinalIgnoreCase)) {
						return;
					}
					int num = -1;
					try {
						num = Convert.ToInt32(line);
					} catch (Exception) {
						Console.WriteLine("Try again with an actual number?");
						line = null;
						continue;
					}
					Console.WriteLine("The factorial of "+num+" is "+Factorial(num));
				}
			} while (line == null);
		}
		public static BigInteger Factorial(int num) {
			if (num < 2) {
				return 1;
			}
			return num * Factorial(num - 1);
		}
	}
}