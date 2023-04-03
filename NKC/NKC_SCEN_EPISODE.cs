using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using ClientPacket.Warfare;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x0200070C RID: 1804
	public class NKC_SCEN_EPISODE : NKC_SCEN_BASIC
	{
		// Token: 0x060046D0 RID: 18128 RVA: 0x00157E10 File Offset: 0x00156010
		public void SetEpisodeID(int id)
		{
			this.m_EpisodeID = id;
			this.m_ReservedActID = -1;
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00157E20 File Offset: 0x00156020
		public bool IsFirstOpen()
		{
			return this.m_bFirstOpen;
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x00157E28 File Offset: 0x00156028
		public void SetFirstOpen()
		{
			this.m_bFirstOpen = true;
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00157E31 File Offset: 0x00156031
		public void SetReservedActID(int actID)
		{
			this.m_ReservedActID = actID;
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00157E3A File Offset: 0x0015603A
		public int GetReservedActID()
		{
			return this.m_ReservedActID;
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x00157E42 File Offset: 0x00156042
		// (set) Token: 0x060046D6 RID: 18134 RVA: 0x00157E4A File Offset: 0x0015604A
		public EPISODE_DIFFICULTY Difficulty { get; set; }

		// Token: 0x060046D7 RID: 18135 RVA: 0x00157E54 File Offset: 0x00156054
		public NKC_SCEN_EPISODE()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_EPISODE;
			this.m_NUM_EPISODE = GameObject.Find("NUM_EPISODE");
			this.m_NUF_EPISODE = GameObject.Find("NUF_EPISODE");
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x00157EB4 File Offset: 0x001560B4
		public void OnRecv(NKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK)
		{
			if (this.m_NKCUIEpisodeViewerCC != null && this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCC.UpdateUI();
			}
			if (this.m_NKCUIEpisodeViewer != null && this.m_NKCUIEpisodeViewer.IsOpen)
			{
				this.m_NKCUIEpisodeViewer.ResetUIByCurrentSetting(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK);
			}
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00157F0E File Offset: 0x0015610E
		public void OnRecv(NKMPacket_COUNTERCASE_UNLOCK_ACK cNKMPacket_COUNTERCASE_UNLOCK_ACK)
		{
			if (this.m_NKCUIEpisodeViewerCCS.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCCS.SetMissionUI();
			}
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x00157F28 File Offset: 0x00156128
		public void SetCounterCaseNormalActID(int id)
		{
			this.m_NKCUIEpisodeViewerCC.SetActID(id);
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00157F36 File Offset: 0x00156136
		public void SetReservedDungeonPopup(string dungeonStrID)
		{
			NKCUIEpisodeViewer.SetReservedDungeonPopup(dungeonStrID);
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00157F3E File Offset: 0x0015613E
		public int GetEpisodeID()
		{
			return this.m_EpisodeID;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x00157F46 File Offset: 0x00156146
		public NKCUIEpisodeViewer GetEpisodeViewer()
		{
			return this.m_NKCUIEpisodeViewer;
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00157F4E File Offset: 0x0015614E
		public void UpdateTicketCountUI()
		{
			this.m_NKCUIEpisodeViewer.UpdateTicketCountUI();
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x00157F5C File Offset: 0x0015615C
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUM_EPISODE.SetActive(true);
			this.m_NUF_EPISODE.SetActive(true);
			if (!this.m_bLoadedUI)
			{
				if (this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUM_EPISODE_PREFAB == null)
				{
					this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUM_EPISODE_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NUM_EPISODE_PREFAB", true, null);
				}
				if (this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUF_EPISODE_PREFAB == null)
				{
					this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUF_EPISODE_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NUF_EPISODE_PREFAB", true, null);
				}
			}
			if (this.GetCurrEPCategory() != EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				NKCUIEpisodeViewer nkcuiepisodeViewer = this.m_NKCUIEpisodeViewer;
				if (nkcuiepisodeViewer == null)
				{
					return;
				}
				nkcuiepisodeViewer.PreLoad();
			}
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x00157FFC File Offset: 0x001561FC
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUM_EPISODE_PREFAB.m_Instant.transform.SetParent(this.m_NUM_EPISODE.transform, false);
			this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUF_EPISODE_PREFAB.m_Instant.transform.SetParent(this.m_NUF_EPISODE.transform, false);
			if (!this.m_bLoadedUI)
			{
				this.m_NKCUIEpisodeViewer = NKCUIEpisodeViewer.InitUI();
				this.m_NKCUIEpisodeViewerCCS = NKCUIEpisodeViewerCCS.InitUI();
				if (this.m_NKCUIEpisodeViewerCCS != null)
				{
					this.m_NKCUIEpisodeViewerCCS.InitOutComponents(this.m_NKC_SCEN_EPISODE_UI_DATA.m_NUM_EPISODE_PREFAB.m_Instant);
					this.m_NKCUIEpisodeViewerCCS.InitUI2();
				}
			}
			if (this.m_bFirstOpen)
			{
				NKCUIEpisodeViewerCC nkcuiepisodeViewerCC = this.m_NKCUIEpisodeViewerCC;
				if (nkcuiepisodeViewerCC != null)
				{
					nkcuiepisodeViewerCC.SetActID(0);
				}
				if (this.m_NKCUIEpisodeViewerCCS != null)
				{
					this.m_NKCUIEpisodeViewerCCS.SetReservedResetBook(true);
				}
			}
			NKCUIEpisodeViewer nkcuiepisodeViewer = this.m_NKCUIEpisodeViewer;
			if (nkcuiepisodeViewer != null)
			{
				nkcuiepisodeViewer.SetEpisodeID(this.m_EpisodeID);
			}
			NKCUIEpisodeViewerCCS nkcuiepisodeViewerCCS = this.m_NKCUIEpisodeViewerCCS;
			if (nkcuiepisodeViewerCCS != null)
			{
				nkcuiepisodeViewerCCS.SetEpisodeID(this.m_EpisodeID);
			}
			NKCPopupDungeonInfo.PreloadInstance();
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x00158114 File Offset: 0x00156314
		private EPISODE_CATEGORY GetCurrEPCategory()
		{
			EPISODE_CATEGORY result = EPISODE_CATEGORY.EC_MAINSTREAM;
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				result = nkmepisodeTempletV.m_EPCategory;
			}
			return result;
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x0015813B File Offset: 0x0015633B
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x00158143 File Offset: 0x00156343
		public void SetReservedCutscenDungeonClearDGID(int dungeonID)
		{
			this.m_ReservedCutscenDungeonClearDGID = dungeonID;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x0015814C File Offset: 0x0015634C
		public void DoAfterLogout()
		{
			this.SetReservedCutscenDungeonClearDGID(0);
			this.SetReservedActID(-1);
			NKCUIEpisodeViewer.SetReservedDungeonPopup("");
			this.Difficulty = EPISODE_DIFFICULTY.NORMAL;
			this.SetFirstOpen();
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x00158174 File Offset: 0x00156374
		public void SetReservedDetailSettingWithoutEPID(NKMStageTempletV2 cNKMStageTemplet)
		{
			if (cNKMStageTemplet == null)
			{
				return;
			}
			if (cNKMStageTemplet.EpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY || cNKMStageTemplet.EpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_SUPPLY)
			{
				List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(cNKMStageTemplet.EpisodeTemplet.m_EPCategory, false, EPISODE_DIFFICULTY.NORMAL);
				if (listNKMEpisodeTempletByCategory != null)
				{
					for (int i = 0; i < listNKMEpisodeTempletByCategory.Count; i++)
					{
						if (listNKMEpisodeTempletByCategory[i].m_EpisodeID == cNKMStageTemplet.EpisodeTemplet.m_EpisodeID)
						{
							this.SetReservedActID(i + 1);
							return;
						}
					}
					return;
				}
			}
			else
			{
				this.SetReservedActID(cNKMStageTemplet.ActId);
				this.Difficulty = cNKMStageTemplet.m_Difficulty;
			}
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x00158204 File Offset: 0x00156404
		public void ReopenEpisodeView()
		{
			if (this.m_NKCUIEpisodeViewer != null && this.m_NKCUIEpisodeViewer.IsOpen)
			{
				this.m_NKCUIEpisodeViewer.Close();
			}
			if (this.m_NKCUIEpisodeViewerCC != null && this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCC.Close();
			}
			if (this.m_NKCUIEpisodeViewerCCS != null && this.m_NKCUIEpisodeViewerCCS.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCCS.Close();
			}
			if (this.GetCurrEPCategory() == EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				if (this.GetEpisodeID() == 50)
				{
					NKCUIEpisodeViewerCC nkcuiepisodeViewerCC = this.m_NKCUIEpisodeViewerCC;
					if (nkcuiepisodeViewerCC == null)
					{
						return;
					}
					nkcuiepisodeViewerCC.Open();
					return;
				}
				else if (this.GetEpisodeID() == 51)
				{
					NKCUIEpisodeViewerCCS nkcuiepisodeViewerCCS = this.m_NKCUIEpisodeViewerCCS;
					if (nkcuiepisodeViewerCCS == null)
					{
						return;
					}
					nkcuiepisodeViewerCCS.Open();
					return;
				}
			}
			else
			{
				NKCUIEpisodeViewer nkcuiepisodeViewer = this.m_NKCUIEpisodeViewer;
				if (nkcuiepisodeViewer == null)
				{
					return;
				}
				nkcuiepisodeViewer.Open(this.m_bFirstOpen, this.Difficulty);
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x001582E0 File Offset: 0x001564E0
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			NKCCamera.GetCamera().orthographic = false;
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, -1000f);
			if (this.m_objNUM_OPERATION_BG == null)
			{
				this.m_objNUM_OPERATION_BG = NKCUIManager.OpenUI("NUM_OPERATION_BG");
			}
			if (this.m_objNUM_OPERATION_BG != null)
			{
				NKCCamera.RescaleRectToCameraFrustrum(this.m_objNUM_OPERATION_BG.GetComponent<RectTransform>(), NKCCamera.GetCamera(), new Vector2(200f, 200f), -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
			}
			if (this.GetCurrEPCategory() == EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				if (this.GetEpisodeID() == 50)
				{
					if (this.m_NKCUIEpisodeViewer.IsOpen)
					{
						this.m_NKCUIEpisodeViewer.Close();
					}
					if (this.m_NKCUIEpisodeViewerCCS.IsOpen)
					{
						this.m_NKCUIEpisodeViewerCCS.Close();
					}
					this.m_NKCUIEpisodeViewerCC.Open();
				}
				else if (this.GetEpisodeID() == 51)
				{
					if (this.m_NKCUIEpisodeViewerCC.IsOpen)
					{
						this.m_NKCUIEpisodeViewerCC.Close();
					}
					if (this.m_NKCUIEpisodeViewer.IsOpen)
					{
						this.m_NKCUIEpisodeViewer.Close();
					}
					this.m_NKCUIEpisodeViewerCCS.Open();
				}
			}
			else
			{
				if (this.m_NKCUIEpisodeViewerCC.IsOpen)
				{
					this.m_NKCUIEpisodeViewerCC.Close();
				}
				if (this.m_NKCUIEpisodeViewerCCS.IsOpen)
				{
					this.m_NKCUIEpisodeViewerCCS.Close();
				}
				this.m_NKCUIEpisodeViewer.Open(true, this.Difficulty);
			}
			if (this.m_ReservedCutscenDungeonClearDGID > 0)
			{
				NKMPacket_CUTSCENE_DUNGEON_CLEAR_REQ nkmpacket_CUTSCENE_DUNGEON_CLEAR_REQ = new NKMPacket_CUTSCENE_DUNGEON_CLEAR_REQ();
				nkmpacket_CUTSCENE_DUNGEON_CLEAR_REQ.dungeonID = this.m_ReservedCutscenDungeonClearDGID;
				this.m_ReservedCutscenDungeonClearDGID = 0;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CUTSCENE_DUNGEON_CLEAR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}
			this.m_bFirstOpen = false;
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetReservedActID(-1);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x001584A0 File Offset: 0x001566A0
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIEpisodeViewer.IsOpen)
			{
				this.m_NKCUIEpisodeViewer.Close();
			}
			if (this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCC.Close();
			}
			if (this.m_NKCUIEpisodeViewerCCS.IsOpen)
			{
				this.m_NKCUIEpisodeViewerCCS.Close();
			}
			this.m_NUM_EPISODE.SetActive(false);
			this.m_NUF_EPISODE.SetActive(false);
			this.UnloadUI();
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x00158519 File Offset: 0x00156719
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUIEpisodeViewer = null;
			this.m_NKCUIEpisodeViewerCC = null;
			this.m_NKCUIEpisodeViewerCCS = null;
			this.m_NKC_SCEN_EPISODE_UI_DATA.Init();
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00158541 File Offset: 0x00156741
		public void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK cNKMPacket_WARFARE_GAME_GIVE_UP_ACK)
		{
			this.m_NKCUIEpisodeViewer.OnRecv(cNKMPacket_WARFARE_GAME_GIVE_UP_ACK);
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x0015854F File Offset: 0x0015674F
		public void OnRecv(NKMPacket_STAGE_UNLOCK_ACK sPacket)
		{
			this.m_NKCUIEpisodeViewer.OnRecv(sPacket);
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00158560 File Offset: 0x00156760
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (this.GetCurrEPCategory() != EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				if (!NKCCamera.IsTrackingCameraPos())
				{
					NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
				}
				this.m_BloomIntensity.Update(Time.deltaTime);
				if (!this.m_BloomIntensity.IsTracking())
				{
					this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
				}
				NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
			}
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x0015860A File Offset: 0x0015680A
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x0015860D File Offset: 0x0015680D
		public void OnCloseResultPopup()
		{
			NKCContentManager.ShowContentUnlockPopup(new NKCContentManager.OnClose(this.TutorialCheck), Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00158625 File Offset: 0x00156825
		public void ProcessRelogin()
		{
			if (NKCGameEventManager.IsEventPlaying())
			{
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKCScenManager.GetScenManager().GetNowScenID(), true);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00158644 File Offset: 0x00156844
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			if (this.m_NKCUIEpisodeViewer != null)
			{
				this.m_NKCUIEpisodeViewer.SetTutorialMainstreamGuide(eventTemplet, Complete);
				return;
			}
			if (Complete != null)
			{
				Complete();
			}
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x0015866B File Offset: 0x0015686B
		public void TutorialCheck()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_EPISODE)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.EpisodeResult, true);
			}
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x00158683 File Offset: 0x00156883
		public RectTransform GetCounterCaseSlot(int unitID)
		{
			if (this.m_NKCUIEpisodeViewerCC == null || !this.m_NKCUIEpisodeViewerCC.IsOpen)
			{
				return null;
			}
			return this.m_NKCUIEpisodeViewerCC.GetSlotByUnitID(unitID);
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x001586AE File Offset: 0x001568AE
		public RectTransform GetStageSlot(int stageIndex)
		{
			if (this.m_NKCUIEpisodeViewer == null || !this.m_NKCUIEpisodeViewer.IsOpen)
			{
				return null;
			}
			return this.m_NKCUIEpisodeViewer.GetStageSlotRect(stageIndex);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x001586D9 File Offset: 0x001568D9
		public RectTransform GetActSlot(int actIndex)
		{
			if (this.m_NKCUIEpisodeViewer == null || !this.m_NKCUIEpisodeViewer.IsOpen)
			{
				return null;
			}
			return this.m_NKCUIEpisodeViewer.GetActSlotRect(actIndex);
		}

		// Token: 0x040037B3 RID: 14259
		private GameObject m_NUM_EPISODE;

		// Token: 0x040037B4 RID: 14260
		private GameObject m_NUF_EPISODE;

		// Token: 0x040037B5 RID: 14261
		private NKC_SCEN_EPISODE_UI_DATA m_NKC_SCEN_EPISODE_UI_DATA = new NKC_SCEN_EPISODE_UI_DATA();

		// Token: 0x040037B6 RID: 14262
		private NKCUIEpisodeViewer m_NKCUIEpisodeViewer;

		// Token: 0x040037B7 RID: 14263
		private NKCUIEpisodeViewerCC m_NKCUIEpisodeViewerCC;

		// Token: 0x040037B8 RID: 14264
		private NKCUIEpisodeViewerCCS m_NKCUIEpisodeViewerCCS;

		// Token: 0x040037B9 RID: 14265
		private int m_EpisodeID;

		// Token: 0x040037BA RID: 14266
		private GameObject m_objNUM_OPERATION_BG;

		// Token: 0x040037BB RID: 14267
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x040037BC RID: 14268
		private int m_ReservedCutscenDungeonClearDGID;

		// Token: 0x040037BD RID: 14269
		private bool m_bFirstOpen = true;

		// Token: 0x040037BE RID: 14270
		private int m_ReservedActID = -1;
	}
}
