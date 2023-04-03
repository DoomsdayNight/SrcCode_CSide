using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000736 RID: 1846
	public static class NKMVector3Extension
	{
		// Token: 0x060049C7 RID: 18887 RVA: 0x00162588 File Offset: 0x00160788
		public static Vector3 GetNowUnityValue(this NKMTrackingVector3 cNKMVector3)
		{
			NKMVector3 nowValue = cNKMVector3.GetNowValue();
			return new Vector3(nowValue.x, nowValue.y, nowValue.z);
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x001625B4 File Offset: 0x001607B4
		public static Vector3 GetUnityDelta(this NKMTrackingVector3 cNKMVector3)
		{
			NKMVector3 delta = cNKMVector3.GetDelta();
			return new Vector3(delta.x, delta.y, delta.z);
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x001625E0 File Offset: 0x001607E0
		public static Vector3 GetBeforeUnityValue(this NKMTrackingVector3 cNKMVector3)
		{
			NKMVector3 beforeValue = cNKMVector3.GetBeforeValue();
			return new Vector3(beforeValue.x, beforeValue.y, beforeValue.z);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0016260C File Offset: 0x0016080C
		public static Vector3 GetTargetUnityValue(this NKMTrackingVector3 cNKMVector3)
		{
			NKMVector3 targetValue = cNKMVector3.GetTargetValue();
			return new Vector3(targetValue.x, targetValue.y, targetValue.z);
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x00162637 File Offset: 0x00160837
		public static void SetNowValue(this NKMTrackingVector3 cNKMVector3, Vector3 NowValue)
		{
			cNKMVector3.SetNowValue(NowValue.x, NowValue.y, NowValue.z);
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x00162654 File Offset: 0x00160854
		public static void SetTracking(this NKMTrackingVector3 cNKMVector3, Vector3 targetVal, float fTime, TRACKING_DATA_TYPE eTrackingType)
		{
			NKMVector3 targetVal2 = new NKMVector3(targetVal.x, targetVal.y, targetVal.z);
			cNKMVector3.SetTracking(targetVal2, fTime, eTrackingType);
		}
	}
}
