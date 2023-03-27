using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class ChallengeTwo {
		// The Challenge:
		// "Write a struct called Vec2 that stores ints X and Y,
		// and write an extension method that interpolates between two Vec2s based on the input and returns a new Vec2 with the resultant coordinate."
		public static void Run() {
			Random rand = new Random();
			for (int i = 0; i < 10; i++) {
				Vec2 u = new Vec2(rand.Next(101), rand.Next(101));
				Vec2 v = new Vec2(rand.Next(101), rand.Next(101));
				int pct = rand.Next(101);
				Vec2 o = u.Interpolate(v, pct);
				Console.WriteLine("Interpolating between " + u + " and " + v + " by " + pct + ". Result: " + o);
			}
		}
		
		// Starting from v, percent is the percentage of the distance to u to travel from 0 to 100, such that 0 returns v,
		// 100 returns u, and a number in between returns an interpolated vector which is that percent of the distance from v to u.
		public static Vec2 Interpolate(this Vec2 u, Vec2 v, int percent) {
			Vec2 diff = v - u;
			return (diff * percent / 100) + u;
		}
	}
}
