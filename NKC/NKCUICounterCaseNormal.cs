using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using Cs.Logging;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200097A RID: 2426
	public class NKCUICounterCaseNormal : NKCUIBase
	{
		// Token: 0x06006296 RID: 25238 RVA: 0x001EF37B File Offset: 0x001ED57B
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUICounterCaseNormal.s_LoadedUIData))
			{
				NKCUICounterCaseNormal.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUICounterCaseNormal>("AB_UI_NKM_UI_COUNTER_CASE", "NKM_UI_COUNTER_CASE_NORMAL", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUICounterCaseNormal.CleanupInstance));
			}
			return NKCUICounterCaseNormal.s_LoadedUIData;
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x001EF3AF File Offset: 0x001ED5AF
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUICounterCaseNormal.s_LoadedUIData != null && NKCUICounterCaseNormal.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x001EF3C4 File Offset: 0x001ED5C4
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUICounterCaseNormal.s_LoadedUIData != null && NKCUICounterCaseNormal.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x001EF3D9 File Offset: 0x001ED5D9
		public static NKCUICounterCaseNormal GetInstance()
		{
			if (NKCUICounterCaseNormal.s_LoadedUIData != null && NKCUICounterCaseNormal.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUICounterCaseNormal.s_LoadedUIData.GetInstance<NKCUICounterCaseNormal>();
			}
			return null;
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x001EF3FA File Offset: 0x001ED5FA
		public static void CleanupInstance()
		{
			NKCUICounterCaseNormal.s_LoadedUIData = null;
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x0600629B RID: 25243 RVA: 0x001EF402 File Offset: 0x001ED602
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_CC;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x0600629C RID: 25244 RVA: 0x001EF409 File Offset: 0x001ED609
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600629D RID: 25245 RVA: 0x001EF40C File Offset: 0x001ED60C
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x0600629E RID: 25246 RVA: 0x001EF40F File Offset: 0x001ED60F
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					3
				};
			}
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x001EF41D File Offset: 0x001ED61D
		public void InitUI()
		{
			NKCUtil.SetScrollHotKey(this.m_NKM_UI_COUNTER_CASE_NORMAL_LIST_ScrollView, null);
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x001EF42B File Offset: 0x001ED62B
		public void SetActID(int actID)
		{
			if (actID <= 0)
			{
				return;
			}
			this.m_ActID = actID;
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x001EF43C File Offset: 0x001ED63C
		private void OnSelectedSlot(int stageIndex, string stageBattleStrID)
		{
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(stageBattleStrID);
			if (nkmstageTempletV != null)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null)
				{
					if (!myUserData.CheckUnlockedCounterCase(stageBattleStrID))
					{
						if (myUserData.CheckPrice(nkmstageTempletV.UnlockReqItem.Count32, nkmstageTempletV.UnlockReqItem.ItemId))
						{
							int dungeonID = NKMDungeonManager.GetDungeonID(stageBattleStrID);
							NKMPacket_COUNTERCASE_UNLOCK_REQ nkmpacket_COUNTERCASE_UNLOCK_REQ = new NKMPacket_COUNTERCASE_UNLOCK_REQ();
							nkmpacket_COUNTERCASE_UNLOCK_REQ.dungeonID = dungeonID;
							NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_COUNTERCASE_UNLOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
							return;
						}
						NKCShopManager.OpenItemLackPopup(nkmstageTempletV.UnlockReqItem.ItemId, nkmstageTempletV.UnlockReqItem.Count32);
						return;
					}
					else
					{
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(stageBattleStrID);
						if (dungeonTempletBase != null)
						{
							if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
							{
								if (dungeonTempletBase.m_CutScenStrIDBefore != "" || dungeonTempletBase.m_CutScenStrIDAfter != "")
								{
									NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedDungeonType(dungeonTempletBase.m_DungeonID);
									NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetCounterCaseNormalActID(this.m_ActID);
									NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
									return;
								}
							}
							else
							{
								NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(nkmstageTempletV, DeckContents.NORMAL);
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x001EF568 File Offset: 0x001ED768
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null)
			{
				base.UIOpened(true);
				return;
			}
			this.UpdateLeftslot();
			this.m_NKM_UI_COUNTER_CASE_NORMAL_UNIT.transform.DOLocalMove(new Vector3(-500f, 150f, 0f), 0.35f, false).From(true).SetEase(Ease.OutCubic);
			if (!nkmepisodeTempletV.m_DicStage.ContainsKey(this.m_ActID))
			{
				Log.Info(string.Format("Act ID not Found : {0}", this.m_ActID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUICounterCaseNormal.cs", 204);
				base.UIOpened(true);
				return;
			}
			this.UpdateRightSlots(true, -1);
			base.UIOpened(true);
			this.CheckTutorial();
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x001EF62C File Offset: 0x001ED82C
		public void UpdateLeftslot()
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			this.m_NKCUIEpisodeActSlotCC.SetData(nkmepisodeTempletV, this.m_ActID);
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x001EF65C File Offset: 0x001ED85C
		public void UpdateRightSlots(bool bSlotAni = false, int dungeonIDForBtnAni = -1)
		{
			if (this.m_ActID <= 0)
			{
				return;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			int count = nkmepisodeTempletV.m_DicStage[this.m_ActID].Count;
			if (this.m_listItemSlot.Count < count)
			{
				int count2 = this.m_listItemSlot.Count;
				for (int i = 0; i < count - count2; i++)
				{
					NKCUICCNormalSlot newInstance = NKCUICCNormalSlot.GetNewInstance(this.m_NKM_UI_COUNTER_CASE_NORMAL_LIST_Content, new NKCUICCNormalSlot.OnSelectedCCSlot(this.OnSelectedSlot));
					if (newInstance != null)
					{
						newInstance.gameObject.GetComponent<RectTransform>().localScale = Vector2.one;
					}
					this.m_listItemSlot.Add(newInstance);
				}
			}
			int num = 0;
			for (int i = 0; i < this.m_listItemSlot.Count; i++)
			{
				NKCUICCNormalSlot nkcuiccnormalSlot = this.m_listItemSlot[i];
				if (i < count)
				{
					NKMStageTempletV2 nkmstageTempletV = nkmepisodeTempletV.m_DicStage[this.m_ActID][i];
					if (nkmstageTempletV != null)
					{
						if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_LOCK)
						{
							num++;
							nkcuiccnormalSlot.SetData(nkmepisodeTempletV.m_DicStage[this.m_ActID][i], dungeonIDForBtnAni);
							if (!nkcuiccnormalSlot.IsActive())
							{
								nkcuiccnormalSlot.SetActive(true);
							}
						}
						else if (nkcuiccnormalSlot.IsActive())
						{
							nkcuiccnormalSlot.SetActive(false);
						}
					}
				}
				else if (nkcuiccnormalSlot.IsActive())
				{
					nkcuiccnormalSlot.SetActive(false);
				}
			}
			this.Sort();
			if (bSlotAni)
			{
				for (int i = 0; i < this.m_listItemSlot.Count; i++)
				{
					NKCUICCNormalSlot nkcuiccnormalSlot2 = this.m_listItemSlot[i];
					if (nkcuiccnormalSlot2.IsActive())
					{
						nkcuiccnormalSlot2.SetAlphaAni(i);
					}
				}
			}
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x001EF804 File Offset: 0x001EDA04
		private void Sort()
		{
			this.m_listItemSlot.Sort(new NKCUICounterCaseNormal.Comp());
			for (int i = 0; i < this.m_listItemSlot.Count; i++)
			{
				NKCUICCNormalSlot nkcuiccnormalSlot = this.m_listItemSlot[i];
				if (nkcuiccnormalSlot.IsActive())
				{
					nkcuiccnormalSlot.transform.SetSiblingIndex(i);
				}
			}
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x001EF85A File Offset: 0x001EDA5A
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x001EF875 File Offset: 0x001EDA75
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.CounterCaseList, true);
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x001EF880 File Offset: 0x001EDA80
		public NKCUICCNormalSlot GetItemByStageIdx(int stageIndex)
		{
			NKCUICCNormalSlot nkcuiccnormalSlot = this.m_listItemSlot.Find((NKCUICCNormalSlot v) => v.GetStageIndex() == stageIndex);
			if (nkcuiccnormalSlot != null)
			{
				this.m_NKM_UI_COUNTER_CASE_NORMAL_LIST_ScrollView.normalizedPosition = new Vector2(0f, 1f);
				return nkcuiccnormalSlot;
			}
			return null;
		}

		// Token: 0x04004E69 RID: 20073
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_COUNTER_CASE";

		// Token: 0x04004E6A RID: 20074
		private const string UI_ASSET_NAME = "NKM_UI_COUNTER_CASE_NORMAL";

		// Token: 0x04004E6B RID: 20075
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04004E6C RID: 20076
		public NKCUIEpisodeActSlotCC m_NKCUIEpisodeActSlotCC;

		// Token: 0x04004E6D RID: 20077
		public Transform m_NKM_UI_COUNTER_CASE_NORMAL_LIST_Content;

		// Token: 0x04004E6E RID: 20078
		public GameObject m_NKM_UI_COUNTER_CASE_NORMAL_UNIT;

		// Token: 0x04004E6F RID: 20079
		public ScrollRect m_NKM_UI_COUNTER_CASE_NORMAL_LIST_ScrollView;

		// Token: 0x04004E70 RID: 20080
		private List<NKCUICCNormalSlot> m_listItemSlot = new List<NKCUICCNormalSlot>();

		// Token: 0x04004E71 RID: 20081
		private int m_EpisodeID = 50;

		// Token: 0x04004E72 RID: 20082
		private int m_ActID;

		// Token: 0x02001624 RID: 5668
		public class Comp : IComparer<NKCUICCNormalSlot>
		{
			// Token: 0x0600AF50 RID: 44880 RVA: 0x0035C236 File Offset: 0x0035A436
			public int Compare(NKCUICCNormalSlot x, NKCUICCNormalSlot y)
			{
				if (!x.IsActive())
				{
					return 1;
				}
				if (!y.IsActive())
				{
					return -1;
				}
				if (y.GetStageIndex() <= x.GetStageIndex())
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
