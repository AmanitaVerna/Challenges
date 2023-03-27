using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class ChallengeTen {
		private static string[] vowelMoras = { "a", "i", "u", "e", "o" }; // moras consisting of just a vowel.
		private static string[] cvMoras = GenerateCVMoras(); // moras consisting of a consonant followed by a vowel.

		// The Challenge:
		// "Write a name generator that creates phonetically pronounceable names of a user-provided length and template,
		// e.g., "5 2" would generate names like "Baoba Ab", "Uruba Po", etc..
		// Do not just use a pre-made list of names, it must generate them from syllables at the largest.
		// Bonus points if it uses rules to build its own syllables."
		public static void Run() {
			Console.WriteLine("How long do you want each name to be? [Enter one or more space-separated integers and press enter]");
			string? line = null;
			int[]? nameLengths = null;
			do {
				line = Console.ReadLine();
				if (line != null) {
					if (line.Count() == 0 || line.Equals("q", StringComparison.OrdinalIgnoreCase)) {
						return;
					}
					// split by spaces
					string[] inputTokens = line.Split(' ');
					nameLengths = new int[inputTokens.Length];
					for (int i = 0; i<nameLengths.Length; i++) {
						string token = inputTokens[i];
						int num = -1;
						try {
							num = Convert.ToInt32(token);
						} catch (Exception) {
							Console.WriteLine("'"+token+"' isn't a valid 32-bit integer. Try again or press enter on a blank line to cancel.");
							line = null;
							break;
						}
						nameLengths[i] = num;
					}
				}
			} while (line == null);
			if (nameLengths != null) {
				string[] names = new string[nameLengths.Length];
				int idx = 0;
				Random rand = new Random();
				// generate names
				foreach (int nameLen in nameLengths) {
					// This builds names that start with either a vowel mora or a consonant-vowel mora, are followed by zero or more cv moras,
					// and may end with an additional vowel mora.
					// If nameLen is <= 0, the generated name will be "".
					// If it is 1, it will contain only a vowel mora.
					// If it is 2, it will contain only a cv mora.
					// If it is > 2, it will be a string that has a 10% chance to start with a vowel mora and a 90% chance to start with a cv mora, followed by however many
					// cv moras can be added while keeping the length below nameLen, and then finally, if an additional character needs to be added on the end to reach nameLen, 
					// a final vowel mora will be added.
					StringBuilder sb = new StringBuilder();
					if (nameLen <= 0) {
						sb.Append("");
					} else if (nameLen == 1) { // one mora long, but it can only be a vowel mora
						sb.Append(Choose(rand, vowelMoras));
					} else if (nameLen == 2) { // one mora long, but it can only be a cv mora
						sb.Append(Choose(rand, cvMoras));
					} else {
						// start with either a v or cv mora
						sb.Append(Choose(rand, rand.Next(10) == 0 ? vowelMoras : cvMoras));
						// Append cv moras until we have 0 or 1 characters left to add.
						for (int i = sb.Length; i + 2 <= nameLen; i+=2) {
							sb.Append(Choose(rand, cvMoras));
						}
						// If we have 1 character left to add, append a vowel mora.
						if (nameLen - sb.Length == 1) {
							sb.Append(Choose(rand, vowelMoras));
						}
					}
					names[idx++] = Capitalize(sb.ToString());
				}
				if (names.Length > 0) {
					Console.WriteLine("We generated the following names:");
					foreach (string name in names) {
						Console.Write($"{name} ");
					}
					Console.WriteLine("");
				} else {
					Console.WriteLine("No names were generated.");
				}				
			}
		}

		/// <summary>
		/// GenerateCVMoras generates the consonant-vowel moras used in name generation. This uses the vowels 'a', 'i', 'u', 'e', and 'o', 
		/// and the consonants 'k', 's', 't', 'n', 'h', 'm', 'y', 'r', 'w', and returns a string for each combination of consonant and vowel
		/// which starts with a consonant and ends with a vowel.
		/// </summary>
		/// <returns>The generated moras.</returns>
		private static string[] GenerateCVMoras() {
			// This function exists because I didn't want to manually type in all 50 moras.
			char[] consonants = { 'k', 's', 't', 'n', 'h', 'm', 'y', 'r', 'w' };
			char[] vowels = { 'a', 'i', 'u', 'e', 'o' };
			string[] ret = new string[consonants.Length * vowels.Length];
			int idx = 0;
			foreach (char consonant in consonants) {
				foreach (char vowel in vowels) {
					ret[idx++] = "" + consonant + vowel;
				}
			}
			return ret;
		}

		/// <summary>
		/// Choose selects a random string from the passed array, and returns it.
		/// </summary>
		/// <param name="rand">The Random object to use to select a random string</param>
		/// <param name="arr">The string array to select strings from</param>
		/// <returns>One string from arr, selected randomly</returns>
		private static string Choose(Random rand, string[] arr) {
			return arr[rand.Next(0, arr.Length)];
		}
		
		/// <summary>
		/// Capitalize returns a new string which is a copy of s with the first character capitalized.
		/// </summary>
		/// <param name="s">The string to capitalize the first character of</param>
		/// <returns>The string with the first character capitalized</returns>
		private static string Capitalize(string s) {
			string ret = s;
			if (s.Length > 0) {
				char[] ca = s.ToCharArray();
				ca[0] = char.ToUpper(ca[0]);
				ret = new string(ca);
			}
			return ret;
		}
	}
}
