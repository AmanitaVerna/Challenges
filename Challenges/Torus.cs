using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Challenges {
	/// <summary>
	/// Used by challenge 9. Incomplete.
	/// </summary>
	public class Torus : Object3D {
		private readonly double _R; // distance from ct to cr.
		private readonly double _r; // radius of ring composing torus.
		private readonly int rext;
		private readonly int rint;
		public double R { get { return _R; } }
		public double r { get { return _r; } }
		
		// not set in variables:
		// cr = center of ring, which is many points, defined as being at rc distance from ct.
		// tr = all points in torus ring, defined as being all points <= rr distance from cr.

		public Torus(Vector3 center, int rext, int rint) : base(center, Quaternion.Identity) {
			this.rext = rext;
			this.rint = rint;
			this._r = (rext - rint) / 2.0f;
			this._R = rext - _r;
			GenerateTriangles();
		}
		public override string ToString() {
			return "(Torus at " + center + ", with R = " + _R + " and r = " + _r + " and rotation "+rot+")";
		}

		// Generate and add triangles to the mesh
		// TODO: 
		// Currently this steps through all y coordinates the torus occupies, and for each y coordinate, steps through all x coordinates the torus occupies.
		// It doesn't generate triangles yet.
		// That's not the optimal way to do this, but some of the math may be useful, so we haven't deleted all the code yet, and will instead probably alter and adapt it.
		// What we want to do is go around the torus like a clock, calculating the coordinates of points on a ring which is angled to match the facing of the clock hand.
		// We can then join the points up into triangles by connecting 12 o'clock to 1 o'clock, 1 o'clock to 2 o'clock, 2 to 3, etc.
		// Each ring should have the same number of vertices since they are all the same size.
		public void GenerateTriangles() {
			int yc = (int) center.Y;
			int xc = (int) center.X;
			int miny = yc - rext;
			int maxy = yc + rext;
			double normY = -1.0;
			double deltaNormY = 2.0 / (maxy - miny);
			// -2, 2, 4, 1/2, 1/2 * -2 = -1
			// -1, 1, 2, 1, 1 * -2 = -2

			int minyI = yc - rint;
			int maxyI = yc + rint;
			double deltaNormYI = 2.0 / (maxyI - minyI);
			double normYI = deltaNormYI * miny;

			// Adjust normY if miny is above the top of the window.
			if (miny < 0) {
				normY += deltaNormY * (-miny);
				miny = 0;
			}
			if (minyI < 0) {
				normYI += deltaNormYI * (-minyI);
				minyI = 0;
			}
			if (maxy < 0) {
				maxy = 0;
			}
			if (maxyI < 0) {
				maxyI = 0;
			}
			// Adjust maxy or miny if either equals or exceeds the height of the window.
			if (maxy > Console.WindowHeight) {
				maxy = Console.WindowHeight;
			}
			if (miny >= Console.WindowHeight) {
				miny = Console.WindowHeight - 1;
			}
			if (maxyI > Console.WindowHeight) {
				maxyI = Console.WindowHeight;
			}
			if (minyI >= Console.WindowHeight) {
				minyI = Console.WindowHeight - 1;
			}
			int sy = -yc;
			int sySq = sy * sy;
			int sySqStep = 2 * sy + 1;
			for (int yp = miny; yp < maxy; yp++, normY += deltaNormY, normYI += deltaNormYI) {
				// This calculates, for any row at y=degree in an r=1 circle, half the length of that row, which I called its sub-radius.
				// As before, normY represents the relative location on the actual circle.
				double SubRadius = Math.Sin(Math.Acos(normY));
				if (SubRadius == 1.0) SubRadius = 0.999; // prevents a visual oddity
				// If normYI < -1 or > -1 then we should skip calculating SubRadiusI, and we only have one for loop for xp instead of two. 

				double SubRadiusI = Math.Sin(Math.Acos(normYI));
				if (SubRadiusI == 1.0) SubRadiusI = 0.999; // prevents a visual oddity

				// ers is half the external length of the row whose y coordinate is yp.
				double ers = rext * SubRadius;
				// irs is half the internal length of the row whose y coordinate is yp.
				double irs = rint * SubRadiusI;

				// Given a torus whose normal vector is facing directly towards or away from the camera, with an external radius rext and internal radius rint, 
				// whose center is at (xc, yc, 0):
				// In the row y = yp, the torus will occupy x coordinates in the ranges (xp = xc - ers; xp < xc + irs;) and (xp = xc + irs; xp < xc + ers;).

				// If a point is above or below the torus in the y dimension, ers will be 0. On the very edge of the torus, it may be 1.
				
				// Skip the rest of the code to save time.
				if (ers <= 1 || irs <= 1) {
					continue;
				}
				int minx = xc - (int)ers;
				int maxx = yc + (int)ers;
				
				// We would next need to work out the z coordinates, if we weren't rewriting all this code.
			}
		}
	}
}
