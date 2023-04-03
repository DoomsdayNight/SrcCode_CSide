using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Mode;
using ClientPacket.Warfare;
using Cs.Logging;
using NKC.UI.Component;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000987 RID: 2439
	public class NKCUIEpisodeViewer : NKCUIBase
	{
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x001FBAF7 File Offset: 0x001F9CF7
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GetEpisodeCategory(this.m_eEpisodeCategory);
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600640C RID: 25612 RVA: 0x001FBB04 File Offset: 0x001F9D04
		public override string GuideTempletID
		{
			get
			{
				switch (this.m_eEpisodeCategory)
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

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x0600640D RID: 25613 RVA: 0x001FBB61 File Offset: 0x001F9D61
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600640E RID: 25614 RVA: 0x001FBB64 File Offset: 0x001F9D64
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x001FBB68 File Offset: 0x001F9D68
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_EpisodeID > 0)
				{
					NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
					if (nkmepisodeTempletV != null && nkmepisodeTempletV.ResourceIdList != null && nkmepisodeTempletV.ResourceIdList.Count > 0)
					{
						return nkmepisodeTempletV.ResourceIdList;
					}
				}
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x001FBBB6 File Offset: 0x001F9DB6
		public void SetBGObj(GameObject go)
		{
			this.m_NUM_OPERATION = go;
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x001FBBBF File Offset: 0x001F9DBF
		public static void SetReservedDungeonPopup(string strID)
		{
			NKCUIEpisodeViewer.m_ReservedDungeonStrID = strID;
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x001FBBC7 File Offset: 0x001F9DC7
		public void SetEpisodeID(int episodeID)
		{
			if (episodeID <= 0)
			{
				return;
			}
			this.m_EpisodeID = episodeID;
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x001FBBD8 File Offset: 0x001F9DD8
		public static NKCUIEpisodeViewer InitUI()
		{
			NKCUIEpisodeViewer nkcuiepisodeViewer = NKCUIManager.OpenUI<NKCUIEpisodeViewer>("NKM_EPISODE_Panel");
			if (nkcuiepisodeViewer != null)
			{
				nkcuiepisodeViewer.m_NKCUIEPActSlotMgr.InitUI(nkcuiepisodeViewer, nkcuiepisodeViewer.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content, nkcuiepisodeViewer.m_NKM_UI_OPERATION_EP_DUNGEON_LIST, nkcuiepisodeViewer.m_NKM_UI_OPERATION_EP_DUNGEON_Content, nkcuiepisodeViewer.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect, nkcuiepisodeViewer.m_NKM_UI_OPERATION_EPISODE_MENU_Viewport, nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST, nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content, nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect, nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL, nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL, nkcuiepisodeViewer.m_lstHideWithActMenu);
				if (nkcuiepisodeViewer.gameObject)
				{
					nkcuiepisodeViewer.gameObject.SetActive(false);
				}
				nkcuiepisodeViewer.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET_PLUS.PointerClick.RemoveAllListeners();
				nkcuiepisodeViewer.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET_PLUS.PointerClick.AddListener(new UnityAction(nkcuiepisodeViewer.OnClickTicketBuyBtn));
				nkcuiepisodeViewer.m_tglDIFFICULTY_01.OnValueChanged.RemoveAllListeners();
				nkcuiepisodeViewer.m_tglDIFFICULTY_01.OnValueChanged.AddListener(new UnityAction<bool>(nkcuiepisodeViewer.OnChangeDifficultyNormal));
				nkcuiepisodeViewer.m_tglDIFFICULTY_02.OnValueChanged.RemoveAllListeners();
				nkcuiepisodeViewer.m_tglDIFFICULTY_02.OnValueChanged.AddListener(new UnityAction<bool>(nkcuiepisodeViewer.OnChangeDifficultyHard));
				NKCUIComButton component = nkcuiepisodeViewer.m_objPlayingOperation.GetComponent<NKCUIComButton>();
				if (component != null)
				{
					component.PointerClick.RemoveAllListeners();
					component.PointerClick.AddListener(new UnityAction(nkcuiepisodeViewer.OnClickINGWarfareDirectGoBtn));
				}
				nkcuiepisodeViewer.m_btnShop.PointerClick.RemoveAllListeners();
				nkcuiepisodeViewer.m_btnShop.PointerClick.AddListener(new UnityAction(nkcuiepisodeViewer.OnClickShop));
				nkcuiepisodeViewer.m_btnChallengeShop.PointerClick.RemoveAllListeners();
				nkcuiepisodeViewer.m_btnChallengeShop.PointerClick.AddListener(new UnityAction(nkcuiepisodeViewer.OnClickShop));
				nkcuiepisodeViewer.m_btnEventShortCut.PointerClick.RemoveAllListeners();
				nkcuiepisodeViewer.m_btnEventShortCut.PointerClick.AddListener(new UnityAction(nkcuiepisodeViewer.OnClickEventShortCut));
				if (nkcuiepisodeViewer.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect != null && !nkcuiepisodeViewer.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect.vertical)
				{
					NKCUtil.SetScrollHotKey(nkcuiepisodeViewer.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect, null);
				}
				if (nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect != null && !nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.vertical)
				{
					NKCUtil.SetScrollHotKey(nkcuiepisodeViewer.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect, null);
				}
			}
			return nkcuiepisodeViewer;
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x001FBDF1 File Offset: 0x001F9FF1
		public void ResetUIByCurrentSetting(NKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK)
		{
			this.SetUIByEPTemplet();
			if (this.m_NKCUIEPActSlotMgr != null)
			{
				this.m_NKCUIEPActSlotMgr.ReOpenDungeonListByCurrentSetting();
			}
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x001FBE0C File Offset: 0x001FA00C
		public void Open(bool bFirstOpen, EPISODE_DIFFICULTY diff)
		{
			NKCUIFadeInOut.FadeIn(0.1f, null, false);
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			this.m_eEpisodeCategory = nkmepisodeTempletV.m_EPCategory;
			this.m_Difficulty = diff;
			if (this.m_eEpisodeCategory == EPISODE_CATEGORY.EC_DAILY || this.m_eEpisodeCategory == EPISODE_CATEGORY.EC_SUPPLY)
			{
				this.m_NKCUIEPActSlotMgr.SetEPSlot(true);
			}
			else
			{
				this.m_NKCUIEPActSlotMgr.SetEPSlot(false);
			}
			if (this.m_eEpisodeCategory == EPISODE_CATEGORY.EC_EVENT)
			{
				if (nkmepisodeTempletV != null)
				{
					NKCUtil.SetGameobjectActive(this.m_objMenuParent, !nkmepisodeTempletV.m_bHideActTab);
					this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.horizontal = (nkmepisodeTempletV.m_ScrollType == EPISODE_SCROLL_TYPE.HORIZONTAL || nkmepisodeTempletV.m_ScrollType == EPISODE_SCROLL_TYPE.FREE);
					this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.vertical = (nkmepisodeTempletV.m_ScrollType == EPISODE_SCROLL_TYPE.VERTICAL || nkmepisodeTempletV.m_ScrollType == EPISODE_SCROLL_TYPE.FREE);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objMenuParent, true);
					NKCUtil.SetGameobjectActive(this.m_btnEventShortCut, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objMenuParent, true);
				NKCUtil.SetGameobjectActive(this.m_btnEventShortCut, false);
			}
			if (!this.IsOpenDifficulty(this.m_Difficulty))
			{
				this.ShowDifficultyMesssage(this.m_Difficulty);
				this.m_Difficulty = EPISODE_DIFFICULTY.NORMAL;
				NKCScenManager.GetScenManager().Get_SCEN_EPISODE().Difficulty = EPISODE_DIFFICULTY.NORMAL;
			}
			this.SetUIByEPTemplet();
			if (bFirstOpen)
			{
				this.m_NKCUIEPActSlotMgr.OpenLatestAct();
			}
			else
			{
				this.m_NKCUIEPActSlotMgr.ReOpenDungeonListByCurrentSetting();
			}
			base.UIOpened(true);
			if (this.m_eEpisodeCategory == EPISODE_CATEGORY.EC_SUPPLY || this.m_eEpisodeCategory == EPISODE_CATEGORY.EC_DAILY)
			{
				NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetReservedActID(-1);
			}
			if (NKCScenManager.GetScenManager().Get_SCEN_EPISODE().GetReservedActID() != -1)
			{
				this.m_NKCUIEPActSlotMgr.SelectActSlot(NKCScenManager.GetScenManager().Get_SCEN_EPISODE().GetReservedActID());
			}
			if (NKCUIEpisodeViewer.m_ReservedDungeonStrID != "")
			{
				NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(NKCUIEpisodeViewer.m_ReservedDungeonStrID);
				if (nkmstageTempletV != null)
				{
					if (nkmstageTempletV.EpisodeTemplet != null && !nkmstageTempletV.EpisodeTemplet.IsOpen)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					NKCPopupDungeonInfo.Instance.Open(nkmstageTempletV, new NKCPopupDungeonInfo.OnButton(this.m_NKCUIEPActSlotMgr.GetNKCUIEPDungeonSlotMgr().OnClickDungeonAttackReady), false);
				}
			}
			NKCUIEpisodeViewer.m_ReservedDungeonStrID = "";
			this.UpdateINGWarfareDirectGoUI();
			base.StartCoroutine(this.AutoMoveAct());
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x001FC075 File Offset: 0x001FA275
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_NUM_OPERATION, false);
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x001FC089 File Offset: 0x001FA289
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_NUM_OPERATION, true);
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x001FC0A0 File Offset: 0x001FA2A0
		public void UpdateTicketCountUI()
		{
			if (!this.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET.activeSelf)
			{
				return;
			}
			this.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET_TEXT.text = "0";
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			if (inventoryData != null)
			{
				int dailyMissionTicketID = NKMEpisodeMgr.GetDailyMissionTicketID(this.m_EpisodeID);
				NKCUtil.SetLabelText(this.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET_TEXT, inventoryData.GetCountMiscItem(dailyMissionTicketID).ToString());
			}
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x001FC104 File Offset: 0x001FA304
		public void SetUpperUI()
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			string assetName = "NKM_UI_OPERATION_TITLE_BG_MAINSTREAM";
			this.m_imgEpisode.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_TITLE_BG", assetName, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EVENT_BG, !string.IsNullOrEmpty(nkmepisodeTempletV.m_EPThumbnail));
			if (!string.IsNullOrEmpty(nkmepisodeTempletV.m_EPThumbnail))
			{
				this.m_NKM_UI_OPERATION_EVENT_BG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_BG", nkmepisodeTempletV.m_EPThumbnail, false);
			}
			this.m_lbEpisodeName.text = NKCUtilString.GetEpisodeName(nkmepisodeTempletV.m_EPCategory, nkmepisodeTempletV.GetEpisodeName(), nkmepisodeTempletV.GetEpisodeTitle());
			if (nkmepisodeTempletV.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_DIFFICULTY, false);
				this.UpdateTicketCountUI();
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET, false);
				this.SetDifficultyUI(nkmepisodeTempletV);
			}
			NKCUtil.SetGameobjectActive(this.m_objEventBadge, NKMEpisodeMgr.CheckEpisodeHasEventDrop(nkmepisodeTempletV));
			base.UpdateUpsideMenu();
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x001FC1FB File Offset: 0x001FA3FB
		private void SetUIByEPTemplet()
		{
			this.SetUpperUI();
			this.m_NKCUIEPActSlotMgr.ResetUIByData(this.m_EpisodeID, this.m_Difficulty);
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x001FC21A File Offset: 0x001FA41A
		public void SetEnableTitleBG(bool bSet)
		{
			this.m_imgEpisode.enabled = bSet;
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x001FC228 File Offset: 0x001FA428
		private IEnumerator AutoMoveAct()
		{
			if (NKCUIEpisodeViewer.m_autoMoveTarget != null)
			{
				while (NKCUIManager.IsAnyPopupOpened())
				{
					yield return null;
				}
				yield return new WaitForSeconds(1.5f);
				NKCUIEpisodeViewer.m_autoMoveTarget.GetComponent<NKCUIComToggle>().Select(true, false, false);
				NKCUIEpisodeViewer.m_autoMoveTarget = null;
			}
			NKCContentManager.ShowContentUnlockPopup(delegate
			{
				this.TutorialCheck_Open();
			}, Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
			yield break;
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x001FC238 File Offset: 0x001FA438
		public void OnClickTicketBuyBtn()
		{
			int dailyMissionTicketShopID = NKCShopManager.GetDailyMissionTicketShopID(this.m_EpisodeID);
			if (NKCShopManager.GetBuyCountLeft(dailyMissionTicketShopID) > 0)
			{
				NKCShopManager.OnBtnProductBuy(ShopItemTemplet.Find(dailyMissionTicketShopID).Key, false);
				return;
			}
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x001FC280 File Offset: 0x001FA480
		private void OnClickEventShortCut()
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(nkmepisodeTempletV.m_ButtonShortCutType, nkmepisodeTempletV.m_ButtonShortCutParam, false);
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x001FC2B5 File Offset: 0x001FA4B5
		private void OnClickShop()
		{
			NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x001FC2CC File Offset: 0x001FA4CC
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

		// Token: 0x06006421 RID: 25633 RVA: 0x001FC37C File Offset: 0x001FA57C
		public void UpdateINGWarfareDirectGoUI()
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKCUtil.SetGameobjectActive(this.m_objPlayingOperation, true);
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet != null)
				{
					NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
					if (nkmstageTempletV != null)
					{
						NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
						if (episodeTemplet != null)
						{
							this.m_lbPlayingOperationName.text = NKCUtilString.GetPlayingWarfare(episodeTemplet.GetEpisodeTitle(), nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum);
							return;
						}
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objPlayingOperation, false);
			}
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x001FC40B File Offset: 0x001FA60B
		public void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK cNKMPacket_WARFARE_GAME_GIVE_UP_ACK)
		{
			this.m_NKCUIEPActSlotMgr.OnRecv(cNKMPacket_WARFARE_GAME_GIVE_UP_ACK);
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x001FC419 File Offset: 0x001FA619
		public void OnRecv(NKMPacket_STAGE_UNLOCK_ACK sPacket)
		{
			this.m_NKCUIEPActSlotMgr.OnRecv(sPacket);
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x001FC428 File Offset: 0x001FA628
		private void OnChangeDifficultyNormal(bool value)
		{
			if (!value)
			{
				return;
			}
			EPISODE_DIFFICULTY episode_DIFFICULTY = EPISODE_DIFFICULTY.NORMAL;
			if (this.m_Difficulty == episode_DIFFICULTY)
			{
				return;
			}
			this.OnChangeDifficulty(episode_DIFFICULTY);
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x001FC44C File Offset: 0x001FA64C
		private void OnChangeDifficultyHard(bool value)
		{
			EPISODE_DIFFICULTY episode_DIFFICULTY = EPISODE_DIFFICULTY.HARD;
			if (!value)
			{
				this.ShowDifficultyMesssage(episode_DIFFICULTY);
				return;
			}
			if (this.m_Difficulty == episode_DIFFICULTY)
			{
				return;
			}
			this.OnChangeDifficulty(episode_DIFFICULTY);
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x001FC477 File Offset: 0x001FA677
		private void OnChangeDifficulty(EPISODE_DIFFICULTY difficulty)
		{
			this.m_Difficulty = difficulty;
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().Difficulty = difficulty;
			this.SetUIByEPTemplet();
			this.m_NKCUIEPActSlotMgr.ResetSelectedAct();
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x001FC4A4 File Offset: 0x001FA6A4
		private void ShowDifficultyMesssage(EPISODE_DIFFICULTY difficulty)
		{
			if (this.IsOpenDifficulty(difficulty))
			{
				return;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
			if (nkmepisodeTempletV != null && nkmepisodeTempletV.GetFirstStage(1) != null)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCContentManager.MakeUnlockConditionString(nkmepisodeTempletV.GetFirstStage(1).m_UnlockInfo, false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x001FC4FC File Offset: 0x001FA6FC
		private bool IsOpenDifficulty(EPISODE_DIFFICULTY difficulty)
		{
			if (difficulty == EPISODE_DIFFICULTY.NORMAL)
			{
				return true;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
			return NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.GetScenManager().GetMyUserData(), nkmepisodeTempletV.m_EpisodeID, difficulty);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x001FC538 File Offset: 0x001FA738
		private void SetDifficultyUI(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (NKMEpisodeTempletV2.Find(cNKMEpisodeTemplet.m_EpisodeID, EPISODE_DIFFICULTY.HARD) == null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_DIFFICULTY, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_DIFFICULTY, true);
			this.m_tglDIFFICULTY_01.Select(this.m_Difficulty == EPISODE_DIFFICULTY.NORMAL, true, false);
			this.m_tglDIFFICULTY_02.Select(this.m_Difficulty == EPISODE_DIFFICULTY.HARD, true, false);
			bool flag = this.IsOpenDifficulty(EPISODE_DIFFICULTY.HARD);
			NKCUtil.SetGameobjectActive(this.m_objDIFFICULTY_02_LOCK, !flag);
			if (flag)
			{
				this.m_tglDIFFICULTY_02.UnLock(false);
				return;
			}
			this.m_tglDIFFICULTY_02.Lock(false);
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x001FC5CD File Offset: 0x001FA7CD
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x001FC5DC File Offset: 0x001FA7DC
		public override void CloseInternal()
		{
			base.StopAllCoroutines();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.m_NKCUIEPActSlotMgr.Close();
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x001FC608 File Offset: 0x001FA808
		public void PreLoad()
		{
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_EP_LIST_REWARD", true);
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_EP_LIST_REWARD_GET", true);
			NKCResourceUtility.PreloadUnitInvenIconEmpty();
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_ITEM_MISC", "AB_INVEN_ICON_ITEM_MISC_RESOURCE_CREDIT", true);
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_ITEM_MISC", "AB_INVEN_ICON_ITEM_MISC_RESOURCE_ETERNIUM", true);
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_ITEM_MISC", "AB_INVEN_ICON_ITEM_MISC_RESOURCE_EXP", true);
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				for (int i = 0; i < Enum.GetValues(typeof(EPISODE_DIFFICULTY)).Length; i++)
				{
					this.LoadRewardResource(NKMEpisodeTempletV2.Find(nkmepisodeTempletV.m_EpisodeID, (EPISODE_DIFFICULTY)i));
				}
			}
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x001FC6AC File Offset: 0x001FA8AC
		private void LoadRewardResource(NKMEpisodeTempletV2 episodeTemplet)
		{
			if (episodeTemplet == null)
			{
				return;
			}
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in episodeTemplet.m_DicStage)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(keyValuePair.Value[i].m_StageBattleStrID);
					if (dungeonTempletBase != null)
					{
						if (!string.IsNullOrEmpty(dungeonTempletBase.m_DungeonIcon))
						{
							NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_" + dungeonTempletBase.m_DungeonIcon, true);
						}
						FirstRewardData firstRewardData = dungeonTempletBase.GetFirstRewardData();
						switch (firstRewardData.Type)
						{
						case NKM_REWARD_TYPE.RT_UNIT:
						case NKM_REWARD_TYPE.RT_SHIP:
						case NKM_REWARD_TYPE.RT_OPERATOR:
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(firstRewardData.RewardId);
							if (unitTempletBase != null)
							{
								NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase, true);
							}
							break;
						}
						case NKM_REWARD_TYPE.RT_MISC:
						case NKM_REWARD_TYPE.RT_MISSION_POINT:
						{
							NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(firstRewardData.RewardId);
							if (itemMiscTempletByID != null)
							{
								NKCResourceUtility.LoadAssetResourceTemp<Sprite>(NKMAssetName.ParseBundleName(NKCResourceUtility.GetMiscItemIconBundleName(itemMiscTempletByID), itemMiscTempletByID.m_ItemMiscIconName, "AB_INVEN_"), true);
							}
							break;
						}
						case NKM_REWARD_TYPE.RT_USER_EXP:
						{
							NKMItemMiscTemplet itemMiscTempletByRewardType = NKMItemManager.GetItemMiscTempletByRewardType(firstRewardData.Type);
							if (itemMiscTempletByRewardType != null)
							{
								NKCResourceUtility.LoadAssetResourceTemp<Sprite>(NKMAssetName.ParseBundleName("AB_INVEN_ICON_ITEM_MISC", itemMiscTempletByRewardType.m_ItemMiscIconName, "AB_INVEN_"), true);
							}
							break;
						}
						case NKM_REWARD_TYPE.RT_SKIN:
						{
							NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(firstRewardData.RewardId);
							NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet, true);
							break;
						}
						}
					}
				}
			}
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x001FC860 File Offset: 0x001FAA60
		private void TutorialCheck_Open()
		{
			EPISODE_CATEGORY eEpisodeCategory = this.m_eEpisodeCategory;
			if (eEpisodeCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.Episode, true);
				return;
			}
			if (eEpisodeCategory == EPISODE_CATEGORY.EC_DAILY)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.DailyEpisode, true);
				return;
			}
			if (eEpisodeCategory != EPISODE_CATEGORY.EC_SUPPLY)
			{
				return;
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.SupplyGuide, true);
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x001FC89C File Offset: 0x001FAA9C
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			int actID;
			if (!string.IsNullOrEmpty(eventTemplet.StringValue) && int.TryParse(eventTemplet.StringValue, out actID))
			{
				this.m_NKCUIEPActSlotMgr.SelectActSlot(actID);
			}
			else
			{
				this.m_NKCUIEPActSlotMgr.SelectActSlot(1);
			}
			this.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect.normalizedPosition = Vector2.zero;
			NKCUIEPActDungeonSlot slot = this.m_NKCUIEPActSlotMgr.GetNKCUIEPDungeonSlotMgr().GetSlotByStageIndex(eventTemplet.Value);
			if (!(slot == null))
			{
				NKCGameEventManager.OpenTutorialGuideBySettedFace(slot.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
					slot.InvokeSelectSlot();
					UnityAction complete2 = Complete;
					if (complete2 == null)
					{
						return;
					}
					complete2();
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

		// Token: 0x06006430 RID: 25648 RVA: 0x001FC964 File Offset: 0x001FAB64
		public RectTransform GetStageSlotRect(int stageIndex)
		{
			NKCUIEPActDungeonSlot slotByStageIndex = this.m_NKCUIEPActSlotMgr.GetNKCUIEPDungeonSlotMgr().GetSlotByStageIndex(stageIndex);
			if (slotByStageIndex == null)
			{
				return null;
			}
			this.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect.verticalNormalizedPosition = 1f;
			return slotByStageIndex.GetComponent<RectTransform>();
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x001FC9A4 File Offset: 0x001FABA4
		public RectTransform GetActSlotRect(int actIndex)
		{
			NKCUIEpisodeActSlot slotByActIndex = this.m_NKCUIEPActSlotMgr.GetSlotByActIndex(actIndex);
			if (slotByActIndex == null)
			{
				return null;
			}
			this.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect.verticalNormalizedPosition = 1f;
			return slotByActIndex.GetComponent<RectTransform>();
		}

		// Token: 0x04004FAD RID: 20397
		private int m_EpisodeID;

		// Token: 0x04004FAE RID: 20398
		private NKCUIEpisodeViewer.NKCUIEPActSlotMgr m_NKCUIEPActSlotMgr = new NKCUIEpisodeViewer.NKCUIEPActSlotMgr();

		// Token: 0x04004FAF RID: 20399
		public GameObject m_objEPOpenEffect;

		// Token: 0x04004FB0 RID: 20400
		public GameObject m_objPlayingOperation;

		// Token: 0x04004FB1 RID: 20401
		public Text m_lbPlayingOperationName;

		// Token: 0x04004FB2 RID: 20402
		public NKCUIComStateButton m_btnShop;

		// Token: 0x04004FB3 RID: 20403
		public NKCUIComStateButton m_btnChallengeShop;

		// Token: 0x04004FB4 RID: 20404
		public Text m_lbEpisodeName;

		// Token: 0x04004FB5 RID: 20405
		public Image m_imgEpisode;

		// Token: 0x04004FB6 RID: 20406
		public GameObject m_objEventBadge;

		// Token: 0x04004FB7 RID: 20407
		public GameObject m_objMenuParent;

		// Token: 0x04004FB8 RID: 20408
		public GameObject m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content;

		// Token: 0x04004FB9 RID: 20409
		public GameObject m_NKM_UI_OPERATION_EP_DUNGEON_LIST;

		// Token: 0x04004FBA RID: 20410
		public GameObject m_NKM_UI_OPERATION_EP_DUNGEON_Content;

		// Token: 0x04004FBB RID: 20411
		public ScrollRect m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect;

		// Token: 0x04004FBC RID: 20412
		public RectTransform m_NKM_UI_OPERATION_EPISODE_MENU_Viewport;

		// Token: 0x04004FBD RID: 20413
		[Header("스테이지뷰어")]
		public GameObject m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST;

		// Token: 0x04004FBE RID: 20414
		public GameObject m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content;

		// Token: 0x04004FBF RID: 20415
		public ScrollRect m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect;

		// Token: 0x04004FC0 RID: 20416
		public GameObject m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL;

		// Token: 0x04004FC1 RID: 20417
		public ScrollRect m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL;

		// Token: 0x04004FC2 RID: 20418
		public Image m_NKM_UI_OPERATION_EVENT_BG;

		// Token: 0x04004FC3 RID: 20419
		[Header("좌측 액트탭 안보일 때 꺼줄 오브젝트")]
		public List<GameObject> m_lstHideWithActMenu = new List<GameObject>();

		// Token: 0x04004FC4 RID: 20420
		[Header("이벤트 미션")]
		public NKCUIComStateButton m_btnEventShortCut;

		// Token: 0x04004FC5 RID: 20421
		public Image m_imgEventShortCut;

		// Token: 0x04004FC6 RID: 20422
		public Text m_lbEventShortCut;

		// Token: 0x04004FC7 RID: 20423
		[Header("티켓")]
		public GameObject m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET;

		// Token: 0x04004FC8 RID: 20424
		public Text m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET_TEXT;

		// Token: 0x04004FC9 RID: 20425
		public NKCUIComButton m_NKM_UI_OPERATION_DAILYMISSION_TRAINING_TICKET_PLUS;

		// Token: 0x04004FCA RID: 20426
		[Header("난이도")]
		public GameObject m_NKM_UI_OPERATION_DIFFICULTY;

		// Token: 0x04004FCB RID: 20427
		public NKCUIComToggle m_tglDIFFICULTY_01;

		// Token: 0x04004FCC RID: 20428
		public NKCUIComToggle m_tglDIFFICULTY_02;

		// Token: 0x04004FCD RID: 20429
		public GameObject m_objDIFFICULTY_02_LOCK;

		// Token: 0x04004FCE RID: 20430
		private static string m_ReservedDungeonStrID = "";

		// Token: 0x04004FCF RID: 20431
		private EPISODE_CATEGORY m_eEpisodeCategory;

		// Token: 0x04004FD0 RID: 20432
		private GameObject m_NUM_OPERATION;

		// Token: 0x04004FD1 RID: 20433
		private EPISODE_DIFFICULTY m_Difficulty;

		// Token: 0x04004FD2 RID: 20434
		private static NKCUIEpisodeActSlot m_autoMoveTarget = null;

		// Token: 0x0200163A RID: 5690
		public class NKCUIEPDungeonSlotMgr
		{
			// Token: 0x17001902 RID: 6402
			// (get) Token: 0x0600AF8C RID: 44940 RVA: 0x0035C5CA File Offset: 0x0035A7CA
			public List<NKCUIEPActDungeonSlot> ListItemSlot
			{
				get
				{
					return this.m_listItemSlot;
				}
			}

			// Token: 0x0600AF8E RID: 44942 RVA: 0x0035C5ED File Offset: 0x0035A7ED
			public int GetActID()
			{
				return NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID;
			}

			// Token: 0x0600AF8F RID: 44943 RVA: 0x0035C5F4 File Offset: 0x0035A7F4
			public void InitUI(GameObject _NKM_UI_OPERATION_EP_DUNGEON_LIST, GameObject _NKM_UI_OPERATION_EP_DUNGEON_Content, ScrollRect _NKM_UI_OPERATION_EP_DUNGEON_ScrollRect, RectTransform _NKM_UI_OPERATION_EPISODE_MENU_Viewport)
			{
				this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST = _NKM_UI_OPERATION_EP_DUNGEON_LIST;
				this.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect = _NKM_UI_OPERATION_EP_DUNGEON_ScrollRect;
				this.m_NKM_UI_OPERATION_EPISODE_MENU_Viewport = _NKM_UI_OPERATION_EPISODE_MENU_Viewport;
				if (this.m_NKM_UI_OPERATION_EP_DUNGEON_Content == null)
				{
					this.m_NKM_UI_OPERATION_EP_DUNGEON_Content = _NKM_UI_OPERATION_EP_DUNGEON_Content;
					this.m_rectListContent = this.m_NKM_UI_OPERATION_EP_DUNGEON_Content.GetComponent<RectTransform>();
					this.m_ContentOrgPosY = this.m_rectListContent.anchoredPosition.y;
					for (int i = 0; i < 10; i++)
					{
						NKCUIEPActDungeonSlot newInstance = NKCUIEPActDungeonSlot.GetNewInstance(this.m_NKM_UI_OPERATION_EP_DUNGEON_Content.transform, new IDungeonSlot.OnSelectedItemSlot(this.OnSelectedActSlot));
						this.InitSlot(newInstance);
						this.m_listItemSlot.Add(newInstance);
					}
				}
				if (this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.activeSelf)
				{
					this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.SetActive(false);
				}
			}

			// Token: 0x0600AF90 RID: 44944 RVA: 0x0035C6AB File Offset: 0x0035A8AB
			public void Open(int _EpisodeID, int actID, EPISODE_DIFFICULTY difficulty)
			{
				NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_EpisodeID = _EpisodeID;
				NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID = actID;
				NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_Difficulty = difficulty;
				if (!this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.activeSelf)
				{
					this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.SetActive(true);
				}
				this.ResetUIByData();
			}

			// Token: 0x0600AF91 RID: 44945 RVA: 0x0035C6DE File Offset: 0x0035A8DE
			public void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK cNKMPacket_WARFARE_GAME_GIVE_UP_ACK)
			{
				this.ReserveEPExtraDataOpen();
			}

			// Token: 0x0600AF92 RID: 44946 RVA: 0x0035C6E6 File Offset: 0x0035A8E6
			public void OnRecv(NKMPacket_STAGE_UNLOCK_ACK sPacket)
			{
				this.ResetUIByData();
			}

			// Token: 0x0600AF93 RID: 44947 RVA: 0x0035C6F0 File Offset: 0x0035A8F0
			private void ReserveEPExtraDataOpen()
			{
				if (this.m_Reserved_EPExtraData != null && this.m_Reserved_EPExtraData.EpisodeCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
				{
					NKCUIOperationIntro.Instance.Open(this.m_Reserved_EPExtraData, delegate
					{
						NKCScenManager.GetScenManager().ScenChangeFade(this.m_Reserved_NKM_SCEN_ID_After_WarfareGiveup, true);
					});
					return;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(this.m_Reserved_NKM_SCEN_ID_After_WarfareGiveup, true);
			}

			// Token: 0x0600AF94 RID: 44948 RVA: 0x0035C740 File Offset: 0x0035A940
			public void ReOpenByCurrentSetting()
			{
				if (!this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.activeSelf)
				{
					this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.SetActive(true);
				}
				this.ResetUIByData();
			}

			// Token: 0x0600AF95 RID: 44949 RVA: 0x0035C761 File Offset: 0x0035A961
			public void CloseOperationIntro()
			{
				NKCUIOperationIntro.CheckInstanceAndClose();
			}

			// Token: 0x0600AF96 RID: 44950 RVA: 0x0035C768 File Offset: 0x0035A968
			public void Close()
			{
				if (this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.activeSelf)
				{
					this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST.SetActive(false);
				}
				NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID = 0;
				this.CloseOperationIntro();
			}

			// Token: 0x0600AF97 RID: 44951 RVA: 0x0035C790 File Offset: 0x0035A990
			public void Clear()
			{
				for (int i = 0; i < this.m_listItemSlot.Count; i++)
				{
					this.m_listItemSlot[i].Close();
				}
				this.m_listItemSlot.Clear();
			}

			// Token: 0x0600AF98 RID: 44952 RVA: 0x0035C7CF File Offset: 0x0035A9CF
			private void InitSlot(NKCUIEPActDungeonSlot cItemSlot)
			{
				if (cItemSlot != null)
				{
					cItemSlot.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
				}
			}

			// Token: 0x0600AF99 RID: 44953 RVA: 0x0035C7F0 File Offset: 0x0035A9F0
			public void OnSelectedActSlot(int dunIndex, string dungeonStrID, bool isPlaying)
			{
				NKMStageTempletV2 stageTemplet = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonStrID);
				if (stageTemplet != null)
				{
					if (stageTemplet.NeedToUnlock && !NKMEpisodeMgr.GetUnlockedStageIds().Contains(stageTemplet.Key))
					{
						long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(stageTemplet.UnlockReqItem.ItemId);
						if (countMiscItem >= (long)stageTemplet.UnlockReqItem.Count32)
						{
							NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_UNLOCK, NKCStringTable.GetString("SI_PF_SIDESTORY_UNLOCK_TEXT", false), stageTemplet.UnlockReqItem.ItemId, stageTemplet.UnlockReqItem.Count32, countMiscItem, delegate()
							{
								NKCPacketSender.Send_NKMPacket_STAGE_UNLOCK_REQ(stageTemplet.Key);
							}, null, false);
							return;
						}
						NKCShopManager.OpenItemLackPopup(stageTemplet.UnlockReqItem.ItemId, stageTemplet.UnlockReqItem.Count32);
						return;
					}
					else
					{
						switch (stageTemplet.m_STAGE_TYPE)
						{
						case STAGE_TYPE.ST_WARFARE:
							if (NKMWarfareTemplet.Find(dungeonStrID) == null)
							{
								return;
							}
							break;
						case STAGE_TYPE.ST_DUNGEON:
							if (NKMDungeonManager.GetDungeonTempletBase(dungeonStrID) == null)
							{
								return;
							}
							break;
						case STAGE_TYPE.ST_PHASE:
							if (NKMPhaseTemplet.Find(dungeonStrID) == null)
							{
								return;
							}
							break;
						}
						NKMEpisodeTempletV2 episodeTemplet = stageTemplet.EpisodeTemplet;
						if (episodeTemplet == null)
						{
							return;
						}
						if (!episodeTemplet.IsOpen)
						{
							NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
							}, "");
							return;
						}
						if (!stageTemplet.IsOpenedDayOfWeek())
						{
							NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
							}, "");
							return;
						}
						NKCPopupDungeonInfo.Instance.Open(stageTemplet, new NKCPopupDungeonInfo.OnButton(this.OnClickDungeonAttackReady), isPlaying);
					}
				}
			}

			// Token: 0x0600AF9A RID: 44954 RVA: 0x0035C9D0 File Offset: 0x0035ABD0
			private void OnClickOkGiveUpINGWarfare()
			{
				NKMPacket_WARFARE_GAME_GIVE_UP_REQ packet = new NKMPacket_WARFARE_GAME_GIVE_UP_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}

			// Token: 0x0600AF9B RID: 44955 RVA: 0x0035C9F6 File Offset: 0x0035ABF6
			private void AlarmWarfareGameIsIng()
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EPISODE_GIVE_UP_WARFARE, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGWarfare), null, false);
			}

			// Token: 0x0600AF9C RID: 44956 RVA: 0x0035CA15 File Offset: 0x0035AC15
			private NKM_SCEN_ID Get_Next_NKM_SCEN_ID_By_DT(NKM_DUNGEON_TYPE eNKM_DUNGEON_TYPE)
			{
				if (eNKM_DUNGEON_TYPE == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
				{
					return NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON;
				}
				return NKM_SCEN_ID.NSI_DUNGEON_ATK_READY;
			}

			// Token: 0x0600AF9D RID: 44957 RVA: 0x0035CA20 File Offset: 0x0035AC20
			public void OnClickDungeonAttackReady(NKMStageTempletV2 stageTemplet)
			{
				if (stageTemplet == null)
				{
					return;
				}
				if (stageTemplet != null)
				{
					NKMEpisodeTempletV2 episodeTemplet = stageTemplet.EpisodeTemplet;
					if (episodeTemplet == null)
					{
						return;
					}
					if (!episodeTemplet.IsOpen)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(myUserData);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
						return;
					}
					if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
					{
						bool flag = true;
						if (stageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_WARFARE)
						{
							WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
							if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP && warfareGameData.warfareTempletID == stageTemplet.WarfareTemplet.Key)
							{
								flag = false;
							}
						}
						if (flag && (long)stageTemplet.m_StageReqItemCount - myUserData.m_InventoryData.GetCountMiscItem(stageTemplet.m_StageReqItemID) > 0L)
						{
							int dailyMissionTicketShopID = NKCShopManager.GetDailyMissionTicketShopID(NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_EpisodeID);
							if (NKCShopManager.GetBuyCountLeft(dailyMissionTicketShopID) > 0)
							{
								NKCShopManager.OnBtnProductBuy(ShopItemTemplet.Find(dailyMissionTicketShopID).Key, false);
								return;
							}
							NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
							return;
						}
					}
					switch (stageTemplet.m_STAGE_TYPE)
					{
					case STAGE_TYPE.ST_WARFARE:
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetWarfareStrID(stageTemplet.m_StageBattleStrID);
						if (NKCScenManager.GetScenManager().WarfareGameData != null && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP && NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID != NKCWarfareManager.GetWarfareID(stageTemplet.m_StageBattleStrID))
						{
							this.m_Reserved_EPExtraData = stageTemplet;
							this.m_Reserved_NKM_SCEN_ID_After_WarfareGiveup = NKM_SCEN_ID.NSI_WARFARE_GAME;
							this.AlarmWarfareGameIsIng();
							return;
						}
						if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
						{
							NKCUIOperationIntro.Instance.Open(stageTemplet, delegate
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
							});
							return;
						}
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
						return;
					case STAGE_TYPE.ST_DUNGEON:
					{
						NKMDungeonTempletBase cNKMDungeonTempletBase = stageTemplet.DungeonTempletBase;
						if (cNKMDungeonTempletBase == null)
						{
							return;
						}
						NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(stageTemplet, DeckContents.NORMAL);
						NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedDungeonType(cNKMDungeonTempletBase.m_DungeonID);
						EPISODE_CATEGORY epcategory = episodeTemplet.m_EPCategory;
						if (epcategory - EPISODE_CATEGORY.EC_DAILY > 1 && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
						{
							this.m_Reserved_EPExtraData = stageTemplet;
							this.m_Reserved_NKM_SCEN_ID_After_WarfareGiveup = this.Get_Next_NKM_SCEN_ID_By_DT(cNKMDungeonTempletBase.m_DungeonType);
							this.AlarmWarfareGameIsIng();
							return;
						}
						if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
						{
							NKCUIOperationIntro.Instance.Open(stageTemplet, delegate
							{
								NKCScenManager.GetScenManager().ScenChangeFade(this.Get_Next_NKM_SCEN_ID_By_DT(cNKMDungeonTempletBase.m_DungeonType), true);
							});
							return;
						}
						NKCScenManager.GetScenManager().ScenChangeFade(this.Get_Next_NKM_SCEN_ID_By_DT(cNKMDungeonTempletBase.m_DungeonType), true);
						return;
					}
					case STAGE_TYPE.ST_PHASE:
					{
						NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(stageTemplet, DeckContents.PHASE);
						if (stageTemplet.PhaseTemplet == null)
						{
							return;
						}
						EPISODE_CATEGORY epcategory = episodeTemplet.m_EPCategory;
						if (epcategory - EPISODE_CATEGORY.EC_DAILY > 1 && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
						{
							this.m_Reserved_EPExtraData = stageTemplet;
							this.m_Reserved_NKM_SCEN_ID_After_WarfareGiveup = NKM_SCEN_ID.NSI_DUNGEON_ATK_READY;
							this.AlarmWarfareGameIsIng();
							return;
						}
						if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
						{
							NKCUIOperationIntro.Instance.Open(stageTemplet, delegate
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
							});
							return;
						}
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
						break;
					}
					default:
						return;
					}
				}
			}

			// Token: 0x0600AF9E RID: 44958 RVA: 0x0035CD60 File Offset: 0x0035AF60
			public void ResetUIByData()
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_EpisodeID, NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_Difficulty);
				if (nkmepisodeTempletV == null)
				{
					return;
				}
				if (!nkmepisodeTempletV.m_DicStage.ContainsKey(NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID))
				{
					Log.Info(string.Format("List<EPExtraData> 찾을 수 없음 - ActID {0}, Difficulty {1}", NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID, NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_Difficulty), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIEpisodeViewer.cs", 537);
					return;
				}
				int count = nkmepisodeTempletV.m_DicStage[NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID].Count;
				if (this.m_listItemSlot.Count < count)
				{
					int count2 = this.m_listItemSlot.Count;
					for (int i = 0; i < count - count2; i++)
					{
						NKCUIEPActDungeonSlot newInstance = NKCUIEPActDungeonSlot.GetNewInstance(this.m_NKM_UI_OPERATION_EP_DUNGEON_Content.transform, new IDungeonSlot.OnSelectedItemSlot(this.OnSelectedActSlot));
						this.InitSlot(newInstance);
						this.m_listItemSlot.Add(newInstance);
					}
				}
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				int num = 0;
				int num2 = 0;
				bool flag = false;
				float num3 = 0f;
				for (int j = 0; j < this.m_listItemSlot.Count; j++)
				{
					NKCUIEPActDungeonSlot nkcuiepactDungeonSlot = this.m_listItemSlot[j];
					nkcuiepactDungeonSlot.SetEnableNewMark(false);
					if (j < count)
					{
						NKMStageTempletV2 nkmstageTempletV = nkmepisodeTempletV.m_DicStage[NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID][j];
						bool flag2 = NKMEpisodeMgr.CheckEpisodeMission(myUserData, nkmstageTempletV);
						if (!nkmstageTempletV.EnableByTag)
						{
							nkcuiepactDungeonSlot.SetActive(false);
						}
						else if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_OPEN)
						{
							if (flag2)
							{
								num++;
								num2++;
								nkcuiepactDungeonSlot.SetData(NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID, nkmstageTempletV.m_StageIndex, nkmstageTempletV.m_StageBattleStrID, false, NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_Difficulty);
								if (!PlayerPrefs.HasKey(string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !myUserData.CheckStageCleared(nkmstageTempletV))
								{
									nkcuiepactDungeonSlot.SetEnableNewMark(true);
								}
								else
								{
									nkcuiepactDungeonSlot.SetEnableNewMark(false);
								}
								if (!nkcuiepactDungeonSlot.IsActive())
								{
									nkcuiepactDungeonSlot.SetActive(true);
								}
							}
							else if (nkcuiepactDungeonSlot.IsActive())
							{
								nkcuiepactDungeonSlot.SetActive(false);
							}
						}
						else if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_LOCK)
						{
							num++;
							nkcuiepactDungeonSlot.SetData(NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_ActID, nkmstageTempletV.m_StageIndex, nkmstageTempletV.m_StageBattleStrID, !flag2, NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_Difficulty);
							if (!nkcuiepactDungeonSlot.CheckLock())
							{
								num2++;
								if (!flag)
								{
									num3 = nkcuiepactDungeonSlot.GetComponent<RectTransform>().sizeDelta.y;
									flag = true;
								}
							}
							if (flag2 && !PlayerPrefs.HasKey(string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !myUserData.CheckStageCleared(nkmstageTempletV))
							{
								nkcuiepactDungeonSlot.SetEnableNewMark(true);
							}
							else
							{
								nkcuiepactDungeonSlot.SetEnableNewMark(false);
							}
							if (!nkcuiepactDungeonSlot.IsActive())
							{
								nkcuiepactDungeonSlot.SetActive(true);
							}
						}
						else if (nkcuiepactDungeonSlot.IsActive())
						{
							nkcuiepactDungeonSlot.SetActive(false);
						}
					}
					else if (nkcuiepactDungeonSlot.IsActive())
					{
						nkcuiepactDungeonSlot.SetActive(false);
					}
				}
				this.Sort();
				if ((nkmepisodeTempletV.m_EPCategory == EPISODE_CATEGORY.EC_DAILY || nkmepisodeTempletV.m_EPCategory == EPISODE_CATEGORY.EC_SUPPLY) && num3 > 0f)
				{
					Vector2 anchoredPosition = this.m_rectListContent.anchoredPosition;
					float y = this.m_NKM_UI_OPERATION_EPISODE_MENU_Viewport.sizeDelta.y;
					float num4 = (float)num2 * num3;
					float num5 = 0f;
					if (num4 > y)
					{
						num5 = num4 - y;
					}
					anchoredPosition.y = this.m_ContentOrgPosY + num5;
					this.m_rectListContent.anchoredPosition = anchoredPosition;
					return;
				}
				Vector2 sizeDelta = this.m_rectListContent.sizeDelta;
				sizeDelta.y = (float)(num * 178 + 26);
				this.m_rectListContent.sizeDelta = sizeDelta;
				Vector2 anchoredPosition2 = this.m_rectListContent.anchoredPosition;
				anchoredPosition2.y = this.m_ContentOrgPosY + this.m_rectListContent.sizeDelta.y;
				this.m_rectListContent.anchoredPosition = anchoredPosition2;
				this.m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect.verticalNormalizedPosition = 0f;
			}

			// Token: 0x0600AF9F RID: 44959 RVA: 0x0035D14C File Offset: 0x0035B34C
			private void Sort()
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_EpisodeID, NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.m_Difficulty);
				if (nkmepisodeTempletV == null)
				{
					return;
				}
				if (nkmepisodeTempletV.m_EPCategory == EPISODE_CATEGORY.EC_DAILY || nkmepisodeTempletV.m_EPCategory == EPISODE_CATEGORY.EC_SUPPLY)
				{
					this.m_listItemSlot.Sort(new NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr.Comp());
					for (int i = 0; i < this.m_listItemSlot.Count; i++)
					{
						NKCUIEPActDungeonSlot nkcuiepactDungeonSlot = this.m_listItemSlot[i];
						if (nkcuiepactDungeonSlot.IsActive())
						{
							nkcuiepactDungeonSlot.transform.SetSiblingIndex(i);
							nkcuiepactDungeonSlot.SetIndexToAnimateAlpha(i);
						}
					}
				}
			}

			// Token: 0x0600AFA0 RID: 44960 RVA: 0x0035D1D0 File Offset: 0x0035B3D0
			public NKCUIEPActDungeonSlot GetSlotByStageIndex(int stageIndex)
			{
				return this.m_listItemSlot.Find((NKCUIEPActDungeonSlot x) => x.GetStageIndex() == stageIndex);
			}

			// Token: 0x0400A3C1 RID: 41921
			private static int m_EpisodeID;

			// Token: 0x0400A3C2 RID: 41922
			private static int m_ActID;

			// Token: 0x0400A3C3 RID: 41923
			private static EPISODE_DIFFICULTY m_Difficulty;

			// Token: 0x0400A3C4 RID: 41924
			private GameObject m_NKM_UI_OPERATION_EP_DUNGEON_LIST;

			// Token: 0x0400A3C5 RID: 41925
			private GameObject m_NKM_UI_OPERATION_EP_DUNGEON_Content;

			// Token: 0x0400A3C6 RID: 41926
			private ScrollRect m_NKM_UI_OPERATION_EP_DUNGEON_ScrollRect;

			// Token: 0x0400A3C7 RID: 41927
			private RectTransform m_NKM_UI_OPERATION_EPISODE_MENU_Viewport;

			// Token: 0x0400A3C8 RID: 41928
			private const int DEFAULT_ITEM_SLOT_COUNT = 10;

			// Token: 0x0400A3C9 RID: 41929
			private const int DUMMY_SIZE = 26;

			// Token: 0x0400A3CA RID: 41930
			private const int ITEM_SIZE_Y = 178;

			// Token: 0x0400A3CB RID: 41931
			private RectTransform m_rectListContent;

			// Token: 0x0400A3CC RID: 41932
			private float m_ContentOrgPosY;

			// Token: 0x0400A3CD RID: 41933
			private List<NKCUIEPActDungeonSlot> m_listItemSlot = new List<NKCUIEPActDungeonSlot>();

			// Token: 0x0400A3CE RID: 41934
			private NKMStageTempletV2 m_Reserved_EPExtraData;

			// Token: 0x0400A3CF RID: 41935
			private NKM_SCEN_ID m_Reserved_NKM_SCEN_ID_After_WarfareGiveup = NKM_SCEN_ID.NSI_WARFARE_GAME;

			// Token: 0x02001A7C RID: 6780
			public class Comp : IComparer<NKCUIEPActDungeonSlot>
			{
				// Token: 0x0600BC1C RID: 48156 RVA: 0x0036FB48 File Offset: 0x0036DD48
				public int Compare(NKCUIEPActDungeonSlot x, NKCUIEPActDungeonSlot y)
				{
					if (!x.IsActive())
					{
						return 1;
					}
					if (!y.IsActive())
					{
						return -1;
					}
					if (!y.CheckLock() && x.CheckLock())
					{
						return 1;
					}
					if (y.CheckLock() && !x.CheckLock())
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

		// Token: 0x0200163B RID: 5691
		public class NKCUIEPActSlotMgr
		{
			// Token: 0x0600AFA4 RID: 44964 RVA: 0x0035D24A File Offset: 0x0035B44A
			public void SetEPSlot(bool bSet)
			{
				this.m_bEPSlot = bSet;
			}

			// Token: 0x0600AFA5 RID: 44965 RVA: 0x0035D253 File Offset: 0x0035B453
			public NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr GetNKCUIEPDungeonSlotMgr()
			{
				return this.m_NKCUIEPDungeonSlotMgr;
			}

			// Token: 0x0600AFA6 RID: 44966 RVA: 0x0035D25C File Offset: 0x0035B45C
			public void Close()
			{
				for (int i = 0; i < this.m_listItemSlot.Count; i++)
				{
					this.m_listItemSlot[i].Close();
				}
				this.m_listItemSlot.Clear();
				this.ResetUnlockEffects();
				this.m_NKCUIEPDungeonSlotMgr.CloseOperationIntro();
				this.m_NKCUIEPDungeonSlotMgr.Clear();
			}

			// Token: 0x0600AFA7 RID: 44967 RVA: 0x0035D2B8 File Offset: 0x0035B4B8
			public void InitUI(NKCUIEpisodeViewer _NKCUIEpisodeViewer, GameObject _NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content, GameObject _NKM_UI_OPERATION_EP_DUNGEON_LIST, GameObject _NKM_UI_OPERATION_EP_DUNGEON_Content, ScrollRect _NKM_UI_OPERATION_EP_DUNGEON_ScrollRect, RectTransform _NKM_UI_OPERATION_EPISODE_MENU_Viewport, GameObject _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST, GameObject _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content, ScrollRect _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect, GameObject _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL, ScrollRect _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL, List<GameObject> _lstHideWithActMenu)
			{
				this.m_NKCUIEpisodeViewer = _NKCUIEpisodeViewer;
				if (this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content == null)
				{
					this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content = _NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content;
					this.m_rectListContent = this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content.GetComponent<RectTransform>();
					this.m_NKCUIComToggleGroup = this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content.GetComponent<NKCUIComToggleGroup>();
					this.m_NKCUIComToggleGroup.SetHotkey(HotkeyEventType.Up, HotkeyEventType.Down);
					this.m_ContentOrgPosY = this.m_rectListContent.anchoredPosition.y;
					for (int i = 0; i < 10; i++)
					{
						NKCUIEpisodeActSlot newInstance = NKCUIEpisodeActSlot.GetNewInstance(this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content.transform, new NKCUIEpisodeActSlot.OnSelectedItemSlot(this.OnSelectedActSlot));
						this.InitSlot(newInstance);
						this.m_listItemSlot.Add(newInstance);
					}
				}
				this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST = _NKM_UI_OPERATION_EP_DUNGEON_LIST;
				this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST = _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST;
				this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content = _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content;
				this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect = _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect;
				this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL = _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL;
				this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL = _NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL;
				this.m_lstHideWithActMenu = _lstHideWithActMenu;
				this.m_NKCUIEPDungeonSlotMgr.InitUI(_NKM_UI_OPERATION_EP_DUNGEON_LIST, _NKM_UI_OPERATION_EP_DUNGEON_Content, _NKM_UI_OPERATION_EP_DUNGEON_ScrollRect, _NKM_UI_OPERATION_EPISODE_MENU_Viewport);
			}

			// Token: 0x0600AFA8 RID: 44968 RVA: 0x0035D3B1 File Offset: 0x0035B5B1
			public void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK cNKMPacket_WARFARE_GAME_GIVE_UP_ACK)
			{
				this.m_NKCUIEPDungeonSlotMgr.OnRecv(cNKMPacket_WARFARE_GAME_GIVE_UP_ACK);
			}

			// Token: 0x0600AFA9 RID: 44969 RVA: 0x0035D3BF File Offset: 0x0035B5BF
			public void OnRecv(NKMPacket_STAGE_UNLOCK_ACK sPacket)
			{
				this.m_NKCUIEPDungeonSlotMgr.OnRecv(sPacket);
			}

			// Token: 0x0600AFAA RID: 44970 RVA: 0x0035D3D0 File Offset: 0x0035B5D0
			public void OpenLatestAct()
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
				if (nkmepisodeTempletV == null)
				{
					return;
				}
				if (this.m_bEPSlot)
				{
					for (int i = 0; i < this.m_listItemSlot.Count; i++)
					{
						NKCUIEpisodeActSlot nkcuiepisodeActSlot = this.m_listItemSlot[i];
						if (nkcuiepisodeActSlot != null && nkcuiepisodeActSlot.IsActive() && nkcuiepisodeActSlot.m_ToggleButton.enabled && nkcuiepisodeActSlot.GetNKMEpisodeTemplet().m_EpisodeID == this.m_EpisodeID)
						{
							nkcuiepisodeActSlot.m_ToggleButton.Select(false, true, false);
							nkcuiepisodeActSlot.m_ToggleButton.Select(true, false, false);
							return;
						}
					}
					return;
				}
				int count = nkmepisodeTempletV.m_DicStage.Count;
				if (this.m_listItemSlot.Count < count)
				{
					return;
				}
				int latestActIndex = this.GetLatestActIndex();
				if (!NKMEpisodeMgr.CheckOpenedAct(this.m_EpisodeID, latestActIndex + 1))
				{
					return;
				}
				NKCUIEpisodeActSlot nkcuiepisodeActSlot2 = this.m_listItemSlot[latestActIndex];
				if (nkcuiepisodeActSlot2 != null)
				{
					nkcuiepisodeActSlot2.m_ToggleButton.Select(false, true, false);
					nkcuiepisodeActSlot2.m_ToggleButton.Select(true, false, false);
				}
			}

			// Token: 0x0600AFAB RID: 44971 RVA: 0x0035D4E0 File Offset: 0x0035B6E0
			private int GetLatestActIndex()
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
				if (nkmepisodeTempletV == null)
				{
					return 0;
				}
				int count = nkmepisodeTempletV.m_DicStage.Count;
				if (count > 1)
				{
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					for (int i = count; i >= 1; i--)
					{
						NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(i);
						if (NKMEpisodeMgr.CheckEpisodeMission(myUserData, firstStage))
						{
							return i - 1;
						}
					}
				}
				return 0;
			}

			// Token: 0x0600AFAC RID: 44972 RVA: 0x0035D544 File Offset: 0x0035B744
			public NKCUIEpisodeActSlot GetSlotByActIndex(int actIndex)
			{
				return this.m_listItemSlot.Find((NKCUIEpisodeActSlot x) => x.GetActIndex() == actIndex);
			}

			// Token: 0x0600AFAD RID: 44973 RVA: 0x0035D578 File Offset: 0x0035B778
			public void SelectActSlot(int actID)
			{
				NKCUIEpisodeActSlot nkcuiepisodeActSlot = this.m_listItemSlot[actID - 1];
				if (nkcuiepisodeActSlot != null)
				{
					nkcuiepisodeActSlot.m_ToggleButton.Select(false, true, false);
					nkcuiepisodeActSlot.m_ToggleButton.Select(true, false, false);
				}
			}

			// Token: 0x0600AFAE RID: 44974 RVA: 0x0035D5BC File Offset: 0x0035B7BC
			private void OnSelectedActSlot(bool bSet, int actID, NKMEpisodeTempletV2 cNKMEpisodeTemplet, EPISODE_DIFFICULTY difficulty)
			{
				if (this.m_bEPSlot)
				{
					actID = 1;
					if (cNKMEpisodeTemplet != null)
					{
						this.m_EpisodeID = cNKMEpisodeTemplet.m_EpisodeID;
						NKC_SCEN_EPISODE scen_EPISODE = NKCScenManager.GetScenManager().Get_SCEN_EPISODE();
						if (scen_EPISODE != null)
						{
							scen_EPISODE.SetEpisodeID(this.m_EpisodeID);
						}
						if (this.m_NKCUIEpisodeViewer != null)
						{
							this.m_NKCUIEpisodeViewer.SetEpisodeID(this.m_EpisodeID);
							this.m_NKCUIEpisodeViewer.SetUpperUI();
						}
					}
				}
				else if (cNKMEpisodeTemplet != null)
				{
					int firstStageID = NKCContentManager.GetFirstStageID(cNKMEpisodeTemplet, actID, difficulty);
					if (this.m_dicUnlockEffectGo.ContainsKey(firstStageID))
					{
						UnityEngine.Object.Destroy(this.m_dicUnlockEffectGo[firstStageID]);
						this.m_dicUnlockEffectGo.Remove(firstStageID);
					}
					NKCContentManager.RemoveUnlockedContent(ContentsType.ACT, firstStageID, true);
				}
				foreach (KeyValuePair<int, NKCUIStageViewer> keyValuePair in this.m_dicStageViewer)
				{
					NKCUtil.SetGameobjectActive(keyValuePair.Value, false);
				}
				if (bSet)
				{
					if (this.IsUsingStageViewer(cNKMEpisodeTemplet))
					{
						NKCScenManager.GetScenManager().Get_SCEN_EPISODE().GetEpisodeViewer().SetEnableTitleBG(false);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST, false);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST, true);
						if (this.m_lstHideWithActMenu != null)
						{
							for (int i = 0; i < this.m_lstHideWithActMenu.Count; i++)
							{
								NKCUtil.SetGameobjectActive(this.m_lstHideWithActMenu[i], !cNKMEpisodeTemplet.m_bHideActTab);
							}
						}
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect, !cNKMEpisodeTemplet.m_bHideActTab);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL, cNKMEpisodeTemplet.m_bHideActTab);
						this.m_NKCUIEPDungeonSlotMgr.Close();
						if (!this.m_dicStageViewer.ContainsKey(cNKMEpisodeTemplet.m_EpisodeID))
						{
							GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>(cNKMEpisodeTemplet.m_Stage_Viewer_Prefab, cNKMEpisodeTemplet.m_Stage_Viewer_Prefab, true);
							if (orLoadAssetResource == null)
							{
								Log.Error(string.Format("이벤트 던전 리스트 로드에 실패 : {0}", cNKMEpisodeTemplet.m_EpisodeID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIEpisodeViewer.cs", 1009);
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
								return;
							}
							NKCUIStageViewer component = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource).GetComponent<NKCUIStageViewer>();
							if (component != null)
							{
								if (!cNKMEpisodeTemplet.m_bHideActTab)
								{
									component.GetComponent<RectTransform>().SetParent(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content.transform, false);
								}
								else
								{
									component.GetComponent<RectTransform>().SetParent(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL.transform, false);
								}
								component.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
								component.GetComponent<RectTransform>().localScale = Vector3.one;
								this.m_dicStageViewer.Add(cNKMEpisodeTemplet.m_EpisodeID, component);
							}
						}
						if (this.m_dicStageViewer[cNKMEpisodeTemplet.m_EpisodeID].GetActCount(difficulty) != cNKMEpisodeTemplet.m_DicStage.Count)
						{
							Log.Error(string.Format("ACt 숫자가 프리팹과 맞지 않음 - EpisodeID : {0}", cNKMEpisodeTemplet.m_EpisodeID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIEpisodeViewer.cs", 1031);
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
							return;
						}
						using (Dictionary<int, NKCUIStageViewer>.Enumerator enumerator = this.m_dicStageViewer.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<int, NKCUIStageViewer> keyValuePair2 = enumerator.Current;
								if (keyValuePair2.Key == cNKMEpisodeTemplet.m_EpisodeID)
								{
									NKCUtil.SetGameobjectActive(keyValuePair2.Value, true);
									Vector2 vector = keyValuePair2.Value.SetData(false, cNKMEpisodeTemplet.m_EpisodeID, actID, difficulty, new IDungeonSlot.OnSelectedItemSlot(this.m_NKCUIEPDungeonSlotMgr.OnSelectedActSlot), cNKMEpisodeTemplet.m_ScrollType);
									if (!cNKMEpisodeTemplet.m_bHideActTab)
									{
										LayoutRebuilder.ForceRebuildLayoutImmediate(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.content);
										if (this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.horizontal)
										{
											this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.horizontalNormalizedPosition = vector.x;
										}
										if (this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.vertical)
										{
											this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.verticalNormalizedPosition = vector.y;
										}
										NKCUIComScrollRectHotkey component2 = this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.GetComponent<NKCUIComScrollRectHotkey>();
										if (component2 != null)
										{
											component2.enabled = !this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect.vertical;
											break;
										}
										break;
									}
									else
									{
										LayoutRebuilder.ForceRebuildLayoutImmediate(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.content);
										if (this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.horizontal)
										{
											this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.horizontalNormalizedPosition = vector.x;
										}
										if (this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.vertical)
										{
											this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.verticalNormalizedPosition = vector.y;
										}
										NKCUIComScrollRectHotkey component3 = this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.GetComponent<NKCUIComScrollRectHotkey>();
										if (component3 != null)
										{
											component3.enabled = !this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL.vertical;
											break;
										}
										break;
									}
								}
							}
							return;
						}
					}
					NKCScenManager.GetScenManager().Get_SCEN_EPISODE().GetEpisodeViewer().SetEnableTitleBG(true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST, false);
					this.m_NKCUIEPDungeonSlotMgr.Open(this.m_EpisodeID, actID, difficulty);
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_EPISODE().GetEpisodeViewer().SetEnableTitleBG(true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EP_DUNGEON_LIST, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST, false);
				this.m_NKCUIEPDungeonSlotMgr.Close();
			}

			// Token: 0x0600AFAF RID: 44975 RVA: 0x0035DAD8 File Offset: 0x0035BCD8
			private bool IsUsingStageViewer(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
			{
				return cNKMEpisodeTemplet != null && !string.IsNullOrEmpty(cNKMEpisodeTemplet.m_Stage_Viewer_Prefab);
			}

			// Token: 0x0600AFB0 RID: 44976 RVA: 0x0035DAED File Offset: 0x0035BCED
			public void ReOpenDungeonListByCurrentSetting()
			{
				if (this.m_bEPSlot)
				{
					this.OpenLatestAct();
					return;
				}
				if (this.m_NKCUIEPDungeonSlotMgr.GetActID() != 0)
				{
					this.SelectActSlot(this.m_NKCUIEPDungeonSlotMgr.GetActID());
					return;
				}
				this.OpenLatestAct();
			}

			// Token: 0x0600AFB1 RID: 44977 RVA: 0x0035DB24 File Offset: 0x0035BD24
			private void InitSlot(NKCUIEpisodeActSlot cNKCUIEpisodeActSlot)
			{
				if (cNKCUIEpisodeActSlot != null && cNKCUIEpisodeActSlot.gameObject != null && cNKCUIEpisodeActSlot.gameObject.GetComponent<RectTransform>() != null)
				{
					cNKCUIEpisodeActSlot.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
				}
			}

			// Token: 0x0600AFB2 RID: 44978 RVA: 0x0035DB70 File Offset: 0x0035BD70
			public void ResetUIByData(int episodeID, EPISODE_DIFFICULTY difficulty)
			{
				this.m_EpisodeID = episodeID;
				this.m_Difficulty = difficulty;
				int num = 0;
				NKCUIEpisodeViewer.m_autoMoveTarget = null;
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
				if (nkmepisodeTempletV == null)
				{
					return;
				}
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (this.m_bEPSlot)
				{
					List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(nkmepisodeTempletV.m_EPCategory, true, nkmepisodeTempletV.m_Difficulty);
					int count = listNKMEpisodeTempletByCategory.Count;
					if (this.m_listItemSlot.Count < count)
					{
						int count2 = this.m_listItemSlot.Count;
						for (int i = 0; i < count - count2; i++)
						{
							NKCUIEpisodeActSlot newInstance = NKCUIEpisodeActSlot.GetNewInstance(this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content.transform, new NKCUIEpisodeActSlot.OnSelectedItemSlot(this.OnSelectedActSlot));
							this.InitSlot(newInstance);
							this.m_listItemSlot.Add(newInstance);
						}
					}
					for (int i = 0; i < this.m_listItemSlot.Count; i++)
					{
						NKCUIEpisodeActSlot nkcuiepisodeActSlot = this.m_listItemSlot[i];
						if (i < count)
						{
							num++;
							NKMEpisodeTempletV2 nkmepisodeTempletV2 = listNKMEpisodeTempletByCategory[i];
							if (NKMEpisodeMgr.IsPossibleEpisode(myUserData, nkmepisodeTempletV2))
							{
								nkcuiepisodeActSlot.SetData(nkmepisodeTempletV2, 1, this.m_Difficulty, this.m_NKCUIComToggleGroup, false, true);
							}
							else
							{
								nkcuiepisodeActSlot.SetData(nkmepisodeTempletV2, 1, this.m_Difficulty, this.m_NKCUIComToggleGroup, true, true);
							}
							if (!nkcuiepisodeActSlot.IsActive())
							{
								nkcuiepisodeActSlot.SetActive(true);
							}
						}
						else if (nkcuiepisodeActSlot.IsActive())
						{
							nkcuiepisodeActSlot.SetActive(false);
						}
					}
				}
				else
				{
					int count3 = nkmepisodeTempletV.m_DicStage.Count;
					if (this.m_listItemSlot.Count < count3)
					{
						int count4 = this.m_listItemSlot.Count;
						for (int j = 0; j < count3 - count4; j++)
						{
							NKCUIEpisodeActSlot newInstance2 = NKCUIEpisodeActSlot.GetNewInstance(this.m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content.transform, new NKCUIEpisodeActSlot.OnSelectedItemSlot(this.OnSelectedActSlot));
							this.InitSlot(newInstance2);
							this.m_listItemSlot.Add(newInstance2);
						}
					}
					for (int j = 0; j < this.m_listItemSlot.Count; j++)
					{
						NKCUIEpisodeActSlot nkcuiepisodeActSlot2 = this.m_listItemSlot[j];
						if (!NKMEpisodeMgr.CheckOpenedAct(this.m_EpisodeID, j + 1))
						{
							nkcuiepisodeActSlot2.SetActive(false);
						}
						else if (j < count3)
						{
							num++;
							if (NKMEpisodeMgr.CheckPossibleAct(myUserData, this.m_EpisodeID, j + 1, this.m_Difficulty))
							{
								nkcuiepisodeActSlot2.SetData(nkmepisodeTempletV, j + 1, this.m_Difficulty, this.m_NKCUIComToggleGroup, false, false);
								int firstStageID = NKCContentManager.GetFirstStageID(nkmepisodeTempletV, j + 1, this.m_Difficulty);
								if (NKCContentManager.UnlockEffectRequired(ContentsType.ACT, firstStageID))
								{
									this.m_dicUnlockEffectGo.Add(firstStageID, NKCContentManager.AddUnlockedEffect(nkcuiepisodeActSlot2.transform));
									NKCUIEpisodeViewer.m_autoMoveTarget = nkcuiepisodeActSlot2;
								}
							}
							else
							{
								nkcuiepisodeActSlot2.SetData(nkmepisodeTempletV, j + 1, this.m_Difficulty, this.m_NKCUIComToggleGroup, true, false);
							}
							if (!nkcuiepisodeActSlot2.IsActive())
							{
								nkcuiepisodeActSlot2.SetActive(true);
							}
						}
						else if (nkcuiepisodeActSlot2.IsActive())
						{
							nkcuiepisodeActSlot2.SetActive(false);
						}
					}
				}
				Vector2 anchoredPosition = this.m_rectListContent.anchoredPosition;
				anchoredPosition.y = this.m_ContentOrgPosY;
				this.m_rectListContent.anchoredPosition = anchoredPosition;
				Vector2 sizeDelta = this.m_rectListContent.sizeDelta;
				sizeDelta.y = (float)(num * 108);
				this.m_rectListContent.sizeDelta = sizeDelta;
			}

			// Token: 0x0600AFB3 RID: 44979 RVA: 0x0035DEB4 File Offset: 0x0035C0B4
			public void ResetSelectedAct()
			{
				if (this.m_bEPSlot)
				{
					return;
				}
				if (this.m_NKCUIEPDungeonSlotMgr.GetActID() == 0)
				{
					this.OpenLatestAct();
					return;
				}
				if (NKMEpisodeMgr.CheckPossibleAct(NKCScenManager.GetScenManager().GetMyUserData(), this.m_EpisodeID, this.m_NKCUIEPDungeonSlotMgr.GetActID(), this.m_Difficulty))
				{
					this.ReOpenDungeonListByCurrentSetting();
					return;
				}
				if (NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty) == null)
				{
					return;
				}
				int latestActIndex = this.GetLatestActIndex();
				NKCUIEpisodeActSlot nkcuiepisodeActSlot = this.m_listItemSlot[latestActIndex];
				if (nkcuiepisodeActSlot != null)
				{
					nkcuiepisodeActSlot.m_ToggleButton.Select(false, true, false);
					nkcuiepisodeActSlot.m_ToggleButton.Select(true, false, false);
				}
			}

			// Token: 0x0600AFB4 RID: 44980 RVA: 0x0035DF5C File Offset: 0x0035C15C
			public void ResetUnlockEffects()
			{
				foreach (KeyValuePair<int, GameObject> keyValuePair in this.m_dicUnlockEffectGo)
				{
					UnityEngine.Object.Destroy(keyValuePair.Value);
				}
				this.m_dicUnlockEffectGo = new Dictionary<int, GameObject>();
			}

			// Token: 0x0400A3D0 RID: 41936
			private bool m_bEPSlot;

			// Token: 0x0400A3D1 RID: 41937
			private int m_EpisodeID;

			// Token: 0x0400A3D2 RID: 41938
			private GameObject m_NKM_UI_OPERATION_EPISODE_MENU_SCROLL_Content;

			// Token: 0x0400A3D3 RID: 41939
			private EPISODE_DIFFICULTY m_Difficulty;

			// Token: 0x0400A3D4 RID: 41940
			private RectTransform m_rectListContent;

			// Token: 0x0400A3D5 RID: 41941
			private NKCUIComToggleGroup m_NKCUIComToggleGroup;

			// Token: 0x0400A3D6 RID: 41942
			private float m_ContentOrgPosY;

			// Token: 0x0400A3D7 RID: 41943
			private GameObject m_NKM_UI_OPERATION_EP_DUNGEON_LIST;

			// Token: 0x0400A3D8 RID: 41944
			private GameObject m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_LIST;

			// Token: 0x0400A3D9 RID: 41945
			private GameObject m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content;

			// Token: 0x0400A3DA RID: 41946
			private ScrollRect m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect;

			// Token: 0x0400A3DB RID: 41947
			private GameObject m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_Content_FULL;

			// Token: 0x0400A3DC RID: 41948
			private ScrollRect m_NKM_UI_OPERATION_STAGE_VIEWER_DUNGEON_ScrollRect_FULL;

			// Token: 0x0400A3DD RID: 41949
			private List<GameObject> m_lstHideWithActMenu;

			// Token: 0x0400A3DE RID: 41950
			private const int DEFAULT_ITEM_SLOT_COUNT = 10;

			// Token: 0x0400A3DF RID: 41951
			private const int ITEM_SIZE_Y = 108;

			// Token: 0x0400A3E0 RID: 41952
			private List<NKCUIEpisodeActSlot> m_listItemSlot = new List<NKCUIEpisodeActSlot>();

			// Token: 0x0400A3E1 RID: 41953
			private NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr m_NKCUIEPDungeonSlotMgr = new NKCUIEpisodeViewer.NKCUIEPDungeonSlotMgr();

			// Token: 0x0400A3E2 RID: 41954
			private NKCUIEpisodeViewer m_NKCUIEpisodeViewer;

			// Token: 0x0400A3E3 RID: 41955
			private Dictionary<int, GameObject> m_dicUnlockEffectGo = new Dictionary<int, GameObject>();

			// Token: 0x0400A3E4 RID: 41956
			private Dictionary<int, NKCUIStageViewer> m_dicStageViewer = new Dictionary<int, NKCUIStageViewer>();
		}
	}
}
