using System;
using Cs.Math;

namespace NKM
{
	// Token: 0x0200042D RID: 1069
	public struct NKMMatrix
	{
		// Token: 0x06001D1C RID: 7452 RVA: 0x00087A35 File Offset: 0x00085C35
		public static float ToRadian(float degree)
		{
			return degree * 0.017453292f;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00087A3E File Offset: 0x00085C3E
		public static float ToDegree(float radian)
		{
			return radian * 57.295776f;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00087A48 File Offset: 0x00085C48
		public static NKMMatrix operator -(NKMMatrix a)
		{
			return new NKMMatrix
			{
				_11 = -a._11,
				_12 = -a._12,
				_13 = -a._13,
				_14 = -a._14,
				_21 = -a._21,
				_22 = -a._22,
				_23 = -a._23,
				_24 = -a._24,
				_31 = -a._31,
				_32 = -a._32,
				_33 = -a._33,
				_34 = -a._34,
				_41 = -a._41,
				_42 = -a._42,
				_43 = -a._43,
				_44 = -a._44
			};
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00087B40 File Offset: 0x00085D40
		public static NKMMatrix operator +(NKMMatrix a, NKMMatrix b)
		{
			return new NKMMatrix
			{
				_11 = a._11 + b._11,
				_12 = a._12 + b._12,
				_13 = a._13 + b._13,
				_14 = a._14 + b._14,
				_21 = a._21 + b._21,
				_22 = a._22 + b._22,
				_23 = a._23 + b._23,
				_24 = a._24 + b._24,
				_31 = a._31 + b._31,
				_32 = a._32 + b._32,
				_33 = a._33 + b._33,
				_34 = a._34 + b._34,
				_41 = a._41 + b._41,
				_42 = a._42 + b._42,
				_43 = a._43 + b._43,
				_44 = a._44 + b._44
			};
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00087C98 File Offset: 0x00085E98
		public static NKMMatrix operator -(NKMMatrix a, NKMMatrix b)
		{
			return new NKMMatrix
			{
				_11 = a._11 - b._11,
				_12 = a._12 - b._12,
				_13 = a._13 - b._13,
				_14 = a._14 - b._14,
				_21 = a._21 - b._21,
				_22 = a._22 - b._22,
				_23 = a._23 - b._23,
				_24 = a._24 - b._24,
				_31 = a._31 - b._31,
				_32 = a._32 - b._32,
				_33 = a._33 - b._33,
				_34 = a._34 - b._34,
				_41 = a._41 - b._41,
				_42 = a._42 - b._42,
				_43 = a._43 - b._43,
				_44 = a._44 - b._44
			};
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00087DF0 File Offset: 0x00085FF0
		public static NKMMatrix operator *(NKMMatrix a, float v)
		{
			return new NKMMatrix
			{
				_11 = a._11 * v,
				_12 = a._12 * v,
				_13 = a._13 * v,
				_14 = a._14 * v,
				_21 = a._21 * v,
				_22 = a._22 * v,
				_23 = a._23 * v,
				_24 = a._24 * v,
				_31 = a._31 * v,
				_32 = a._32 * v,
				_33 = a._33 * v,
				_34 = a._34 * v,
				_41 = a._41 * v,
				_42 = a._42 * v,
				_43 = a._43 * v,
				_44 = a._44 * v
			};
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x00087EF8 File Offset: 0x000860F8
		public static NKMMatrix operator /(NKMMatrix a, float v)
		{
			return new NKMMatrix
			{
				_11 = a._11 / v,
				_12 = a._12 / v,
				_13 = a._13 / v,
				_14 = a._14 / v,
				_21 = a._21 / v,
				_22 = a._22 / v,
				_23 = a._23 / v,
				_24 = a._24 / v,
				_31 = a._31 / v,
				_32 = a._32 / v,
				_33 = a._33 / v,
				_34 = a._34 / v,
				_41 = a._41 / v,
				_42 = a._42 / v,
				_43 = a._43 / v,
				_44 = a._44 / v
			};
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x00088000 File Offset: 0x00086200
		public static NKMMatrix MatrixRotationYawPitchRoll(out NKMMatrix pout, float yaw, float pitch, float roll)
		{
			NKMMatrix pM;
			NKMMatrix.MatrixIdentity(out pM);
			NKMMatrix pM2;
			NKMMatrix.MatrixRotationZ(out pM2, roll);
			NKMMatrix pM3;
			NKMMatrix.MatrixMultiply(out pM3, pM, pM2);
			NKMMatrix.MatrixRotationX(out pM2, pitch);
			NKMMatrix pM4;
			NKMMatrix.MatrixMultiply(out pM4, pM3, pM2);
			NKMMatrix.MatrixRotationY(out pM2, yaw);
			NKMMatrix.MatrixMultiply(out pout, pM4, pM2);
			return pout;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00088054 File Offset: 0x00086254
		public static NKMMatrix MatrixIdentity(out NKMMatrix pOut)
		{
			pOut._11 = 1f;
			pOut._12 = 0f;
			pOut._13 = 0f;
			pOut._14 = 0f;
			pOut._21 = 0f;
			pOut._22 = 1f;
			pOut._23 = 0f;
			pOut._24 = 0f;
			pOut._31 = 0f;
			pOut._32 = 0f;
			pOut._33 = 1f;
			pOut._34 = 0f;
			pOut._41 = 0f;
			pOut._42 = 0f;
			pOut._43 = 0f;
			pOut._44 = 1f;
			return pOut;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00088118 File Offset: 0x00086318
		public static NKMMatrix MatrixRotationX(out NKMMatrix pOut, float Angle)
		{
			NKMMatrix.MatrixIdentity(out pOut);
			float num = (float)Math.Cos((double)Angle);
			float num2 = (float)Math.Sin((double)Angle);
			pOut._22 = num;
			pOut._33 = num;
			pOut._23 = num2;
			pOut._32 = -num2;
			return pOut;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00088164 File Offset: 0x00086364
		public static NKMMatrix MatrixRotationY(out NKMMatrix pOut, float Angle)
		{
			NKMMatrix.MatrixIdentity(out pOut);
			float num = (float)Math.Cos((double)Angle);
			float num2 = (float)Math.Sin((double)Angle);
			pOut._11 = num;
			pOut._33 = num;
			pOut._13 = -num2;
			pOut._31 = num2;
			return pOut;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000881B0 File Offset: 0x000863B0
		public static NKMMatrix MatrixRotationZ(out NKMMatrix pOut, float Angle)
		{
			NKMMatrix.MatrixIdentity(out pOut);
			float num = (float)Math.Cos((double)Angle);
			float num2 = (float)Math.Sin((double)Angle);
			pOut._11 = num;
			pOut._22 = num;
			pOut._12 = num2;
			pOut._21 = -num2;
			return pOut;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000881FC File Offset: 0x000863FC
		public static NKMMatrix MatrixMultiply(out NKMMatrix pOut, NKMMatrix pM1, NKMMatrix pM2)
		{
			NKMMatrix.MatrixIdentity(out pOut);
			pOut._11 = pM1._11 * pM2._11 + pM1._12 * pM2._21 + pM1._13 * pM2._31 + pM1._14 * pM2._41;
			pOut._12 = pM1._11 * pM2._12 + pM1._12 * pM2._22 + pM1._13 * pM2._32 + pM1._14 * pM2._42;
			pOut._13 = pM1._11 * pM2._13 + pM1._12 * pM2._23 + pM1._13 * pM2._33 + pM1._14 * pM2._43;
			pOut._14 = pM1._11 * pM2._14 + pM1._12 * pM2._24 + pM1._13 * pM2._34 + pM1._14 * pM2._44;
			pOut._21 = pM1._21 * pM2._11 + pM1._22 * pM2._21 + pM1._23 * pM2._31 + pM1._24 * pM2._41;
			pOut._22 = pM1._21 * pM2._12 + pM1._22 * pM2._22 + pM1._23 * pM2._32 + pM1._24 * pM2._42;
			pOut._23 = pM1._21 * pM2._13 + pM1._22 * pM2._23 + pM1._23 * pM2._33 + pM1._24 * pM2._43;
			pOut._24 = pM1._21 * pM2._14 + pM1._22 * pM2._24 + pM1._23 * pM2._34 + pM1._24 * pM2._44;
			pOut._31 = pM1._31 * pM2._11 + pM1._32 * pM2._21 + pM1._33 * pM2._31 + pM1._34 * pM2._41;
			pOut._32 = pM1._31 * pM2._12 + pM1._32 * pM2._22 + pM1._33 * pM2._32 + pM1._34 * pM2._42;
			pOut._33 = pM1._31 * pM2._13 + pM1._32 * pM2._23 + pM1._33 * pM2._33 + pM1._34 * pM2._43;
			pOut._34 = pM1._31 * pM2._14 + pM1._32 * pM2._24 + pM1._33 * pM2._34 + pM1._34 * pM2._44;
			pOut._41 = pM1._41 * pM2._11 + pM1._42 * pM2._21 + pM1._43 * pM2._31 + pM1._44 * pM2._41;
			pOut._42 = pM1._41 * pM2._12 + pM1._42 * pM2._22 + pM1._43 * pM2._32 + pM1._44 * pM2._42;
			pOut._43 = pM1._41 * pM2._13 + pM1._42 * pM2._23 + pM1._43 * pM2._33 + pM1._44 * pM2._43;
			pOut._44 = pM1._41 * pM2._14 + pM1._42 * pM2._24 + pM1._43 * pM2._34 + pM1._44 * pM2._44;
			return pOut;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000885E8 File Offset: 0x000867E8
		public static NKMMatrix MatrixLookAtLH(out NKMMatrix pOut, NKMVector3 pEye, NKMVector3 pAt, NKMVector3 pUp)
		{
			NKMVector3 nkmvector;
			nkmvector.x = pAt.x - pEye.x;
			nkmvector.y = pAt.y - pEye.y;
			nkmvector.z = pAt.z - pEye.z;
			nkmvector.Normalize();
			NKMVector3 nkmvector2;
			NKMVector3.Vec3Cross(out nkmvector2, pUp, nkmvector);
			nkmvector2.Normalize();
			NKMVector3 nkmvector3;
			NKMVector3.Vec3Cross(out nkmvector3, nkmvector, nkmvector2);
			pOut._11 = nkmvector2.x;
			pOut._12 = nkmvector3.x;
			pOut._13 = nkmvector.x;
			pOut._14 = 0f;
			pOut._21 = nkmvector2.y;
			pOut._22 = nkmvector3.y;
			pOut._23 = nkmvector.y;
			pOut._24 = 0f;
			pOut._31 = nkmvector2.z;
			pOut._32 = nkmvector3.z;
			pOut._33 = nkmvector.z;
			pOut._34 = 0f;
			pOut._41 = -NKMVector3.Vec3Dot(nkmvector2, pEye);
			pOut._42 = -NKMVector3.Vec3Dot(nkmvector3, pEye);
			pOut._43 = -NKMVector3.Vec3Dot(nkmvector, pEye);
			pOut._44 = 1f;
			return pOut;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0008871B File Offset: 0x0008691B
		public static NKMMatrix MatrixInverse(out NKMMatrix pOut, float pDeterminant, NKMMatrix pM)
		{
			return NKMMatrix.MatrixInverseTrace(out pOut, pDeterminant, pM);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00088728 File Offset: 0x00086928
		public static NKMMatrix MatrixInverseTrace(out NKMMatrix pOut, float pDeterminant, NKMMatrix pM)
		{
			float num = NKMMatrix.MatrixDeterminant(pM);
			NKMMatrix nkmmatrix;
			NKMMatrix.MatrixMultiply(out nkmmatrix, pM, pM);
			NKMMatrix nkmmatrix2;
			NKMMatrix.MatrixMultiply(out nkmmatrix2, nkmmatrix, pM);
			float num2 = NKMMatrix.MatrixTrace(pM);
			float num3 = NKMMatrix.MatrixTrace(nkmmatrix);
			float num4 = NKMMatrix.MatrixTrace(nkmmatrix2);
			float v = (num2 * num2 * num2 - 3f * num2 * num3 + 2f * num4) / 6f;
			float v2 = -(num2 * num2 - num3) / 2f;
			NKMMatrix a;
			NKMMatrix.MatrixIdentity(out a);
			pOut = (a * v + pM * v2 + nkmmatrix * num2 - nkmmatrix2) / num;
			if (!pDeterminant.IsNearlyZero(1E-05f))
			{
				pDeterminant = num;
			}
			return pOut;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000887F0 File Offset: 0x000869F0
		public static float MatrixDeterminant(NKMMatrix pM)
		{
			return pM._11 * pM._22 * pM._33 * pM._44 + pM._11 * pM._23 * pM._34 * pM._42 + pM._11 * pM._24 * pM._32 * pM._43 + pM._12 * pM._21 * pM._34 * pM._43 + pM._12 * pM._23 * pM._31 * pM._44 + pM._12 * pM._24 * pM._33 * pM._41 + pM._13 * pM._21 * pM._32 * pM._44 + pM._13 * pM._22 * pM._34 * pM._41 + pM._13 * pM._24 * pM._31 * pM._42 + pM._14 * pM._21 * pM._33 * pM._42 + pM._14 * pM._22 * pM._31 * pM._43 + pM._14 * pM._23 * pM._32 * pM._41 - (pM._11 * pM._22 * pM._34 * pM._44 + pM._11 * pM._23 * pM._32 * pM._44 + pM._11 * pM._24 * pM._33 * pM._42 + pM._12 * pM._21 * pM._33 * pM._44 + pM._12 * pM._23 * pM._34 * pM._41 + pM._12 * pM._24 * pM._32 * pM._41 + pM._13 * pM._21 * pM._34 * pM._42 + pM._13 * pM._22 * pM._31 * pM._44 + pM._13 * pM._24 * pM._32 * pM._41 + pM._14 * pM._21 * pM._32 * pM._43 + pM._14 * pM._22 * pM._33 * pM._41 + pM._14 * pM._23 * pM._31 * pM._42);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00088A9C File Offset: 0x00086C9C
		public static float MatrixTrace(NKMMatrix pM)
		{
			return pM._11 + pM._22 + pM._33 + pM._44;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00088ABC File Offset: 0x00086CBC
		public static NKMVector4 Vec3Transform(out NKMVector4 pOut, NKMVector3 pV, NKMMatrix pM)
		{
			pOut.x = pV.x * pM._11 + pV.y * pM._21 + pV.z * pM._31 + pM._41;
			pOut.y = pV.x * pM._12 + pV.y * pM._22 + pV.z * pM._32 + pM._42;
			pOut.z = pV.x * pM._13 + pV.y * pM._23 + pV.z * pM._33 + pM._43;
			pOut.w = pV.x * pM._14 + pV.y * pM._24 + pV.z * pM._34 + pM._44;
			return pOut;
		}

		// Token: 0x04001C85 RID: 7301
		public float _11;

		// Token: 0x04001C86 RID: 7302
		public float _12;

		// Token: 0x04001C87 RID: 7303
		public float _13;

		// Token: 0x04001C88 RID: 7304
		public float _14;

		// Token: 0x04001C89 RID: 7305
		public float _21;

		// Token: 0x04001C8A RID: 7306
		public float _22;

		// Token: 0x04001C8B RID: 7307
		public float _23;

		// Token: 0x04001C8C RID: 7308
		public float _24;

		// Token: 0x04001C8D RID: 7309
		public float _31;

		// Token: 0x04001C8E RID: 7310
		public float _32;

		// Token: 0x04001C8F RID: 7311
		public float _33;

		// Token: 0x04001C90 RID: 7312
		public float _34;

		// Token: 0x04001C91 RID: 7313
		public float _41;

		// Token: 0x04001C92 RID: 7314
		public float _42;

		// Token: 0x04001C93 RID: 7315
		public float _43;

		// Token: 0x04001C94 RID: 7316
		public float _44;
	}
}
