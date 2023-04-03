using System;
using NKC.UI.Office;
using NKM.Templet;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x02000831 RID: 2097
	public class NKCOfficeFacilityLab : NKCOfficeFacility
	{
		// Token: 0x06005361 RID: 21345 RVA: 0x00196B14 File Offset: 0x00194D14
		public override void Init()
		{
			base.Init();
			if (this.m_fnUnitList != null)
			{
				this.m_fnUnitList.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickUnitList);
			}
			if (this.m_fnRearmament != null)
			{
				NKCUtil.SetGameobjectActive(this.m_fnRearmament, NKCRearmamentUtil.IsCanUseContent());
				this.m_fnRearmament.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.REARM, 0, 0));
				this.m_fnRearmament.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickRearmament);
			}
			if (this.m_fnExtract != null)
			{
				NKCUtil.SetGameobjectActive(this.m_fnExtract, NKCRearmamentUtil.IsCanUseContent());
				this.m_fnExtract.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.EXTRACT, 0, 0));
				this.m_fnExtract.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickExtract);
			}
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00196BE4 File Offset: 0x00194DE4
		private void OnClickUnitList(int id, long uid)
		{
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOfficeFacilityButtons officeFacilityInterfaces = NKCUIOffice.GetInstance().OfficeFacilityInterfaces;
				if (officeFacilityInterfaces == null)
				{
					return;
				}
				officeFacilityInterfaces.OnLabUnitList();
			}
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x00196C01 File Offset: 0x00194E01
		public void OnClickRearmament(int id, long uid)
		{
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOfficeFacilityButtons officeFacilityInterfaces = NKCUIOffice.GetInstance().OfficeFacilityInterfaces;
				if (officeFacilityInterfaces == null)
				{
					return;
				}
				officeFacilityInterfaces.OnLabUnitRearm();
			}
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00196C1E File Offset: 0x00194E1E
		public void OnClickExtract(int id, long uid)
		{
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOfficeFacilityButtons officeFacilityInterfaces = NKCUIOffice.GetInstance().OfficeFacilityInterfaces;
				if (officeFacilityInterfaces == null)
				{
					return;
				}
				officeFacilityInterfaces.OnLabUnitExtract();
			}
		}

		// Token: 0x040042D2 RID: 17106
		[Header("연구소 가구들")]
		public NKCOfficeFacilityFuniture m_fnUnitList;

		// Token: 0x040042D3 RID: 17107
		public NKCOfficeFacilityFuniture m_fnRearmament;

		// Token: 0x040042D4 RID: 17108
		public NKCOfficeFacilityFuniture m_fnExtract;
	}
}
