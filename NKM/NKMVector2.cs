using System;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000506 RID: 1286
	public struct NKMVector2
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x000BE75C File Offset: 0x000BC95C
		public NKMVector2(float fx = 0f, float fy = 0f)
		{
			this.x = fx;
			this.y = fy;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000BE76C File Offset: 0x000BC96C
		public void Init(float fx = 0f, float fy = 0f)
		{
			this.x = fx;
			this.y = fy;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000BE77C File Offset: 0x000BC97C
		public void DeepCopyFromSource(NKMVector2 source)
		{
			this.x = source.x;
			this.y = source.y;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000BE796 File Offset: 0x000BC996
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				cNKMLua.GetData(1, ref this.x);
				cNKMLua.GetData(2, ref this.y);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000BE7C5 File Offset: 0x000BC9C5
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				cNKMLua.GetData(1, ref this.x);
				cNKMLua.GetData(2, ref this.y);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000BE7F4 File Offset: 0x000BC9F4
		public static NKMVector2 operator -(NKMVector2 a, NKMVector2 b)
		{
			return new NKMVector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000BE815 File Offset: 0x000BCA15
		public static NKMVector2 operator -(NKMVector2 a)
		{
			return new NKMVector2(-a.x, -a.y);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000BE82A File Offset: 0x000BCA2A
		public static NKMVector2 operator +(NKMVector2 a, NKMVector2 b)
		{
			return new NKMVector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000BE84B File Offset: 0x000BCA4B
		public static NKMVector2 operator /(NKMVector2 a, NKMVector2 b)
		{
			return new NKMVector2(a.x / b.x, a.y / b.y);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000BE86C File Offset: 0x000BCA6C
		public static NKMVector2 operator *(NKMVector2 a, NKMVector2 b)
		{
			return new NKMVector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000BE88D File Offset: 0x000BCA8D
		public static NKMVector2 operator *(NKMVector2 a, float b)
		{
			return new NKMVector2(a.x * b, a.y * b);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000BE8A4 File Offset: 0x000BCAA4
		public static NKMVector2 operator /(NKMVector2 a, float b)
		{
			return new NKMVector2(a.x / b, a.y / b);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000BE8BC File Offset: 0x000BCABC
		public void Normalize()
		{
			float num = this.x * this.x + this.y * this.y;
			if (num > 0f)
			{
				num = (float)Math.Sqrt((double)num);
				this.x /= num;
				this.y /= num;
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000BE914 File Offset: 0x000BCB14
		public static NKMVector2 Vec2Normalize(out NKMVector2 pOut, NKMVector2 pV)
		{
			float num = pV.x * pV.x + pV.y * pV.y;
			if (num > 0f)
			{
				num = (float)Math.Sqrt((double)num);
				pOut.x = pV.x / num;
				pOut.y = pV.y / num;
				return pOut;
			}
			pOut = pV;
			return pOut;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000BE97E File Offset: 0x000BCB7E
		public static float Vec2Dot(NKMVector2 a, NKMVector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000BE99B File Offset: 0x000BCB9B
		public static implicit operator Vector2(NKMVector2 nv)
		{
			return new Vector2(nv.x, nv.y);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000BE9AE File Offset: 0x000BCBAE
		public static implicit operator NKMVector2(Vector2 uv)
		{
			return new NKMVector2(uv.x, uv.y);
		}

		// Token: 0x04002652 RID: 9810
		public float x;

		// Token: 0x04002653 RID: 9811
		public float y;
	}
}
