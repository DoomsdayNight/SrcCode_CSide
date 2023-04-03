using System;

namespace NKM
{
	// Token: 0x020003B1 RID: 945
	public class NKMColor
	{
		// Token: 0x060018CE RID: 6350 RVA: 0x00064AAF File Offset: 0x00062CAF
		public NKMColor()
		{
			this.Init(1f, 1f, 1f, 1f);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00064AD1 File Offset: 0x00062CD1
		public void Init(float fr = 1f, float fg = 1f, float fb = 1f, float fa = 1f)
		{
			this.r = fr;
			this.g = fg;
			this.b = fb;
			this.a = fa;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00064AF0 File Offset: 0x00062CF0
		public void DeepCopyFromSource(NKMColor source)
		{
			this.r = source.r;
			this.g = source.g;
			this.b = source.b;
			this.a = source.a;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00064B24 File Offset: 0x00062D24
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				cNKMLua.GetData(1, ref this.r);
				cNKMLua.GetData(2, ref this.g);
				cNKMLua.GetData(3, ref this.b);
				cNKMLua.GetData(4, ref this.a);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00064B7C File Offset: 0x00062D7C
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				cNKMLua.GetData(1, ref this.r);
				cNKMLua.GetData(2, ref this.g);
				cNKMLua.GetData(3, ref this.b);
				cNKMLua.GetData(4, ref this.a);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x040010EE RID: 4334
		public float r;

		// Token: 0x040010EF RID: 4335
		public float g;

		// Token: 0x040010F0 RID: 4336
		public float b;

		// Token: 0x040010F1 RID: 4337
		public float a;
	}
}
