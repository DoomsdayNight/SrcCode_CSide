using System;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000508 RID: 1288
	public struct NKMVector4
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x000BED6C File Offset: 0x000BCF6C
		public NKMVector4(float fx = 0f, float fy = 0f, float fz = 0f, float fw = 0f)
		{
			this.x = fx;
			this.y = fy;
			this.z = fz;
			this.w = fw;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000BED8B File Offset: 0x000BCF8B
		public void Set(float fx = 0f, float fy = 0f, float fz = 0f, float fw = 0f)
		{
			this.x = fx;
			this.y = fy;
			this.z = fz;
			this.w = fw;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000BEDAC File Offset: 0x000BCFAC
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				cNKMLua.GetData(1, ref this.x);
				cNKMLua.GetData(2, ref this.y);
				cNKMLua.GetData(3, ref this.z);
				cNKMLua.GetData(4, ref this.w);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000BEE04 File Offset: 0x000BD004
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				cNKMLua.GetData(1, ref this.x);
				cNKMLua.GetData(2, ref this.y);
				cNKMLua.GetData(3, ref this.z);
				cNKMLua.GetData(4, ref this.w);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000BEE5A File Offset: 0x000BD05A
		public static implicit operator Vector4(NKMVector4 nv)
		{
			return new Vector4(nv.x, nv.y, nv.z, nv.w);
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000BEE79 File Offset: 0x000BD079
		public static implicit operator NKMVector4(Vector4 uv)
		{
			return new NKMVector4(uv.x, uv.y, uv.z, uv.w);
		}

		// Token: 0x04002657 RID: 9815
		public float x;

		// Token: 0x04002658 RID: 9816
		public float y;

		// Token: 0x04002659 RID: 9817
		public float z;

		// Token: 0x0400265A RID: 9818
		public float w;
	}
}
