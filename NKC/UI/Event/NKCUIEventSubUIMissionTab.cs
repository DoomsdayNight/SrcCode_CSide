using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Event
{
	// Token: 0x02000BE2 RID: 3042
	public class NKCUIEventSubUIMissionTab : MonoBehaviour
	{
		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06008D27 RID: 36135 RVA: 0x003003F1 File Offset: 0x002FE5F1
		public bool Locked
		{
			get
			{
				return this.m_bLocked;
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06008D28 RID: 36136 RVA: 0x003003F9 File Offset: 0x002FE5F9
		public bool Completed
		{
			get
			{
				return this.m_bCompleted;
			}
		}

		// Token: 0x06008D29 RID: 36137 RVA: 0x00300404 File Offset: 0x002FE604
		internal void SetData(NKMMissionTabTemplet tabTemplet, NKCUIComToggleGroup toggleGroup, Action<int, bool> onSelectTab)
		{
			this.m_tglButton.OnValueChanged.RemoveAllListeners();
			this.m_tglButton.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelected));
			this.m_tglButton.SetToggleGroup(toggleGroup);
			this.dOnSelectTab = onSelectTab;
			this.m_tabID = tabTemplet.m_tabID;
			this.m_tglButton.SetTitleText(tabTemplet.GetDesc());
			this.CheckTabState(tabTemplet, out this.m_bLocked, out this.m_bCompleted);
			this.UpdateReddot();
		}

		// Token: 0x06008D2A RID: 36138 RVA: 0x00300488 File Offset: 0x002FE688
		private void UpdateReddot()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool bValue = !this.m_bLocked && !this.m_bCompleted && nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, this.m_tabID, false);
			NKCUtil.SetGameobjectActive(this.m_objReddot, bValue);
		}

		// Token: 0x06008D2B RID: 36139 RVA: 0x003004D0 File Offset: 0x002FE6D0
		private void CheckTabState(NKMMissionTabTemplet tabTemplet, out bool bLocked, out bool bCompleted)
		{
			bCompleted = false;
			bLocked = false;
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(tabTemplet.m_tabID);
			bCompleted = missionTempletListByType.All((NKMMissionTemplet x) => NKCScenManager.CurrentUserData().m_MissionData.IsMissionCompleted(x));
			if (!NKMMissionManager.CheckMissionTabUnlocked(tabTemplet.m_tabID, NKCScenManager.CurrentUserData()))
			{
				bLocked = true;
			}
			NKCUtil.SetGameobjectActive(this.m_objOn, !bLocked);
			NKCUtil.SetGameobjectActive(this.m_objOff, !bLocked);
			NKCUtil.SetGameobjectActive(this.m_objOnLock, bLocked);
			NKCUtil.SetGameobjectActive(this.m_objOffLock, bLocked);
			if (bLocked && !tabTemplet.m_VisibleWhenLocked)
			{
				this.m_tglButton.Lock(false);
			}
			else
			{
				this.m_tglButton.UnLock(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objOffComplete, bCompleted);
		}

		// Token: 0x06008D2C RID: 36140 RVA: 0x003005A0 File Offset: 0x002FE7A0
		private void OnSelected(bool bChecked)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_tabID);
			if (this.m_bLocked)
			{
				string missionTabUnlockCondition = NKMMissionManager.GetMissionTabUnlockCondition(this.m_tabID, NKCScenManager.CurrentUserData());
				if (!string.IsNullOrEmpty(missionTabUnlockCondition))
				{
					NKCPopupMessageManager.AddPopupMessage(missionTabUnlockCondition, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
			}
			if (!this.m_bLocked || missionTabTemplet.m_VisibleWhenLocked)
			{
				Action<int, bool> action = this.dOnSelectTab;
				if (action == null)
				{
					return;
				}
				action(this.m_tabID, bChecked);
			}
		}

		// Token: 0x040079F9 RID: 31225
		public NKCUIComToggle m_tglButton;

		// Token: 0x040079FA RID: 31226
		public GameObject m_objOn;

		// Token: 0x040079FB RID: 31227
		public GameObject m_objOnLock;

		// Token: 0x040079FC RID: 31228
		public GameObject m_objOff;

		// Token: 0x040079FD RID: 31229
		public GameObject m_objOffLock;

		// Token: 0x040079FE RID: 31230
		public GameObject m_objReddot;

		// Token: 0x040079FF RID: 31231
		public GameObject m_objOffComplete;

		// Token: 0x04007A00 RID: 31232
		private Action<int, bool> dOnSelectTab;

		// Token: 0x04007A01 RID: 31233
		private int m_tabID;

		// Token: 0x04007A02 RID: 31234
		private bool m_bLocked;

		// Token: 0x04007A03 RID: 31235
		private bool m_bCompleted;
	}
}
