using System;
using System.Text;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C0E RID: 3086
	public class NKCUILobbyMenuContract : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EDE RID: 36574 RVA: 0x00309974 File Offset: 0x00307B74
		public void Init(ContentsType contentsType)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
				this.m_ContentsType = contentsType;
				NKCUtil.SetGameobjectActive(this.m_objAlert, false);
			}
		}

		// Token: 0x06008EDF RID: 36575 RVA: 0x003099CE File Offset: 0x00307BCE
		private void Update()
		{
			if (this.m_bLocked)
			{
				return;
			}
			this.m_fUpdateTimer -= Time.deltaTime;
			if (this.m_fUpdateTimer <= 0f)
			{
				this.m_fUpdateTimer = 1f;
				base.UpdateData(null);
			}
		}

		// Token: 0x06008EE0 RID: 36576 RVA: 0x00309A0C File Offset: 0x00307C0C
		protected override void ContentsUpdate(NKMUserData userData)
		{
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr == null)
			{
				return;
			}
			if (!nkccontractDataMgr.PossibleFreeContract && !this.m_bNewFreeChanceContract)
			{
				NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_ON, false);
				NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_OFF, true);
				NKCUtil.SetLabelText(this.m_NKM_UI_LOBBY_RIGHT_MENU_1_CONTRACT_TEXT3_OFF, "--:--:--");
				return;
			}
			if (this.m_bNewFreeChanceContract || nkccontractDataMgr.IsPossibleFreeChance())
			{
				NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_ON, true);
				NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_OFF, false);
				string timeLeftString = NKCSynchronizedTime.GetTimeLeftString(NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetNextResetTime().Ticks);
				NKCUtil.SetLabelText(this.m_NKM_UI_LOBBY_RIGHT_MENU_1_CONTRACT_TEXT3_ON, timeLeftString);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_ON, false);
			NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_OFF, true);
			string timeLeftString2 = NKCSynchronizedTime.GetTimeLeftString(NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetNextResetTime().Ticks);
			NKCUtil.SetLabelText(this.m_NKM_UI_LOBBY_RIGHT_MENU_1_CONTRACT_TEXT3_OFF, timeLeftString2);
		}

		// Token: 0x06008EE1 RID: 36577 RVA: 0x00309AF8 File Offset: 0x00307CF8
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_ON, false);
				NKCUtil.SetGameobjectActive(this.m_CONTRACT_FREE_OFF, true);
				NKCUtil.SetLabelText(this.m_NKM_UI_LOBBY_RIGHT_MENU_1_CONTRACT_TEXT3_OFF, "--:--:--");
				NKCContentManager.ShowLockedMessagePopup(ContentsType.CONTRACT, 0);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CONTRACT, false);
		}

		// Token: 0x06008EE2 RID: 36578 RVA: 0x00309B4C File Offset: 0x00307D4C
		private void OnEnable()
		{
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr != null)
			{
				this.m_bNewFreeChanceContract = nkccontractDataMgr.IsActiveNewFreeChance();
			}
		}

		// Token: 0x04007BE2 RID: 31714
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007BE3 RID: 31715
		public GameObject m_objAlert;

		// Token: 0x04007BE4 RID: 31716
		public Text m_lbCompletedContracts;

		// Token: 0x04007BE5 RID: 31717
		public Text m_lbOnProgressContracts;

		// Token: 0x04007BE6 RID: 31718
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04007BE7 RID: 31719
		public GameObject m_CONTRACT_FREE_ON;

		// Token: 0x04007BE8 RID: 31720
		public GameObject m_CONTRACT_FREE_OFF;

		// Token: 0x04007BE9 RID: 31721
		public Text m_NKM_UI_LOBBY_RIGHT_MENU_1_CONTRACT_TEXT3_ON;

		// Token: 0x04007BEA RID: 31722
		public Text m_NKM_UI_LOBBY_RIGHT_MENU_1_CONTRACT_TEXT3_OFF;

		// Token: 0x04007BEB RID: 31723
		private float m_fUpdateTimer = 1f;

		// Token: 0x04007BEC RID: 31724
		private bool m_bNewFreeChanceContract;
	}
}
