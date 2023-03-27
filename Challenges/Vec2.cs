using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	/// <summary>
	/// Vec2 represents a 2 dimensional vector or point with integer coordinates. It's used by challenge 2.
	/// </summary>
	public struct Vec2 {
		public int x, y;
		public Vec2(int x, int y) {
			this.x = x;
			this.y = y;
		}
		public static Vec2 operator -(Vec2 u) => new Vec2(-u.x, -u.y); 
		public static Vec2 operator +(Vec2 u, Vec2 v) => new Vec2(u.x + v.x, u.y + v.y);
		public static Vec2 operator -(Vec2 u, Vec2 v) => new Vec2(u.x - v.x, u.y - v.y);
		public static Vec2 operator *(Vec2 u, int amt) => new Vec2(u.x * amt, u.y * amt);
		public static Vec2 operator /(Vec2 u, int amt) => new Vec2(u.x / amt, u.y / amt);

		public override string ToString() {
			return "("+x+", "+y+")";
		}

		// I've only added these to test challenge seven
		public int X { get { return x; } set { x = value; } }
		public int Y { get { return y; } set { y = value; } }
	}
}
