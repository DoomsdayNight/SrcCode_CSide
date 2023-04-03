using System;
using NKC.UI;
using NKC.UI.Guild;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200071B RID: 1819
	public class NKC_SCEN_GUILD_COOP : NKC_SCEN_BASIC
	{
		// Token: 0x06004805 RID: 18437 RVA: 0x0015C58A File Offset: 0x0015A78A
		public NKC_SCEN_GUILD_COOP()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GUILD_COOP;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0015C59A File Offset: 0x0015A79A
		public override void ScenDataReq()
		{
			if (!NKCGuildCoopManager.m_bGuildCoopMemberDataRecved)
			{
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ(NKCGuildManager.MyData.guildUid);
			}
			this.m_deltaTime = 0f;
			base.ScenDataReq();
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0015C5C4 File Offset: 0x0015A7C4
		public override void ScenDataReqWaitUpdate()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 5f)
			{
				this.m_deltaTime = 0f;
				base.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_FAIL);
				return;
			}
			if (NKCGuildCoopManager.m_bGuildCoopMemberDataRecved)
			{
				this.m_deltaTime = 0f;
				base.ScenDataReqWaitUpdate();
			}
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0015C61C File Offset: 0x0015A81C
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_NKCUIGuildCoopUIData))
			{
				this.m_NKCUIGuildCoopUIData = NKCUIGuildCoop.OpenNewInstanceAsync();
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0015C63C File Offset: 0x0015A83C
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!(this.m_NKCUIGuildCoop == null))
			{
				return;
			}
			if (this.m_NKCUIGuildCoopUIData != null && this.m_NKCUIGuildCoopUIData.CheckLoadAndGetInstance<NKCUIGuildCoop>(out this.m_NKCUIGuildCoop))
			{
				this.m_NKCUIGuildCoop.InitUI();
				return;
			}
			Debug.LogError("Error - NKC_SCEN_GUILD_DUNGEON.ScenLoadComplete() : UI Load Failed!");
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0015C68F File Offset: 0x0015A88F
		public override void ScenLoadUpdate()
		{
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0015C69F File Offset: 0x0015A89F
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0015C6A7 File Offset: 0x0015A8A7
		public override void ScenStart()
		{
			base.ScenStart();
			this.OpenGuildDungeon();
			NKCCamera.GetCamera().orthographic = false;
			this.TutorialCheck();
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0015C6C6 File Offset: 0x0015A8C6
		private void OpenGuildDungeon()
		{
			if (this.m_NKCUIGuildCoop != null)
			{
				this.m_NKCUIGuildCoop.Open();
			}
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0015C6E1 File Offset: 0x0015A8E1
		public override void ScenEnd()
		{
			if (this.m_NKCUIGuildCoop != null)
			{
				this.m_NKCUIGuildCoop.Close();
			}
			this.m_NKCUIGuildCoop = null;
			NKCUIManager.LoadedUIData nkcuiguildCoopUIData = this.m_NKCUIGuildCoopUIData;
			if (nkcuiguildCoopUIData != null)
			{
				nkcuiguildCoopUIData.CloseInstance();
			}
			this.m_NKCUIGuildCoopUIData = null;
			base.ScenEnd();
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x0015C721 File Offset: 0x0015A921
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0015C729 File Offset: 0x0015A929
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0015C72C File Offset: 0x0015A92C
		public void TutorialCheck()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_COOP)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.ConsortiumDungeon, true);
			}
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0015C745 File Offset: 0x0015A945
		public void OnCloseInfoPopup()
		{
			if (this.m_NKCUIGuildCoop != null && this.m_NKCUIGuildCoop.IsOpen)
			{
				NKCUIGuildCoop nkcuiguildCoop = this.m_NKCUIGuildCoop;
				if (nkcuiguildCoop == null)
				{
					return;
				}
				nkcuiguildCoop.OnCloseInfoPopup();
			}
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0015C772 File Offset: 0x0015A972
		public void Refresh()
		{
			if (NKCPopupGuildCoopSeasonReward.IsInstanceOpen)
			{
				NKCPopupGuildCoopSeasonReward.Instance.Close();
			}
			if (NKCUIGuildCoopEnd.IsInstanceOpen)
			{
				NKCUIGuildCoopEnd.Instance.Close();
			}
			this.OpenGuildDungeon();
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x0015C79C File Offset: 0x0015A99C
		public void RefreshArenaSlot(int arenaIdx)
		{
			if (NKCPopupGuildCoopArenaInfo.IsInstanceOpen)
			{
				NKCPopupGuildCoopArenaInfo.Instance.Refresh();
			}
			NKCUIGuildCoop nkcuiguildCoop = this.m_NKCUIGuildCoop;
			if (nkcuiguildCoop == null)
			{
				return;
			}
			nkcuiguildCoop.RefreshArenaSlot(arenaIdx);
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0015C7C0 File Offset: 0x0015A9C0
		public void RefreshBossInfo()
		{
			if (NKCPopupGuildCoopBossInfo.IsInstanceOpen)
			{
				NKCPopupGuildCoopBossInfo.Instance.Refresh();
			}
			NKCUIGuildCoop nkcuiguildCoop = this.m_NKCUIGuildCoop;
			if (nkcuiguildCoop == null)
			{
				return;
			}
			nkcuiguildCoop.RefreshBossSlot();
		}

		// Token: 0x04003813 RID: 14355
		private NKCUIGuildCoop m_NKCUIGuildCoop;

		// Token: 0x04003814 RID: 14356
		private NKCUIManager.LoadedUIData m_NKCUIGuildCoopUIData;

		// Token: 0x04003815 RID: 14357
		private const float FIVE_SECONDS = 5f;

		// Token: 0x04003816 RID: 14358
		private float m_deltaTime;
	}
}
