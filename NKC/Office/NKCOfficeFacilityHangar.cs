using System;
using NKC.UI;
using NKC.UI.Office;
using NKM.Templet;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x02000830 RID: 2096
	public class NKCOfficeFacilityHangar : NKCOfficeFacility
	{
		// Token: 0x0600535C RID: 21340 RVA: 0x00196A24 File Offset: 0x00194C24
		public override void Init()
		{
			base.Init();
			if (this.m_fnBuild != null)
			{
				this.m_fnBuild.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickBuild);
				this.m_fnBuild.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0));
			}
			if (this.m_fnShipList != null)
			{
				this.m_fnShipList.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickShipList);
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x00196A98 File Offset: 0x00194C98
		public override void UpdateAlarm()
		{
			if (this.m_fnBuild != null)
			{
				this.m_fnBuild.SetAlarm(NKCAlarmManager.ALARM_TYPE.HANGAR);
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x00196AB4 File Offset: 0x00194CB4
		private void OnClickBuild(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPBUILD, 0);
				return;
			}
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOfficeFacilityButtons officeFacilityInterfaces = NKCUIOffice.GetInstance().OfficeFacilityInterfaces;
				if (officeFacilityInterfaces != null)
				{
					officeFacilityInterfaces.OnHangarBuild();
				}
			}
			this.UpdateAlarm();
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x00196AED File Offset: 0x00194CED
		private void OnClickShipList(int id, long uid)
		{
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOfficeFacilityButtons officeFacilityInterfaces = NKCUIOffice.GetInstance().OfficeFacilityInterfaces;
				if (officeFacilityInterfaces == null)
				{
					return;
				}
				officeFacilityInterfaces.OnHangerShipList();
			}
		}

		// Token: 0x040042D0 RID: 17104
		[Header("격납고 정보")]
		public NKCOfficeFacilityFuniture m_fnBuild;

		// Token: 0x040042D1 RID: 17105
		public NKCOfficeFacilityFuniture m_fnShipList;
	}
}
