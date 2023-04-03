using System;
using ClientPacket.Game;
using ClientPacket.LeaderBoard;
using NKC.UI;
using NKC.UI.Fierce;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200070D RID: 1805
	public class NKC_SCEN_FIERCE_BATTLE_SUPPORT : NKC_SCEN_BASIC
	{
		// Token: 0x060046F5 RID: 18165 RVA: 0x00158704 File Offset: 0x00156904
		public NKC_SCEN_FIERCE_BATTLE_SUPPORT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT;
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x00158714 File Offset: 0x00156914
		public override void ScenLoadUIStart()
		{
			if (!NKCUIManager.IsValid(this.m_UIFierceBattleSupportData))
			{
				this.m_UIFierceBattleSupportData = NKCUIManager.OpenNewInstanceAsync<NKCUIFierceBattleSupport>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_FIERCE_BATTLE", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null);
			}
			base.ScenLoadUIStart();
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x00158748 File Offset: 0x00156948
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_NKCUIFierceBattleSupport == null)
			{
				if (this.m_UIFierceBattleSupportData != null && this.m_UIFierceBattleSupportData.CheckLoadAndGetInstance<NKCUIFierceBattleSupport>(out this.m_NKCUIFierceBattleSupport))
				{
					this.m_NKCUIFierceBattleSupport.Init();
					return;
				}
				Debug.LogError("Error - NKC_SCEN_FIERCE_BATTLE_SUPPORT.ScenLoadComplete() : UI Load Failed!");
			}
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x0015879A File Offset: 0x0015699A
		public override void ScenStart()
		{
			base.ScenStart();
			this.Open();
			this.CheckTutorial();
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x001587AE File Offset: 0x001569AE
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.Close();
			NKCUIManager.LoadedUIData uifierceBattleSupportData = this.m_UIFierceBattleSupportData;
			if (uifierceBattleSupportData != null)
			{
				uifierceBattleSupportData.CloseInstance();
			}
			this.m_UIFierceBattleSupportData = null;
			this.m_NKCUIFierceBattleSupport = null;
			NKCCamera.GetTrackingPos().SetPause(false);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x001587E6 File Offset: 0x001569E6
		public void Open()
		{
			if (this.m_NKCUIFierceBattleSupport != null)
			{
				this.m_NKCUIFierceBattleSupport.Open();
			}
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x00158801 File Offset: 0x00156A01
		public void Close()
		{
			if (this.m_NKCUIFierceBattleSupport != null)
			{
				this.m_NKCUIFierceBattleSupport.Close();
			}
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x0015881C File Offset: 0x00156A1C
		public void RefreshLeaderBoard()
		{
			if (this.m_NKCUIFierceBattleSupport != null)
			{
				this.m_NKCUIFierceBattleSupport.RefreshLeaderBoard();
			}
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x00158837 File Offset: 0x00156A37
		public void SetDataReq(bool bReceived)
		{
			this.m_bChangeDataReceived = bReceived;
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x00158840 File Offset: 0x00156A40
		public override void ScenDataReq()
		{
			this.m_bChangeDataReceived = false;
			this.UpdateFirceData();
			this.m_deltaTime = 0f;
			base.ScenDataReq();
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x00158860 File Offset: 0x00156A60
		public override void ScenDataReqWaitUpdate()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 5f)
			{
				this.m_deltaTime = 0f;
				base.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_FAIL);
				return;
			}
			if (!this.m_bChangeDataReceived)
			{
				return;
			}
			Debug.LogFormat("{0}.ScenDataReqWaitUpdate", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.ScenLoadUIStart();
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x001588D3 File Offset: 0x00156AD3
		public void ResetUI()
		{
			if (this.m_NKCUIFierceBattleSupport.IsOpen)
			{
				this.m_NKCUIFierceBattleSupport.ResetUI();
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x001588ED File Offset: 0x00156AED
		private void UpdateFirceData()
		{
			NKCPacketSender.Send_NKMPacket_FIERCE_DATA_REQ();
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x001588F4 File Offset: 0x00156AF4
		public void OnRecv(NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_ACK sPacket)
		{
			this.m_NKCUIFierceBattleSupport.UpdateFierceBattleRank();
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x00158901 File Offset: 0x00156B01
		public void OnRecv(NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ACK sPacket)
		{
			this.m_NKCUIFierceBattleSupport.UpdatePointRewardRedDot();
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x0015890E File Offset: 0x00156B0E
		public void OnRecv(NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_ACK sPacket)
		{
			this.m_NKCUIFierceBattleSupport.UpdatePointRewardRedDot();
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x0015891B File Offset: 0x00156B1B
		public void OnRecv(NKMPacket_FIERCE_DAILY_REWARD_ACK sPacket)
		{
			this.m_NKCUIFierceBattleSupport.UpdateDailyRewardRedDot();
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00158928 File Offset: 0x00156B28
		public void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.FierceLobby, true);
		}

		// Token: 0x040037C0 RID: 14272
		private NKCUIFierceBattleSupport m_NKCUIFierceBattleSupport;

		// Token: 0x040037C1 RID: 14273
		private NKCUIManager.LoadedUIData m_UIFierceBattleSupportData;

		// Token: 0x040037C2 RID: 14274
		private bool m_bChangeDataReceived;

		// Token: 0x040037C3 RID: 14275
		private const float FIVE_SECONDS = 5f;

		// Token: 0x040037C4 RID: 14276
		private float m_deltaTime;
	}
}
