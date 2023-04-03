using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000735 RID: 1845
	public static class NKMVector2Extension
	{
		// Token: 0x060049C1 RID: 18881 RVA: 0x00162494 File Offset: 0x00160694
		public static Vector2 GetNowUnityValue(this NKMTrackingVector2 cNKMVector2)
		{
			NKMVector2 nowValue = cNKMVector2.GetNowValue();
			return new Vector2(nowValue.x, nowValue.y);
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x001624BC File Offset: 0x001606BC
		public static Vector2 GetUnityDelta(this NKMTrackingVector2 cNKMVector2)
		{
			NKMVector2 delta = cNKMVector2.GetDelta();
			return new Vector2(delta.x, delta.y);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x001624E4 File Offset: 0x001606E4
		public static Vector2 GetBeforeUnityValue(this NKMTrackingVector2 cNKMVector2)
		{
			NKMVector2 beforeValue = cNKMVector2.GetBeforeValue();
			return new Vector2(beforeValue.x, beforeValue.y);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0016250C File Offset: 0x0016070C
		public static Vector2 GetTargetUnityValue(this NKMTrackingVector2 cNKMVector2)
		{
			NKMVector2 targetValue = cNKMVector2.GetTargetValue();
			return new Vector2(targetValue.x, targetValue.y);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x00162534 File Offset: 0x00160734
		public static void SetNowValue(this NKMTrackingVector2 cNKMVector2, Vector2 NowValue)
		{
			NKMVector2 nowValue = new NKMVector2(NowValue.x, NowValue.y);
			cNKMVector2.SetNowValue(nowValue);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0016255C File Offset: 0x0016075C
		public static void SetTracking(this NKMTrackingVector2 cNKMVector2, Vector2 targetVal, float fTime, TRACKING_DATA_TYPE eTrackingType)
		{
			NKMVector2 targetVal2 = new NKMVector2(targetVal.x, targetVal.y);
			cNKMVector2.SetTracking(targetVal2, fTime, eTrackingType);
		}
	}
}
