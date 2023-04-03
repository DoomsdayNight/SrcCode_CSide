using System;
using UnityEngine;

namespace NKC.Advertise
{
	// Token: 0x02000C6D RID: 3181
	public abstract class NKCAdBase : MonoBehaviour
	{
		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x060093D5 RID: 37845 RVA: 0x00327AA6 File Offset: 0x00325CA6
		public static NKCAdBase Instance
		{
			get
			{
				if (NKCAdBase.adInstance == null)
				{
					NKCAdBase.CreateInstance();
				}
				return NKCAdBase.adInstance;
			}
		}

		// Token: 0x060093D6 RID: 37846 RVA: 0x00327ABF File Offset: 0x00325CBF
		private static void CreateInstance()
		{
			if (NKCAdBase.objInstance == null)
			{
				NKCAdBase.objInstance = new GameObject("NKCAdvertise");
				UnityEngine.Object.DontDestroyOnLoad(NKCAdBase.objInstance);
			}
			NKCAdBase.adInstance = NKCAdBase.objInstance.AddComponent<NKCAdNone>();
		}

		// Token: 0x060093D7 RID: 37847 RVA: 0x00327AF6 File Offset: 0x00325CF6
		public static void InitInstance()
		{
			NKCAdBase.Instance.Initialize();
		}

		// Token: 0x060093D8 RID: 37848 RVA: 0x00327B02 File Offset: 0x00325D02
		public virtual bool IsAdvertiseEnabled()
		{
			return false;
		}

		// Token: 0x060093D9 RID: 37849
		public abstract void Initialize();

		// Token: 0x060093DA RID: 37850
		public abstract void WatchRewardedAd(NKCAdBase.AD_TYPE adType, NKCAdBase.OnUserEarnedReward onUserEarnedReward, NKCAdBase.OnAdFailedToShowAd onFailedToShowAd);

		// Token: 0x040080CC RID: 32972
		private static GameObject objInstance;

		// Token: 0x040080CD RID: 32973
		private static NKCAdBase adInstance;

		// Token: 0x02001A22 RID: 6690
		public enum NKC_ADMOB_ERROR_CODE
		{
			// Token: 0x0400ADC3 RID: 44483
			NARC_FAILED_TO_LOAD,
			// Token: 0x0400ADC4 RID: 44484
			NARC_FAILED_TO_SHOW
		}

		// Token: 0x02001A23 RID: 6691
		public enum AD_TYPE
		{
			// Token: 0x0400ADC6 RID: 44486
			CREDIT,
			// Token: 0x0400ADC7 RID: 44487
			ETERNIUM,
			// Token: 0x0400ADC8 RID: 44488
			UNIT_INV,
			// Token: 0x0400ADC9 RID: 44489
			EQUIP_INV,
			// Token: 0x0400ADCA RID: 44490
			SHIP_INV,
			// Token: 0x0400ADCB RID: 44491
			OPERATOR_INV
		}

		// Token: 0x02001A24 RID: 6692
		// (Invoke) Token: 0x0600BB32 RID: 47922
		public delegate void OnUserEarnedReward();

		// Token: 0x02001A25 RID: 6693
		// (Invoke) Token: 0x0600BB36 RID: 47926
		public delegate void OnAdFailedToShowAd(NKCAdBase.NKC_ADMOB_ERROR_CODE resultCode, string message);
	}
}
