using System;
using NKC.UI;
using NKM.Templet;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x0200082E RID: 2094
	public class NKCOfficeFacilityCEO : NKCOfficeFacility
	{
		// Token: 0x06005351 RID: 21329 RVA: 0x001967D4 File Offset: 0x001949D4
		public override void Init()
		{
			base.Init();
			if (this.m_fnScout != null)
			{
				this.m_fnScout.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_SCOUT, 0, 0));
				this.m_fnScout.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickScout);
			}
			if (this.m_fnLifetime != null)
			{
				this.m_fnLifetime.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_LIFETIME, 0, 0));
				this.m_fnLifetime.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickLifetime);
			}
			if (this.m_fnJukeBox != null)
			{
				this.m_fnJukeBox.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0));
				this.m_fnJukeBox.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickJukebox);
			}
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0019689C File Offset: 0x00194A9C
		public override void UpdateAlarm()
		{
			if (this.m_fnScout != null)
			{
				this.m_fnScout.SetAlarm(NKCAlarmManager.CheckScoutNotify(NKCScenManager.CurrentUserData()));
			}
			if (this.m_fnJukeBox != null)
			{
				this.m_fnJukeBox.SetAlarm(NKCAlarmManager.CheckJukeBoxNotifiy());
			}
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x001968EA File Offset: 0x00194AEA
		private void OnClickScout(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_SCOUT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_SCOUT, 0);
				return;
			}
			NKCUIScout.Instance.Open();
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0019690B File Offset: 0x00194B0B
		private void OnClickLifetime(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_LIFETIME, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_LIFETIME, 0);
				return;
			}
			NKCUIPersonnel.Instance.Open();
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0019692C File Offset: 0x00194B2C
		private void OnClickJukebox(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_JUKEBOX_CONTENTS_UNLOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIJukeBox.Instance.Open(false, 0, null);
		}

		// Token: 0x040042CA RID: 17098
		[Header("사장실 가구")]
		public NKCOfficeFacilityFuniture m_fnScout;

		// Token: 0x040042CB RID: 17099
		public NKCOfficeFacilityFuniture m_fnLifetime;

		// Token: 0x040042CC RID: 17100
		public NKCOfficeFacilityFuniture m_fnJukeBox;
	}
}
