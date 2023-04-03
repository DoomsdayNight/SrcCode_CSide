using System;
using ClientPacket.Mode;
using NKC.UI;
using NKC.UI.Result;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x02000726 RID: 1830
	public class NKC_SCEN_OPERATION_V2 : NKC_SCEN_BASIC
	{
		// Token: 0x060048BC RID: 18620 RVA: 0x0015F234 File Offset: 0x0015D434
		public void SetLastPlayedMainStream(int id)
		{
			this.m_LastPlayedMainStreamID = id;
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0015F23D File Offset: 0x0015D43D
		public int GetLastPlayedMainStream()
		{
			return this.m_LastPlayedMainStreamID;
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0015F245 File Offset: 0x0015D445
		public void SetLastPlayedSubStream(int id)
		{
			this.m_LastPlayedSubStreamID = id;
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0015F24E File Offset: 0x0015D44E
		public int GetLastPlayedSubStream()
		{
			return this.m_LastPlayedSubStreamID;
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x0015F256 File Offset: 0x0015D456
		// (set) Token: 0x060048C1 RID: 18625 RVA: 0x0015F25E File Offset: 0x0015D45E
		public bool PlayByFavorite
		{
			get
			{
				return this.m_bPlayByFavorite;
			}
			set
			{
				this.m_bPlayByFavorite = value;
			}
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0015F267 File Offset: 0x0015D467
		public NKC_SCEN_OPERATION_V2()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_OPERATION;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0015F27F File Offset: 0x0015D47F
		public void DoAfterLogOut()
		{
			this.SetReservedCutscenDungeonClearDGID(0);
			NKMEpisodeMgr.ClearFavoriteStage();
			this.m_ReservedEpisodeCategory = EPISODE_CATEGORY.EC_COUNT;
			this.m_ReservedStageTemplet = null;
			this.m_ReservedEpisodeTemplet = null;
			this.m_ReservedCutscenDungeonClearDGID = 0;
			this.m_LastPlayedMainStreamID = 0;
			this.m_LastPlayedSubStreamID = 0;
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0015F2B8 File Offset: 0x0015D4B8
		public EPISODE_CATEGORY GetReservedEpisodeCategory()
		{
			return this.m_ReservedEpisodeCategory;
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x0015F2C0 File Offset: 0x0015D4C0
		public void SetReservedEpisodeCategory(int groupID)
		{
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(groupID);
			this.SetReservedEpisodeCategory(nkmepisodeGroupTemplet.EpCategory);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0015F2E0 File Offset: 0x0015D4E0
		public void SetReservedEpisodeCategory(EPISODE_CATEGORY epCate)
		{
			this.m_ReservedEpisodeCategory = epCate;
			this.m_ReservedEpisodeTemplet = null;
			this.m_ReservedStageTemplet = null;
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x0015F2F7 File Offset: 0x0015D4F7
		public NKMEpisodeTempletV2 GetReservedEpisodeTemplet()
		{
			return this.m_ReservedEpisodeTemplet;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x0015F2FF File Offset: 0x0015D4FF
		public void SetReservedEpisodeTemplet(NKMEpisodeTempletV2 epTemplet)
		{
			this.m_ReservedEpisodeTemplet = epTemplet;
			if (epTemplet != null)
			{
				this.m_ReservedEpisodeCategory = epTemplet.m_EPCategory;
				this.m_ReservedStageTemplet = null;
				return;
			}
			this.m_ReservedStageTemplet = null;
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x0015F326 File Offset: 0x0015D526
		public NKMStageTempletV2 GetReservedStageTemplet()
		{
			return this.m_ReservedStageTemplet;
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0015F32E File Offset: 0x0015D52E
		public void SetReservedStage(NKMStageTempletV2 stageTemplet)
		{
			this.m_ReservedStageTemplet = stageTemplet;
			if (stageTemplet != null)
			{
				this.m_ReservedEpisodeCategory = stageTemplet.EpisodeCategory;
				this.m_ReservedEpisodeTemplet = stageTemplet.EpisodeTemplet;
			}
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0015F354 File Offset: 0x0015D554
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIDataCC))
			{
				this.m_loadUIDataCC = NKCUIEpisodeViewerCC.OpenNewInstanceAsync();
			}
			if (!NKCUIManager.IsValid(this.m_loadUIDataCCNormal))
			{
				this.m_loadUIDataCCNormal = NKCUICounterCaseNormal.OpenNewInstanceAsync();
			}
			if (!NKCUIManager.IsValid(this.m_loadUIDataOperation))
			{
				this.m_loadUIDataOperation = NKCUIOperationV2.OpenNewInstanceAsync();
			}
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x0015F3B0 File Offset: 0x0015D5B0
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_NKCUIOperationV2 == null)
			{
				if (this.m_loadUIDataOperation != null && this.m_loadUIDataOperation.CheckLoadAndGetInstance<NKCUIOperationV2>(out this.m_NKCUIOperationV2))
				{
					this.m_NKCUIOperationV2.InitUI();
					NKCUtil.SetGameobjectActive(this.m_NKCUIOperationV2.gameObject, false);
				}
				else
				{
					Debug.LogError("NKC_SCEN_OPERATION.ScenLoadUIComplete - ui load m_NKCUIOperationV2 failed");
				}
			}
			if (this.m_NKCUIEpisodeViewerCC == null)
			{
				if (this.m_loadUIDataCC != null && this.m_loadUIDataCC.CheckLoadAndGetInstance<NKCUIEpisodeViewerCC>(out this.m_NKCUIEpisodeViewerCC))
				{
					this.m_NKCUIEpisodeViewerCC.InitUI();
					NKCUtil.SetGameobjectActive(this.m_NKCUIEpisodeViewerCC.gameObject, false);
				}
				else
				{
					Debug.LogError("NKC_SCEN_OPERATION.ScenLoadUIComplete - ui load m_NKCUIEpisodeViewerCC failed");
				}
			}
			if (this.m_NKCUICounterCaseNormal == null)
			{
				if (this.m_loadUIDataCCNormal != null && this.m_loadUIDataCCNormal.CheckLoadAndGetInstance<NKCUICounterCaseNormal>(out this.m_NKCUICounterCaseNormal))
				{
					this.m_NKCUICounterCaseNormal.InitUI();
					NKCUtil.SetGameobjectActive(this.m_NKCUICounterCaseNormal.gameObject, false);
				}
				else
				{
					Debug.LogError("NKC_SCEN_OPERATION.ScenLoadUIComplete - ui load m_NKCUICounterCaseNormal failed");
				}
			}
			if (this.m_NKCUIOperationV2 != null)
			{
				this.m_NKCUIOperationV2.PreLoad();
			}
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x0015F4D0 File Offset: 0x0015D6D0
		public override void ScenDataReq()
		{
			base.ScenDataReq();
			this.m_deltaTime = 0f;
			if (this.m_ReservedCutscenDungeonClearDGID > 0)
			{
				NKMPacket_CUTSCENE_DUNGEON_CLEAR_REQ nkmpacket_CUTSCENE_DUNGEON_CLEAR_REQ = new NKMPacket_CUTSCENE_DUNGEON_CLEAR_REQ();
				nkmpacket_CUTSCENE_DUNGEON_CLEAR_REQ.dungeonID = this.m_ReservedCutscenDungeonClearDGID;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CUTSCENE_DUNGEON_CLEAR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0015F51C File Offset: 0x0015D71C
		public override void ScenDataReqWaitUpdate()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 5f)
			{
				this.m_deltaTime = 0f;
				base.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_FAIL);
				return;
			}
			if (this.m_ReservedCutscenDungeonClearDGID > 0)
			{
				return;
			}
			base.ScenDataReqWaitUpdate();
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0015F56C File Offset: 0x0015D76C
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUIOperationV2.Open();
			if (this.m_RewardData != null)
			{
				NKCUIResult.Instance.OpenComplexResult(NKCScenManager.CurrentUserData().m_ArmyData, this.m_RewardData, new NKCUIResult.OnClose(NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OnCloseResultPopup), 0L, null, false, false);
				this.m_RewardData = null;
			}
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0015F5CD File Offset: 0x0015D7CD
		public override void PlayScenMusic()
		{
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0015F5CF File Offset: 0x0015D7CF
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_NKCUICounterCaseNormal.Close();
			this.m_NKCUIEpisodeViewerCC.Close();
			this.m_NKCUIOperationV2.Close();
			this.UnloadUI();
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x0015F600 File Offset: 0x0015D800
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUICounterCaseNormal = null;
			NKCUIManager.LoadedUIData loadUIDataCCNormal = this.m_loadUIDataCCNormal;
			if (loadUIDataCCNormal != null)
			{
				loadUIDataCCNormal.CloseInstance();
			}
			this.m_loadUIDataCCNormal = null;
			this.m_NKCUIEpisodeViewerCC = null;
			NKCUIManager.LoadedUIData loadUIDataCC = this.m_loadUIDataCC;
			if (loadUIDataCC != null)
			{
				loadUIDataCC.CloseInstance();
			}
			this.m_loadUIDataCC = null;
			this.m_NKCUIOperationV2 = null;
			NKCUIManager.LoadedUIData loadUIDataOperation = this.m_loadUIDataOperation;
			if (loadUIDataOperation != null)
			{
				loadUIDataOperation.CloseInstance();
			}
			this.m_loadUIDataOperation = null;
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x0015F670 File Offset: 0x0015D870
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x0015F6C1 File Offset: 0x0015D8C1
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
		public void ProcessRelogin()
		{
			if (NKCGameEventManager.IsEventPlaying())
			{
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKCScenManager.GetScenManager().GetNowScenID(), true);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x0015F6E3 File Offset: 0x0015D8E3
		public void SetCounterCaseNormalActID(int id)
		{
			if (this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCC.SetActID(id);
			}
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x0015F6FE File Offset: 0x0015D8FE
		public void OnRecv(NKMPacket_COUNTERCASE_UNLOCK_ACK cNKMPacket_COUNTERCASE_UNLOCK_ACK)
		{
			if (this.m_NKCUICounterCaseNormal != null && this.m_NKCUICounterCaseNormal.IsOpen)
			{
				this.m_NKCUICounterCaseNormal.UpdateLeftslot();
				this.m_NKCUICounterCaseNormal.UpdateRightSlots(false, cNKMPacket_COUNTERCASE_UNLOCK_ACK.dungeonID);
			}
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x0015F738 File Offset: 0x0015D938
		public void OnRecv(NKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK)
		{
			this.m_ReservedCutscenDungeonClearDGID = 0;
			if (this.m_NKCUICounterCaseNormal != null && this.m_NKCUICounterCaseNormal.IsOpen)
			{
				this.m_NKCUICounterCaseNormal.UpdateLeftslot();
				this.m_NKCUICounterCaseNormal.UpdateRightSlots(false, -1);
			}
			if (this.m_NKCUIEpisodeViewerCC != null && this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCC.UpdateUI();
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData.dungeonId);
			if (dungeonTempletBase != null && dungeonTempletBase.StageTemplet != null)
			{
				this.SetReservedEpisodeTemplet(dungeonTempletBase.StageTemplet.EpisodeTemplet);
			}
			this.m_RewardData = cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData.rewardData;
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0015F7E4 File Offset: 0x0015D9E4
		public void SetReservedCutscenDungeonClearDGID(int dungeonID)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase != null)
			{
				this.m_ReservedStageTemplet = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonTempletBase.m_DungeonStrID);
				if (this.m_ReservedStageTemplet != null)
				{
					NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeTemplet(this.m_ReservedStageTemplet.EpisodeTemplet);
				}
				this.m_ReservedCutscenDungeonClearDGID = dungeonID;
				return;
			}
			this.m_ReservedCutscenDungeonClearDGID = 0;
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x0015F83D File Offset: 0x0015DA3D
		public void OpenCounterCaseViewer()
		{
			if (NKCUIOperationNodeViewer.isOpen())
			{
				NKCUIOperationNodeViewer.Instance.Close();
			}
			if (this.m_NKCUIEpisodeViewerCC != null)
			{
				this.m_NKCUIEpisodeViewerCC.Open();
			}
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0015F869 File Offset: 0x0015DA69
		public void OpenCounterCaseNormalAct(int actID)
		{
			this.m_NKCUICounterCaseNormal.SetActID(actID);
			this.m_NKCUICounterCaseNormal.Open();
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0015F884 File Offset: 0x0015DA84
		public void ReopenEpisodeView()
		{
			if (this.m_NKCUICounterCaseNormal != null && this.m_NKCUICounterCaseNormal.IsOpen)
			{
				this.m_NKCUICounterCaseNormal.Close();
			}
			if (this.m_NKCUIEpisodeViewerCC != null && this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCC.Close();
			}
			if (NKCUIOperationNodeViewer.isOpen())
			{
				NKCUIOperationNodeViewer.Instance.Close();
			}
			this.m_NKCUIOperationV2.Close();
			this.m_NKCUIOperationV2.Open();
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0015F904 File Offset: 0x0015DB04
		public void OnRecv(NKMPacket_STAGE_UNLOCK_ACK sPacket)
		{
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x0015F906 File Offset: 0x0015DB06
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			if (this.m_NKCUIOperationV2 != null)
			{
				this.m_NKCUIOperationV2.SetTutorialMainstreamGuide(eventTemplet, Complete);
				return;
			}
			if (Complete != null)
			{
				Complete();
			}
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x0015F92D File Offset: 0x0015DB2D
		public RectTransform GetDailyRect()
		{
			if (this.m_NKCUIOperationV2 != null)
			{
				return this.m_NKCUIOperationV2.GetDailyRect();
			}
			return null;
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x0015F94A File Offset: 0x0015DB4A
		public RectTransform GetStageSlot(int stageIndex)
		{
			if (this.m_NKCUIOperationV2 == null || !this.m_NKCUIOperationV2.IsOpen)
			{
				return null;
			}
			return this.m_NKCUIOperationV2.GetStageSlotRect(stageIndex);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0015F975 File Offset: 0x0015DB75
		public RectTransform GetActSlot(int actIndex)
		{
			if (this.m_NKCUIOperationV2 == null || !this.m_NKCUIOperationV2.IsOpen)
			{
				return null;
			}
			return this.m_NKCUIOperationV2.GetActSlotRect(actIndex);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0015F9A0 File Offset: 0x0015DBA0
		public RectTransform GetCounterCaseSlot(int unitID)
		{
			if (this.m_NKCUIEpisodeViewerCC == null || !this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				return null;
			}
			return this.m_NKCUIEpisodeViewerCC.GetSlotByUnitID(unitID);
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x0015F9CC File Offset: 0x0015DBCC
		public NKCUICCNormalSlot GetCounterCaseListItem(int index)
		{
			if (this.m_NKCUIEpisodeViewerCC == null || !this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				return null;
			}
			if (this.m_NKCUICounterCaseNormal == null || !this.m_NKCUICounterCaseNormal.IsOpen)
			{
				return null;
			}
			return this.m_NKCUICounterCaseNormal.GetItemByStageIdx(index);
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x0015FA1F File Offset: 0x0015DC1F
		public void OnCloseResultPopup()
		{
			NKCContentManager.ShowContentUnlockPopup(new NKCContentManager.OnClose(this.TutorialCheck), Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0015FA37 File Offset: 0x0015DC37
		public void TutorialCheck()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OPERATION)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.EpisodeResult, true);
			}
		}

		// Token: 0x0400385F RID: 14431
		private NKCUIEpisodeViewerCC m_NKCUIEpisodeViewerCC;

		// Token: 0x04003860 RID: 14432
		private NKCUIManager.LoadedUIData m_loadUIDataCC;

		// Token: 0x04003861 RID: 14433
		private NKCUICounterCaseNormal m_NKCUICounterCaseNormal;

		// Token: 0x04003862 RID: 14434
		private NKCUIManager.LoadedUIData m_loadUIDataCCNormal;

		// Token: 0x04003863 RID: 14435
		private NKCUIOperationV2 m_NKCUIOperationV2;

		// Token: 0x04003864 RID: 14436
		private NKCUIManager.LoadedUIData m_loadUIDataOperation;

		// Token: 0x04003865 RID: 14437
		private EPISODE_CATEGORY m_ReservedEpisodeCategory = EPISODE_CATEGORY.EC_COUNT;

		// Token: 0x04003866 RID: 14438
		private NKMEpisodeTempletV2 m_ReservedEpisodeTemplet;

		// Token: 0x04003867 RID: 14439
		private NKMStageTempletV2 m_ReservedStageTemplet;

		// Token: 0x04003868 RID: 14440
		private int m_ReservedCutscenDungeonClearDGID;

		// Token: 0x04003869 RID: 14441
		private NKMRewardData m_RewardData;

		// Token: 0x0400386A RID: 14442
		private int m_LastPlayedMainStreamID;

		// Token: 0x0400386B RID: 14443
		private int m_LastPlayedSubStreamID;

		// Token: 0x0400386C RID: 14444
		private bool m_bPlayByFavorite;

		// Token: 0x0400386D RID: 14445
		private const float FIVE_SECONDS = 5f;

		// Token: 0x0400386E RID: 14446
		private float m_deltaTime;
	}
}
