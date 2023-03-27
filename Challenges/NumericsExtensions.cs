using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Challenges {
	public static class NumericsExtensions {

		/// <summary>
		/// RotateAround rotates the vector p around center by rot and returns the resulting rotated vector. (Used by challenge 9)
		/// Why this method doesn't already exist in System.Numerics is beyond me.
		/// </summary>
		/// <param name="p">A point or vector to rotate</param>
		/// <param name="center">Point to rotate p around</param>
		/// <param name="rot">A rotation quaternion representing the rotation to do</param>
		/// <returns>p, rotated around center by rot</returns>
		public static Vector3 RotateAround(this Vector3 p, Vector3 center, Quaternion rot) {
			Quaternion q = (rot * new Quaternion(p - center, 0) * Quaternion.Conjugate(rot));
			Vector3 v = new Vector3(q.X, q.Y, q.Z);
			return v;
		}
	}
}
