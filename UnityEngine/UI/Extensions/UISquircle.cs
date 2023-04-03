using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000327 RID: 807
	[AddComponentMenu("UI/Extensions/Primitives/Squircle")]
	public class UISquircle : UIPrimitiveBase
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x000458E8 File Offset: 0x00043AE8
		private float SquircleFunc(float t, bool xByY)
		{
			if (xByY)
			{
				return (float)Math.Pow(1.0 - Math.Pow((double)(t / this.a), (double)this.n), (double)(1f / this.n)) * this.b;
			}
			return (float)Math.Pow(1.0 - Math.Pow((double)(t / this.b), (double)this.n), (double)(1f / this.n)) * this.a;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004596C File Offset: 0x00043B6C
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = base.rectTransform.rect.width / 2f;
			float num4 = base.rectTransform.rect.height / 2f;
			if (this.squircleType == UISquircle.Type.Classic)
			{
				this.a = num3;
				this.b = num4;
			}
			else
			{
				this.a = Mathf.Min(new float[]
				{
					num3,
					num4,
					this.radius
				});
				this.b = this.a;
				num = num3 - this.a;
				num2 = num4 - this.a;
			}
			float num5 = 0f;
			float num6 = 1f;
			this.vert.Clear();
			this.vert.Add(new Vector2(0f, num4));
			while (num5 < num6)
			{
				num6 = this.SquircleFunc(num5, true);
				this.vert.Add(new Vector2(num + num5, num2 + num6));
				num5 += this.delta;
			}
			if (float.IsNaN(this.vert.Last<Vector2>().y))
			{
				this.vert.RemoveAt(this.vert.Count - 1);
			}
			while (num6 > 0f)
			{
				num5 = this.SquircleFunc(num6, false);
				this.vert.Add(new Vector2(num + num5, num2 + num6));
				num6 -= this.delta;
			}
			this.vert.Add(new Vector2(num3, 0f));
			for (int i = 1; i < this.vert.Count - 1; i++)
			{
				if (this.vert[i].x < this.vert[i].y)
				{
					if (this.vert[i - 1].y - this.vert[i].y < this.quality)
					{
						this.vert.RemoveAt(i);
						i--;
					}
				}
				else if (this.vert[i].x - this.vert[i - 1].x < this.quality)
				{
					this.vert.RemoveAt(i);
					i--;
				}
			}
			this.vert.AddRange(from t in this.vert.AsEnumerable<Vector2>().Reverse<Vector2>()
			select new Vector2(t.x, -t.y));
			this.vert.AddRange(from t in this.vert.AsEnumerable<Vector2>().Reverse<Vector2>()
			select new Vector2(-t.x, t.y));
			vh.Clear();
			for (int j = 0; j < this.vert.Count - 1; j++)
			{
				vh.AddVert(this.vert[j], this.color, Vector2.zero);
				vh.AddVert(this.vert[j + 1], this.color, Vector2.zero);
				vh.AddVert(Vector2.zero, this.color, Vector2.zero);
				vh.AddTriangle(j * 3, j * 3 + 1, j * 3 + 2);
			}
		}

		// Token: 0x04000D04 RID: 3332
		private const float C = 1f;

		// Token: 0x04000D05 RID: 3333
		[Space]
		public UISquircle.Type squircleType = UISquircle.Type.Scaled;

		// Token: 0x04000D06 RID: 3334
		[Range(1f, 40f)]
		public float n = 4f;

		// Token: 0x04000D07 RID: 3335
		[Min(0.1f)]
		public float delta = 5f;

		// Token: 0x04000D08 RID: 3336
		public float quality = 0.1f;

		// Token: 0x04000D09 RID: 3337
		[Min(0f)]
		public float radius = 1000f;

		// Token: 0x04000D0A RID: 3338
		private float a;

		// Token: 0x04000D0B RID: 3339
		private float b;

		// Token: 0x04000D0C RID: 3340
		private List<Vector2> vert = new List<Vector2>();

		// Token: 0x02001164 RID: 4452
		public enum Type
		{
			// Token: 0x0400923D RID: 37437
			Classic,
			// Token: 0x0400923E RID: 37438
			Scaled
		}
	}
}
