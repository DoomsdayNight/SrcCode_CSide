using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Guild;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200071D RID: 1821
	public class NKC_SCEN_GUILD_LOBBY : NKC_SCEN_BASIC
	{
		// Token: 0x06004820 RID: 18464 RVA: 0x0015C936 File Offset: 0x0015AB36
		public NKC_SCEN_GUILD_LOBBY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GUILD_LOBBY;
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0015C94D File Offset: 0x0015AB4D
		public void SetReserveLobbyTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE tab)
		{
			this.m_eReservedTab = tab;
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x0015C956 File Offset: 0x0015AB56
		public void ClearCacheData()
		{
			if (this.m_NKCUIGuildLobby != null)
			{
				this.m_NKCUIGuildLobby.CloseInstance();
				this.m_NKCUIGuildLobby = null;
			}
			this.m_bReservedMoveToCoop = false;
			this.m_bReservedMoveToShop = false;
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x0015C988 File Offset: 0x0015AB88
		public override void ScenDataReq()
		{
			if (NKCGuildManager.MyData.guildUid > 0L)
			{
				this.m_bChatDataRecved = false;
				this.m_bCoopDataRecved = true;
				this.m_deltaTime = 0f;
				NKCPacketSender.Send_NKMPacket_GUILD_CHAT_LIST_REQ(NKCGuildManager.MyData.guildUid);
				bool flag;
				if (NKCContentManager.CheckContentStatus(ContentsType.GUILD_DUNGEON, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && (!NKCGuildCoopManager.m_bGuildCoopDataRecved || (NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC) && NKCGuildCoopManager.HasNextSessionData(NKCGuildCoopManager.m_NextSessionStartDateUTC))))
				{
					this.m_bCoopDataRecved = false;
					NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_INFO_REQ(NKCGuildManager.MyData.guildUid);
				}
				base.ScenDataReq();
				return;
			}
			base.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_FAIL);
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x0015CA1C File Offset: 0x0015AC1C
		public override void ScenDataReqWaitUpdate()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 5f)
			{
				this.m_deltaTime = 0f;
				base.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_FAIL);
				return;
			}
			if (this.m_bChatDataRecved && this.m_bCoopDataRecved)
			{
				this.m_deltaTime = 0f;
				base.ScenDataReqWaitUpdate();
			}
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x0015CA7D File Offset: 0x0015AC7D
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.m_NKCUIGuildLobby == null)
			{
				this.m_UILoadResourceData = NKCUIGuildLobby.OpenInstanceAsync();
			}
			else
			{
				this.m_UILoadResourceData = null;
			}
			this.m_bReservedMoveToCoop = false;
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x0015CAB0 File Offset: 0x0015ACB0
		public override void ScenLoadUIComplete()
		{
			if (this.m_NKCUIGuildLobby == null && this.m_UILoadResourceData != null)
			{
				if (!NKCUIGuildLobby.CheckInstanceLoaded(this.m_UILoadResourceData, out this.m_NKCUIGuildLobby))
				{
					Debug.LogError("Error - NKC_SCEN_GUILD_LOBBY.ScenLoadUIComplete() : UI Load Failed!");
					return;
				}
				this.m_UILoadResourceData = null;
				this.m_NKCUIGuildLobby.InitUI();
				NKCUtil.SetGameobjectActive(this.m_NKCUIGuildLobby, false);
			}
			base.ScenLoadUIComplete();
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0015CB17 File Offset: 0x0015AD17
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_NKCUIGuildLobby != null)
			{
				this.m_NKCUIGuildLobby.InitUI();
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0015CB38 File Offset: 0x0015AD38
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			if (this.m_NKCUIGuildLobby != null)
			{
				this.m_NKCUIGuildLobby.Open(this.m_eReservedTab);
			}
			if (this.m_bReservedMoveToShop)
			{
				this.m_NKCUIGuildLobby.OpenShop();
			}
			this.m_eReservedTab = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info;
			this.m_bReservedMoveToShop = false;
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x0015CB91 File Offset: 0x0015AD91
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIGuildLobby != null)
			{
				this.m_NKCUIGuildLobby.Close();
			}
			this.ClearCacheData();
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x0015CBB8 File Offset: 0x0015ADB8
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x0015CBC0 File Offset: 0x0015ADC0
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0015CBC3 File Offset: 0x0015ADC3
		public void SetChatDataRecved(bool bValue)
		{
			this.m_bChatDataRecved = bValue;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x0015CBCC File Offset: 0x0015ADCC
		public void SetCoopDataRecved(bool bValue)
		{
			this.m_bCoopDataRecved = bValue;
			if (this.m_bReservedMoveToCoop && this.m_bCoopDataRecved && NKCGuildCoopManager.m_GuildDungeonState != GuildDungeonState.Invalid)
			{
				this.SetReserveMoveToCoopScen(false);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_COOP, true);
				return;
			}
			if (this.m_NKCUIGuildLobby != null && this.m_NKCUIGuildLobby.IsOpen)
			{
				this.RefreshUI();
			}
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x0015CC2D File Offset: 0x0015AE2D
		public void SetReserveMoveToCoopScen(bool bValue)
		{
			this.m_bReservedMoveToCoop = bValue;
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x0015CC38 File Offset: 0x0015AE38
		public void SetReserveMoveToShop(bool bValue)
		{
			bool flag;
			this.m_bReservedMoveToShop = (NKCContentManager.CheckContentStatus(ContentsType.GUILD_SHOP, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && bValue);
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x0015CC5B File Offset: 0x0015AE5B
		public void OnRecv(List<FriendListData> list)
		{
			if (NKCPopupGuildInvite.IsInstanceOpen)
			{
				NKCPopupGuildInvite.Instance.OnRecv(list);
			}
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x0015CC6F File Offset: 0x0015AE6F
		public void RefreshUI()
		{
			if (this.m_NKCUIGuildLobby != null && this.m_NKCUIGuildLobby.gameObject.activeSelf)
			{
				this.m_NKCUIGuildLobby.OnGuildDataChanged();
			}
		}

		// Token: 0x04003819 RID: 14361
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x0400381A RID: 14362
		private NKCUIGuildLobby m_NKCUIGuildLobby;

		// Token: 0x0400381B RID: 14363
		private bool m_bChatDataRecved;

		// Token: 0x0400381C RID: 14364
		private bool m_bCoopDataRecved;

		// Token: 0x0400381D RID: 14365
		private bool m_bReservedMoveToCoop;

		// Token: 0x0400381E RID: 14366
		private bool m_bReservedMoveToShop;

		// Token: 0x0400381F RID: 14367
		private NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE m_eReservedTab = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info;

		// Token: 0x04003820 RID: 14368
		private const float FIVE_SECONDS = 5f;

		// Token: 0x04003821 RID: 14369
		private float m_deltaTime;
	}
}
