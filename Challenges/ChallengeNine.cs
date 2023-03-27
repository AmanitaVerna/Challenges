using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Challenges {
	public static class ChallengeNine {
		// The Challenge:
		// "Make a spinning 3D donut with lighting, rendered in ASCII. Use this video as a guide but feel free to take any approach
		// (except hardcoding the frames, that's cheating). https://youtu.be/DEqXNfs_HhY
		// WARNING: This one is hard enough that I'd have to work at it, but it would serve as an introduction to 3D rendering
		// if you are unfamiliar, and the math involved, and it's pretty cool.
		// If this is too easy (perhaps you know C and can just read the mess on the right), do a teapot instead."
		public static void Run() {
			// Create a torus, scaled to fit inside the console window.
			int rext = Math.Min(Console.WindowWidth, Console.WindowHeight) / 2; // external or outer radius of torus.
			int rint = rext / 2; // Internal or inner radius of torus. (It could be anything > 0 and < rext)
			Vector3 ct = new Vector3(Console.WindowWidth / 2.0f, Console.WindowHeight / 2.0f, 0.0f); // center of the torus
			Torus torus = new Torus(ct, rext, rint);

			// Set up the initial rotation quaternion, the zbuffer, camera position, and camera rotation.
			Quaternion rot = Quaternion.CreateFromAxisAngle(new Vector3(0, 1.0f, 0), (float)(Math.PI/180.0));			
			float[] zbuffer = new float[Console.WindowWidth * Console.WindowHeight];
			Vector3 camPos = new Vector3(0, 0, -10);
			Quaternion camRot = Quaternion.Identity; // This might be pointing in the wrong direction - I haven't worked through the math or tested this yet.

			// Loop until key pressed
			// This isn't ensuring that it runs at any particular framerate, currently.
			// It runs as fast as it can, and is limited by the slowness of Console.Write (called by Torus.Render).
			while (!Console.KeyAvailable) {
				// Clear the console and zbuffer each frame
				Console.Clear();
				Array.Clear(zbuffer, 0, zbuffer.Length);

				// render the torus
				torus.Render(zbuffer);

				// rotate the torus
				torus.Rotate(rot);
			}
		}

	}
}
