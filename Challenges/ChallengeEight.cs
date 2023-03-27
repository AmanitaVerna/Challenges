using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class ChallengeEight {
		// The Challenge:
		// "Write a program that takes input from the user and sorts the characters alphanumerically(using a method), and spits it back out."
		public static void Run() {
			Console.WriteLine("Please type something to have its characters sorted alphanumerically: ");
			string? input = null;
			while (input == null) {
				input = Console.ReadLine();
			}
			if (input.Length == 0) {
				return;
			}
			string output = Sort(input);
			
			Console.WriteLine("Result: "+output);
		}

		// Not the fastest sort, but given today's processor speeds, and that the user won't be entering a million characters or anything, it doesn't really matter
		public static string Sort(string input) {
			char[] arr = input.ToCharArray();
			for (int i = 0; i < arr.Length; i++) {
				for (int j = i + 1; j < arr.Length; j++) {
					if (arr[i] > arr[j]) {
						char tmp = arr[i];
						arr[i] = arr[j];
						arr[j] = tmp;
					}
				}
			}
			return new String(arr);
		}
	}
}
