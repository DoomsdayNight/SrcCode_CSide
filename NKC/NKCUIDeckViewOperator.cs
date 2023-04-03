using System;
using NKM;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A19 RID: 2585
	public class NKCUIDeckViewOperator : NKCUIInstantiatable
	{
		// Token: 0x060070E3 RID: 28899 RVA: 0x0025773B File Offset: 0x0025593B
		public void Init()
		{
			this.m_ScaleX.SetNowValue(this.m_fSelectedScale);
			this.m_ScaleY.SetNowValue(this.m_fSelectedScale);
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x0025775F File Offset: 0x0025595F
		public void Close()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x0025777C File Offset: 0x0025597C
		public void Update()
		{
			this.m_ScaleX.Update(Time.deltaTime);
			this.m_ScaleY.Update(Time.deltaTime);
			if (!this.m_ScaleX.IsTracking() && this.m_bTrackingStarted)
			{
				this.m_bTrackingStarted = false;
			}
			Vector2 v = this.m_rectAnchor.localScale;
			v.Set(this.m_ScaleX.GetNowValue(), this.m_ScaleY.GetNowValue());
			this.m_rectAnchor.localScale = v;
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x00257804 File Offset: 0x00255A04
		public void Enable()
		{
			this.m_ScaleX.SetTracking(this.m_fSelectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_ScaleY.SetTracking(this.m_fSelectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_bTrackingStarted = true;
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x0025783B File Offset: 0x00255A3B
		public void Disable()
		{
			this.m_ScaleX.SetTracking(this.m_fDeselectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_ScaleY.SetTracking(this.m_fDeselectedScale, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_bTrackingStarted = true;
		}

		// Token: 0x04005CA6 RID: 23718
		public RectTransform m_rectAnchor;

		// Token: 0x04005CA7 RID: 23719
		public float m_fSelectedScale = 1f;

		// Token: 0x04005CA8 RID: 23720
		public float m_fDeselectedScale = 0.7f;

		// Token: 0x04005CA9 RID: 23721
		private NKMTrackingFloat m_ScaleX = new NKMTrackingFloat();

		// Token: 0x04005CAA RID: 23722
		private NKMTrackingFloat m_ScaleY = new NKMTrackingFloat();

		// Token: 0x04005CAB RID: 23723
		private bool m_bTrackingStarted;
	}
}
