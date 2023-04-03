using System;
using System.Collections.Generic;
using ClientPacket.Game;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000700 RID: 1792
	public class NKC_SCEN_CUTSCEN_DUNGEON : NKC_SCEN_BASIC
	{
		// Token: 0x06004656 RID: 18006 RVA: 0x00155E81 File Offset: 0x00154081
		public NKC_SCEN_CUTSCEN_DUNGEON()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON;
			this.m_NUF_CUTSCEN_DUNGEON = GameObject.Find("NUF_CUTSCEN_DUNGEON");
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x00155EAC File Offset: 0x001540AC
		public void OnRecv(NKMPacket_GAME_LOAD_ACK cNKMPacket_GAME_LOAD_ACK, int multiply = 1)
		{
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(cNKMPacket_GAME_LOAD_ACK.costItemDataList);
			if (cNKMPacket_GAME_LOAD_ACK.gameData != null && cNKMPacket_GAME_LOAD_ACK.gameData.m_WarfareID == 0 && cNKMPacket_GAME_LOAD_ACK.gameData.m_DungeonID > 0)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMPacket_GAME_LOAD_ACK.gameData.m_DungeonID);
				if (dungeonTempletBase != null)
				{
					string key = string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, dungeonTempletBase.m_DungeonStrID);
					if (!PlayerPrefs.HasKey(key) && !NKCScenManager.CurrentUserData().CheckDungeonClear(dungeonTempletBase.m_DungeonStrID))
					{
						PlayerPrefs.SetInt(key, 0);
					}
				}
			}
			NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(cNKMPacket_GAME_LOAD_ACK.gameData, false);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
			Debug.Log("Cutscen_dungeon - NKMPacket_GAME_LOAD_ACK - update - start");
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x00155F77 File Offset: 0x00154177
		public void SetReservedDungeonType(int dungeonID)
		{
			this.m_CUTSCEN_DUNGEON_START_TYPE = NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_DUNGEON;
			this.m_DungeonID = dungeonID;
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x00155F88 File Offset: 0x00154188
		public void SetReservedOneCutscenType(string cutscenStrID, NKCUICutScenPlayer.CutScenCallBack callBack, string dungeonStrID)
		{
			this.m_CUTSCEN_DUNGEON_START_TYPE = NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_ONE_CUTSCEN;
			this.m_cutscenStrID = cutscenStrID;
			this.m_CutscenCallBack = callBack;
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonStrID);
			this.m_stageID = ((nkmstageTempletV != null) ? nkmstageTempletV.Key : 0);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x00155FC3 File Offset: 0x001541C3
		public void SetReservedOneCutscenType(string cutscenStrID, NKCUICutScenPlayer.CutScenCallBack callBack, int stageID = 0)
		{
			this.m_CUTSCEN_DUNGEON_START_TYPE = NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_ONE_CUTSCEN;
			this.m_cutscenStrID = cutscenStrID;
			this.m_CutscenCallBack = callBack;
			this.m_stageID = stageID;
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x00155FE4 File Offset: 0x001541E4
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (NKCUICutScenPlayer.HasInstance)
			{
				NKCUICutScenPlayer.Instance.UnLoad();
			}
			if (this.m_CUTSCEN_DUNGEON_START_TYPE == NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_DUNGEON)
			{
				if (this.m_DungeonID > 0)
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_DungeonID);
					if (dungeonTempletBase != null)
					{
						NKCUICutScenPlayer.Instance.Load(dungeonTempletBase.m_CutScenStrIDBefore, true);
						NKCUICutScenPlayer.Instance.Load(dungeonTempletBase.m_CutScenStrIDAfter, true);
					}
				}
			}
			else if (this.m_CUTSCEN_DUNGEON_START_TYPE == NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_ONE_CUTSCEN && !string.IsNullOrEmpty(this.m_cutscenStrID))
			{
				NKCUICutScenPlayer.Instance.Load(this.m_cutscenStrID, true);
			}
			this.m_NUF_CUTSCEN_DUNGEON.SetActive(true);
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x00156080 File Offset: 0x00154280
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKCUICutscenDungeonUIData = NKCUICutscenDungeon.OpenNewInstance();
				if (this.m_NKCUICutscenDungeon == null && (this.m_NKCUICutscenDungeonUIData == null || !this.m_NKCUICutscenDungeonUIData.CheckLoadAndGetInstance<NKCUICutscenDungeon>(out this.m_NKCUICutscenDungeon)))
				{
					Debug.LogError("Error - NKC_SCEN_CUTSCEN_DUNGEON.ScenLoadComplete() : UI Load Failed!");
					return;
				}
			}
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x001560DA File Offset: 0x001542DA
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x001560E4 File Offset: 0x001542E4
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUICutscenDungeon.Open();
			if (this.m_CUTSCEN_DUNGEON_START_TYPE == NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_DUNGEON)
			{
				if (this.m_DungeonID > 0)
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_DungeonID);
					if (dungeonTempletBase != null)
					{
						Queue<string> queue = new Queue<string>();
						if (dungeonTempletBase.m_CutScenStrIDBefore != "")
						{
							queue.Enqueue(dungeonTempletBase.m_CutScenStrIDBefore);
						}
						if (dungeonTempletBase.m_CutScenStrIDAfter != "")
						{
							queue.Enqueue(dungeonTempletBase.m_CutScenStrIDAfter);
						}
						NKCPacketSender.Send_NKMPacket_CUTSCENE_DUNGEON_START_REQ(this.m_DungeonID);
						NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonTempletBase.m_DungeonStrID);
						int stageID = (nkmstageTempletV != null) ? nkmstageTempletV.Key : 0;
						NKCUICutScenPlayer.Instance.Play(queue, stageID, new NKCUICutScenPlayer.CutScenCallBack(this.DoAfterCutscenForDungeonScenStartType));
						return;
					}
				}
			}
			else if (this.m_CUTSCEN_DUNGEON_START_TYPE == NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE.CDST_ONE_CUTSCEN)
			{
				NKCUICutScenPlayer.Instance.Play(this.m_cutscenStrID, this.m_stageID, this.m_CutscenCallBack);
			}
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x001561CF File Offset: 0x001543CF
		private void DoAfterCutscenForDungeonScenStartType()
		{
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedCutscenDungeonClearDGID(this.m_DungeonID);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x001561F4 File Offset: 0x001543F4
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUICutscenDungeon != null)
			{
				this.m_NKCUICutscenDungeon.Close();
			}
			if (NKCUICutScenPlayer.HasInstance)
			{
				NKCUICutScenPlayer.Instance.StopWithInvalidatingCallBack();
				NKCUICutScenPlayer.Instance.UnLoad();
			}
			if (this.m_NUF_CUTSCEN_DUNGEON != null)
			{
				this.m_NUF_CUTSCEN_DUNGEON.SetActive(false);
			}
			this.UnloadUI();
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x0015625B File Offset: 0x0015445B
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUICutscenDungeon = null;
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x0015626A File Offset: 0x0015446A
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x00156272 File Offset: 0x00154472
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x04003773 RID: 14195
		private GameObject m_NUF_CUTSCEN_DUNGEON;

		// Token: 0x04003774 RID: 14196
		private int m_DungeonID;

		// Token: 0x04003775 RID: 14197
		private string m_cutscenStrID = "";

		// Token: 0x04003776 RID: 14198
		private int m_stageID;

		// Token: 0x04003777 RID: 14199
		private NKCUICutScenPlayer.CutScenCallBack m_CutscenCallBack;

		// Token: 0x04003778 RID: 14200
		private NKCUIManager.LoadedUIData m_NKCUICutscenDungeonUIData;

		// Token: 0x04003779 RID: 14201
		private NKCUICutscenDungeon m_NKCUICutscenDungeon;

		// Token: 0x0400377A RID: 14202
		private NKC_SCEN_CUTSCEN_DUNGEON.CUTSCEN_DUNGEON_START_TYPE m_CUTSCEN_DUNGEON_START_TYPE;

		// Token: 0x020013E3 RID: 5091
		public enum CUTSCEN_DUNGEON_START_TYPE
		{
			// Token: 0x04009C7B RID: 40059
			CDST_DUNGEON,
			// Token: 0x04009C7C RID: 40060
			CDST_ONE_CUTSCEN
		}
	}
}
