using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using ClientPacket.Raid;
using NKC.UI;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000728 RID: 1832
	public class NKC_SCEN_RAID_READY : NKC_SCEN_BASIC
	{
		// Token: 0x060048F2 RID: 18674 RVA: 0x0015FC0E File Offset: 0x0015DE0E
		public NKC_SCEN_RAID_READY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_RAID_READY;
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x0015FC2B File Offset: 0x0015DE2B
		public void SetRaidUID(long raidUID)
		{
			this.m_RaidUID = raidUID;
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x0015FC34 File Offset: 0x0015DE34
		public void SetGuildRaid(bool bGuildRaid)
		{
			this.m_bIsGuildRaid = bGuildRaid;
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x0015FC3D File Offset: 0x0015DE3D
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x0015FC48 File Offset: 0x0015DE48
		public override void ScenDataReq()
		{
			if (this.m_bIsGuildRaid && NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.Invalid && NKCGuildManager.GetMyGuildSimpleData() != null)
			{
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_INFO_REQ(NKCGuildManager.GetMyGuildSimpleData().guildUid);
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ(NKCGuildManager.GetMyGuildSimpleData().guildUid);
				this.m_PacketWaitingCount = 2;
			}
			base.ScenDataReq();
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x0015FC96 File Offset: 0x0015DE96
		public override void ScenDataReqWaitUpdate()
		{
			base.ScenDataReqWaitUpdate();
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x0015FCA0 File Offset: 0x0015DEA0
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_RAID;
			if (this.m_RaidUID == 0L)
			{
				options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.DeckSetupOnly;
				options.dOnBackButton = delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				};
			}
			else if (this.m_bIsGuildRaid)
			{
				options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss;
				options.dOnBackButton = delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_COOP, true);
				};
			}
			else
			{
				options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PrepareRaid;
				options.dOnBackButton = delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID, true);
				};
			}
			options.dOnSideMenuButtonConfirm = null;
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(this.m_LastDeckIndex) == null)
			{
				this.m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_RAID, 0);
			}
			options.DeckIndex = this.m_LastDeckIndex;
			options.SelectLeaderUnitOnOpen = true;
			options.bEnableDefaultBackground = true;
			options.bUpsideMenuHomeButton = false;
			options.raidUID = this.m_RaidUID;
			NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (nkmraidDetailData != null)
			{
				NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
				if (nkmraidTemplet.StageReqItemID == 1)
				{
					options.upsideMenuShowResourceList = new List<int>
					{
						1,
						101
					};
				}
				else
				{
					options.upsideMenuShowResourceList = new List<int>
					{
						1,
						nkmraidTemplet.StageReqItemID,
						101
					};
				}
			}
			else
			{
				options.upsideMenuShowResourceList = new List<int>
				{
					1,
					3,
					101
				};
			}
			options.StageBattleStrID = string.Empty;
			options.bSlot24Extend = true;
			options.bNoUseLeaderBtn = true;
			NKCUIDeckViewer.Instance.Open(options, true);
			this.CheckTutorial();
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x0015FE94 File Offset: 0x0015E094
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIDeckViewer.CheckInstanceAndClose();
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x0015FEA1 File Offset: 0x0015E0A1
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x0015FEA9 File Offset: 0x0015E0A9
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0015FEAC File Offset: 0x0015E0AC
		public void OnRecv(NKMPacket_GUILD_DUNGEON_INFO_ACK sPacket)
		{
			this.m_PacketWaitingCount--;
			if (this.m_PacketWaitingCount == 0)
			{
				this.ScenDataReqWaitUpdate();
			}
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x0015FECA File Offset: 0x0015E0CA
		public void OnRecv(NKMPacket_GUILD_DUNGEON_MEMBER_INFO_ACK sPacket)
		{
			this.m_PacketWaitingCount--;
			if (this.m_PacketWaitingCount == 0)
			{
				this.ScenDataReqWaitUpdate();
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0015FEE8 File Offset: 0x0015E0E8
		public void SetLastDeckIndex(NKMDeckIndex deckIndex)
		{
			this.m_LastDeckIndex = deckIndex;
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0015FEF1 File Offset: 0x0015E0F1
		public void DoAfterLogout()
		{
			this.m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_RAID, 0);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0015FF00 File Offset: 0x0015E100
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.RaidStart, true);
		}

		// Token: 0x04003872 RID: 14450
		private long m_RaidUID;

		// Token: 0x04003873 RID: 14451
		private NKMDeckIndex m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_RAID, 0);

		// Token: 0x04003874 RID: 14452
		private bool m_bIsGuildRaid;

		// Token: 0x04003875 RID: 14453
		private int m_PacketWaitingCount;
	}
}
