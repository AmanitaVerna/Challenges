using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	/// <summary>
	/// ChallengeRunner keeps a list of registered challenges, and can execute a specified challenge by number if it has been registered.
	/// </summary>
	public static class ChallengeRunner {
		public const int MaxChallenges = 10;

		private static Challenge[] challenges = new Challenge[MaxChallenges];

		/// <summary>
		/// Register registers challenge num with the function challenge.
		/// </summary>
		/// <param name="num">The challenge's number</param>
		/// <param name="challenge">The function to call when the challenge is run via ChallengeRunner.Run</param>
		public static void Register(int num, Challenge challenge) {
			while (challenges.Count() < num) {
				challenges.Append(null);
			}
			challenges[num-1] = challenge;
		}

		/// <summary>
		/// Run runs a challenge's registered function, or prints an error message to the console if it hasn't been registered or if the number is out of range.
		/// </summary>
		/// <param name="num">The challenge to run</param>
		public static void Run(int num) {
			if (num >= 1 && num <= MaxChallenges) {
				if (challenges[num-1] != null) {
					challenges[num-1]();
				} else {
					Console.WriteLine("Challenge " + num + " hasn't been coded yet!");
				}
			} else {
				Console.WriteLine("Unable to run challenge "+num+": No such challenge is known. Expected a number from 1 to " + MaxChallenges + ".");
			}
		}
	}
}
