using System;
using NKC.UI;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200068C RID: 1676
	public class NKCHealthyGame
	{
		// Token: 0x060036C1 RID: 14017 RVA: 0x0011A1D6 File Offset: 0x001183D6
		public void Start()
		{
			this.m_bEnable = true;
			this.m_fLastTime = Time.time;
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x0011A1EC File Offset: 0x001183EC
		public void Update()
		{
			if (!this.m_bEnable)
			{
				return;
			}
			if (this.m_fLastTime + 3600f <= Time.time)
			{
				this.m_fLastTime = Time.time;
				this.m_HourCount++;
				NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, NKCStringTable.GetString("SI_DP_PC_HEALTH_WARNING", false), 1);
				NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, NKCUtilString.GetPlayTimeWarning(this.m_HourCount), 1);
			}
		}

		// Token: 0x040033F4 RID: 13300
		private bool m_bEnable;

		// Token: 0x040033F5 RID: 13301
		private float m_fLastTime = float.MinValue;

		// Token: 0x040033F6 RID: 13302
		private int m_HourCount;
	}
}
