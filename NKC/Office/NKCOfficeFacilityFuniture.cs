using System;
using NKC.UI;
using NKC.UI.Component.Office;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x0200082F RID: 2095
	public class NKCOfficeFacilityFuniture : NKCOfficeFuniture
	{
		// Token: 0x06005357 RID: 21335 RVA: 0x00196962 File Offset: 0x00194B62
		public void SetLock(bool value)
		{
			this.m_bLock = value;
			if (this.m_comNameTag != null)
			{
				this.m_comNameTag.SetLock(value);
			}
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x00196985 File Offset: 0x00194B85
		public void SetAlarm(NKCAlarmManager.ALARM_TYPE type)
		{
			if (this.m_comNameTag != null)
			{
				this.m_comNameTag.SetAlarm(type);
			}
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x001969A1 File Offset: 0x00194BA1
		public void SetAlarm(bool value)
		{
			if (this.m_comNameTag != null)
			{
				this.m_comNameTag.SetAlarm(value);
			}
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x001969C0 File Offset: 0x00194BC0
		public override RectTransform MakeHighlightRect()
		{
			if (this.m_rtTutorialHighlightArea != null)
			{
				return this.m_rtTutorialHighlightArea;
			}
			if (this.m_comNameTag != null && this.m_comNameTag.m_objOpen)
			{
				return this.m_comNameTag.m_objOpen.GetComponent<RectTransform>();
			}
			return base.MakeHighlightRect();
		}

		// Token: 0x040042CD RID: 17101
		[Header("시설 가구 관련")]
		public NKCUIComOfficeFacilityNametag m_comNameTag;

		// Token: 0x040042CE RID: 17102
		public RectTransform m_rtTutorialHighlightArea;

		// Token: 0x040042CF RID: 17103
		private bool m_bLock;
	}
}
