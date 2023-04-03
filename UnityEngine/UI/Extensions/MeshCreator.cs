using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F0 RID: 752
	[ExecuteInEditMode]
	public class MeshCreator : MonoBehaviour
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x00039EB4 File Offset: 0x000380B4
		public void CreateMesh(List<Vector2> points)
		{
			Vector2[] array = points.ToArray();
			int[] triangles = new Triangulator(array).Triangulate();
			Vector3[] array2 = new Vector3[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new Vector3(array[i].x, array[i].y, 0f);
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array2;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			base.GetComponent<MeshFilter>().mesh = mesh;
		}
	}
}
