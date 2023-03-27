namespace Challenges {
	/// <summary>
	/// Menu contains the Main function and shows the challenge-selection prompt.
	/// </summary>
	public class Menu {
		/// <summary>
		/// Main registers the challenges and displays a prompt to select which challenge to run, and then runs it (if it has been registered).
		/// </summary>
		public static void Main() {
			int MaxChallenges = ChallengeRunner.MaxChallenges;
			
			// Register challenges
			ChallengeRunner.Register(1, ChallengeOne.Run);
			ChallengeRunner.Register(2, ChallengeTwo.Run);
			ChallengeRunner.Register(3, ChallengeThree.Run);
			ChallengeRunner.Register(4, ChallengeFour.Run);
			ChallengeRunner.Register(5, ChallengeFive.Run);
			ChallengeRunner.Register(6, ChallengeSix.Run);
			ChallengeRunner.Register(7, ChallengeSeven.Run);
			ChallengeRunner.Register(8, ChallengeEight.Run);
			// 9 isn't registered because I haven't finished it.
			ChallengeRunner.Register(10, ChallengeTen.Run);

			Console.WriteLine("Which challenge do you wish to run? [Enter a number from 1 to " + MaxChallenges + ", or q to quit]");
			string? line = null;
			do {
				line = Console.ReadLine();
				if (line != null) {
					// q for quit
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
					if (num >= 1 && num <= MaxChallenges) {
						Console.Clear();
						ChallengeRunner.Run(num);
					} else {
						Console.WriteLine("Sorry, that number is out of range. We expected a number from 1 to 10.");
						line = null;
					}
				}
			} while (line == null);
		}
	}
}