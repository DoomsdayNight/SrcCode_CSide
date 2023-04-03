using System;
using System.Collections.Generic;
using ClientPacket.Warfare;
using NKC.Publisher;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C6 RID: 2502
	public class NKCUIOperationViewer : NKCUIBase
	{
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06006A8C RID: 27276 RVA: 0x00229BAF File Offset: 0x00227DAF
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_OPERATION_VIEWER;
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06006A8D RID: 27277 RVA: 0x00229BB6 File Offset: 0x00227DB6
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06006A8E RID: 27278 RVA: 0x00229BB9 File Offset: 0x00227DB9
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06006A8F RID: 27279 RVA: 0x00229BBC File Offset: 0x00227DBC
		public override string GuideTempletID
		{
			get
			{
				switch (this.m_Now_EPISODE_CATEGORY)
				{
				case EPISODE_CATEGORY.EC_MAINSTREAM:
					return "ARTICLE_OPERATION_MAINSTREAM";
				case EPISODE_CATEGORY.EC_DAILY:
					return "ARTICLE_OPERATION_DAILY_MISSION";
				case EPISODE_CATEGORY.EC_COUNTERCASE:
					return "ARTICLE_OPERATION_COUNTER_CASE";
				case EPISODE_CATEGORY.EC_SIDESTORY:
					return "ARTICLE_OPERATION_SIDE_STORY";
				case EPISODE_CATEGORY.EC_FIELD:
					return "ARTICLE_OPERATION_FIELD";
				case EPISODE_CATEGORY.EC_EVENT:
					return "";
				default:
					return "";
				}
			}
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x00229C19 File Offset: 0x00227E19
		public override void OnBackButton()
		{
			base.OnBackButton();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x00229C30 File Offset: 0x00227E30
		public static NKCUIOperationViewer InitUI()
		{
			NKCUIOperationViewer nkcuioperationViewer = NKCUIManager.OpenUI<NKCUIOperationViewer>("NKM_OPERATION_Panel");
			if (nkcuioperationViewer == null)
			{
				return null;
			}
			nkcuioperationViewer.m_NKCUIOperationEPList = NKCUIOperationEPList.InitUI(nkcuioperationViewer.gameObject);
			nkcuioperationViewer.m_NKCUIOPDailyMission.InitUI();
			nkcuioperationViewer.m_NKCUIOPSupplyMission.InitUI();
			nkcuioperationViewer.m_NKCUIOPCounterCase.InitUI();
			NKCUIComButton component = nkcuioperationViewer.m_NKM_UI_OPERATION_ING_WF_DIRECT_GO.GetComponent<NKCUIComButton>();
			if (component != null)
			{
				component.PointerClick.RemoveAllListeners();
				component.PointerClick.AddListener(new UnityAction(nkcuioperationViewer.OnClickINGWarfareDirectGoBtn));
			}
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_MainStreamToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickMainStream));
			nkcuioperationViewer.m_MainStreamToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_DailyToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickDaily));
			nkcuioperationViewer.m_DailyToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_SupplyToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickSupply));
			nkcuioperationViewer.m_SupplyToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_SideStoryToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickSideStory));
			nkcuioperationViewer.m_SideStoryToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_CounterSideToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickCounterCase));
			nkcuioperationViewer.m_CounterSideToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_FieldToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickField));
			nkcuioperationViewer.m_FieldToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_EventToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickEvent));
			nkcuioperationViewer.m_EventToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(nkcuioperationViewer.m_ChallangeToggleBtn, new UnityAction<bool>(nkcuioperationViewer.OnClickChallenge));
			nkcuioperationViewer.m_ChallangeToggleBtn.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetGameobjectActive(nkcuioperationViewer.m_SupplyToggleBtn, NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY.EC_SUPPLY, false, EPISODE_DIFFICULTY.NORMAL).Count > 0);
			NKCUIComToggle mainStreamToggleBtn = nkcuioperationViewer.m_MainStreamToggleBtn;
			if (((mainStreamToggleBtn != null) ? mainStreamToggleBtn.m_ToggleGroup : null) != null)
			{
				nkcuioperationViewer.m_MainStreamToggleBtn.m_ToggleGroup.SetHotkey(HotkeyEventType.Up, HotkeyEventType.Down);
			}
			nkcuioperationViewer.m_objCounterCaseRedDot.SetActive(false);
			nkcuioperationViewer.gameObject.SetActive(false);
			return nkcuioperationViewer;
		}

		// Token: 0x06006A92 RID: 27282 RVA: 0x00229E34 File Offset: 0x00228034
		public void SetFirstOpen()
		{
			if (this.m_NKCUIOperationEPList != null)
			{
				this.m_NKCUIOperationEPList.SetFirstOpen();
			}
		}

		// Token: 0x06006A93 RID: 27283 RVA: 0x00229E4F File Offset: 0x0022804F
		public void PreLoad()
		{
			if (this.m_NKCUIOperationEPList)
			{
				this.m_NKCUIOperationEPList.PreLoad();
			}
		}

		// Token: 0x06006A94 RID: 27284 RVA: 0x00229E6C File Offset: 0x0022806C
		public void Open(bool bFirstOpen, int reservedEpisodeID = 0)
		{
			NKCUIFadeInOut.FadeIn(0.1f, null, false);
			base.UIOpened(true);
			int num = 12;
			if (this.m_objEventDropEventBadge != null)
			{
				for (int i = 0; i < num; i++)
				{
					if (i < this.m_objEventDropEventBadge.Length)
					{
						NKCUtil.SetGameobjectActive(this.m_objEventDropEventBadge[i], this.CheckEpisodeCategoryEventDrop((EPISODE_CATEGORY)i));
					}
				}
			}
			NKCContentManager.SetUnlockedCounterCaseKey();
			if (bFirstOpen)
			{
				this.OnSelectCategory(EPISODE_CATEGORY.EC_MAINSTREAM);
			}
			else
			{
				this.m_Now_EPISODE_CATEGORY = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeCategory();
				if (this.m_NKCUIOperationEPList.IsLockCategory(this.m_Now_EPISODE_CATEGORY))
				{
					this.m_Now_EPISODE_CATEGORY = EPISODE_CATEGORY.EC_MAINSTREAM;
				}
				this.OnSelectCategory(this.m_Now_EPISODE_CATEGORY);
			}
			this.CheckLockedContents();
			this.CheckFirstUnlockedEffect();
			this.CheckOpenedContents();
			this.UpdateINGWarfareDirectGoUI();
			this.CheckTutorial();
			if (reservedEpisodeID > 0)
			{
				this.m_NKCUIOperationEPList.SetEPSlotToCenter(this.m_Now_EPISODE_CATEGORY, reservedEpisodeID);
			}
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x00229F44 File Offset: 0x00228144
		private void CheckOpenedContents()
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_DAILY))
			{
				NKCUtil.SetGameobjectActive(this.m_DailyToggleBtn, false);
			}
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_SUPPLY))
			{
				NKCUtil.SetGameobjectActive(this.m_SupplyToggleBtn, false);
			}
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_SIDESTORY))
			{
				NKCUtil.SetGameobjectActive(this.m_SideStoryToggleBtn, false);
			}
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_COUNTERCASE))
			{
				NKCUtil.SetGameobjectActive(this.m_CounterSideToggleBtn, false);
			}
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_FIELD))
			{
				NKCUtil.SetGameobjectActive(this.m_FieldToggleBtn, false);
			}
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_EVENT))
			{
				NKCUtil.SetGameobjectActive(this.m_EventToggleBtn, false);
			}
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.EPISODE_TAB_CHALLENGE))
			{
				NKCUtil.SetGameobjectActive(this.m_ChallangeToggleBtn, false);
			}
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x00229FE4 File Offset: 0x002281E4
		private void CheckLockedContents()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.SIDESTORY, 0, 0))
			{
				this.m_SideStoryToggleBtn.Lock(false);
			}
			else
			{
				this.m_SideStoryToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.SUPPLY_MISSION, 0, 0))
			{
				this.m_SupplyToggleBtn.Lock(false);
			}
			else
			{
				this.m_SupplyToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.DAILY, 0, 0))
			{
				this.m_DailyToggleBtn.Lock(false);
			}
			else
			{
				this.m_DailyToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.COUNTERCASE, 0, 0))
			{
				this.m_CounterSideToggleBtn.Lock(false);
				this.m_objCounterCaseRedDot.SetActive(false);
			}
			else
			{
				this.m_CounterSideToggleBtn.UnLock(false);
				this.m_objCounterCaseRedDot.SetActive(NKCContentManager.CheckNewCounterCase(NKMEpisodeTempletV2.Find(50, EPISODE_DIFFICULTY.NORMAL)));
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FIELD, 0, 0))
			{
				this.m_FieldToggleBtn.Lock(false);
			}
			else
			{
				this.m_FieldToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.EVENT, 0, 0) || this.m_NKCUIOperationEPList.IsLockCategory(EPISODE_CATEGORY.EC_EVENT))
			{
				if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong)
				{
					NKCUtil.SetGameobjectActive(this.m_EventToggleBtn, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_EventToggleBtn, true);
					this.m_EventToggleBtn.Lock(false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_EventToggleBtn, true);
				this.m_EventToggleBtn.UnLock(false);
			}
			bool flag;
			NKCContentManager.eContentStatus eContentStatus = NKCContentManager.CheckContentStatus(ContentsType.CHALLENGE, out flag, 0, 0);
			NKCUtil.SetGameobjectActive(this.m_ChallangeToggleBtn, eContentStatus != NKCContentManager.eContentStatus.Hide);
			NKCUIComToggle challangeToggleBtn = this.m_ChallangeToggleBtn;
			if (challangeToggleBtn == null)
			{
				return;
			}
			challangeToggleBtn.SetLock(eContentStatus == NKCContentManager.eContentStatus.Lock || this.m_NKCUIOperationEPList.IsLockCategory(EPISODE_CATEGORY.EC_CHALLENGE), false);
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x0022A174 File Offset: 0x00228374
		private void CheckFirstUnlockedEffect()
		{
			if (NKCContentManager.UnlockEffectRequired(ContentsType.DAILY, 0))
			{
				this.AddUnlockedEffect(ContentsType.DAILY);
			}
			if (NKCContentManager.UnlockEffectRequired(ContentsType.SIDESTORY, 0))
			{
				this.AddUnlockedEffect(ContentsType.SIDESTORY);
			}
			if (NKCContentManager.UnlockEffectRequired(ContentsType.COUNTERCASE, 0))
			{
				this.AddUnlockedEffect(ContentsType.COUNTERCASE);
			}
			if (NKCContentManager.UnlockEffectRequired(ContentsType.FIELD, 0))
			{
				this.AddUnlockedEffect(ContentsType.FIELD);
			}
			if (NKCContentManager.UnlockEffectRequired(ContentsType.EVENT, 0))
			{
				this.AddUnlockedEffect(ContentsType.EVENT);
			}
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x0022A1DC File Offset: 0x002283DC
		private void AddUnlockedEffect(ContentsType type)
		{
			Transform transform;
			switch (type)
			{
			case ContentsType.SIDESTORY:
			{
				NKCUIComToggle sideStoryToggleBtn = this.m_SideStoryToggleBtn;
				transform = ((sideStoryToggleBtn != null) ? sideStoryToggleBtn.transform : null);
				break;
			}
			case ContentsType.DAILY:
			{
				NKCUIComToggle dailyToggleBtn = this.m_DailyToggleBtn;
				transform = ((dailyToggleBtn != null) ? dailyToggleBtn.transform : null);
				break;
			}
			case ContentsType.COUNTERCASE:
			{
				NKCUIComToggle counterSideToggleBtn = this.m_CounterSideToggleBtn;
				transform = ((counterSideToggleBtn != null) ? counterSideToggleBtn.transform : null);
				break;
			}
			case ContentsType.FIELD:
			{
				NKCUIComToggle fieldToggleBtn = this.m_FieldToggleBtn;
				transform = ((fieldToggleBtn != null) ? fieldToggleBtn.transform : null);
				break;
			}
			case ContentsType.EVENT:
			{
				NKCUIComToggle eventToggleBtn = this.m_EventToggleBtn;
				transform = ((eventToggleBtn != null) ? eventToggleBtn.transform : null);
				break;
			}
			default:
			{
				if (type != ContentsType.SUPPLY_MISSION)
				{
					return;
				}
				NKCUIComToggle supplyToggleBtn = this.m_SupplyToggleBtn;
				transform = ((supplyToggleBtn != null) ? supplyToggleBtn.transform : null);
				break;
			}
			}
			if (transform == null)
			{
				return;
			}
			GameObject value = NKCContentManager.AddUnlockedEffect(transform);
			this.m_dicUnlockedEffect.Add(type, value);
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x0022A2AA File Offset: 0x002284AA
		public void UpdateEPRewardStatus()
		{
			this.m_NKCUIOperationEPList.UpdateSlots();
		}

		// Token: 0x06006A9A RID: 27290 RVA: 0x0022A2B8 File Offset: 0x002284B8
		public void OnClickINGWarfareDirectGoBtn()
		{
			if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID);
				if (nkmwarfareTemplet == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_TEMPLET), null, "");
					return;
				}
				if (nkmwarfareTemplet.MapTemplet == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_MAP_TEMPLET), null, "");
					return;
				}
				NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
				if (nkc_SCEN_WARFARE_GAME != null)
				{
					int warfareTempletID = NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID;
					nkc_SCEN_WARFARE_GAME.SetWarfareStrID(NKCWarfareManager.GetWarfareStrID(warfareTempletID));
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
				}
			}
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x0022A368 File Offset: 0x00228568
		public void UpdateINGWarfareDirectGoUI()
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet != null)
				{
					NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
					if (nkmstageTempletV != null)
					{
						NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
						if (episodeTemplet != null)
						{
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_ING_WF_DIRECT_GO, true);
							this.m_NKM_UI_OPERATION_MENU_EP_TEXT.text = NKCUtilString.GetPlayingWarfare(episodeTemplet.GetEpisodeTitle(), nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum);
							return;
						}
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_ING_WF_DIRECT_GO, false);
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x0022A3F7 File Offset: 0x002285F7
		private void PlayTextUV()
		{
			this.m_fNKMTrackingFloatDecoText.SetNowValue(1f);
			this.m_fNKMTrackingFloatDecoText.SetTracking(-1f, 20f, TRACKING_DATA_TYPE.TDT_NORMAL);
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0022A420 File Offset: 0x00228620
		private bool CheckEpisodeCategoryEventDrop(EPISODE_CATEGORY episodeCategory)
		{
			bool result = false;
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(episodeCategory, true, EPISODE_DIFFICULTY.HARD);
			int count = listNKMEpisodeTempletByCategory.Count;
			for (int i = 0; i < count; i++)
			{
				if (NKMEpisodeMgr.CheckEpisodeHasEventDrop(listNKMEpisodeTempletByCategory[i]))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x0022A45C File Offset: 0x0022865C
		private void Update()
		{
			if (base.IsOpen && this.m_fNKMTrackingFloatDecoText != null)
			{
				this.m_fNKMTrackingFloatDecoText.Update(Time.deltaTime);
				if (!this.m_fNKMTrackingFloatDecoText.IsTracking())
				{
					this.PlayTextUV();
				}
			}
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x0022A494 File Offset: 0x00228694
		public override void CloseInternal()
		{
			foreach (KeyValuePair<ContentsType, GameObject> keyValuePair in this.m_dicUnlockedEffect)
			{
				UnityEngine.Object.Destroy(keyValuePair.Value);
			}
			this.m_dicUnlockedEffect.Clear();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06006AA0 RID: 27296 RVA: 0x0022A510 File Offset: 0x00228710
		public void OnSelectCategory(EPISODE_CATEGORY target)
		{
			this.m_Now_EPISODE_CATEGORY = target;
			this.PlayTextUV();
			ContentsType contentsType = NKCContentManager.GetContentsType(target);
			if (NKCContentManager.UnlockEffectRequired(contentsType, 0))
			{
				if (this.m_dicUnlockedEffect.ContainsKey(contentsType))
				{
					UnityEngine.Object.Destroy(this.m_dicUnlockedEffect[contentsType]);
					this.m_dicUnlockedEffect.Remove(contentsType);
				}
				NKCContentManager.RemoveUnlockedContent(contentsType, 0, true);
			}
			NKCUIComToggle mainStreamToggleBtn = this.m_MainStreamToggleBtn;
			if (mainStreamToggleBtn != null)
			{
				mainStreamToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_MAINSTREAM, true, true);
			}
			NKCUIComToggle sideStoryToggleBtn = this.m_SideStoryToggleBtn;
			if (sideStoryToggleBtn != null)
			{
				sideStoryToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_SIDESTORY, true, true);
			}
			NKCUIComToggle fieldToggleBtn = this.m_FieldToggleBtn;
			if (fieldToggleBtn != null)
			{
				fieldToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_FIELD, true, true);
			}
			NKCUIComToggle eventToggleBtn = this.m_EventToggleBtn;
			if (eventToggleBtn != null)
			{
				eventToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_EVENT, true, true);
			}
			NKCUIComToggle dailyToggleBtn = this.m_DailyToggleBtn;
			if (dailyToggleBtn != null)
			{
				dailyToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_DAILY, true, true);
			}
			NKCUIComToggle supplyToggleBtn = this.m_SupplyToggleBtn;
			if (supplyToggleBtn != null)
			{
				supplyToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_SUPPLY, true, true);
			}
			NKCUIComToggle challangeToggleBtn = this.m_ChallangeToggleBtn;
			if (challangeToggleBtn != null)
			{
				challangeToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_CHALLENGE, true, true);
			}
			NKCUIComToggle counterSideToggleBtn = this.m_CounterSideToggleBtn;
			if (counterSideToggleBtn != null)
			{
				counterSideToggleBtn.Select(this.m_Now_EPISODE_CATEGORY == EPISODE_CATEGORY.EC_COUNTERCASE, true, true);
			}
			switch (target)
			{
			case EPISODE_CATEGORY.EC_MAINSTREAM:
			case EPISODE_CATEGORY.EC_SIDESTORY:
			case EPISODE_CATEGORY.EC_FIELD:
			case EPISODE_CATEGORY.EC_EVENT:
				this.m_NKCUIOperationEPList.Open(this.m_Now_EPISODE_CATEGORY);
				this.m_NKCUIOPDailyMission.Close();
				this.m_NKCUIOPSupplyMission.Close();
				this.m_NKCUIOPCounterCase.Close();
				break;
			case EPISODE_CATEGORY.EC_DAILY:
				this.m_NKCUIOperationEPList.Close();
				this.m_NKCUIOPDailyMission.Open();
				this.m_NKCUIOPSupplyMission.Close();
				this.m_NKCUIOPCounterCase.Close();
				break;
			case EPISODE_CATEGORY.EC_COUNTERCASE:
				this.m_NKCUIOperationEPList.Close();
				this.m_NKCUIOPDailyMission.Close();
				this.m_NKCUIOPSupplyMission.Close();
				this.m_NKCUIOPCounterCase.Open();
				break;
			case EPISODE_CATEGORY.EC_SUPPLY:
			case EPISODE_CATEGORY.EC_CHALLENGE:
				this.m_NKCUIOperationEPList.Close();
				this.m_NKCUIOPDailyMission.Close();
				this.m_NKCUIOPSupplyMission.Open(target);
				this.m_NKCUIOPCounterCase.Close();
				break;
			}
			NKCUIManager.UpdateUpsideMenu();
			this.CheckLockedContents();
		}

		// Token: 0x06006AA1 RID: 27297 RVA: 0x0022A74C File Offset: 0x0022894C
		public void OnClickMainStream(bool bSet = true)
		{
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_MAINSTREAM);
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x0022A759 File Offset: 0x00228959
		public void OnClickDaily(bool bSet = true)
		{
			if (this.m_DailyToggleBtn.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.DAILY, 0);
				return;
			}
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_DAILY);
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x0022A77D File Offset: 0x0022897D
		public void OnClickSupply(bool bSet = true)
		{
			if (this.m_SupplyToggleBtn.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.SUPPLY_MISSION, 0);
				return;
			}
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_SUPPLY);
		}

		// Token: 0x06006AA4 RID: 27300 RVA: 0x0022A7A1 File Offset: 0x002289A1
		public void OnClickSideStory(bool bSet = true)
		{
			if (this.m_SideStoryToggleBtn.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.SIDESTORY, 0);
				return;
			}
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_SIDESTORY);
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x0022A7C5 File Offset: 0x002289C5
		public void OnClickCounterCase(bool bSet = true)
		{
			if (this.m_CounterSideToggleBtn.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.COUNTERCASE, 0);
				return;
			}
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_COUNTERCASE);
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x0022A7E9 File Offset: 0x002289E9
		public void OnClickField(bool bSet = true)
		{
			if (this.m_FieldToggleBtn.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FIELD, 0);
				return;
			}
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_FIELD);
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x0022A810 File Offset: 0x00228A10
		public void OnClickEvent(bool bSet = true)
		{
			if (this.m_EventToggleBtn.m_bLock)
			{
				if (this.m_NKCUIOperationEPList.IsLockCategory(EPISODE_CATEGORY.EC_EVENT))
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_NO_EVENT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCContentManager.ShowLockedMessagePopup(ContentsType.EVENT, 0);
				return;
			}
			else
			{
				if (!bSet)
				{
					return;
				}
				this.PlayTextUV();
				this.m_Now_EPISODE_CATEGORY = EPISODE_CATEGORY.EC_EVENT;
				if (NKCContentManager.UnlockEffectRequired(ContentsType.EVENT, 0))
				{
					if (this.m_dicUnlockedEffect.ContainsKey(ContentsType.EVENT))
					{
						UnityEngine.Object.Destroy(this.m_dicUnlockedEffect[ContentsType.EVENT]);
						this.m_dicUnlockedEffect.Remove(ContentsType.EVENT);
					}
					NKCContentManager.RemoveUnlockedContent(ContentsType.EVENT, 0, true);
				}
				this.m_NKCUIOPDailyMission.Close();
				this.m_NKCUIOPSupplyMission.Close();
				this.m_NKCUIOperationEPList.Open(this.m_Now_EPISODE_CATEGORY);
				this.m_NKCUIOPCounterCase.Close();
				NKCUIManager.UpdateUpsideMenu();
				this.CheckLockedContents();
				return;
			}
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x0022A8E6 File Offset: 0x00228AE6
		public void OnClickChallenge(bool bSet)
		{
			if (this.m_ChallangeToggleBtn.m_bLock)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.CHALLENGE, 0);
				return;
			}
			if (!bSet)
			{
				return;
			}
			this.OnSelectCategory(EPISODE_CATEGORY.EC_CHALLENGE);
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x0022A90A File Offset: 0x00228B0A
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
			NKCUIOPDailyMission nkcuiopdailyMission = this.m_NKCUIOPDailyMission;
			if (nkcuiopdailyMission == null)
			{
				return;
			}
			nkcuiopdailyMission.OnInventoryChange(itemData);
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x0022A924 File Offset: 0x00228B24
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			return this.m_NKCUIOperationEPList != null && this.m_NKCUIOperationEPList.OnHotkey(hotkey);
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x0022A945 File Offset: 0x00228B45
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation, true);
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x0022A950 File Offset: 0x00228B50
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			this.m_NKCUIOperationEPList.Open(EPISODE_CATEGORY.EC_MAINSTREAM);
			NKCUIOperationEPSlot nkcuioperationEPSlot = this.m_NKCUIOperationEPList.SetEPSlotToCenter(EPISODE_CATEGORY.EC_MAINSTREAM, eventTemplet.Value);
			if (!(nkcuioperationEPSlot == null))
			{
				NKCGameEventManager.OpenTutorialGuideBySettedFace(nkcuioperationEPSlot.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
					UnityAction complete2 = Complete;
					if (complete2 != null)
					{
						complete2();
					}
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetEpisodeID(eventTemplet.Value);
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetReservedActID(1);
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().Difficulty = EPISODE_DIFFICULTY.NORMAL;
				});
				return;
			}
			UnityAction complete = Complete;
			if (complete == null)
			{
				return;
			}
			complete();
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x0022A9D9 File Offset: 0x00228BD9
		public RectTransform GetDailyRect()
		{
			return this.m_NKCUIOPDailyMission.GetDailyRect();
		}

		// Token: 0x0400565C RID: 22108
		private NKCUIOperationEPList m_NKCUIOperationEPList;

		// Token: 0x0400565D RID: 22109
		public NKCUIOPDailyMission m_NKCUIOPDailyMission;

		// Token: 0x0400565E RID: 22110
		public NKCUIOPSupplyMission m_NKCUIOPSupplyMission;

		// Token: 0x0400565F RID: 22111
		public NKCUIOPCounterCase m_NKCUIOPCounterCase;

		// Token: 0x04005660 RID: 22112
		private NKMTrackingFloat m_fNKMTrackingFloatDecoText = new NKMTrackingFloat();

		// Token: 0x04005661 RID: 22113
		public GameObject m_NKM_UI_OPERATION_ING_WF_DIRECT_GO;

		// Token: 0x04005662 RID: 22114
		public Text m_NKM_UI_OPERATION_MENU_EP_TEXT;

		// Token: 0x04005663 RID: 22115
		public NKCUIComToggle m_MainStreamToggleBtn;

		// Token: 0x04005664 RID: 22116
		public NKCUIComToggle m_DailyToggleBtn;

		// Token: 0x04005665 RID: 22117
		public NKCUIComToggle m_SupplyToggleBtn;

		// Token: 0x04005666 RID: 22118
		public NKCUIComToggle m_SideStoryToggleBtn;

		// Token: 0x04005667 RID: 22119
		public NKCUIComToggle m_CounterSideToggleBtn;

		// Token: 0x04005668 RID: 22120
		public NKCUIComToggle m_FieldToggleBtn;

		// Token: 0x04005669 RID: 22121
		public NKCUIComToggle m_EventToggleBtn;

		// Token: 0x0400566A RID: 22122
		public NKCUIComToggle m_ChallangeToggleBtn;

		// Token: 0x0400566B RID: 22123
		public GameObject m_objCounterCaseRedDot;

		// Token: 0x0400566C RID: 22124
		[Header("이벤트 드랍 표시")]
		public GameObject[] m_objEventDropEventBadge;

		// Token: 0x0400566D RID: 22125
		private EPISODE_CATEGORY m_Now_EPISODE_CATEGORY;

		// Token: 0x0400566E RID: 22126
		private Dictionary<ContentsType, GameObject> m_dicUnlockedEffect = new Dictionary<ContentsType, GameObject>();
	}
}
