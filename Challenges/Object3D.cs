using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Challenges {
	/// <summary>
	/// Used by challenge 9. Incomplete.
	/// </summary>
	public class Object3D {
		protected Vector3 center; // center of torus
		protected Quaternion rot; // rotation of torus
		public Vector3 Center { get { return center; } }
		public Quaternion Rot { get { return rot; } }
		protected List<Vector3> mesh; // vertices composing the triangles of the mesh
		protected List<Vector3> meshNorm; // surface normals of the triangles in the mesh, one per triangle. Used to calculate shading.

		public Object3D(Vector3 center, Quaternion rot) {
			this.center = center;
			this.rot = rot;
			this.mesh = new List<Vector3>();
			this.meshNorm = new List<Vector3>();
		}

		// Rotate the object using the passed quaternion, by multiplying the object's existing rotation by t.
		public void Rotate(Quaternion t) {
			this.rot *= rot;
		}

		// Add a triangle to the mesh, given the coordinates of the three vertices.
		// Triangles should be wound clockwise.
		// Normals are calculated from the vertices.
		protected void AddTriangle(Vector3[] v) {
			if (v.Length == 3) {
				this.mesh.Add(v[0]);
				this.mesh.Add(v[1]);
				this.mesh.Add(v[2]);
				// calculate surface normal and append to meshNorm
				meshNorm.Add(Vector3.Cross(Vector3.Normalize(v[1] - v[0]), Vector3.Normalize(v[2] - v[0])));
			} else {
				throw new ArgumentException("AddTriangle expects a Vector3[3], but received a Vector3[" + v.Length + "] instead.");
			}
		}

		// Render the mesh.
		public void Render(float[] zbuffer) {
			for (int idx = 0; idx < mesh.Count; idx += 3) {
				// rotate the components of the triangle (around the center of the object) by this.rot
				Vector3 v0 = this.mesh[idx].RotateAround(center, rot);
				Vector3 v1 = this.mesh[idx+1].RotateAround(center, rot);
				Vector3 v2 = this.mesh[idx+2].RotateAround(center, rot);
				// determine which "pixels" the vertices are on
				// make sure at least one is within the screen bounds (continue loop if not)
				// step through each pixel the triangle covers
				//		make sure that the pixel is within the screen bounds (continue loop if not)
				//		interpolate the z coordinate, which is distance from camera
				//		check the z coordinate against the zbuffer, and if it contains something closer, continue loop
				//		rotate surface normal by this.rot
				//		calculate angle between light and surface normal using dot product
				//		angle 180 is 100% brightness, angle 90 (1/2 pi) or less is 0% brightness
				//		shadow map?
				//		convert brightness to character
				//		draw pixel
			}
		}

	}	
}
