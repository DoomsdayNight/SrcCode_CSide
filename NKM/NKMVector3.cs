using System;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000507 RID: 1287
	public struct NKMVector3
	{
		// Token: 0x060024E3 RID: 9443 RVA: 0x000BE9C1 File Offset: 0x000BCBC1
		public NKMVector3(float fx = 0f, float fy = 0f, float fz = 0f)
		{
			this.x = fx;
			this.y = fy;
			this.z = fz;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000BE9D8 File Offset: 0x000BCBD8
		public void Set(float fx = 0f, float fy = 0f, float fz = 0f)
		{
			this.x = fx;
			this.y = fy;
			this.z = fz;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000BE9EF File Offset: 0x000BCBEF
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				cNKMLua.GetData(1, ref this.x);
				cNKMLua.GetData(2, ref this.y);
				cNKMLua.GetData(3, ref this.z);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000BEA2C File Offset: 0x000BCC2C
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				cNKMLua.GetData(1, ref this.x);
				cNKMLua.GetData(2, ref this.y);
				cNKMLua.GetData(3, ref this.z);
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000BEA6C File Offset: 0x000BCC6C
		public void Normalize()
		{
			float num = this.x * this.x + this.z * this.z + this.y * this.y;
			if (num > 0f)
			{
				num = (float)Math.Sqrt((double)num);
				this.x /= num;
				this.y /= num;
				this.z /= num;
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000BEAE0 File Offset: 0x000BCCE0
		public static NKMVector3 Vec3Normalize(out NKMVector3 pOut, NKMVector3 pV)
		{
			float num = pV.x * pV.x + pV.z * pV.z + pV.y * pV.y;
			if (num > 0f)
			{
				num = (float)Math.Sqrt((double)num);
				pOut.x = pV.x / num;
				pOut.y = pV.y / num;
				pOut.z = pV.z / num;
				return pOut;
			}
			pOut = pV;
			return pOut;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000BEB68 File Offset: 0x000BCD68
		public static NKMVector3 Vec3Cross(out NKMVector3 pOut, NKMVector3 A, NKMVector3 B)
		{
			pOut.x = A.y * B.z - B.y * A.z;
			pOut.y = A.z * B.x - B.z * A.x;
			pOut.z = A.x * B.y - B.x * A.y;
			return pOut;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000BEBDE File Offset: 0x000BCDDE
		public static float Vec3Dot(NKMVector3 a, NKMVector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000BEC09 File Offset: 0x000BCE09
		public static NKMVector3 operator -(NKMVector3 a, NKMVector3 b)
		{
			return new NKMVector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000BEC37 File Offset: 0x000BCE37
		public static NKMVector3 operator -(NKMVector3 a)
		{
			return new NKMVector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000BEC53 File Offset: 0x000BCE53
		public static NKMVector3 operator +(NKMVector3 a, NKMVector3 b)
		{
			return new NKMVector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000BEC81 File Offset: 0x000BCE81
		public static NKMVector3 operator /(NKMVector3 a, NKMVector3 b)
		{
			return new NKMVector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000BECAF File Offset: 0x000BCEAF
		public static NKMVector3 operator *(NKMVector3 a, NKMVector3 b)
		{
			return new NKMVector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000BECDD File Offset: 0x000BCEDD
		public static NKMVector3 operator *(float a, NKMVector3 b)
		{
			return new NKMVector3(a * b.x, a * b.y, a * b.z);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000BECFC File Offset: 0x000BCEFC
		public static NKMVector3 operator *(NKMVector3 a, float b)
		{
			return new NKMVector3(a.x * b, a.y * b, a.z * b);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000BED1B File Offset: 0x000BCF1B
		public static NKMVector3 operator /(NKMVector3 a, float b)
		{
			return new NKMVector3(a.x / b, a.y / b, a.z / b);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000BED3A File Offset: 0x000BCF3A
		public static implicit operator Vector3(NKMVector3 nv)
		{
			return new Vector3(nv.x, nv.y, nv.z);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000BED53 File Offset: 0x000BCF53
		public static implicit operator NKMVector3(Vector3 uv)
		{
			return new NKMVector3(uv.x, uv.y, uv.z);
		}

		// Token: 0x04002654 RID: 9812
		public float x;

		// Token: 0x04002655 RID: 9813
		public float y;

		// Token: 0x04002656 RID: 9814
		public float z;
	}
}
