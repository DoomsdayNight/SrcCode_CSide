using System;
using UnityEngine;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C67 RID: 3175
	public class NKCUIComOfficeFacilityNametag : MonoBehaviour
	{
		// Token: 0x060093BA RID: 37818 RVA: 0x00327280 File Offset: 0x00325480
		public void SetLock(bool value)
		{
			this.m_bLock = value;
			NKCUtil.SetGameobjectActive(this.m_objOpen, !this.m_bLock);
			NKCUtil.SetGameobjectActive(this.m_objLock, this.m_bLock);
		}

		// Token: 0x060093BB RID: 37819 RVA: 0x003272AE File Offset: 0x003254AE
		public void SetAlarm(NKCAlarmManager.ALARM_TYPE type)
		{
			NKCUtil.SetGameobjectActive(this.m_objReddot, NKCAlarmManager.CheckNotify(NKCScenManager.CurrentUserData(), type));
		}

		// Token: 0x060093BC RID: 37820 RVA: 0x003272C6 File Offset: 0x003254C6
		public void SetAlarm(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objReddot, value);
		}

		// Token: 0x040080B4 RID: 32948
		public GameObject m_objOpen;

		// Token: 0x040080B5 RID: 32949
		public GameObject m_objLock;

		// Token: 0x040080B6 RID: 32950
		public GameObject m_objReddot;

		// Token: 0x040080B7 RID: 32951
		private bool m_bLock;
	}
}
