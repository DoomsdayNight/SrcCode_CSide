using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007D2 RID: 2002
	public class NKCUIOpenAnimator
	{
		// Token: 0x06004EFF RID: 20223 RVA: 0x0017DAF4 File Offset: 0x0017BCF4
		public NKCUIOpenAnimator(GameObject go)
		{
			this.m_go = go;
			this.m_rt = this.m_go.GetComponent<RectTransform>();
			this.m_canvas = this.m_go.GetComponent<CanvasGroup>();
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0017DB48 File Offset: 0x0017BD48
		public void PlayOpenAni()
		{
			this.m_trackAlpha.SetNowValue(0f);
			if (this.m_canvas != null)
			{
				this.m_canvas.alpha = this.m_trackAlpha.GetNowValue();
			}
			this.m_trackScale.SetNowValue(1.1f);
			this.m_rt.localScale = Vector3.one * this.m_trackScale.GetNowValue();
			this.m_trackAlpha.SetTracking(1f, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_trackScale.SetTracking(1f, 0.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0017DBE8 File Offset: 0x0017BDE8
		public void Update()
		{
			float num = Time.deltaTime;
			if (NKCScenManager.GetScenManager() != null && num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
			{
				num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
			}
			this.m_trackAlpha.Update(num);
			this.m_trackScale.Update(num);
			if (this.m_trackAlpha.IsTracking() && this.m_canvas != null)
			{
				this.m_canvas.alpha = this.m_trackAlpha.GetNowValue();
			}
			if (this.m_trackScale.IsTracking())
			{
				this.m_rt.localScale = new Vector3(this.m_trackScale.GetNowValue(), this.m_trackScale.GetNowValue(), this.m_trackScale.GetNowValue());
			}
		}

		// Token: 0x04003EEC RID: 16108
		private GameObject m_go;

		// Token: 0x04003EED RID: 16109
		private RectTransform m_rt;

		// Token: 0x04003EEE RID: 16110
		private CanvasGroup m_canvas;

		// Token: 0x04003EEF RID: 16111
		private NKMTrackingFloat m_trackAlpha = new NKMTrackingFloat();

		// Token: 0x04003EF0 RID: 16112
		private NKMTrackingFloat m_trackScale = new NKMTrackingFloat();
	}
}
