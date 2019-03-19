using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosmos.Geometry
{
	/// <summary>
	/// Funciones utiles para calculos geometricos.
	/// </summary>
	public class Utility 
	{
		/// <summary>
		/// Determines if is point inside rect the specified point r.
		/// </summary>
		/// <returns><c>true</c> if is point inside rect the specified point r; otherwise, <c>false</c>.</returns>
		/// <param name="point">Point.</param>
		/// <param name="r">The rect.</param>
		public static bool IsPointInsideRect(Vector2 point, Rect r)
		{
			Debug.Log("point: " + point + ", rect: " + r);

			bool up = point.y > r.y;
			bool bottom = point.y < (r.y + r.height);
			bool left = point.x > r.x;
			bool right = point.x < (r.x + r.width);

			return up && bottom && left && right;
		}
	}
}


