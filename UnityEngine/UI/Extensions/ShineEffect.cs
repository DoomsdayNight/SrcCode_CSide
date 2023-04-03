using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E7 RID: 743
	public class ShineEffect : MaskableGraphic
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000370D7 File Offset: 0x000352D7
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x000370DF File Offset: 0x000352DF
		public float Yoffset
		{
			get
			{
				return this.yoffset;
			}
			set
			{
				this.SetVerticesDirty();
				this.yoffset = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000370EE File Offset: 0x000352EE
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x000370F6 File Offset: 0x000352F6
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.SetAllDirty();
				this.width = value;
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00037108 File Offset: 0x00035308
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
			float num = (vector.w - vector.y) * 2f;
			Color32 color = this.color;
			vh.Clear();
			color.a = 0;
			vh.AddVert(new Vector3(vector.x - 50f, this.width * vector.y + this.yoffset * num), color, new Vector2(0f, 0f));
			vh.AddVert(new Vector3(vector.z + 50f, this.width * vector.y + this.yoffset * num), color, new Vector2(1f, 0f));
			color.a = (byte)(this.color.a * 255f);
			vh.AddVert(new Vector3(vector.x - 50f, this.width * (vector.y / 4f) + this.yoffset * num), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(vector.z + 50f, this.width * (vector.y / 4f) + this.yoffset * num), color, new Vector2(1f, 1f));
			color.a = (byte)(this.color.a * 255f);
			vh.AddVert(new Vector3(vector.x - 50f, this.width * (vector.w / 4f) + this.yoffset * num), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(vector.z + 50f, this.width * (vector.w / 4f) + this.yoffset * num), color, new Vector2(1f, 1f));
			color.a = (byte)(this.color.a * 255f);
			color.a = 0;
			vh.AddVert(new Vector3(vector.x - 50f, this.width * vector.w + this.yoffset * num), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(vector.z + 50f, this.width * vector.w + this.yoffset * num), color, new Vector2(1f, 1f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 1);
			vh.AddTriangle(2, 3, 4);
			vh.AddTriangle(4, 5, 3);
			vh.AddTriangle(4, 5, 6);
			vh.AddTriangle(6, 7, 5);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00037430 File Offset: 0x00035630
		public void Triangulate(VertexHelper vh)
		{
			int num = vh.currentVertCount - 2;
			Debug.Log(num);
			for (int i = 0; i <= num / 2 + 1; i += 2)
			{
				vh.AddTriangle(i, i + 1, i + 2);
				vh.AddTriangle(i + 2, i + 3, i + 1);
			}
		}

		// Token: 0x04000B44 RID: 2884
		[SerializeField]
		private float yoffset = -1f;

		// Token: 0x04000B45 RID: 2885
		[SerializeField]
		private float width = 1f;
	}
}
