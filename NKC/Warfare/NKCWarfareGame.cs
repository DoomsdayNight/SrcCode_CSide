using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Community;
using ClientPacket.Warfare;
using Cs.Logging;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI.Option;
using NKC.UI.Result;
using NKM;
using NKM.Templet;
using NKM.Warfare;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Warfare
{
	// Token: 0x02000AFF RID: 2815
	public class NKCWarfareGame : NKCUIBase
	{
		// Token: 0x06007F53 RID: 32595 RVA: 0x002AB339 File Offset: 0x002A9539
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCWarfareGame.s_LoadedUIData))
			{
				NKCWarfareGame.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCWarfareGame>("ab_ui_nkm_ui_warfare", "NUM_WARFARE_UI", NKCUIManager.eUIBaseRect.UIMidCanvas, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCWarfareGame.CleanupInstance));
			}
			return NKCWarfareGame.s_LoadedUIData;
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06007F54 RID: 32596 RVA: 0x002AB36D File Offset: 0x002A956D
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCWarfareGame.s_LoadedUIData != null && NKCWarfareGame.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06007F55 RID: 32597 RVA: 0x002AB382 File Offset: 0x002A9582
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCWarfareGame.s_LoadedUIData != null && NKCWarfareGame.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x002AB397 File Offset: 0x002A9597
		public static void CleanupInstance()
		{
			NKCWarfareGame.s_LoadedUIData = null;
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x002AB39F File Offset: 0x002A959F
		public static NKCWarfareGame GetInstance()
		{
			if (NKCWarfareGame.s_LoadedUIData != null && NKCWarfareGame.s_LoadedUIData.IsLoadComplete)
			{
				return NKCWarfareGame.s_LoadedUIData.GetInstance<NKCWarfareGame>();
			}
			return null;
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06007F58 RID: 32600 RVA: 0x002AB3C0 File Offset: 0x002A95C0
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_WARFARE;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06007F59 RID: 32601 RVA: 0x002AB3C7 File Offset: 0x002A95C7
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06007F5A RID: 32602 RVA: 0x002AB3CF File Offset: 0x002A95CF
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_WARFARE_CONTROL";
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06007F5B RID: 32603 RVA: 0x002AB3D8 File Offset: 0x002A95D8
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				NKM_WARFARE_GAME_STATE warfareGameState = NKCScenManager.GetScenManager().WarfareGameData.warfareGameState;
				if (warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP && warfareGameState - NKM_WARFARE_GAME_STATE.NWGS_PLAYING <= 4)
				{
					return NKCUIUpsideMenu.eMode.Disable;
				}
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x002AB401 File Offset: 0x002A9601
		public NKCWarfareGameUnitMgr GetNKCWarfareGameUnitMgr()
		{
			return this.m_unitMgr;
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06007F5D RID: 32605 RVA: 0x002AB409 File Offset: 0x002A9609
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x002AB40C File Offset: 0x002A960C
		public bool GetPause()
		{
			return this.m_RefPause > 0;
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x002AB417 File Offset: 0x002A9617
		public void AddPauseRef()
		{
			this.m_RefPause++;
		}

		// Token: 0x06007F60 RID: 32608 RVA: 0x002AB427 File Offset: 0x002A9627
		public void MinusPauseRef()
		{
			this.m_RefPause--;
			if (this.m_RefPause <= 0)
			{
				this.m_RefPause = 0;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06007F61 RID: 32609 RVA: 0x002AB448 File Offset: 0x002A9648
		private NKCPopupWarfareSelectShip NKCPopupWarfareSelectShip
		{
			get
			{
				if (this.m_NKCPopupWarfareSelectShip == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWarfareSelectShip>("AB_UI_NKM_UI_POPUP_WARFARE_SELECT", "NKM_UI_POPUP_WARFARE_SELECT_SHIP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupWarfareSelectShip = loadedUIData.GetInstance<NKCPopupWarfareSelectShip>();
					NKCPopupWarfareSelectShip nkcpopupWarfareSelectShip = this.m_NKCPopupWarfareSelectShip;
					if (nkcpopupWarfareSelectShip != null)
					{
						nkcpopupWarfareSelectShip.InitUI();
					}
				}
				return this.m_NKCPopupWarfareSelectShip;
			}
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x002AB4A0 File Offset: 0x002A96A0
		public void InitUI()
		{
			this.m_NUM_WARFARE = base.gameObject;
			this.m_NUM_WARFARE.SetActive(false);
			EventTrigger component = this.m_NUM_WARFARE.GetComponent<EventTrigger>();
			if (component != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData e)
				{
					this.InvalidSelectedUnit();
				});
				component.triggers.Add(entry);
				EventTrigger.Entry entry2 = new EventTrigger.Entry();
				entry2.eventID = EventTriggerType.Drag;
				entry2.callback.AddListener(new UnityAction<BaseEventData>(this.OnDragByInstance));
				component.triggers.Add(entry2);
			}
			this.m_NKCWarfareGameHUD.InitUI(this, new NKCWarfareGameHUD.OnStartUserPhase(this.OnStartUserPhase), new NKCWarfareGameHUD.OnStartEnemyPhase(this.OnStartEnemyPhase));
			this.m_NKCWarfareGameHUD.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), false);
			this.m_NKCWarfareGameHUD.transform.SetAsLastSibling();
			NKCUIManager.OpenUI(this.m_NKCWarfareGameHUD.gameObject);
			this.m_tileMgr = new NKCWarfareGameTileMgr(this.m_NUM_WARFARE_TILE_Panel.transform);
			this.m_unitMgr = new NKCWarfareGameUnitMgr(this.m_NUM_WARFARE_UNIT_LIST.transform, this.m_NUM_WARFARE_UNIT_INFO_LIST.transform);
			this.m_battleCondition = new NKCWarfareGameBattleCondition(this.m_NUM_WARFARE_BATTILE_CONDITION_LIST.transform);
			this.m_labelMgr = new NKCWarfareGameLabelMgr(this.m_NUM_WARFARE_LABEL.transform);
			this.m_containerMgr = new NKCWarfareGameItemMgr(this.m_NUM_WARFARE_LABEL.transform);
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x002AB60F File Offset: 0x002A980F
		public void OpenWaitBox()
		{
			this.m_NKCWarfareGameHUD.SetWaitBox(true);
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x002AB61D File Offset: 0x002A981D
		public void SetWarfareStrID(string strID)
		{
			this.m_WarfareStrID = strID;
			if (this.m_NKCWarfareGameHUD != null)
			{
				this.m_NKCWarfareGameHUD.SetWarfareStrID(this.m_WarfareStrID);
			}
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x002AB648 File Offset: 0x002A9848
		public void SetBG(bool bReadyBG, bool bStart = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_BG_A, bReadyBG);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_BG_B, !bReadyBG);
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet != null)
			{
				if (bReadyBG)
				{
					if (nkmwarfareTemplet.m_WarfareBG_Stop.Length > 0)
					{
						this.m_NUM_WARFARE_BG_A_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WARFARE_TEXTURE", nkmwarfareTemplet.m_WarfareBG_Stop, false);
					}
				}
				else if (nkmwarfareTemplet.m_WarfareBG_Playing.Length > 0)
				{
					this.m_NUM_WARFARE_BG_B_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WARFARE_TEXTURE", nkmwarfareTemplet.m_WarfareBG_Playing, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_BG_B_MOVIE, !bReadyBG && bStart);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_BG_B_MOVIE_IMG, !bReadyBG && !bStart);
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x002AB700 File Offset: 0x002A9900
		private void CheckValidWarfare()
		{
			this.m_NKMWarfareMapTemplet = null;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				Debug.LogError("warfare open - NKMWarfareTemplet is null");
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				Debug.LogError("warfare open - NKMWarfareMapTemplet is null");
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_NKMWarfareMapTemplet = mapTemplet;
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x002AB764 File Offset: 0x002A9964
		private void InitWhenFirstOpen()
		{
			if (this.m_listUnitPos.Count > 0)
			{
				return;
			}
			for (int i = 0; i < 70; i++)
			{
				this.m_listUnitPos.Add(new Vector3(0f, 0f, 0f));
			}
			this.m_tileMgr.Init(new NKCWarfareGameTile.onClickPossibleArrivalTile(this.OnClickPossibleArrivalTile));
			this.m_unitMgr.Init();
			this.m_battleCondition.Init();
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x002AB7DC File Offset: 0x002A99DC
		public void Open(bool bAfterBattle, NKCWarfareGame.DataBeforeBattle dataBeforeBattle, NKCWarfareGame.RetryData retryData)
		{
			this.m_bReservedShowBattleResult = bAfterBattle;
			this.m_DataBeforeBattle = dataBeforeBattle;
			this.m_RetryData = retryData;
			this.CheckValidWarfare();
			this.InitWhenFirstOpen();
			this.m_CanvasGroup.alpha = 1f;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				Debug.LogError("전역 알수 없는 템플릿을 찾으려함 - " + this.m_WarfareStrID);
				return;
			}
			this.m_twUnitInfoDie = null;
			this.m_bWaitEnemyTurn = false;
			this.m_bWaitingNextOreder = false;
			this.WaitAutoPacket = false;
			this.m_bReservedBattle = false;
			this.m_fElapsedTimeForShakeEnd = 0f;
			this.m_bReservedCallOnUnitShakeEnd = false;
			this.m_bPlayingIntro = false;
			if (this.m_NKCWarfareGameHUD == null)
			{
				Debug.LogError("WarfareGameHUD is null");
				return;
			}
			this.m_NKCWarfareGameHUD.SetWarfareStrID(this.m_WarfareStrID);
			this.m_NKCWarfareGameHUD.DeActivateAllTriggerUI();
			this.m_NKCWarfareGameHUD.CloseSelectedSquadUI();
			this.m_NKCWarfareGameHUD.SetActiveContainer(false);
			this.m_NUM_WARFARE.SetActive(true);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
			this.m_UserUnitMaxCount = nkmwarfareTemplet.m_UserTeamCount;
			NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID = nkmwarfareTemplet.m_WarfareID;
			this.m_NKCWarfareGameHUD.UpdateMedalInfo();
			this.m_NKCWarfareGameHUD.UpdateWinCondition();
			if (nkmwarfareTemplet.StageTemplet.EpisodeCategory == EPISODE_CATEGORY.EC_DAILY)
			{
				this.RESOURCE_LIST = new List<int>
				{
					nkmwarfareTemplet.StageTemplet.m_StageReqItemID,
					101
				};
			}
			else
			{
				this.RESOURCE_LIST = new List<int>
				{
					1,
					2,
					3,
					101
				};
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				Debug.LogError("WarfareGameData is null");
				return;
			}
			base.UIOpened(true);
			if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT)
				{
					bool flag = false;
					if (!NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing() && warfareGameData.isWinTeamA && (NKCUtil.m_sHsFirstClearWarfare.Contains(warfareGameData.warfareTempletID) || NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene))
					{
						NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(nkmwarfareTemplet.m_CutScenStrIDAfter);
						if (cutScenTemple != null)
						{
							NKCUtil.SetGameobjectActive(base.gameObject, false);
							this.m_NKCWarfareGameHUD.Close();
							NKCUICutScenPlayer.Instance.Play(cutScenTemple, nkmwarfareTemplet.StageTemplet.Key, new NKCUICutScenPlayer.CutScenCallBack(this.OnEndCutScenEnd));
							flag = true;
						}
					}
					if (!flag)
					{
						this.OnEndCutScenEnd();
						return;
					}
				}
				else
				{
					this.OnEndCutScenEnd();
				}
				return;
			}
			if (this.m_bReservedShowBattleResult)
			{
				this.ForceBack();
				return;
			}
			bool flag2 = false;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing() && (!myUserData.CheckWarfareClear(warfareGameData.warfareTempletID) || NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene))
			{
				NKCCutScenTemplet cutScenTemple2 = NKCCutScenManager.GetCutScenTemple(nkmwarfareTemplet.m_CutScenStrIDBefore);
				if (cutScenTemple2 != null)
				{
					base.gameObject.SetActive(false);
					this.m_NKCWarfareGameHUD.Close();
					flag2 = true;
					NKCUICutScenPlayer.Instance.Play(cutScenTemple2, nkmwarfareTemplet.StageTemplet.Key, new NKCUICutScenPlayer.CutScenCallBack(this.OnStartCutScenEnd));
				}
			}
			if (!flag2)
			{
				this.PrepareWarfareGameReady();
			}
			this.m_bReservedShowBattleResult = false;
		}

		// Token: 0x06007F69 RID: 32617 RVA: 0x002ABB24 File Offset: 0x002A9D24
		private void OnEndCutScenEnd()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUIManager.UpdateUpsideMenu();
			this.m_NKCWarfareGameHUD.Open();
			this.PrepareWarfareGameIntrude();
			this.RefreshContainerWhenOpen(this.m_bReservedShowBattleResult);
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			NKCUtil.m_sHsFirstClearWarfare.Remove(warfareGameData.warfareTempletID);
			if (this.m_bReservedShowBattleResult && warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY_READY && warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY)
			{
				this.CheckTutorialAfterBattle();
				if (!this.ShowBattleResult())
				{
					this.OpenActionWhenNotStop();
				}
			}
			else
			{
				this.OpenActionWhenNotStop();
			}
			this.m_bReservedShowBattleResult = false;
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x002ABBB8 File Offset: 0x002A9DB8
		private void ShowEndCutWhenOpened()
		{
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareGameData != null && nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT)
			{
				bool flag = false;
				if (nkmwarfareGameData.isWinTeamA && !NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing() && (NKCUtil.m_sHsFirstClearWarfare.Contains(nkmwarfareGameData.warfareTempletID) || NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene) && nkmwarfareTemplet != null)
				{
					NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(nkmwarfareTemplet.m_CutScenStrIDAfter);
					if (cutScenTemple != null)
					{
						NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL, false);
						NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE, false);
						NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, false);
						NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG, false);
						NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
						NKCUtil.SetGameobjectActive(base.gameObject, false);
						this.m_NKCWarfareGameHUD.Close();
						NKCUICutScenPlayer.Instance.Play(cutScenTemple, nkmwarfareTemplet.StageTemplet.Key, new NKCUICutScenPlayer.CutScenCallBack(this.OnEndCutScenEndWhenOpened));
						flag = true;
					}
				}
				if (!flag)
				{
					this.OnEndCutScenEndWhenOpened();
				}
			}
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x002ABCC8 File Offset: 0x002A9EC8
		private void OnEndCutScenEndWhenOpened()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCWarfareGameHUD.Open();
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			NKCUtil.m_sHsFirstClearWarfare.Remove(warfareGameData.warfareTempletID);
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_TILE_ENTER)
			{
				this.PlayEnterAni();
				return;
			}
			if (nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD)
			{
				Debug.Log("warfare next order REQ - OnEndCutScenEndWhenOpened");
				this.SendGetNextOrderREQ();
			}
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x002ABD40 File Offset: 0x002A9F40
		private bool ShowBattleResult()
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return false;
			}
			if (nkmwarfareTemplet.MapTemplet == null)
			{
				return false;
			}
			this.m_WarfareGameUnitToDie = null;
			List<WarfareUnitData> unitDataList = warfareGameData.GetUnitDataList();
			for (int i = 0; i < unitDataList.Count; i++)
			{
				WarfareUnitData warfareUnitData = unitDataList[i];
				if (warfareUnitData.hp > 0f)
				{
					this.m_WarfareGameUnitToDie = this.CheckAndAddBattleEnemy(warfareUnitData.warfareGameUnitUID, (int)warfareUnitData.tileIndex);
					if (this.m_WarfareGameUnitToDie != null)
					{
						break;
					}
				}
			}
			if (this.m_WarfareGameUnitToDie != null)
			{
				if (this.m_UnitDieSequence != null)
				{
					this.m_UnitDieSequence.Kill(false);
				}
				this.m_UnitDieSequence = null;
				WarfareUnitData nkmwarfareUnitData = this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData();
				NKMDungeonTempletBase nkmdungeonTempletBase = null;
				WarfareUnitData.Type unitType = nkmwarfareUnitData.unitType;
				if (unitType != WarfareUnitData.Type.User && unitType == WarfareUnitData.Type.Dungeon)
				{
					nkmdungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(nkmwarfareUnitData.dungeonID);
				}
				bool flag = true;
				if (nkmdungeonTempletBase != null && nkmdungeonTempletBase.m_NKM_WARFARE_GAME_UNIT_DIE_TYPE == NKMDungeonTempletBase.NKM_WARFARE_GAME_UNIT_DIE_TYPE.NWGUDT_RUNAWAY)
				{
					flag = false;
					this.PlayUnitRunawayAni();
				}
				if (flag)
				{
					this.PlayUnitDieAni();
				}
				this.m_fElapsedTimeForShakeEnd = 0f;
				this.m_bReservedCallOnUnitShakeEnd = true;
				return true;
			}
			return false;
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x002ABE68 File Offset: 0x002AA068
		private void OpenActionWhenNotStop()
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT)
			{
				this.m_NKCWarfareGameHUD.DeActivateAllTriggerUI();
				Debug.Log("warfare next order REQ - OpenActionWhenNotStop NWGS_RESULT");
				this.SendGetNextOrderREQ();
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING && (!warfareGameData.isTurnA || this.IsAutoWarfare()))
			{
				Debug.Log("warfare next order REQ - OpenActionWhenNotStop NWGS_PLAYING");
				this.SendGetNextOrderREQ(null);
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY_READY)
			{
				Debug.Log("warfare next order REQ - OpenActionWhenNotStop NWGS_INGAME_PLAY_TRY_READY");
				this.SendGetNextOrderREQ(null);
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY)
			{
				this.TryGameLoadReq();
			}
		}

		// Token: 0x06007F6E RID: 32622 RVA: 0x002ABEFC File Offset: 0x002AA0FC
		public override void Hide()
		{
			this.m_bHide = true;
			base.gameObject.SetActive(this.m_bOpenDeckView);
			this.m_bOpenDeckView = false;
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
			this.m_unitMgr.Hide();
			this.m_NKCWarfareGameHUD.Close();
			NKCCamera.EnableBlur(true, 2f, 2);
		}

		// Token: 0x06007F6F RID: 32623 RVA: 0x002ABF88 File Offset: 0x002AA188
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE, true);
			this.m_NKCWarfareGameHUD.Open();
			if (this.GetNKMWarfareGameData().warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				this.m_NKCWarfareGameHUD.SetBatchedShipCount(this.m_unitMgr.GetCurrentUserUnit(true));
			}
			NKCCamera.EnableBlur(false, 2f, 2);
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x002ABFE2 File Offset: 0x002AA1E2
		private void OnStartCutScenEnd()
		{
			NKCUIManager.UpdateUpsideMenu();
			base.gameObject.SetActive(true);
			this.m_NKCWarfareGameHUD.Open();
			this.PrepareWarfareGameReady();
		}

		// Token: 0x06007F71 RID: 32625 RVA: 0x002AC006 File Offset: 0x002AA206
		private void SetEnemyTurnUI()
		{
			Debug.Log("warfare show Enemy Turn");
			this.m_NKCWarfareGameHUD.TriggerEnemyTurnUI();
			this.m_NKCWarfareGameHUD.SetPhaseUserType(false);
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x002AC029 File Offset: 0x002AA229
		private void OnCommonUserTurnFinished(bool bTileEffectShow = false)
		{
			this.m_NKCWarfareGameHUD.SetActiveTurnFinishBtn(false);
			if (bTileEffectShow)
			{
				this.m_bWaitEnemyTurn = true;
				this.m_fElapsedTimeForEnemyTurn = 1f;
			}
			else
			{
				this.SetEnemyTurnUI();
			}
			this.InvalidSelectedUnitPure();
		}

		// Token: 0x06007F73 RID: 32627 RVA: 0x002AC05C File Offset: 0x002AA25C
		private void OnStartEnemyPhase()
		{
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				Debug.Log("warfare next order REQ fail - State : " + nkmwarfareGameData.warfareGameState.ToString());
				return;
			}
			if (nkmwarfareGameData.isTurnA)
			{
				Debug.Log("warfare next order REQ fail - Turn A is true");
				return;
			}
			Debug.Log("warfare next order REQ - OnStartEnemyPhase");
			this.SendGetNextOrderREQ();
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x002AC0C0 File Offset: 0x002AA2C0
		private void OnStartUserPhase()
		{
			if (!this.IsAutoWarfare())
			{
				return;
			}
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				Debug.Log("warfare next order REQ fail - State : " + nkmwarfareGameData.warfareGameState.ToString());
				return;
			}
			if (!nkmwarfareGameData.isTurnA)
			{
				Debug.Log("warfare next order REQ fail - Turn A is false");
				return;
			}
			Debug.Log("warfare next order REQ - OnStartUserPhase");
			this.SendGetNextOrderREQ();
		}

		// Token: 0x06007F75 RID: 32629 RVA: 0x002AC12C File Offset: 0x002AA32C
		private int GetSelectedSquadWFGUUID()
		{
			NKCWarfareGameUnit wfgameUnitByDeckIndex = this.m_unitMgr.GetWFGameUnitByDeckIndex(this.m_NKCWarfareGameHUD.GetNKMDeckIndexSelected());
			if (wfgameUnitByDeckIndex != null && wfgameUnitByDeckIndex.GetNKMWarfareUnitData() != null)
			{
				return wfgameUnitByDeckIndex.GetNKMWarfareUnitData().warfareGameUnitUID;
			}
			return -1;
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x002AC170 File Offset: 0x002AA370
		private void SendGetNextOrderREQ(NKCWarfareGameUnit cNKCWarfareGameUnit)
		{
			if (cNKCWarfareGameUnit != null && cNKCWarfareGameUnit.GetNKMWarfareUnitData() != null)
			{
				if (cNKCWarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
				{
					if (!this.GetNKMWarfareGameData().isTurnA)
					{
						return;
					}
				}
				else if (this.GetNKMWarfareGameData().isTurnA)
				{
					return;
				}
			}
			Debug.Log("warfare next order REQ - SendGetNextOrderREQ(NKCWarfareGameUnit");
			this.SendGetNextOrderREQ();
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x002AC1C8 File Offset: 0x002AA3C8
		private void SendGetNextOrderREQ()
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCWarfareManager.CheckGetNextOrderCond(NKCScenManager.GetScenManager().GetMyUserData());
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				Debug.LogWarning("warfare next order NKM_ERROR_CODE : " + nkm_ERROR_CODE.ToString());
				return;
			}
			if (this.m_bWaitingNextOreder)
			{
				Debug.LogWarning("warfare next order is ignored by waiting prev order");
				return;
			}
			this.m_bWaitingNextOreder = true;
			this.SetPause(true);
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ();
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x002AC22B File Offset: 0x002AA42B
		private void SendGetNextOrderREQIfAuto(NKCWarfareGameUnit cNKCWarfareGameUnit)
		{
			if (this.IsAutoWarfare())
			{
				Debug.Log("warfare next order REQ - SendGetNextOrderREQIfAuto");
				this.SendGetNextOrderREQ(cNKCWarfareGameUnit);
			}
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x002AC246 File Offset: 0x002AA446
		private void OnCallBackForResult(NKM_SCEN_ID scenID)
		{
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x002AC254 File Offset: 0x002AA454
		private WarfareGameData GetNKMWarfareGameData()
		{
			return NKCScenManager.GetScenManager().WarfareGameData;
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x002AC260 File Offset: 0x002AA460
		private void ProcessWarfareGameSyncData(WarfareSyncData syncData)
		{
			if (syncData == null)
			{
				return;
			}
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			bool flag = false;
			WarfareGameSyncData gameState = syncData.gameState;
			if (gameState != null)
			{
				bool flag2 = false;
				if (!nkmwarfareGameData.isTurnA && gameState.isTurnA)
				{
					if (gameState.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_RESULT)
					{
						this.m_NKCWarfareGameHUD.TriggerPlayerTurnUI();
						this.m_NKCWarfareGameHUD.SetPhaseUserType(true);
						this.m_NKCWarfareGameHUD.SetActiveTurnFinishBtn(!this.IsAutoWarfare());
						Debug.Log("warfare my turn show");
					}
					else
					{
						flag2 = true;
					}
				}
				else if (nkmwarfareGameData.isTurnA && !gameState.isTurnA)
				{
					this.OnCommonUserTurnFinished(syncData.updatedUnits.Count + syncData.newUnits.Count > 0);
					flag = true;
				}
				if (nkmwarfareGameData.turnCount != gameState.turnCount)
				{
					this.m_NKCWarfareGameHUD.SetTurnCount(gameState.turnCount);
				}
				nkmwarfareGameData.UpdateGameState(gameState);
				if (nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY)
				{
					this.TryGameLoadReq();
				}
				else if (nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT)
				{
					if (gameState.isWinTeamA)
					{
						if (!NKCScenManager.GetScenManager().GetMyUserData().CheckWarfareClear(nkmwarfareGameData.warfareTempletID) && !NKCUtil.m_sHsFirstClearWarfare.Contains(nkmwarfareGameData.warfareTempletID))
						{
							NKCUtil.m_sHsFirstClearWarfare.Add(nkmwarfareGameData.warfareTempletID);
						}
						if (flag2)
						{
							this.ShowEndCutWhenOpened();
						}
					}
					else if (flag2)
					{
						Debug.Log("warfare next order REQ - ProcessWarfareGameSyncData bShowResult");
						this.SendGetNextOrderREQ();
					}
				}
			}
			for (int i = 0; i < syncData.updatedUnits.Count; i++)
			{
				WarfareUnitSyncData warfareUnitSyncData = syncData.updatedUnits[i];
				WarfareUnitData unitData = nkmwarfareGameData.GetUnitData(warfareUnitSyncData.warfareGameUnitUID);
				if (unitData != null)
				{
					if (flag && unitData.unitType == WarfareUnitData.Type.User)
					{
						this.m_unitMgr.ShowUserUnitTileFX(unitData, warfareUnitSyncData);
					}
					bool flag3 = !unitData.isTurnEnd && unitData.isTurnEnd != warfareUnitSyncData.isTurnEnd;
					bool flag4 = syncData.movedUnits.Exists((WarfareSyncData.MovedUnit v) => v.unitUID == unitData.warfareGameUnitUID);
					nkmwarfareGameData.UpdateUnitData(unitData, warfareUnitSyncData);
					if (flag3 && !flag4)
					{
						NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(unitData.warfareGameUnitUID);
						if (nkcwarfareGameUnit != null)
						{
							if (unitData.unitType == WarfareUnitData.Type.User)
							{
								if (syncData.movedUnits.Count == 0 && syncData.retreaters.Count == 0)
								{
									nkcwarfareGameUnit.SetTurnEndTimer(new NKCWarfareGameUnit.OnTimeEndCallback(this.SendGetNextOrderREQIfAuto));
								}
							}
							else
							{
								NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_unitMgr.GetNKCWarfareGameUnitInfo(unitData.warfareGameUnitUID);
								if (nkcwarfareGameUnitInfo != null && nkcwarfareGameUnitInfo.IsMovableActionType())
								{
									if (syncData.movedUnits.Count == 0 && syncData.retreaters.Count == 0)
									{
										nkcwarfareGameUnit.Move(this.m_listUnitPos[(int)unitData.tileIndex], 0.9f, new NKCWarfareUnitMover.OnCompleteMove(this.SendGetNextOrderREQ), true);
									}
									else
									{
										nkcwarfareGameUnit.Move(this.m_listUnitPos[(int)unitData.tileIndex], 0.9f, null, true);
									}
								}
							}
						}
					}
					this.m_unitMgr.UpdateGameUnitUI(warfareUnitSyncData.warfareGameUnitUID);
				}
			}
			for (int j = 0; j < syncData.newUnits.Count; j++)
			{
				WarfareUnitData newUnit = syncData.newUnits[j];
				if (newUnit.unitType == WarfareUnitData.Type.User)
				{
					Vector3 localPosition = this.m_listUnitPos[(int)newUnit.tileIndex];
					WarfareTeamData warfareTeamDataA = nkmwarfareGameData.warfareTeamDataA;
					if (this.m_NUM_WARFARE_FX_SHIP_DIVE != null)
					{
						this.m_NUM_WARFARE_FX_SHIP_DIVE.gameObject.transform.localPosition = localPosition;
						this.m_NUM_WARFARE_FX_SHIP_DIVE.SetActive(false);
						this.m_NUM_WARFARE_FX_SHIP_DIVE.SetActive(true);
					}
					WarfareUnitData warfareUnitData = warfareTeamDataA.warfareUnitDataByUIDMap.Values.ToList<WarfareUnitData>().Find((WarfareUnitData v) => v.deckIndex.Compare(newUnit.deckIndex));
					if (warfareUnitData != null)
					{
						warfareTeamDataA.warfareUnitDataByUIDMap.Remove(warfareUnitData.warfareGameUnitUID);
					}
					warfareTeamDataA.warfareUnitDataByUIDMap.Add(newUnit.warfareGameUnitUID, newUnit);
					NKCWarfareGameUnit nkcwarfareGameUnit2 = this.m_unitMgr.CreateNewUserUnit(newUnit.deckIndex, newUnit.tileIndex, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), newUnit, 0L);
					if (nkcwarfareGameUnit2 != null)
					{
						nkcwarfareGameUnit2.gameObject.transform.localPosition = localPosition;
						nkcwarfareGameUnit2.transform.DOMove(this.BATCH_EFFECT_POS, 1.5f, false).SetEase(Ease.OutCubic).From(true);
						if (this.m_containerMgr.IsItem(nkcwarfareGameUnit2.TileIndex))
						{
							this.PlayGetContainer(nkcwarfareGameUnit2.TileIndex, false);
						}
					}
					NKCOperatorUtil.PlayVoice(newUnit.deckIndex, VOICE_TYPE.VT_SHIP_RECALL, true);
				}
				else
				{
					NKCWarfareGameUnit nkcwarfareGameUnit3 = this.m_unitMgr.CreateNewEnemyUnit(NKMDungeonManager.GetDungeonStrID(newUnit.dungeonID), nkmwarfareGameData.warfareTeamDataB.flagShipWarfareUnitUID == newUnit.warfareGameUnitUID, newUnit.isTarget, newUnit.tileIndex, newUnit.warfareEnemyActionType, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), newUnit);
					WarfareTeamData warfareTeamDataB = nkmwarfareGameData.warfareTeamDataB;
					if (!warfareTeamDataB.warfareUnitDataByUIDMap.ContainsKey(newUnit.warfareGameUnitUID))
					{
						warfareTeamDataB.warfareUnitDataByUIDMap.Add(newUnit.warfareGameUnitUID, newUnit);
					}
					else
					{
						warfareTeamDataB.warfareUnitDataByUIDMap[newUnit.warfareGameUnitUID] = newUnit;
					}
					if (nkcwarfareGameUnit3 != null)
					{
						nkcwarfareGameUnit3.gameObject.transform.localPosition = this.m_listUnitPos[(int)newUnit.tileIndex];
						nkcwarfareGameUnit3.PlayEnemySpawnAni();
					}
				}
			}
			bool flag5 = false;
			List<int> list = new List<int>();
			for (int k = 0; k < syncData.movedUnits.Count; k++)
			{
				WarfareSyncData.MovedUnit movedUnit = syncData.movedUnits[k];
				int unitUID = movedUnit.unitUID;
				short tileIndex = movedUnit.tileIndex;
				NKCWarfareGameUnit nkcwarfareGameUnit4 = this.m_unitMgr.GetNKCWarfareGameUnit(unitUID);
				if (!(nkcwarfareGameUnit4 == null))
				{
					int tileIndex2 = nkcwarfareGameUnit4.TileIndex;
					bool flag6 = false;
					NKCWarfareGameUnit gameUnitByTileIndex = this.m_unitMgr.GetGameUnitByTileIndex((int)tileIndex);
					if (gameUnitByTileIndex != null)
					{
						flag6 = nkmwarfareGameData.CheckTeamA_By_GameUnitUID(gameUnitByTileIndex.GetNKMWarfareUnitData().warfareGameUnitUID);
					}
					nkcwarfareGameUnit4.GetNKMWarfareUnitData().tileIndex = tileIndex;
					bool flag7 = nkmwarfareGameData.CheckTeamA_By_GameUnitUID(unitUID);
					if (gameUnitByTileIndex != null && flag6 != flag7)
					{
						this.InvalidSelectedUnitPure();
						Vector3 vector = this.m_listUnitPos[(int)tileIndex];
						Vector3 localPosition2 = gameUnitByTileIndex.gameObject.transform.localPosition;
						if (nkcwarfareGameUnit4.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
						{
							vector.x -= 100f;
							localPosition2.x += 100f;
						}
						else
						{
							vector.x += 100f;
							localPosition2.x -= 100f;
						}
						bool flag8 = syncData.retreaters.Contains(unitUID);
						bool flag9 = syncData.retreaters.Contains(gameUnitByTileIndex.GetNKMWarfareUnitData().warfareGameUnitUID);
						if (!flag8 && !flag9)
						{
							nkcwarfareGameUnit4.Move(vector, 0.9f, new NKCWarfareUnitMover.OnCompleteMove(this.SendGetNextOrderREQ), false);
							gameUnitByTileIndex.Move(localPosition2, 0.9f, null, false);
							gameUnitByTileIndex.SetPause(this.GetPause());
						}
						else if ((flag8 && !flag9) || (flag9 && !flag8))
						{
							if (flag8 && !flag9)
							{
								nkcwarfareGameUnit4.GetNKMWarfareUnitData().hp = 0f;
								this.RetreatUnit(nkcwarfareGameUnit4, gameUnitByTileIndex, vector, localPosition2);
								list.Add(unitUID);
							}
							else
							{
								gameUnitByTileIndex.GetNKMWarfareUnitData().hp = 0f;
								this.RetreatUnit(gameUnitByTileIndex, nkcwarfareGameUnit4, localPosition2, vector);
								list.Add(gameUnitByTileIndex.GetNKMWarfareUnitData().warfareGameUnitUID);
							}
						}
						else
						{
							Log.Error(string.Format("[전역 후퇴] m? {0}, t? {1}", flag8, flag9), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCWarfareGame.cs", 1271);
						}
					}
					else if (nkcwarfareGameUnit4.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.Dungeon)
					{
						nkcwarfareGameUnit4.Move(this.m_listUnitPos[(int)tileIndex], 0.9f, new NKCWarfareUnitMover.OnCompleteMove(this.SendGetNextOrderREQ), false);
						this.m_containerMgr.SetPos((int)tileIndex, true);
						this.m_containerMgr.SetPos(tileIndex2, false);
					}
					else
					{
						if (this.m_LastClickedUnitTileIndex != -1 && !flag5 && this.m_LastClickedUnitTileIndex != (int)tileIndex)
						{
							this.m_LastClickedUnitTileIndex = (int)tileIndex;
							flag5 = true;
						}
						if (gameUnitByTileIndex != null)
						{
							nkcwarfareGameUnit4.Move(this.m_listUnitPos[(int)tileIndex], 0.9f, null, false);
						}
						else
						{
							nkcwarfareGameUnit4.Move(this.m_listUnitPos[(int)tileIndex], 0.9f, new NKCWarfareUnitMover.OnCompleteMove(this.DoWhenUnitMoveEnd), false);
						}
					}
					nkcwarfareGameUnit4.SetPause(this.GetPause());
					this.CamTrackMovingUnit(nkcwarfareGameUnit4.transform.localPosition, this.m_listUnitPos[(int)tileIndex]);
				}
			}
			for (int l = 0; l < syncData.retreaters.Count; l++)
			{
				int num = syncData.retreaters[l];
				if (!list.Contains(num))
				{
					NKCWarfareGameUnit nkcwarfareGameUnit5 = this.m_unitMgr.GetNKCWarfareGameUnit(num);
					if (!(nkcwarfareGameUnit5 == null))
					{
						nkcwarfareGameUnit5.GetNKMWarfareUnitData().hp = 0f;
						this.PlayRetreatUnit(nkcwarfareGameUnit5);
						break;
					}
				}
			}
			for (int m = 0; m < syncData.tiles.Count; m++)
			{
				WarfareTileData warfareTileData = syncData.tiles[m];
				int index = (int)warfareTileData.index;
				WarfareTileData tileData = nkmwarfareGameData.GetTileData(index);
				if (tileData != null)
				{
					if (warfareTileData.tileType != tileData.tileType)
					{
						tileData.tileType = warfareTileData.tileType;
					}
					if (warfareTileData.battleConditionId != tileData.battleConditionId)
					{
						tileData.battleConditionId = warfareTileData.battleConditionId;
					}
				}
			}
			this.DoAfterSync();
			this.CheckTutorialAfterSync();
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x002ACCA0 File Offset: 0x002AAEA0
		private void DoAfterSync()
		{
			this.SetTileDefaultWhenPlay();
			this.UpdateLabel(true);
			this.m_unitMgr.UpdateGameUnitUI();
			this.m_NKCWarfareGameHUD.SetRemainTurnOnUnitCount(this.m_unitMgr.GetRemainTurnOnUserUnitCount());
			this.m_unitMgr.ResetIcon(0);
			this.CloseAssistFX();
			this.UpdateSelectedSquadUI();
			this.UpdateRecoveryCount();
			this.CancelRecovery();
		}

		// Token: 0x06007F7D RID: 32637 RVA: 0x002ACD00 File Offset: 0x002AAF00
		private void DoWhenUnitMoveEnd(NKCWarfareGameUnit cNKCWarfareGameUnit)
		{
			Debug.Log("MoveEnd call Log");
			if (cNKCWarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && this.m_containerMgr.IsItem(cNKCWarfareGameUnit.TileIndex))
			{
				this.PlayGetContainer(cNKCWarfareGameUnit.TileIndex, false);
			}
			if (this.CheckEnterUnit(cNKCWarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID))
			{
				this.ShowEndCutWhenOpened();
				return;
			}
			this.SendGetNextOrderREQIfAuto(cNKCWarfareGameUnit);
		}

		// Token: 0x06007F7E RID: 32638 RVA: 0x002ACD68 File Offset: 0x002AAF68
		private void RetreatUnit(NKCWarfareGameUnit retreatUnit, NKCWarfareGameUnit tileUnit, Vector3 retreatPos, Vector3 tilePos)
		{
			retreatUnit.Move(retreatPos, 0.9f, new NKCWarfareUnitMover.OnCompleteMove(this.PlayRetreatUnit), false);
			retreatUnit.SetPause(this.GetPause());
			tileUnit.Move(tilePos, 0.9f, null, false);
			tileUnit.SetPause(this.GetPause());
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x002ACDB5 File Offset: 0x002AAFB5
		private void PlayRetreatUnit(NKCWarfareGameUnit unit)
		{
			this.m_WarfareGameUnitToDie = unit;
			this.PlayUnitRunawayAni();
			this.m_fElapsedTimeForShakeEnd = 0f;
			this.m_bReservedCallOnUnitShakeEnd = true;
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x002ACDD8 File Offset: 0x002AAFD8
		private void PlayGetContainer(int index, bool bWithEnemy)
		{
			this.m_containerMgr.Set(index, WARFARE_ITEM_STATE.Recv, this.m_listUnitPos[index], bWithEnemy);
			Vector3 position = NKCCamera.GetCamera().WorldToScreenPoint(this.m_containerMgr.GetWorldPos(index));
			Vector3 itemPos = NKCCamera.GetSubUICamera().ScreenToWorldPoint(position);
			this.m_NKCWarfareGameHUD.PlayGetContainer(itemPos, (int)this.GetNKMWarfareGameData().containerCount);
			NKCSoundManager.PlaySound("FX_UI_WARFARE_GET_CONTANIER", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x002ACE60 File Offset: 0x002AB060
		public void SetActiveAutoOnOff(bool bAuto, bool bAutoSupply)
		{
			this.m_NKCWarfareGameHUD.SetActiveAutoOnOff(bAuto, bAutoSupply);
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData == null)
			{
				return;
			}
			if (nkmwarfareGameData.isTurnA)
			{
				if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
				{
					this.m_NKCWarfareGameHUD.SetActiveTurnFinishBtn(!bAuto);
				}
				if (nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING && bAuto && !this.m_unitMgr.CheckExistMovingUserUnit() && !this.m_NKCWarfareGameHUD.CheckVisibleWarfareStateEffectUI())
				{
					Debug.Log("warfare next order REQ - SetActiveAutoOnOff");
					this.SendGetNextOrderREQ(null);
				}
				this.UpdateRecoveryCount();
			}
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x002ACEE4 File Offset: 0x002AB0E4
		private void Update()
		{
			if (base.IsOpen)
			{
				if (this.m_bReservedBattle)
				{
					if (!this.GetPause())
					{
						this.m_fElapsedTimeToBattle += Time.deltaTime;
					}
					if (this.m_fElapsedTimeToBattle >= 0.6f)
					{
						this.m_bReservedBattle = false;
						WarfareUnitData unitData = this.GetNKMWarfareGameData().GetUnitData(this.GetNKMWarfareGameData().battleMonsterUid);
						if (unitData != null)
						{
							NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
							if (nkmwarfareTemplet != null)
							{
								NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(0, nkmwarfareTemplet.StageTemplet.Key, 0, NKMDungeonManager.GetDungeonStrID(unitData.dungeonID), 0, false, 1, 0);
							}
						}
					}
				}
				if (this.m_bWaitEnemyTurn)
				{
					this.m_fElapsedTimeForEnemyTurn -= Time.deltaTime;
					if (this.m_fElapsedTimeForEnemyTurn <= 0f)
					{
						this.m_fElapsedTimeForEnemyTurn = 0f;
						this.m_bWaitEnemyTurn = false;
						this.SetEnemyTurnUI();
					}
				}
				if (this.m_bReservedCallOnUnitShakeEnd)
				{
					this.m_fElapsedTimeForShakeEnd += Time.deltaTime;
					if (this.m_fElapsedTimeForShakeEnd >= 2.6999998f)
					{
						this.OnUnitShakeEnd();
					}
				}
				if (this.m_bPlayingIntro && !NKCCamera.GetTrackingPos().IsTracking())
				{
					this.m_bPlayingIntro = false;
				}
			}
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x002AD003 File Offset: 0x002AB203
		private void SetActiveForDeckView(bool bAcitve)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this, bAcitve);
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x002AD01B File Offset: 0x002AB21B
		private void OnSetFlagShip(int gameUnitUID)
		{
			this.m_unitMgr.SetUserFlagShip(gameUnitUID, true);
		}

		// Token: 0x06007F85 RID: 32645 RVA: 0x002AD02C File Offset: 0x002AB22C
		private void UpdateAttackCostUI()
		{
			int num = 0;
			int num2 = 0;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet.StageTemplet != null && nkmwarfareTemplet.StageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(nkmwarfareTemplet.StageTemplet.Key, false, false, false);
				int enterLimit = nkmwarfareTemplet.StageTemplet.EnterLimit;
				if (statePlayCnt >= enterLimit && nkmwarfareTemplet.StageTemplet.RestoreLimit > 0)
				{
					num = nkmwarfareTemplet.StageTemplet.RestoreReqItem.ItemId;
					num2 = nkmwarfareTemplet.StageTemplet.RestoreReqItem.Count32;
				}
			}
			if (num == 0 || num2 == 0)
			{
				NKCWarfareManager.GetCurrWarfareAttackCost(out num, out num2);
			}
			this.m_NKCWarfareGameHUD.SetAttackCost(num, num2);
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x002AD0D4 File Offset: 0x002AB2D4
		private void OpenUserUnitInfoPopup(NKCWarfareGameUnit cNKCWarfareGameUnit, bool bPlaying = false)
		{
			if (cNKCWarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
			{
				if (!cNKCWarfareGameUnit.IsSupporter)
				{
					NKMDeckIndex deckIndex = cNKCWarfareGameUnit.GetNKMWarfareUnitData().deckIndex;
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(deckIndex);
					if (deckData != null)
					{
						NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
						if (shipFromUID != null)
						{
							if (bPlaying)
							{
								NKCPopupWarfareSelectShip nkcpopupWarfareSelectShip = this.NKCPopupWarfareSelectShip;
								if (nkcpopupWarfareSelectShip == null)
								{
									return;
								}
								nkcpopupWarfareSelectShip.OpenForMyShipInWarfare(deckIndex, cNKCWarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID, shipFromUID.m_UnitID);
								return;
							}
							else
							{
								NKCPopupWarfareSelectShip nkcpopupWarfareSelectShip2 = this.NKCPopupWarfareSelectShip;
								if (nkcpopupWarfareSelectShip2 == null)
								{
									return;
								}
								nkcpopupWarfareSelectShip2.OpenForMyShipInWarfare(deckIndex, cNKCWarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID, shipFromUID.m_UnitID, new NKCPopupWarfareSelectShip.OnSetFlagShipButton(this.OnSetFlagShip), new NKCPopupWarfareSelectShip.OnCancelBatchButton(this.OnCancelBatch), new NKCPopupWarfareSelectShip.OnDeckViewBtn(this.OnDeckViewBtn));
								return;
							}
						}
					}
				}
				else
				{
					if (bPlaying)
					{
						this.NKCPopupWarfareSelectShip.OpenForSupporterInWarfare(this.GetNKMWarfareGameData().supportUnitData, cNKCWarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID, null);
						return;
					}
					this.NKCPopupWarfareSelectShip.OpenForSupporterInWarfare(this.GetNKMWarfareGameData().supportUnitData, cNKCWarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID, new NKCPopupWarfareSelectShip.OnCancelBatchButton(this.OnCancelBatch));
				}
			}
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x002AD1FE File Offset: 0x002AB3FE
		private void InvalidSelectedUnitPure()
		{
			this.m_LastClickedUnitTileIndex = -1;
			this.m_NKCWarfareGameHUD.CloseSelectedSquadUI();
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x002AD214 File Offset: 0x002AB414
		public void InvalidSelectedUnit()
		{
			if (this.GetNKMWarfareGameData().warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				return;
			}
			if (!this.GetNKMWarfareGameData().isTurnA)
			{
				return;
			}
			if (this.IsAutoWarfare())
			{
				return;
			}
			this.InvalidSelectedUnitPure();
			this.SetTileDefaultWhenPlay();
			this.m_unitMgr.ResetIcon(0);
			this.CloseAssistFX();
			this.CancelRecovery();
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x002AD26C File Offset: 0x002AB46C
		private void SetTileDefaultWhenPlay()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			for (int i = 0; i < warfareGameData.warfareTileDataList.Count; i++)
			{
				WarfareTileData tileData = warfareGameData.GetTileData(i);
				if (tileData != null)
				{
					NKMWarfareTileTemplet tile = mapTemplet.GetTile(i);
					NKCWarfareGameTile tile2 = this.m_tileMgr.GetTile(i);
					if (!(tile2 == null) && tile != null)
					{
						if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
						{
							tile2.SetDummyActive(false);
							this.m_battleCondition.RemoveBattleCondition(i);
						}
						else
						{
							tile2.SetTileLayer1Type(tileData.tileType);
							tile2.SetTileLayer2Type(tile.m_TileWinType, tile.m_TileLoseType);
							WarfareUnitData unitDataByTileIndex = warfareGameData.GetUnitDataByTileIndex(i);
							if (unitDataByTileIndex == null)
							{
								tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_NORMAL);
							}
							else if (warfareGameData.CheckTeamA_By_GameUnitUID(unitDataByTileIndex.warfareGameUnitUID))
							{
								if (!warfareGameData.isTurnA)
								{
									tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED);
								}
								else if (unitDataByTileIndex.isTurnEnd)
								{
									if (this.m_LastClickedUnitTileIndex == i)
									{
										tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED_SELECTED);
									}
									else
									{
										tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED);
									}
								}
								else
								{
									tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT);
								}
							}
							else
							{
								tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_ENEMY);
							}
							this.m_battleCondition.SetBattleCondition(i, tileData.battleConditionId, this.m_listUnitPos[i]);
						}
					}
				}
			}
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x002AD3D4 File Offset: 0x002AB5D4
		private void UpdateLabel(bool bPlaying = true)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			this.m_labelMgr.HideAll();
			for (int i = 0; i < mapTemplet.TileCount; i++)
			{
				NKMWarfareTileTemplet tile = mapTemplet.GetTile(i);
				WarfareTileData tileData = warfareGameData.GetTileData(i);
				if (tile != null && tileData != null)
				{
					if (bPlaying)
					{
						if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
						{
							goto IL_12A;
						}
						if (tile.m_TileWinType == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD)
						{
							this.m_labelMgr.SetLabel(i, WARFARE_LABEL_TYPE.HOLD, this.m_listUnitPos[i]);
							int count = nkmwarfareTemplet.m_WFWinValue - warfareGameData.holdCount;
							this.m_labelMgr.SetText(i, count);
						}
					}
					NKM_WARFARE_MAP_TILE_TYPE tileType;
					if (bPlaying)
					{
						tileType = tileData.tileType;
					}
					else
					{
						tileType = tile.m_TileType;
					}
					if (tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_INCR && tile.m_SummonCondition == WARFARE_SUMMON_CONDITION.PHASE)
					{
						int count2;
						if (bPlaying)
						{
							int num = warfareGameData.turnCount % (int)tile.m_SummonConditionValue;
							count2 = (int)tile.m_SummonConditionValue - num;
							if (num == 0)
							{
								count2 = 0;
							}
						}
						else
						{
							count2 = (int)(tile.m_SummonConditionValue - 1);
						}
						this.m_labelMgr.SetLabel(i, WARFARE_LABEL_TYPE.SUMMON, this.m_listUnitPos[i]);
						this.m_labelMgr.SetText(i, count2);
					}
				}
				IL_12A:;
			}
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x002AD51C File Offset: 0x002AB71C
		private void SetTilePossibleArrival_(int i, int j, NKMWarfareMapTemplet cNKMWarfareMapTemplet, int selectedTileIndex, bool bRetreat)
		{
			int indexByPos = (int)cNKMWarfareMapTemplet.GetIndexByPos(i, j);
			if (indexByPos == -1)
			{
				return;
			}
			NKMWarfareTileTemplet tile = cNKMWarfareMapTemplet.GetTile(indexByPos);
			if (tile == null)
			{
				return;
			}
			WarfareTileData tileData = this.GetNKMWarfareGameData().GetTileData(indexByPos);
			if (tileData == null)
			{
				return;
			}
			if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
			{
				return;
			}
			this.m_tileMgr.SetTileLayer0Type(indexByPos, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_POSSIBLE_ARRIVAL);
			NKCWarfareGameUnit gameUnitByTileIndex = this.m_unitMgr.GetGameUnitByTileIndex(indexByPos);
			if (gameUnitByTileIndex == null)
			{
				return;
			}
			if (gameUnitByTileIndex.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.Dungeon)
			{
				gameUnitByTileIndex.SetAttackIcon(true, bRetreat);
				using (IEnumerator<short> enumerator = tile.NeighborTiles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						short num = enumerator.Current;
						if ((int)num != selectedTileIndex && (int)num != indexByPos)
						{
							NKCWarfareGameUnitInfo wfgameUnitInfoByTileIndex = this.m_unitMgr.GetWFGameUnitInfoByTileIndex((int)num);
							if (!(wfgameUnitInfoByTileIndex == null) && wfgameUnitInfoByTileIndex.GetNKMWarfareUnitData().unitType != WarfareUnitData.Type.Dungeon)
							{
								wfgameUnitInfoByTileIndex.SetBattleAssistIcon(true);
								this.AddAssistFX(this.m_listUnitPos[(int)num], this.m_listUnitPos[indexByPos]);
							}
						}
					}
					return;
				}
			}
			gameUnitByTileIndex.SetChangeIcon(true);
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x002AD63C File Offset: 0x002AB83C
		private void SetTilePossibleArrival(int selectedTileIndex, NKM_UNIT_STYLE_TYPE eNKM_UNIT_STYLE_TYPE, bool bRetreat)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			int posXByIndex = mapTemplet.GetPosXByIndex(selectedTileIndex);
			int posYByIndex = mapTemplet.GetPosYByIndex(selectedTileIndex);
			MovableTileSet tileSet = MovableTileSet.GetTileSet(eNKM_UNIT_STYLE_TYPE);
			int num = 0;
			int num2 = 2;
			for (int i = posXByIndex - num2; i <= posXByIndex + num2; i++)
			{
				int num3 = 0;
				for (int j = posYByIndex - num2; j <= posYByIndex + num2; j++)
				{
					if (tileSet[num3, num])
					{
						this.SetTilePossibleArrival_(i, j, mapTemplet, selectedTileIndex, bRetreat);
					}
					num3++;
				}
				num++;
			}
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x002AD6D8 File Offset: 0x002AB8D8
		private bool CheckWFGameStartCond(out NKMPacket_WARFARE_GAME_START_REQ startReq)
		{
			startReq = new NKMPacket_WARFARE_GAME_START_REQ
			{
				warfareTempletID = NKCWarfareManager.GetWarfareID(this.m_WarfareStrID)
			};
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return false;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return false;
			}
			this.m_unitMgr.OnClickGameStart(startReq, mapTemplet);
			this.m_unitMgr.ResetAllDeckState();
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCWarfareManager.CheckWFGameStartCond(NKCScenManager.GetScenManager().GetMyUserData(), startReq);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), null, "");
				return false;
			}
			startReq.rewardMultiply = this.m_NKCWarfareGameHUD.GetCurrMultiplyRewardCount();
			return true;
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x002AD780 File Offset: 0x002AB980
		private void reqGameStart()
		{
			NKMPacket_WARFARE_GAME_START_REQ req;
			if (!this.CheckWFGameStartCond(out req))
			{
				return;
			}
			if (this.m_unitMgr != null)
			{
				this.m_unitMgr.OnStartGameVoice();
			}
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_START_REQ(req);
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x002AD7B4 File Offset: 0x002AB9B4
		private void SetPlayingCommonUI(bool bStart = false)
		{
			this.m_NKCWarfareGameHUD.Open();
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			this.m_NKCWarfareGameHUD.SetActiveTitle(false);
			this.m_NKCWarfareGameHUD.SetActiveBatchGuideText(false);
			this.m_NKCWarfareGameHUD.SetActiveBatchSupportGuideText(false);
			this.m_NKCWarfareGameHUD.SetActiveOperationBtn(false);
			this.m_NKCWarfareGameHUD.SetActiveTurnFinishBtn(warfareGameData.isTurnA && !this.IsAutoWarfare());
			this.m_NKCWarfareGameHUD.SetActiveBatchCountUI(false);
			this.m_NKCWarfareGameHUD.SetActivePhase(true);
			this.m_NKCWarfareGameHUD.SetActiveDeco(false);
			this.m_NKCWarfareGameHUD.SetActiveAuto(this.IsAutoVisible(), this.IsAutoUsable());
			this.m_NKCWarfareGameHUD.SetActiveRepeatOperation(this.IsRepeatOperationVisible());
			this.m_NKCWarfareGameHUD.SetActiveAutoOnOff(this.IsAutoWarfare(), this.IsAutoWarfareSupply());
			this.m_NKCWarfareGameHUD.SetActivePause(true);
			this.m_NKCWarfareGameHUD.SetTurnCount(warfareGameData.turnCount);
			this.m_NKCWarfareGameHUD.SetPhaseUserType(warfareGameData.isTurnA);
			this.m_NKCWarfareGameHUD.HideOperationEnterLimit();
			this.m_NKCWarfareGameHUD.UpdateMultiplyUI();
			this.SetBG(false, bStart);
			base.UpdateUpsideMenu();
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x002AD8DC File Offset: 0x002ABADC
		private void PrepareWarfareGameIntrude()
		{
			this.PlayWarfareOnGoingMusic();
			this.SetPlayingCommonUI(false);
			this.m_NKCWarfareGameHUD.SetUpperRightMenuPosition(true);
			NKCCamera.GetCamera().orthographic = false;
			NKCCamera.SetPos(0f, -308f, (float)this.GetFinalCameraZDist(), true, true);
			NKCCamera.GetTrackingRotation().SetNowValue(-20f, 0f, 0f);
			this.DoWhenMapSizeCalculated();
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			if (nkmwarfareTemplet.MapTemplet == null)
			{
				return;
			}
			List<WarfareUnitData> unitDataList = warfareGameData.GetUnitDataList();
			for (int i = 0; i < unitDataList.Count; i++)
			{
				WarfareUnitData warfareUnitData = unitDataList[i];
				if (warfareUnitData.hp > 0f)
				{
					if (warfareGameData.CheckTeamA_By_GameUnitUID(warfareUnitData.warfareGameUnitUID))
					{
						NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.CreateNewUserUnit(warfareUnitData.deckIndex, warfareUnitData.tileIndex, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), warfareUnitData, warfareUnitData.friendCode);
						if (nkcwarfareGameUnit != null)
						{
							nkcwarfareGameUnit.gameObject.transform.localPosition = this.m_listUnitPos[(int)warfareUnitData.tileIndex];
							nkcwarfareGameUnit.SetNKMWarfareUnitData(warfareUnitData);
						}
					}
					else
					{
						NKCWarfareGameUnit nkcwarfareGameUnit2 = this.m_unitMgr.CreateNewEnemyUnit(NKMDungeonManager.GetDungeonStrID(warfareUnitData.dungeonID), warfareGameData.warfareTeamDataB.flagShipWarfareUnitUID == warfareUnitData.warfareGameUnitUID, warfareUnitData.isTarget, warfareUnitData.tileIndex, warfareUnitData.warfareEnemyActionType, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), warfareUnitData);
						if (nkcwarfareGameUnit2 != null)
						{
							nkcwarfareGameUnit2.gameObject.transform.localPosition = this.m_listUnitPos[(int)warfareUnitData.tileIndex];
							nkcwarfareGameUnit2.SetNKMWarfareUnitData(warfareUnitData);
						}
					}
					if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY || warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAY_TRY_READY)
					{
						this.CheckAndAddBattleEnemy(warfareUnitData.warfareGameUnitUID, (int)warfareUnitData.tileIndex);
					}
				}
			}
			this.m_unitMgr.SetUserFlagShip(warfareGameData.warfareTeamDataA.flagShipWarfareUnitUID, false);
			this.m_unitMgr.SetFlagDungeon(warfareGameData.warfareTeamDataB.flagShipWarfareUnitUID);
			this.m_NKCWarfareGameHUD.SetRemainTurnOnUnitCount(this.m_unitMgr.GetRemainTurnOnUserUnitCount());
			this.UpdateRecoveryCount();
			this.SetTileDefaultWhenPlay();
			this.UpdateLabel(true);
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x002ADB20 File Offset: 0x002ABD20
		private void RefreshContainerWhenOpen(bool bBattle)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			this.m_containerMgr.HideAll();
			NKCWarfareGame.DataBeforeBattle dataBeforeBattle = this.m_DataBeforeBattle;
			int num = (dataBeforeBattle != null) ? dataBeforeBattle.PrevContainerCount : 0;
			if (bBattle && num < (int)warfareGameData.containerCount)
			{
				this.m_NKCWarfareGameHUD.SetContainerCount(num);
			}
			else
			{
				this.m_NKCWarfareGameHUD.SetContainerCount((int)warfareGameData.containerCount);
			}
			int i;
			int j;
			for (i = 0; i < warfareGameData.warfareTileDataList.Count; i = j + 1)
			{
				WarfareTileData tileData = warfareGameData.GetTileData(i);
				if (tileData != null)
				{
					bool flag = warfareGameData.GetUnitDataByTileIndex_TeamB(i) != null;
					if (!flag && bBattle)
					{
						flag = (warfareGameData.GetUnitDataByTileIndex_TeamA(i) != null);
					}
					if (this.m_DataBeforeBattle != null)
					{
						WarfareTileData warfareTileData = this.m_DataBeforeBattle.PrevTiles.Find((WarfareTileData v) => (int)v.index == i);
						if (warfareTileData != null && warfareTileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWNTT_CHEST && tileData.tileType != warfareTileData.tileType)
						{
							this.m_containerMgr.Set(i, WARFARE_ITEM_STATE.Item, this.m_listUnitPos[i], flag);
							goto IL_13C;
						}
					}
					if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWNTT_CHEST)
					{
						this.m_containerMgr.Set(i, WARFARE_ITEM_STATE.Item, this.m_listUnitPos[i], flag);
					}
				}
				IL_13C:
				j = i;
			}
			this.m_DataBeforeBattle = null;
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x002ADC98 File Offset: 0x002ABE98
		private void PrepareWarfareGameStart()
		{
			this.PlayWarfareOnGoingMusic();
			this.SetPlayingCommonUI(true);
			this.m_NKCWarfareGameHUD.SetUpperRightMenuPosition(true);
			if (this.m_NUM_WARFARE_FX_SHIP_DIVE != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE, false);
			}
			if (this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL, false);
			}
			this.SetCamDefaultWhenPlaying(1.6f, TRACKING_DATA_TYPE.TDT_SLOWER);
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			this.m_NKCWarfareGameHUD.TriggerPlayerTurnUI();
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			if (nkmwarfareTemplet.MapTemplet == null)
			{
				return;
			}
			foreach (WarfareUnitData warfareUnitData in warfareGameData.warfareTeamDataA.warfareUnitDataByUIDMap.Values)
			{
				NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(warfareUnitData.deckIndex);
				if (deckData != null)
				{
					deckData.SetState(NKM_DECK_STATE.DECK_STATE_WARFARE);
					NKCScenManager.CurrentUserData().m_ArmyData.DeckUpdated(warfareUnitData.deckIndex, deckData);
				}
			}
			this.m_containerMgr.HideAll();
			for (int i = 0; i < warfareGameData.warfareTileDataList.Count; i++)
			{
				WarfareTileData tileData = warfareGameData.GetTileData(i);
				this.SetActiveDivePoint(i, false);
				this.SetActiveAssultPoint(i, false);
				NKCWarfareGameTile tile = this.m_tileMgr.GetTile(i);
				if (!(tile == null) && tileData != null)
				{
					if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
					{
						tile.SetDummyActive(false);
					}
					else
					{
						tile.SetDummyActive(true);
						tile.SetTileLayer1Type(tileData.tileType);
						bool bWithEnemy = warfareGameData.GetUnitDataByTileIndex_TeamB(i) != null;
						if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWNTT_CHEST)
						{
							this.m_containerMgr.Set(i, WARFARE_ITEM_STATE.Item, this.m_listUnitPos[i], bWithEnemy);
						}
						WarfareUnitData unitDataByTileIndex = warfareGameData.GetUnitDataByTileIndex(i);
						if (unitDataByTileIndex == null)
						{
							tile.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_NORMAL);
						}
						else
						{
							if (warfareGameData.CheckTeamA_By_GameUnitUID(unitDataByTileIndex.warfareGameUnitUID))
							{
								WarfareTeamData warfareTeamDataA = warfareGameData.warfareTeamDataA;
								tile.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT);
							}
							else
							{
								WarfareTeamData warfareTeamDataB = warfareGameData.warfareTeamDataB;
								tile.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_ENEMY);
							}
							NKCWarfareGameUnit gameUnitByTileIndex = this.m_unitMgr.GetGameUnitByTileIndex((int)unitDataByTileIndex.tileIndex);
							if (gameUnitByTileIndex != null)
							{
								gameUnitByTileIndex.SetNKMWarfareUnitData(unitDataByTileIndex);
							}
							NKCWarfareGameUnitInfo wfgameUnitInfoByTileIndex = this.m_unitMgr.GetWFGameUnitInfoByTileIndex((int)unitDataByTileIndex.tileIndex);
							if (wfgameUnitInfoByTileIndex != null)
							{
								wfgameUnitInfoByTileIndex.SetNKMWarfareUnitData(unitDataByTileIndex);
							}
						}
					}
				}
			}
			this.m_unitMgr.RefreshDicUnit();
			if (this.m_NUM_WARFARE_BG_IMG_B != null)
			{
				Vector3 localPosition = this.m_NUM_WARFARE_BG_IMG_B.transform.localPosition;
				localPosition.z = 0f;
				this.m_NUM_WARFARE_BG_IMG_B.transform.localPosition = localPosition;
				this.m_NUM_WARFARE_BG_IMG_B.transform.DOMoveZ(-200f, 2f, false).From(true).SetEase(Ease.OutExpo);
			}
			this.m_NKCWarfareGameHUD.SetRemainTurnOnUnitCount(this.m_unitMgr.GetRemainTurnOnUserUnitCount());
			this.UpdateRecoveryCount();
			this.m_NKCWarfareGameHUD.UpdateMedalInfo();
			this.m_NKCWarfareGameHUD.UpdateWinCondition();
			this.m_unitMgr.UpdateGameUnitUI();
			this.UpdateLabel(false);
			this.m_NKCWarfareGameHUD.UpdateMultiplyUI();
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x002ADFD8 File Offset: 0x002AC1D8
		private void DoWhenMapSizeCalculated()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			int tileCount = mapTemplet.TileCount;
			this.m_tileMgr.SetTileActive(tileCount);
			this.m_NUM_WARFARE_TILE_Panel_GLG.constraintCount = mapTemplet.m_MapSizeY;
			Canvas.ForceUpdateCanvases();
			for (int i = 0; i < tileCount; i++)
			{
				NKCWarfareGameTile tile = this.m_tileMgr.GetTile(i);
				if (tile != null)
				{
					this.m_listUnitPos[i] = tile.transform.localPosition;
				}
			}
			for (int j = 0; j < this.m_listDivePoint.Count; j++)
			{
				if (this.m_listDivePoint[j] != null)
				{
					NKCUtil.SetGameobjectActive(this.m_listDivePoint[j], false);
				}
			}
			for (int k = 0; k < this.m_listAssultPoint.Count; k++)
			{
				if (this.m_listAssultPoint[k] != null)
				{
					NKCUtil.SetGameobjectActive(this.m_listAssultPoint[k], false);
				}
			}
			if (tileCount > 0)
			{
				this.m_rtCamBound = new Rect
				{
					xMin = this.m_listUnitPos[0].x,
					xMax = this.m_listUnitPos[tileCount - 1].x,
					yMax = this.m_listUnitPos[0].y + -250f,
					yMin = this.m_listUnitPos[tileCount - 1].y + -250f
				};
			}
			float newSize = Math.Abs(this.m_rtCamBound.xMax - this.m_rtCamBound.xMin) + 500f;
			float newSize2 = Math.Abs(this.m_rtCamBound.yMax - this.m_rtCamBound.yMin) + 500f;
			this.m_NUM_WARFARE_BG_WARBOX_A.SetWidth(newSize);
			this.m_NUM_WARFARE_BG_WARBOX_A.SetHeight(newSize2);
			this.m_NUM_WARFARE_BG_WARBOX_B.SetWidth(newSize);
			this.m_NUM_WARFARE_BG_WARBOX_B.SetHeight(newSize2);
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x002AE1F0 File Offset: 0x002AC3F0
		private void PrepareWarfareGameReady()
		{
			this.SetBG(true, false);
			this.m_CanvasGroup.alpha = this.m_fStartAlhpa;
			this.m_CanvasGroup.DOFade(1f, this.m_fFadeTime).SetEase(Ease.InSine);
			this.m_NKCWarfareGameHUD.SetActiveTitle(true);
			this.m_NKCWarfareGameHUD.SetActiveBatchGuideText(true);
			this.m_NKCWarfareGameHUD.SetActiveBatchCountUI(true);
			this.m_NKCWarfareGameHUD.SetActivePhase(false);
			this.m_NKCWarfareGameHUD.SetActiveAuto(this.IsAutoVisible(), this.IsAutoUsable());
			this.m_NKCWarfareGameHUD.SetActiveRepeatOperation(this.IsRepeatOperationVisible());
			this.m_NKCWarfareGameHUD.SetActiveAutoOnOff(this.IsAutoWarfare() && this.IsAutoUsable(), this.IsAutoWarfareSupply());
			if (!this.IsAutoUsable() && NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfare)
			{
				Debug.Log("NKMPacket_WARFARE_GAME_AUTO_REQ - NKCWarfareGame.PrepareWarfareGameReady");
				this.SetPause(true);
				this.WaitAutoPacket = true;
				NKCPacketSender.Send_NKMPacket_WARFARE_GAME_AUTO_REQ(false);
			}
			this.m_NKCWarfareGameHUD.SetUpperRightMenuPosition(false);
			this.m_NKCWarfareGameHUD.SetActiveDeco(true);
			this.m_NKCWarfareGameHUD.SetActivePause(false);
			this.m_NKCWarfareGameHUD.SetActiveTurnFinishBtn(false);
			NKCCamera.GetCamera().orthographic = false;
			this.SetCameraIntro();
			this.m_unitMgr.ClearUnits();
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			this.DoWhenMapSizeCalculated();
			this.PreProcessSpawnPoint();
			this.m_labelMgr.HideAll();
			this.m_containerMgr.HideAll();
			int num = 0;
			for (int i = 0; i < mapTemplet.TileCount; i++)
			{
				NKMWarfareTileTemplet tile = mapTemplet.GetTile(i);
				NKCWarfareGameTile tile2 = this.m_tileMgr.GetTile(i);
				if (tile != null && !(tile2 == null))
				{
					if (tile.m_TileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
					{
						tile2.SetDummyActive(false);
						this.SetActiveDivePoint(i, false);
						this.SetActiveAssultPoint(i, false);
					}
					else
					{
						tile2.SetDummyActive(true);
						tile2.SetTileLayer1Type(tile.m_TileType);
						tile2.SetTileLayer2Type(tile.m_TileWinType, tile.m_TileLoseType);
						if (tile.m_TileWinType == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD)
						{
							this.m_labelMgr.SetLabel(i, WARFARE_LABEL_TYPE.HOLD, this.m_listUnitPos[i]);
							int wfwinValue = nkmwarfareTemplet.m_WFWinValue;
							this.m_labelMgr.SetText(i, wfwinValue);
						}
						bool flag = !string.IsNullOrEmpty(tile.m_DungeonStrID);
						if (tile.m_TileType == NKM_WARFARE_MAP_TILE_TYPE.NWNTT_CHEST)
						{
							this.m_containerMgr.Set(i, WARFARE_ITEM_STATE.Item, this.m_listUnitPos[i], flag);
						}
						else if (tile.NeedQuestionMark())
						{
							this.m_containerMgr.Set(i, WARFARE_ITEM_STATE.Question, this.m_listUnitPos[i], flag);
						}
						if (flag)
						{
							bool bFlag = false;
							if (tile.m_bFlagDungeon && nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_KILL_BOSS)
							{
								bFlag = true;
							}
							NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.CreateNewEnemyUnit(tile.m_DungeonStrID, bFlag, tile.m_bTargetUnit, (short)i, tile.m_NKM_WARFARE_ENEMY_ACTION_TYPE, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), null);
							if (nkcwarfareGameUnit != null)
							{
								nkcwarfareGameUnit.gameObject.transform.localPosition = this.m_listUnitPos[i];
							}
							tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ENEMY);
						}
						else
						{
							bool flag2 = false;
							bool flag3 = false;
							if (this.SetActiveDivePoint(i, true))
							{
								flag2 = true;
							}
							if (this.SetActiveAssultPoint(i, true))
							{
								flag3 = true;
							}
							if (flag2 || flag3)
							{
								if (flag2)
								{
									tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_DIVE_POINT);
								}
								else if (flag3)
								{
									tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ASSULT_POINT);
								}
								GameObject gameObject = this.GetDivePointGO(i);
								if (gameObject == null)
								{
									gameObject = this.GetAssultPointGO(i);
								}
								this.AnimateSpawnPoint(gameObject, 0.75f + (float)num * 0.1f);
								num++;
							}
							else
							{
								tile2.SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
							}
						}
						if (tile.BattleCondition != null)
						{
							this.m_battleCondition.SetBattleCondition(i, tile.BattleCondition.BattleCondID, this.m_listUnitPos[i]);
						}
					}
				}
			}
			this.m_NKCWarfareGameHUD.Open();
			this.m_NKCWarfareGameHUD.SetBatchedShipCount(0);
			if (this.ProcessRetry() && NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
			{
				this.reqGameStart();
			}
			this.UpdateLabel(false);
			this.CheckTutorial();
		}

		// Token: 0x06007F95 RID: 32661 RVA: 0x002AE618 File Offset: 0x002AC818
		public override void CloseInternal()
		{
			this.OnCloseGameOption();
			this.m_bReservedBattle = false;
			if (this.m_twUnitInfoDie != null && this.m_twUnitInfoDie.IsActive())
			{
				this.m_twUnitInfoDie.Kill(false);
			}
			this.m_twUnitInfoDie = null;
			if (this.m_UnitDieSequence != null)
			{
				this.m_UnitDieSequence.Kill(false);
			}
			this.m_UnitDieSequence = null;
			this.m_WarfareGameUnitToDie = null;
			this.CloseAssistFX();
			if (this.m_NUM_WARFARE_BG_IMG_B != null)
			{
				this.m_NUM_WARFARE_BG_IMG_B.transform.DOKill(false);
			}
			if (this.m_listDivePoint != null)
			{
				for (int i = 0; i < this.m_listDivePoint.Count; i++)
				{
					this.m_listDivePoint[i].transform.DOKill(false);
				}
			}
			if (this.m_listAssultPoint != null)
			{
				for (int j = 0; j < this.m_listAssultPoint.Count; j++)
				{
					this.m_listAssultPoint[j].transform.DOKill(false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_SHIP_DIVE, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
			if (this.m_NUM_WARFARE.activeSelf)
			{
				this.m_NUM_WARFARE.SetActive(false);
			}
			for (int k = 0; k < this.m_listAssetInstance.Count; k++)
			{
				NKCAssetResourceManager.CloseInstance(this.m_listAssetInstance[k]);
			}
			this.m_listAssetInstance.Clear();
			this.m_unitMgr.ClearUnits();
			this.m_battleCondition.Close();
			this.m_containerMgr.Close();
			this.m_tileMgr.Close();
			this.m_NKCWarfareGameHUD.Close();
			NKCUIWarfareResult.CheckInstanceAndClose();
			NKCCamera.StopTrackingCamera();
			NKCCamera.GetTrackingRotation().SetNowValue(0f, 0f, 0f);
			NKCCamera.EnableBlur(false, 2f, 2);
		}

		// Token: 0x06007F96 RID: 32662 RVA: 0x002AE7FB File Offset: 0x002AC9FB
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			UnityEngine.Object.Destroy(this.m_NKCWarfareGameHUD.gameObject);
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x002AE814 File Offset: 0x002ACA14
		public void TempLeave()
		{
			if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().CheckRepeatOperationRealStop())
			{
				return;
			}
			NKCUIGameOption.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetEpisodeID(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x002AE864 File Offset: 0x002ACA64
		public void ResetGameOption()
		{
			if (!this.GetPause())
			{
				return;
			}
			NKCUIGameOption.CheckInstanceAndClose();
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.WARFARE, new NKCUIGameOption.OnCloseCallBack(this.OnCloseGameOption));
		}

		// Token: 0x06007F99 RID: 32665 RVA: 0x002AE88B File Offset: 0x002ACA8B
		public void ForceBack()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetEpisodeID(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x002AE8AA File Offset: 0x002ACAAA
		private bool IsAutoUsable()
		{
			return this.IsAutoVisible();
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x002AE8B2 File Offset: 0x002ACAB2
		private bool IsAutoVisible()
		{
			return NKCContentManager.IsContentsUnlocked(ContentsType.WARFARE_AUTO_MOVE, 0, 0);
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x002AE8BD File Offset: 0x002ACABD
		public bool IsAutoWarfare()
		{
			return NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfare;
		}

		// Token: 0x06007F9D RID: 32669 RVA: 0x002AE8D3 File Offset: 0x002ACAD3
		public bool IsAutoWarfareSupply()
		{
			return NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfareRepair;
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x002AE8E9 File Offset: 0x002ACAE9
		private bool IsRepeatOperationVisible()
		{
			return NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_REPEAT, 0, 0) && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.WARFARE_REPEAT) && (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP || NKCScenManager.GetScenManager().WarfareGameData.rewardMultiply <= 1);
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x002AE929 File Offset: 0x002ACB29
		public void SetUserUnitDeckWarfareState()
		{
			this.m_unitMgr.SetUserUnitDeckWarfareState();
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x002AE938 File Offset: 0x002ACB38
		private void PlayUnitDieAni()
		{
			if (this.m_WarfareGameUnitToDie == null)
			{
				return;
			}
			this.m_UnitDieSequence = DOTween.Sequence();
			this.m_WarfareGameUnitToDie.PlayDieAni();
			this.m_UnitDieSequence.Append(this.m_WarfareGameUnitToDie.transform.DOShakePosition(2f, 30f, 30, 90f, false, true, ShakeRandomnessMode.Full));
			this.m_UnitDieSequence.AppendCallback(new TweenCallback(this.FadeOutUnitInfoToDie));
			this.m_UnitDieSequence.Append(this.m_WarfareGameUnitToDie.GetComponent<CanvasGroup>().DOFade(0f, 0.6f));
			Vector3 vector = this.m_listUnitPos[this.m_WarfareGameUnitToDie.TileIndex];
			NKCCamera.SetPos(vector.x, vector.y + -250f, (float)this.GetFinalCameraZDist(), true, true);
			bool flag = false;
			NKCWarfareGameUnitInfo wfgameUnitInfoByWFUnitData = this.m_unitMgr.GetWFGameUnitInfoByWFUnitData(this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData());
			if (wfgameUnitInfoByWFUnitData != null)
			{
				flag = wfgameUnitInfoByWFUnitData.GetFlag();
			}
			if (!flag)
			{
				this.m_NUM_WARFARE_FX_UNIT_EXPLOSION.transform.localPosition = this.m_WarfareGameUnitToDie.transform.localPosition;
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, true);
				return;
			}
			this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG.transform.localPosition = this.m_WarfareGameUnitToDie.transform.localPosition;
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG, true);
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x002AEAAC File Offset: 0x002ACCAC
		private void PlayUnitRunawayAni()
		{
			if (this.m_WarfareGameUnitToDie == null)
			{
				return;
			}
			this.m_WarfareGameUnitToDie.PlayAni(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_RUNAWAY, null);
			bool flag = false;
			NKCWarfareGameUnitInfo wfgameUnitInfoByWFUnitData = this.m_unitMgr.GetWFGameUnitInfoByWFUnitData(this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData());
			if (wfgameUnitInfoByWFUnitData != null)
			{
				flag = wfgameUnitInfoByWFUnitData.GetFlag();
				wfgameUnitInfoByWFUnitData.PlayAni(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_RUNAWAY);
			}
			if (flag)
			{
				this.m_NUM_WARFARE_FX_UNIT_ESCAPE.transform.localPosition = this.m_WarfareGameUnitToDie.transform.localPosition;
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, true);
				NKCSoundManager.PlaySound("FX_UI_WARFARE_RUNAWAY", 1f, 0f, 0f, false, 0f, false, 0f);
				if (this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
				{
					NKCOperatorUtil.PlayVoice(this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData().deckIndex, VOICE_TYPE.VT_BACK_LACK, true);
				}
			}
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x002AEB94 File Offset: 0x002ACD94
		private void FadeOutUnitInfoToDie()
		{
			if (this.m_WarfareGameUnitToDie != null)
			{
				NKCWarfareGameUnitInfo wfgameUnitInfoByWFUnitData = this.m_unitMgr.GetWFGameUnitInfoByWFUnitData(this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData());
				if (wfgameUnitInfoByWFUnitData != null)
				{
					if (this.m_twUnitInfoDie != null && this.m_twUnitInfoDie.IsActive())
					{
						this.m_twUnitInfoDie.Kill(false);
					}
					this.m_twUnitInfoDie = wfgameUnitInfoByWFUnitData.GetComponent<CanvasGroup>().DOFade(0f, 0.57000005f);
				}
			}
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x002AEC0C File Offset: 0x002ACE0C
		private void OnUnitShakeEnd()
		{
			if (!this.m_bReservedCallOnUnitShakeEnd)
			{
				return;
			}
			this.m_bReservedCallOnUnitShakeEnd = false;
			Debug.Log("WarfareUnitShake End");
			if (this.m_UnitDieSequence != null)
			{
				this.m_UnitDieSequence.Kill(false);
			}
			this.m_UnitDieSequence = null;
			if (this.m_WarfareGameUnitToDie != null)
			{
				int tileIndex = this.m_WarfareGameUnitToDie.TileIndex;
				WarfareUnitData nkmwarfareUnitData = this.m_WarfareGameUnitToDie.GetNKMWarfareUnitData();
				this.m_unitMgr.ClearUnit(nkmwarfareUnitData.warfareGameUnitUID);
				this.m_WarfareGameUnitToDie = null;
				NKCWarfareGameUnit gameUnitByTileIndex = this.m_unitMgr.GetGameUnitByTileIndex(tileIndex);
				if (!(gameUnitByTileIndex != null))
				{
					Debug.LogWarning("WarfareGameUnitToDie 타일에 혼자인데 죽음, tileIndex : " + tileIndex.ToString());
					this.OnReturnMoveEnd(null);
					return;
				}
				Debug.Log("MoveAfterShake, tileIndex : " + tileIndex.ToString());
				if (gameUnitByTileIndex.gameObject.activeInHierarchy)
				{
					gameUnitByTileIndex.Move(this.m_listUnitPos[tileIndex], 0.9f, new NKCWarfareUnitMover.OnCompleteMove(this.OnReturnMoveEnd), false);
				}
				gameUnitByTileIndex.SetPause(this.GetPause());
				if (NKCGameEventManager.IsWaiting())
				{
					NKCGameEventManager.WaitFinished();
					return;
				}
			}
			else
			{
				Debug.LogError("WarfareGameUnitToDie is null");
				this.OnReturnMoveEnd(null);
			}
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x002AED34 File Offset: 0x002ACF34
		private void OnReturnMoveEnd(NKCWarfareGameUnit cNKCWarfareGameUnit)
		{
			if (cNKCWarfareGameUnit != null && cNKCWarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
			{
				if (this.m_containerMgr.IsItem(cNKCWarfareGameUnit.TileIndex))
				{
					this.PlayGetContainer(cNKCWarfareGameUnit.TileIndex, true);
				}
				if (NKCWarfareManager.UseServiceType != NKM_WARFARE_SERVICE_TYPE.NWST_NONE)
				{
					this.UseServiceFX(cNKCWarfareGameUnit, NKCWarfareManager.UseServiceType);
				}
				if (this.CheckEnterUnit(cNKCWarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID))
				{
					this.PlayEnterAni();
					return;
				}
			}
			this.OpenActionWhenNotStop();
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x002AEDAA File Offset: 0x002ACFAA
		private void UseServiceFX(NKCWarfareGameUnit gameUnit, NKM_WARFARE_SERVICE_TYPE seriveType)
		{
			if (seriveType != NKM_WARFARE_SERVICE_TYPE.NWST_REPAIR)
			{
				if (seriveType == NKM_WARFARE_SERVICE_TYPE.NWST_RESUPPLY)
				{
					gameUnit.TriggerSupplyFX();
				}
			}
			else
			{
				gameUnit.TriggerRepairFX();
			}
			NKCWarfareManager.ResetServiceType();
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x002AEDCC File Offset: 0x002ACFCC
		private bool CheckEnterUnit(int unitUID)
		{
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			WarfareUnitData unitData = nkmwarfareGameData.GetUnitData(unitUID);
			if (unitData == null)
			{
				return false;
			}
			if (unitData.unitType != WarfareUnitData.Type.User)
			{
				return false;
			}
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(unitUID);
			if (nkcwarfareGameUnit == null)
			{
				return false;
			}
			int tileIndex = nkcwarfareGameUnit.TileIndex;
			if (!this.m_tileMgr.IsTileLayer2Type(tileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_WIN_ENTER))
			{
				return false;
			}
			if (nkmwarfareGameData.GetTileData(tileIndex) == null)
			{
				return false;
			}
			this.m_LastEnterUnitUID = unitUID;
			return nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT;
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x002AEE48 File Offset: 0x002AD048
		private void PlayEnterAni()
		{
			if (this.m_LastEnterUnitUID > 0)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.m_LastEnterUnitUID);
				if (nkcwarfareGameUnit == null)
				{
					Debug.LogError("NKCWarfareGameUnit이 없음 - " + this.m_LastEnterUnitUID.ToString());
					return;
				}
				this.m_bReservedCallNextOrderREQ = true;
				nkcwarfareGameUnit.PlayAni(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_ENTER, new UnityAction(this.OnCompleteEnterAni));
				NKCWarfareGameUnitInfo wfgameUnitInfoByWFUnitData = this.m_unitMgr.GetWFGameUnitInfoByWFUnitData(nkcwarfareGameUnit.GetNKMWarfareUnitData());
				if (wfgameUnitInfoByWFUnitData != null)
				{
					wfgameUnitInfoByWFUnitData.PlayAni(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_ENTER);
				}
				this.m_NUM_WARFARE_FX_UNIT_ESCAPE.transform.localPosition = nkcwarfareGameUnit.transform.localPosition;
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, true);
			}
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x002AEF08 File Offset: 0x002AD108
		private void OnCompleteEnterAni()
		{
			if (!this.m_bReservedCallNextOrderREQ)
			{
				return;
			}
			this.m_bReservedCallNextOrderREQ = false;
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.m_LastEnterUnitUID);
			if (nkcwarfareGameUnit == null)
			{
				Debug.LogError("NKCWarfareGameUnit이 없음 - " + this.m_LastEnterUnitUID.ToString());
				return;
			}
			this.m_unitMgr.ClearUnit(this.m_LastEnterUnitUID);
			this.m_LastEnterUnitUID = 0;
			if (nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT)
			{
				Debug.Log("warfare next order REQ - OnCompleteEnterAni");
				this.SendGetNextOrderREQ(nkcwarfareGameUnit);
			}
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x002AEF94 File Offset: 0x002AD194
		private void DoMove(byte fromTileIndex, byte toTileIndex)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCWarfareManager.CheckMoveCond(NKCScenManager.GetScenManager().GetMyUserData(), (int)fromTileIndex, (int)toTileIndex);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), null, "");
				return;
			}
			this.SetPause(true);
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_MOVE_REQ(fromTileIndex, toTileIndex);
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x002AEFE8 File Offset: 0x002AD1E8
		private NKCWarfareGameUnit CheckAndAddBattleEnemy(int gameUnitUID, int tileIndex)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			WarfareUnitData unitData = warfareGameData.GetUnitData(gameUnitUID);
			NKCWarfareGameUnit nkcwarfareGameUnit = null;
			if (unitData == null)
			{
				return null;
			}
			if (unitData.unitType == WarfareUnitData.Type.User && unitData.warfareGameUnitUID == warfareGameData.battleAllyUid)
			{
				WarfareUnitData unitData2 = warfareGameData.GetUnitData(warfareGameData.battleMonsterUid);
				if (unitData2 != null)
				{
					NKCWarfareGameUnit nkcwarfareGameUnit2 = this.m_unitMgr.CreateNewEnemyUnit(NKMDungeonManager.GetDungeonStrID(unitData2.dungeonID), warfareGameData.warfareTeamDataB.flagShipWarfareUnitUID == unitData2.warfareGameUnitUID, unitData2.isTarget, unitData2.tileIndex, unitData2.warfareEnemyActionType, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), unitData2);
					if (nkcwarfareGameUnit2 != null)
					{
						Vector3 localPosition = this.m_listUnitPos[tileIndex];
						localPosition.x += 100f;
						nkcwarfareGameUnit2.gameObject.transform.localPosition = localPosition;
						nkcwarfareGameUnit2.SetNKMWarfareUnitData(unitData2);
					}
					nkcwarfareGameUnit = nkcwarfareGameUnit2;
				}
			}
			else if (unitData.unitType == WarfareUnitData.Type.Dungeon && warfareGameData.battleMonsterUid == unitData.warfareGameUnitUID)
			{
				WarfareUnitData unitData3 = warfareGameData.GetUnitData(warfareGameData.battleAllyUid);
				if (unitData3 != null)
				{
					NKCWarfareGameUnit nkcwarfareGameUnit3 = this.m_unitMgr.CreateNewUserUnit(unitData3.deckIndex, unitData3.tileIndex, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), unitData3, unitData3.friendCode);
					if (nkcwarfareGameUnit3 != null)
					{
						Vector3 localPosition2 = this.m_listUnitPos[tileIndex];
						localPosition2.x -= 100f;
						nkcwarfareGameUnit3.gameObject.transform.localPosition = localPosition2;
						nkcwarfareGameUnit3.SetNKMWarfareUnitData(unitData3);
					}
					nkcwarfareGameUnit = nkcwarfareGameUnit3;
				}
			}
			if (nkcwarfareGameUnit != null)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit4 = this.m_unitMgr.GetNKCWarfareGameUnit(gameUnitUID);
				if (nkcwarfareGameUnit4 != null)
				{
					Vector3 localPosition3 = nkcwarfareGameUnit4.gameObject.transform.localPosition;
					if (nkcwarfareGameUnit4.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
					{
						localPosition3.x -= 100f;
					}
					else
					{
						localPosition3.x += 100f;
					}
					nkcwarfareGameUnit4.gameObject.transform.localPosition = localPosition3;
				}
			}
			return nkcwarfareGameUnit;
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x002AF1F4 File Offset: 0x002AD3F4
		public void UseRepairItem()
		{
			if (NKMItemManager.GetItemMiscTempletByID(2) != null)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.GetSelectedSquadWFGUUID());
				if (nkcwarfareGameUnit == null)
				{
					return;
				}
				if (nkcwarfareGameUnit.IsSupporter)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_SUPPORTER_REPAIR, null, "");
					return;
				}
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_WARFARE_REPAIR_TITLE, NKCUtilString.GET_STRING_WARFARE_REPAIR_DESC, 2, 0, new NKCPopupResourceConfirmBox.OnButton(this.UseRepairItemConfirm), null, false);
			}
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x002AF267 File Offset: 0x002AD467
		private void UseRepairItemConfirm()
		{
			this.Send_NKMPacket_WARFARE_GAME_USE_SERVICE_REQ(this.GetSelectedSquadWFGUUID(), NKM_WARFARE_SERVICE_TYPE.NWST_REPAIR);
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x002AF278 File Offset: 0x002AD478
		public void UseSupplyItem()
		{
			if (NKMItemManager.GetItemMiscTempletByID(2) != null)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.GetSelectedSquadWFGUUID());
				if (nkcwarfareGameUnit == null)
				{
					return;
				}
				if (nkcwarfareGameUnit.IsSupporter)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_SUPPORTER_SUPPLY, null, "");
					return;
				}
				int num = 0;
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.GetNKMWarfareGameData().warfareTempletID);
				if (nkmwarfareTemplet != null)
				{
					NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
				}
				int num2 = this.GetNKMWarfareGameData().rewardMultiply;
				if (num2 <= 0)
				{
					num2 = 1;
				}
				num *= num2;
				if (num <= 0)
				{
					Debug.LogError("전역 보급 비용이 이상합니다. warfareID : " + nkmwarfareTemplet.m_WarfareStrID);
					return;
				}
				string content;
				if (this.GetNKMWarfareGameData().rewardMultiply > 1)
				{
					content = NKCUtilString.GET_STRING_WARFARE_SUPPLY_DESC_MULTIPLY;
				}
				else
				{
					content = NKCUtilString.GET_STRING_WARFARE_SUPPLY_DESC;
				}
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_WARFARE_SUPPLY_TITLE, content, 2, num * this.m_NKCWarfareGameHUD.GetCurrMultiplyRewardCount(), new NKCPopupResourceConfirmBox.OnButton(this.UseSupplyItemConfirm), null, false);
			}
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x002AF367 File Offset: 0x002AD567
		private void UseSupplyItemConfirm()
		{
			this.Send_NKMPacket_WARFARE_GAME_USE_SERVICE_REQ(this.GetSelectedSquadWFGUUID(), NKM_WARFARE_SERVICE_TYPE.NWST_RESUPPLY);
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x002AF378 File Offset: 0x002AD578
		private void Send_NKMPacket_WARFARE_GAME_USE_SERVICE_REQ(int warfareGameUnitUID, NKM_WARFARE_SERVICE_TYPE serviceType)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCWarfareManager.CanTryServiceUse(NKCScenManager.GetScenManager().GetMyUserData(), NKCScenManager.GetScenManager().WarfareGameData, warfareGameUnitUID, serviceType);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				string errorMessage = NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE);
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, errorMessage, null, "");
				return;
			}
			this.SetPause(true);
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_USE_SERVICE_REQ(warfareGameUnitUID, serviceType);
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x002AF3CC File Offset: 0x002AD5CC
		private void OnCancelBatch(int gameUnitUID)
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(gameUnitUID);
			bool flag = false;
			bool flag2 = false;
			if (nkcwarfareGameUnit != null)
			{
				int tileIndex = nkcwarfareGameUnit.TileIndex;
				flag2 = nkcwarfareGameUnit.IsSupporter;
				if (this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL != null)
				{
					this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL.gameObject.transform.localPosition = this.m_listUnitPos[tileIndex];
					this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL.SetActive(false);
					this.m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL.SetActive(true);
				}
				if (this.SetActiveDivePoint(tileIndex, true))
				{
					this.m_tileMgr.SetTileLayer0Type(tileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_DIVE_POINT);
				}
				if (this.SetActiveAssultPoint(tileIndex, true))
				{
					this.m_tileMgr.SetTileLayer0Type(tileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ASSULT_POINT);
				}
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_unitMgr.GetNKCWarfareGameUnitInfo(gameUnitUID);
				if (nkcwarfareGameUnitInfo != null)
				{
					flag = nkcwarfareGameUnitInfo.GetFlag();
				}
				this.m_unitMgr.ClearUnit(gameUnitUID);
			}
			if (flag)
			{
				this.m_unitMgr.ResetUserFlagShip(true);
			}
			this.m_NKCWarfareGameHUD.SetBatchedShipCount(this.m_unitMgr.GetCurrentUserUnit(true));
			this.m_NKCWarfareGameHUD.UpdateSupportShipTile(this.m_unitMgr.GetCurrentUserUnitTileIndex());
			this.UpdateAttackCostUI();
			if (this.m_unitMgr.GetCurrentUserUnit(true) + 1 == this.m_UserUnitMaxCount || flag2)
			{
				this.TurnOnAllAvaiableUserUnitSpawnPoint();
			}
			if (flag2)
			{
				this.m_NKCWarfareGameHUD.SetBatchedSupportShipCount(false);
			}
		}

		// Token: 0x06007FB1 RID: 32689 RVA: 0x002AF516 File Offset: 0x002AD716
		public void OnClickAssultPoint(int index)
		{
			if (this.IsBatchMax())
			{
				return;
			}
			this.OpenDeckView(NKCUIDeckViewer.DeckViewerMode.WarfareBatch_Assault);
			this.m_LastClickedSpawnPoint = index;
			this.m_Last_WARFARE_SPAWN_POINT_TYPE = NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT;
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x002AF537 File Offset: 0x002AD737
		public void OnClickDivePoint(int index)
		{
			if (this.IsBatchMax())
			{
				return;
			}
			this.OpenDeckView(NKCUIDeckViewer.DeckViewerMode.WarfareBatch);
			this.m_LastClickedSpawnPoint = index;
			this.m_Last_WARFARE_SPAWN_POINT_TYPE = NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE;
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x002AF558 File Offset: 0x002AD758
		public void OnClickWarfareBatch(NKMDeckIndex selectedDeckIndex)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!NKCUtil.ProcessDeckErrorMsg(NKMMain.IsValidDeck(myUserData.m_ArmyData, selectedDeckIndex)))
			{
				return;
			}
			if (this.m_unitMgr.CheckDuplicateDeckIndex(selectedDeckIndex))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_START_BY_DUPLICATE_DECK_INDEX), null, "");
				return;
			}
			int num = -1;
			if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE)
			{
				num = this.GetDivePointTileIndex(this.m_LastClickedSpawnPoint);
			}
			else if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
			{
				num = this.GetAssultPointTileIndex(this.m_LastClickedSpawnPoint);
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			bool flag = false;
			NKMWarfareTileTemplet tile = mapTemplet.GetTile(num);
			if (!NKCWarfareManager.CheckValidSpawnPoint(mapTemplet, tile, myUserData, selectedDeckIndex, out flag))
			{
				if (flag)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_ASSAULT_POSITION), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_POSITION), null, "");
				return;
			}
			else
			{
				if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE)
				{
					if (this.SetActiveDivePoint(num, false))
					{
						this.m_tileMgr.SetTileLayer0Type(num, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
					}
				}
				else if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT && this.SetActiveAssultPoint(num, false))
				{
					this.m_tileMgr.SetTileLayer0Type(num, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
				}
				if (this.m_NUM_WARFARE_FX_SHIP_DIVE != null)
				{
					this.m_NUM_WARFARE_FX_SHIP_DIVE.gameObject.transform.localPosition = this.m_listUnitPos[num];
					this.m_NUM_WARFARE_FX_SHIP_DIVE.SetActive(false);
					this.m_NUM_WARFARE_FX_SHIP_DIVE.SetActive(true);
				}
				NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(selectedDeckIndex);
				if (deckData != null)
				{
					deckData.SetState(NKM_DECK_STATE.DECK_STATE_WARFARE);
					myUserData.m_ArmyData.DeckUpdated(selectedDeckIndex, deckData);
				}
				NKCUIDeckViewer.Instance.Close();
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.CreateNewUserUnit(selectedDeckIndex, (short)num, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), null, 0L);
				if (nkcwarfareGameUnit == null)
				{
					return;
				}
				nkcwarfareGameUnit.gameObject.transform.localPosition = this.m_listUnitPos[num];
				nkcwarfareGameUnit.transform.DOMove(this.BATCH_EFFECT_POS, 1.5f, false).SetEase(Ease.OutCubic).From(true);
				if (deckData != null)
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_BATTLE_READY, myUserData.m_ArmyData.GetUnitFromUID(deckData.GetLeaderUnitUID()), false, false);
				}
				if (!this.m_unitMgr.CheckExistFlagUserUnit())
				{
					this.m_unitMgr.SetUserFlagShip(nkcwarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID, false);
				}
				this.m_NKCWarfareGameHUD.SetBatchedShipCount(this.m_unitMgr.GetCurrentUserUnit(true));
				this.m_NKCWarfareGameHUD.UpdateSupportShipTile(this.m_unitMgr.GetCurrentUserUnitTileIndex());
				this.UpdateAttackCostUI();
				if (this.IsBatchMax())
				{
					this.TurnOffAllUserUnitSpawnPoint();
				}
				return;
			}
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x002AF7F8 File Offset: 0x002AD9F8
		public NKM_ERROR_CODE CheckWarfareBatch(NKMDeckIndex selectedDeckIndex)
		{
			if (this.m_unitMgr.GetCurrentUserUnit(true) >= this.m_UserUnitMaxCount)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_START_BY_MAX_USER_UNIT_OVERFLOW;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x002AF818 File Offset: 0x002ADA18
		private bool IsBatchMax()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			return this.m_unitMgr.GetCurrentUserUnit(true) >= this.m_UserUnitMaxCount && (!nkmwarfareTemplet.m_bFriendSummon || this.m_unitMgr.ContainSupporterUnit());
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x002AF85C File Offset: 0x002ADA5C
		private bool ProcessRetry()
		{
			if (this.m_RetryData == null)
			{
				return false;
			}
			if (this.m_RetryData.WarfareStrID != this.m_WarfareStrID)
			{
				this.m_RetryData = null;
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMArmyData armyData = myUserData.m_ArmyData;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return false;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return false;
			}
			foreach (NKCWarfareGame.RetryData.UnitData unitData in this.m_RetryData.UnitList)
			{
				NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, unitData.DeckIndex);
				int tileIndex = unitData.TileIndex;
				if (NKMMain.IsValidDeck(armyData, nkmdeckIndex) == NKM_ERROR_CODE.NEC_OK && !this.m_unitMgr.CheckDuplicateDeckIndex(nkmdeckIndex))
				{
					NKMWarfareTileTemplet tile = mapTemplet.GetTile(tileIndex);
					bool flag;
					if (NKCWarfareManager.CheckValidSpawnPoint(mapTemplet, tile, myUserData, nkmdeckIndex, out flag) && (this.SetActiveDivePoint(tileIndex, false) || this.SetActiveAssultPoint(tileIndex, false)))
					{
						this.m_tileMgr.SetTileLayer0Type(tileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
						NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(nkmdeckIndex);
						if (deckData != null)
						{
							deckData.SetState(NKM_DECK_STATE.DECK_STATE_WARFARE);
							myUserData.m_ArmyData.DeckUpdated(nkmdeckIndex, deckData);
							NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.CreateNewUserUnit(nkmdeckIndex, (short)tileIndex, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), null, 0L);
							if (!(nkcwarfareGameUnit == null))
							{
								nkcwarfareGameUnit.gameObject.transform.localPosition = this.m_listUnitPos[tileIndex];
							}
						}
					}
				}
			}
			NKCWarfareGameUnit wfgameUnitByDeckIndex = this.m_unitMgr.GetWFGameUnitByDeckIndex(new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, this.m_RetryData.FlagShipDeckIndex));
			if (wfgameUnitByDeckIndex != null)
			{
				this.m_unitMgr.SetUserFlagShip(wfgameUnitByDeckIndex.GetNKMWarfareUnitData().warfareGameUnitUID, false);
			}
			this.m_NKCWarfareGameHUD.SetBatchedShipCount(this.m_unitMgr.GetCurrentUserUnit(true));
			this.m_NKCWarfareGameHUD.UpdateSupportShipTile(this.m_unitMgr.GetCurrentUserUnitTileIndex());
			this.UpdateAttackCostUI();
			if (this.IsBatchMax())
			{
				this.TurnOffAllUserUnitSpawnPoint();
			}
			this.m_RetryData = null;
			return true;
		}

		// Token: 0x06007FB7 RID: 32695 RVA: 0x002AFA94 File Offset: 0x002ADC94
		public override void OnBackButton()
		{
			if (NKCUICutScenPlayer.IsInstanceOpen && NKCUICutScenPlayer.Instance.IsPlaying())
			{
				NKCUICutScenPlayer.Instance.StopWithCallBack();
				return;
			}
			if (this.GetNKMWarfareGameData().warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				this.ForceBack();
				return;
			}
			this.OnClickPause();
		}

		// Token: 0x06007FB8 RID: 32696 RVA: 0x002AFAD0 File Offset: 0x002ADCD0
		public bool OnClickGameStart(bool bRepeatOperation = false)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return false;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_WarfareStrID);
			if (nkmstageTempletV == null)
			{
				return false;
			}
			int num = 0;
			int num2;
			if (nkmstageTempletV.m_StageReqItemID > 0)
			{
				num2 = nkmstageTempletV.m_StageReqItemID;
				num = nkmstageTempletV.m_StageReqItemCount;
			}
			else
			{
				num2 = 2;
			}
			if (num2 == 2)
			{
				NKCCompanyBuff.SetDiscountOfEterniumInEnteringWarfare(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
			}
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(num2) < (long)(num * this.m_NKCWarfareGameHUD.GetCurrMultiplyRewardCount()))
			{
				NKCShopManager.OpenItemLackPopup(num2, num);
				return false;
			}
			if (this.m_NKCWarfareGameHUD.GetCurrMultiplyRewardCount() > 1)
			{
				NKMRewardMultiplyTemplet.RewardMultiplyItem costItem = NKMRewardMultiplyTemplet.GetCostItem(NKMRewardMultiplyTemplet.ScopeType.General);
				int num3 = costItem.MiscItemCount * (this.m_NKCWarfareGameHUD.GetCurrMultiplyRewardCount() - 1);
				if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(costItem.MiscItemId) < (long)num3)
				{
					NKCShopManager.OpenItemLackPopup(costItem.MiscItemId, num3);
					return false;
				}
			}
			NKMPacket_WARFARE_GAME_START_REQ nkmpacket_WARFARE_GAME_START_REQ;
			if (!this.CheckWFGameStartCond(out nkmpacket_WARFARE_GAME_START_REQ))
			{
				this.SetUserUnitDeckWarfareState();
				return false;
			}
			this.SetUserUnitDeckWarfareState();
			if (nkmwarfareTemplet.m_UserTeamCount > this.m_unitMgr.GetCurrentUserUnit(true))
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_WARNING_GAME_START, delegate()
				{
					this.reqGameStart();
					if (bRepeatOperation)
					{
						NKCPopupRepeatOperation.Instance.CloseAndStartWithCurrOption();
					}
				}, null, false);
				return false;
			}
			this.reqGameStart();
			return true;
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x002AFC1C File Offset: 0x002ADE1C
		public void OnClickNextTurn()
		{
			if (NKCGameEventManager.IsWaiting())
			{
				return;
			}
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				return;
			}
			if (!nkmwarfareGameData.isTurnA)
			{
				return;
			}
			if (this.m_unitMgr.GetRemainTurnOnUserUnitCount() > 0)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_WARNING_FINISH_TURN, delegate()
				{
					this.SetPause(true);
					NKCPacketSender.Send_NKMPacket_WARFARE_GAME_TURN_FINISH_REQ();
				}, null, false);
				return;
			}
			this.SetPause(true);
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_TURN_FINISH_REQ();
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x002AFC84 File Offset: 0x002ADE84
		public void GiveUp()
		{
			if (this.GetNKMWarfareGameData() == null)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().CheckRepeatOperationRealStop())
			{
				return;
			}
			string content;
			if (this.GetNKMWarfareGameData().rewardMultiply > 1)
			{
				content = NKCUtilString.GET_STRING_WARFARE_WARNING_GIVE_UP_MULTIPLY;
			}
			else
			{
				content = NKCUtilString.GET_STRING_WARFARE_WARNING_GIVE_UP;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, content, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGWarfare), null, false);
		}

		// Token: 0x06007FBB RID: 32699 RVA: 0x002AFCE1 File Offset: 0x002ADEE1
		private void OnClickOkGiveUpINGWarfare()
		{
			if (NKCGameEventManager.IsWaiting())
			{
				return;
			}
			NKCUIGameOption.CheckInstanceAndClose();
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_GIVE_UP_REQ();
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x002AFCF5 File Offset: 0x002ADEF5
		public bool CheckEnablePause()
		{
			return !NKCGameEventManager.IsWaiting() && this.GetNKMWarfareGameData().warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP;
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x002AFD10 File Offset: 0x002ADF10
		public void SetPause(bool bSet)
		{
			if (!this.CheckEnablePause())
			{
				return;
			}
			if (bSet)
			{
				this.AddPauseRef();
			}
			else
			{
				this.MinusPauseRef();
			}
			if (this.m_unitMgr != null)
			{
				this.m_unitMgr.PauseUnits(this.GetPause());
			}
			NKCCamera.GetTrackingPos().SetPause(this.GetPause());
			this.m_NKCWarfareGameHUD.SetPauseState(this.GetPause());
		}

		// Token: 0x06007FBE RID: 32702 RVA: 0x002AFD71 File Offset: 0x002ADF71
		public void OnClickPause()
		{
			if (!this.CheckEnablePause())
			{
				return;
			}
			if (NKCUIGameOption.Instance.IsOpen)
			{
				return;
			}
			this.SetPause(true);
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.WARFARE, new NKCUIGameOption.OnCloseCallBack(this.OnCloseGameOption));
		}

		// Token: 0x06007FBF RID: 32703 RVA: 0x002AFDA7 File Offset: 0x002ADFA7
		private void OnCloseGameOption()
		{
			this.SetPause(false);
		}

		// Token: 0x06007FC0 RID: 32704 RVA: 0x002AFDB0 File Offset: 0x002ADFB0
		private void OnClickUnit(int gameUID)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(gameUID);
			if (nkcwarfareGameUnit == null)
			{
				return;
			}
			nkcwarfareGameUnit.transform.SetAsLastSibling();
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				this.OnClickUnitWhenStop(nkcwarfareGameUnit);
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				this.OnClickUnitWhenPlaying(warfareGameData, nkcwarfareGameUnit);
			}
		}

		// Token: 0x06007FC1 RID: 32705 RVA: 0x002AFE10 File Offset: 0x002AE010
		public void OnClickSquadInfo()
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.GetSelectedSquadWFGUUID());
			if (nkcwarfareGameUnit != null)
			{
				this.OpenUserUnitInfoPopup(nkcwarfareGameUnit, true);
			}
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x002AFE40 File Offset: 0x002AE040
		private void OnClickUnitWhenPlaying(WarfareGameData cNKMWarfareGameData, NKCWarfareGameUnit cNKCWarfareGameUnit)
		{
			if (!cNKMWarfareGameData.isTurnA)
			{
				return;
			}
			if (this.IsAutoWarfare())
			{
				return;
			}
			WarfareUnitData nkmwarfareUnitData = cNKCWarfareGameUnit.GetNKMWarfareUnitData();
			if (nkmwarfareUnitData == null)
			{
				return;
			}
			int tileIndex = cNKCWarfareGameUnit.TileIndex;
			int beforeLastClickedUnitTileIndex = this.m_LastClickedUnitTileIndex;
			if (beforeLastClickedUnitTileIndex != -1 && beforeLastClickedUnitTileIndex != tileIndex)
			{
				NKCWarfareGameUnit gameUnitByTileIndex = this.m_unitMgr.GetGameUnitByTileIndex(beforeLastClickedUnitTileIndex);
				if (gameUnitByTileIndex != null && !gameUnitByTileIndex.GetNKMWarfareUnitData().isTurnEnd && this.m_tileMgr.IsTileLayer0Type(beforeLastClickedUnitTileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT_SELECTED) && this.m_tileMgr.IsTileLayer0Type(tileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_POSSIBLE_ARRIVAL))
				{
					WarfareUnitData nkmwarfareUnitData2 = gameUnitByTileIndex.GetNKMWarfareUnitData();
					if (nkmwarfareUnitData2.supply == 0 && nkmwarfareUnitData2.unitType != nkmwarfareUnitData.unitType)
					{
						NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_WARFARE_WARNING_SUPPLY, delegate()
						{
							this.m_LastClickedUnitTileIndex = tileIndex;
							this.DoMove((byte)beforeLastClickedUnitTileIndex, (byte)tileIndex);
						}, null, false);
						return;
					}
					this.m_LastClickedUnitTileIndex = tileIndex;
					this.DoMove((byte)beforeLastClickedUnitTileIndex, (byte)tileIndex);
					return;
				}
			}
			if (!cNKMWarfareGameData.CheckTeamA_By_GameUnitUID(nkmwarfareUnitData.warfareGameUnitUID))
			{
				string battleConditionStrID = "";
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
				if (nkmwarfareTemplet != null)
				{
					battleConditionStrID = nkmwarfareTemplet.MapTemplet.GetTile(cNKCWarfareGameUnit.TileIndex).m_BattleConditionStrID;
				}
				NKCPopupEnemyList.Instance.Open(nkmwarfareUnitData.dungeonID, battleConditionStrID);
				return;
			}
			this.m_LastClickedUnitTileIndex = tileIndex;
			if (beforeLastClickedUnitTileIndex == this.m_LastClickedUnitTileIndex)
			{
				this.InvalidSelectedUnit();
				return;
			}
			if (!nkmwarfareUnitData.isTurnEnd)
			{
				this.SetTileDefaultWhenPlay();
				this.m_unitMgr.ResetIcon(0);
				this.CloseAssistFX();
				this.CancelRecovery();
				this.m_tileMgr.SetTileLayer0Type(tileIndex, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT_SELECTED);
				this.SetTilePossibleArrival(tileIndex, NKCWarfareManager.GetShipStyleTypeByGUUID(NKCScenManager.GetScenManager().GetMyUserData(), cNKMWarfareGameData, nkmwarfareUnitData.warfareGameUnitUID), nkmwarfareUnitData.supply == 0);
				if (nkmwarfareUnitData.supply == 0)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_WARNING_NO_EXIST_SUPPLY, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					if (nkmwarfareUnitData.unitType == WarfareUnitData.Type.User)
					{
						NKCOperatorUtil.PlayVoice(nkmwarfareUnitData.deckIndex, VOICE_TYPE.VT_BULLET_LACK, true);
					}
				}
			}
			else
			{
				this.SetTileDefaultWhenPlay();
				this.m_unitMgr.ResetIcon(0);
				this.CloseAssistFX();
				this.CancelRecovery();
			}
			bool flag = NKCWarfareManager.CheckOnTileType(cNKMWarfareGameData, tileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWMTT_REPAIR);
			bool flag2 = NKCWarfareManager.CheckOnTileType(cNKMWarfareGameData, tileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWMTT_RESUPPLY);
			bool flag3 = NKCWarfareManager.CheckOnTileType(cNKMWarfareGameData, tileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWNTT_SERVICE);
			this.m_NKCWarfareGameHUD.OpenSelectedSquadUI(nkmwarfareUnitData.deckIndex, flag || flag3, flag2 || flag3);
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x002B00D4 File Offset: 0x002AE2D4
		private void UpdateSelectedSquadUI()
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.GetSelectedSquadWFGUUID());
			if (nkcwarfareGameUnit == null || nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.Dungeon)
			{
				this.m_NKCWarfareGameHUD.CloseSelectedSquadUI();
				return;
			}
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			int tileIndex = nkcwarfareGameUnit.TileIndex;
			bool flag = NKCWarfareManager.CheckOnTileType(nkmwarfareGameData, tileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWMTT_REPAIR);
			bool flag2 = NKCWarfareManager.CheckOnTileType(nkmwarfareGameData, tileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWMTT_RESUPPLY);
			bool flag3 = NKCWarfareManager.CheckOnTileType(nkmwarfareGameData, tileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWNTT_SERVICE);
			this.m_NKCWarfareGameHUD.UpdateSelectedSquadUI(flag || flag3, flag2 || flag3);
		}

		// Token: 0x06007FC4 RID: 32708 RVA: 0x002B0151 File Offset: 0x002AE351
		public void OnClickPossibleArrivalTile(int tileIndex)
		{
			this.DoMove((byte)this.m_LastClickedUnitTileIndex, (byte)tileIndex);
		}

		// Token: 0x06007FC5 RID: 32709 RVA: 0x002B0164 File Offset: 0x002AE364
		private void OnClickUnitWhenStop(NKCWarfareGameUnit cNKCWarfareGameUnit)
		{
			if (cNKCWarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
			{
				this.OpenUserUnitInfoPopup(cNKCWarfareGameUnit, false);
				return;
			}
			if (cNKCWarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.Dungeon)
			{
				string battleConditionStrID = "";
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
				if (nkmwarfareTemplet != null)
				{
					NKMWarfareTileTemplet tile = nkmwarfareTemplet.MapTemplet.GetTile(cNKCWarfareGameUnit.TileIndex);
					if (tile != null)
					{
						battleConditionStrID = tile.m_BattleConditionStrID;
					}
				}
				NKCPopupEnemyList.Instance.Open(cNKCWarfareGameUnit.GetNKMWarfareUnitData().dungeonID, battleConditionStrID);
			}
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x002B01DC File Offset: 0x002AE3DC
		private void OpenDeckView(NKCUIDeckViewer.DeckViewerMode eDeckViewerMode)
		{
			this.m_lastDeckView = eDeckViewerMode;
			if (this.m_bFirstOpenDeck && NKMWarfareTemplet.Find(this.m_WarfareStrID).m_bFriendSummon)
			{
				NKCPacketSender.Send_NKMPacket_WARFARE_FRIEND_LIST_REQ();
				return;
			}
			this.OpenDeckView();
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x002B020C File Offset: 0x002AE40C
		private void OpenDeckView()
		{
			this.m_bFirstOpenDeck = false;
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_MENU_NAME_WARFARE;
			options.eDeckviewerMode = this.m_lastDeckView;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnClickWarfareBatch);
			options.dCheckSideMenuButton = new NKCUIDeckViewer.DeckViewerOption.CheckDeckButtonConfirm(this.CheckWarfareBatch);
			options.dOnChangeDeckUnit = new NKCUIDeckViewer.DeckViewerOption.OnChangeDeckUnit(this.OnChangeDeckUnit);
			int num = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetAvailableDeckIndex(NKM_DECK_TYPE.NDT_NORMAL);
			if (num == -1)
			{
				num = 0;
			}
			options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, num);
			options.upsideMenuShowResourceList = this.UpsideMenuShowResourceList;
			options.SelectLeaderUnitOnOpen = false;
			options.bEnableDefaultBackground = false;
			options.bUpsideMenuHomeButton = false;
			options.bOpenAlphaAni = true;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			options.bUsableSupporter = nkmwarfareTemplet.m_bFriendSummon;
			if (nkmwarfareTemplet.m_bFriendSummon)
			{
				options.lstSupporter = NKCWarfareManager.SupporterList;
				options.dOnSelectSupporter = new NKCUIDeckViewer.DeckViewerOption.OnSelectSupporter(this.BatchSupporter);
				options.dIsValidSupport = new NKCUIDeckViewer.DeckViewerOption.IsValidSupport(this.CheckSupport);
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_WarfareStrID);
			if (nkmstageTempletV != null)
			{
				if (nkmstageTempletV.m_StageReqItemID == 0)
				{
					options.CostItemID = 2;
				}
				else
				{
					options.CostItemID = nkmstageTempletV.m_StageReqItemID;
				}
				options.StageBattleStrID = nkmstageTempletV.m_StageBattleStrID;
			}
			options.dOnHide = delegate()
			{
				this.SetActiveForDeckView(false);
			};
			options.dOnUnhide = delegate()
			{
				this.SetActiveForDeckView(true);
			};
			this.m_bOpenDeckView = true;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x06007FC8 RID: 32712 RVA: 0x002B0398 File Offset: 0x002AE598
		private void OnChangeDeckUnit(NKMDeckIndex selectedDeckIndex, long newlyAddedUnitUID)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(selectedDeckIndex) == null)
			{
				return;
			}
			NKCWarfareGameUnit wfgameUnitByDeckIndex = this.m_unitMgr.GetWFGameUnitByDeckIndex(selectedDeckIndex);
			if (wfgameUnitByDeckIndex != null)
			{
				this.OnCancelBatch(wfgameUnitByDeckIndex.GetNKMWarfareUnitData().warfareGameUnitUID);
			}
		}

		// Token: 0x06007FC9 RID: 32713 RVA: 0x002B03E4 File Offset: 0x002AE5E4
		private void OnDeckViewBtn(int gameUnitUID)
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(gameUnitUID);
			if (nkcwarfareGameUnit != null)
			{
				if (this.GetDivePointIndex(nkcwarfareGameUnit.TileIndex) != -1)
				{
					this.m_LastClickedSpawnPoint = this.GetDivePointIndex(nkcwarfareGameUnit.TileIndex);
					this.m_Last_WARFARE_SPAWN_POINT_TYPE = NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE;
				}
				else if (this.GetAssultPointIndex(nkcwarfareGameUnit.TileIndex) != -1)
				{
					this.m_LastClickedSpawnPoint = this.GetAssultPointIndex(nkcwarfareGameUnit.TileIndex);
					this.m_Last_WARFARE_SPAWN_POINT_TYPE = NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT;
				}
				this.OnCancelBatch(gameUnitUID);
				if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
				{
					this.OpenDeckView(NKCUIDeckViewer.DeckViewerMode.WarfareBatch_Assault);
					return;
				}
				this.OpenDeckView(NKCUIDeckViewer.DeckViewerMode.WarfareBatch);
			}
		}

		// Token: 0x06007FCA RID: 32714 RVA: 0x002B047C File Offset: 0x002AE67C
		private void BatchSupporter(long friendCode)
		{
			if (!this.CheckSupport(friendCode))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			WarfareSupporterListData supportUnitData = NKCWarfareManager.FindSupporter(friendCode);
			int num = -1;
			if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE)
			{
				num = this.GetDivePointTileIndex(this.m_LastClickedSpawnPoint);
			}
			else if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
			{
				num = this.GetAssultPointTileIndex(this.m_LastClickedSpawnPoint);
			}
			if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE)
			{
				if (this.SetActiveDivePoint(num, false))
				{
					this.m_tileMgr.SetTileLayer0Type(num, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
				}
			}
			else if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT && this.SetActiveAssultPoint(num, false))
			{
				this.m_tileMgr.SetTileLayer0Type(num, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
			}
			if (this.m_NUM_WARFARE_FX_SHIP_DIVE != null)
			{
				this.m_NUM_WARFARE_FX_SHIP_DIVE.gameObject.transform.localPosition = this.m_listUnitPos[num];
				this.m_NUM_WARFARE_FX_SHIP_DIVE.SetActive(false);
				this.m_NUM_WARFARE_FX_SHIP_DIVE.SetActive(true);
			}
			NKCUIDeckViewer.Instance.Close();
			this.GetNKMWarfareGameData().supportUnitData = supportUnitData;
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.CreateNewUserUnit(NKMDeckIndex.None, (short)num, new NKCWarfareGameUnit.onClickUnit(this.OnClickUnit), null, friendCode);
			if (nkcwarfareGameUnit == null)
			{
				return;
			}
			nkcwarfareGameUnit.gameObject.transform.localPosition = this.m_listUnitPos[num];
			nkcwarfareGameUnit.transform.DOMove(this.BATCH_EFFECT_POS, 1.5f, false).SetEase(Ease.OutCubic).From(true);
			this.m_NKCWarfareGameHUD.SetBatchedShipCount(this.m_unitMgr.GetCurrentUserUnit(true));
			this.m_NKCWarfareGameHUD.SetBatchedSupportShipCount(true);
			this.UpdateAttackCostUI();
			if (this.IsBatchMax())
			{
				this.TurnOffAllUserUnitSpawnPoint();
			}
		}

		// Token: 0x06007FCB RID: 32715 RVA: 0x002B061C File Offset: 0x002AE81C
		private bool CheckSupport(long friendCode)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return false;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return false;
			}
			if (!nkmwarfareTemplet.m_bFriendSummon)
			{
				Debug.Log("친구 소대 사용 불가능한 맵");
				return false;
			}
			int spawnPointCountByType = mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE);
			int spawnPointCountByType2 = mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT);
			if (spawnPointCountByType + spawnPointCountByType2 <= 1)
			{
				Debug.Log(string.Format("출격 포인트 {0} = 서포터 자리 없음", spawnPointCountByType + spawnPointCountByType2));
				return false;
			}
			if (this.m_unitMgr.ContainSupporterUnit())
			{
				Debug.Log("이미 서포터가 배치되어 있음");
				return false;
			}
			if (NKCWarfareManager.FindSupporter(friendCode) == null)
			{
				Debug.Log(string.Format("친구 없음 {0}", friendCode));
				return false;
			}
			if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
			{
				Debug.Log("게스트/친구 소대는 강습지점에 착륙 불가");
				return false;
			}
			int index = -1;
			if (this.m_Last_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE)
			{
				index = this.GetDivePointTileIndex(this.m_LastClickedSpawnPoint);
			}
			NKMWarfareTileTemplet tile = mapTemplet.GetTile(index);
			if (tile == null)
			{
				return false;
			}
			if (tile.m_NKM_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_NONE)
			{
				Debug.Log(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_POSITION));
				return false;
			}
			if (tile.m_NKM_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
			{
				Debug.Log("게스트/친구 소대는 강습지점에 착륙 불가");
				return false;
			}
			return true;
		}

		// Token: 0x06007FCC RID: 32716 RVA: 0x002B0734 File Offset: 0x002AE934
		private void PlayWarfareOnGoingMusic()
		{
			string text = this.FindMusic();
			if (text != null)
			{
				NKCSoundManager.PlayMusic(text, true, 1f, false, 0f, 0f);
			}
		}

		// Token: 0x06007FCD RID: 32717 RVA: 0x002B0764 File Offset: 0x002AE964
		private string FindMusic()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet.m_WarfareBGM.Length > 1)
			{
				return nkmwarfareTemplet.m_WarfareBGM;
			}
			string cutScenStrIDBefore = nkmwarfareTemplet.m_CutScenStrIDBefore;
			if (!string.IsNullOrEmpty(cutScenStrIDBefore))
			{
				NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(cutScenStrIDBefore);
				if (cutScenTemple != null)
				{
					string lastMusicAssetName = cutScenTemple.GetLastMusicAssetName();
					if (lastMusicAssetName != null && lastMusicAssetName.Length > 1)
					{
						return lastMusicAssetName;
					}
				}
			}
			return null;
		}

		// Token: 0x06007FCE RID: 32718 RVA: 0x002B07CC File Offset: 0x002AE9CC
		public void OnRecv(NKMPacket_WARFARE_GAME_START_ACK cNKMPacket_WARFARE_GAME_START_ACK)
		{
			this.PrepareWarfareGameStart();
			this.CheckTutorialAfterStart();
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion != null && nkcrepeatOperaion.GetIsOnGoing())
			{
				if (!this.IsAutoWarfare())
				{
					this.m_NKCWarfareGameHUD.SendAutoReq(true);
				}
				this.m_NKCWarfareGameHUD.SetActiveRepeatOperationOnOff(true);
				if (cNKMPacket_WARFARE_GAME_START_ACK.costItemDataList != null && cNKMPacket_WARFARE_GAME_START_ACK.costItemDataList.Count > 0)
				{
					nkcrepeatOperaion.SetCostIncreaseCount(nkcrepeatOperaion.GetCostIncreaseCount() + 1L);
				}
			}
		}

		// Token: 0x06007FCF RID: 32719 RVA: 0x002B0840 File Offset: 0x002AEA40
		public void OnRecv(NKMPacket_WARFARE_GAME_TURN_FINISH_ACK cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK)
		{
			this.InvalidSelectedUnitPure();
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK.warfareSyncData == null)
			{
				warfareGameData.isTurnA = false;
				warfareGameData.SetUnitTurnEnd(false);
				this.OnCommonUserTurnFinished(false);
				this.SetTileDefaultWhenPlay();
				this.UpdateLabel(true);
				this.m_unitMgr.UpdateGameUnitUI();
				return;
			}
			warfareGameData.SetUnitTurnEnd(false);
			this.ProcessWarfareGameSyncData(cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK.warfareSyncData);
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x002B08A8 File Offset: 0x002AEAA8
		public void OnRecv(NKMPacket_WARFARE_GAME_USE_SERVICE_ACK cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK)
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK.warfareGameUnitUID);
			if (nkcwarfareGameUnit != null)
			{
				this.UseServiceFX(nkcwarfareGameUnit, cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK.warfareServiceType);
			}
			NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_unitMgr.GetNKCWarfareGameUnitInfo(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK.warfareGameUnitUID);
			if (nkcwarfareGameUnitInfo != null)
			{
				nkcwarfareGameUnitInfo.SetUnitInfoUI();
				nkcwarfareGameUnitInfo.OnPlayServiceSound(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK.warfareServiceType);
			}
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x002B090A File Offset: 0x002AEB0A
		public void InitWaitNextOrder()
		{
			this.m_bWaitingNextOreder = false;
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x002B0914 File Offset: 0x002AEB14
		public void OnRecv(NKMPacket_WARFARE_GAME_NEXT_ORDER_ACK cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK)
		{
			Debug.Log("warfare next order ack process start");
			this.InvalidSelectedUnit();
			this.ProcessWarfareGameSyncData(cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.warfareSyncData);
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				myUserData.m_ArmyData.ResetDeckStateOf(NKM_DECK_STATE.DECK_STATE_WARFARE);
				int flagShipWarfareUnitUID = nkmwarfareGameData.warfareTeamDataA.flagShipWarfareUnitUID;
				bool flag = !NKCScenManager.GetScenManager().GetMyUserData().CheckWarfareClear(this.GetNKMWarfareGameData().warfareTempletID);
				bool bNoAllClearBefore = flag;
				if (!flag)
				{
					NKMWarfareClearData warfareClearData = myUserData.GetWarfareClearData(this.GetNKMWarfareGameData().warfareTempletID);
					if (warfareClearData != null && (!warfareClearData.m_mission_result_1 || !warfareClearData.m_mission_result_2 || !warfareClearData.m_MissionRewardResult || (cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.warfareClearData != null && cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.warfareClearData.m_MissionReward != null)))
					{
						bNoAllClearBefore = true;
					}
				}
				this.m_NKMWarfareClearData = cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.warfareClearData;
				myUserData.SetWarfareClearDataOnlyTrue(this.m_NKMWarfareClearData);
				this.m_TeamAFlagDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, (int)nkmwarfareGameData.flagshipDeckIndex);
				NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
				if (this.m_NKMWarfareClearData != null)
				{
					string warfareStrID = NKCWarfareManager.GetWarfareStrID(nkmwarfareGameData.warfareTempletID);
					if (!string.IsNullOrEmpty(warfareStrID))
					{
						string key = string.Format("{0}_{1}", myUserData.m_UserUID, warfareStrID);
						if (PlayerPrefs.HasKey(key))
						{
							PlayerPrefs.DeleteKey(key);
						}
						myUserData.UpdateStagePlayData(this.m_NKMWarfareClearData.m_StagePlayData);
					}
					myUserData.GetReward(this.m_NKMWarfareClearData.m_RewardDataList);
					if (this.m_NKMWarfareClearData.m_ContainerRewards != null)
					{
						myUserData.GetReward(this.m_NKMWarfareClearData.m_ContainerRewards);
					}
					if (this.m_NKMWarfareClearData.m_OnetimeRewards != null)
					{
						myUserData.GetReward(this.m_NKMWarfareClearData.m_OnetimeRewards);
					}
					if (this.m_NKMWarfareClearData.m_MissionReward != null)
					{
						myUserData.GetReward(this.m_NKMWarfareClearData.m_MissionReward);
					}
					NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, this.m_NKMWarfareClearData.m_WarfareID);
					if (nkcrepeatOperaion.GetIsOnGoing())
					{
						nkcrepeatOperaion.AddReward(this.m_NKMWarfareClearData.m_RewardDataList);
						nkcrepeatOperaion.AddReward(this.m_NKMWarfareClearData.m_ContainerRewards);
						nkcrepeatOperaion.AddReward(this.m_NKMWarfareClearData.m_OnetimeRewards);
						nkcrepeatOperaion.AddReward(this.m_NKMWarfareClearData.m_MissionReward);
						nkcrepeatOperaion.SetCurrRepeatCount(nkcrepeatOperaion.GetCurrRepeatCount() + 1L);
						if (nkcrepeatOperaion.GetCurrRepeatCount() >= nkcrepeatOperaion.GetMaxRepeatCount())
						{
							nkcrepeatOperaion.Init();
							nkcrepeatOperaion.SetStopReason(NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_TERMINATED);
							nkcrepeatOperaion.SetAlarmRepeatOperationSuccess(true);
						}
					}
					NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.WarfareGameClear, nkmwarfareGameData.warfareTempletID, warfareStrID);
				}
				else if (nkcrepeatOperaion.GetIsOnGoing())
				{
					nkcrepeatOperaion.SetCurrRepeatCount(nkcrepeatOperaion.GetCurrRepeatCount() + 1L);
					nkcrepeatOperaion.Init();
					nkcrepeatOperaion.SetAlarmRepeatOperationQuitByDefeat(true);
				}
				myUserData.UpdateEpisodeCompleteData(cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.episodeCompleteData);
				WarfareSupporterListData guestSptData = null;
				if (nkmwarfareGameData.supportUnitData != null && NKCWarfareManager.IsGeustSupporter(nkmwarfareGameData.supportUnitData.commonProfile.friendCode))
				{
					guestSptData = nkmwarfareGameData.supportUnitData;
				}
				this.m_NUM_WARFARE.SetActive(false);
				NKCPopupRepeatOperation.CheckInstanceAndClose();
				NKCUIWarfareResult.Instance.Open(this.m_NKMWarfareClearData, this.m_TeamAFlagDeckIndex, new NKCUIWarfareResult.CallBackWhenClosed(this.OnCallBackForResult), flag, bNoAllClearBefore, guestSptData);
			}
			this.m_NKCWarfareGameHUD.UpdateMedalInfo();
			this.m_NKCWarfareGameHUD.UpdateWinCondition();
			this.m_NKCWarfareGameHUD.SetRemainTurnOnUnitCount(this.m_unitMgr.GetRemainTurnOnUserUnitCount());
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x002B0C53 File Offset: 0x002AEE53
		public void OnRecv(NKMPacket_WARFARE_GAME_MOVE_ACK cNKMPacket_WARFARE_GAME_MOVE_ACK)
		{
			this.ProcessWarfareGameSyncData(cNKMPacket_WARFARE_GAME_MOVE_ACK.warfareSyncData);
			this.UpdateSelectedSquadUI();
		}

		// Token: 0x06007FD4 RID: 32724 RVA: 0x002B0C67 File Offset: 0x002AEE67
		public void OnRecv(NKMPacket_WARFARE_FRIEND_LIST_ACK sPacket)
		{
			this.OpenDeckView();
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x002B0C6F File Offset: 0x002AEE6F
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x002B0C78 File Offset: 0x002AEE78
		private int GetDivePointTileIndex(int divePointIndex)
		{
			if (this.m_NKMWarfareMapTemplet == null)
			{
				return -1;
			}
			return this.m_NKMWarfareMapTemplet.GetDivePointTileIndex(divePointIndex);
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x002B0C90 File Offset: 0x002AEE90
		private int GetDivePointIndex(int tileIndex)
		{
			if (this.m_NKMWarfareMapTemplet == null)
			{
				return -1;
			}
			return this.m_NKMWarfareMapTemplet.GetDivePointIndex(tileIndex);
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x002B0CA8 File Offset: 0x002AEEA8
		private int GetAssultPointIndex(int tileIndex)
		{
			if (this.m_NKMWarfareMapTemplet == null)
			{
				return -1;
			}
			return this.m_NKMWarfareMapTemplet.GetAssultPointIndex(tileIndex);
		}

		// Token: 0x06007FD9 RID: 32729 RVA: 0x002B0CC0 File Offset: 0x002AEEC0
		private int GetAssultPointTileIndex(int assultPointIndex)
		{
			if (this.m_NKMWarfareMapTemplet == null)
			{
				return -1;
			}
			return this.m_NKMWarfareMapTemplet.GetAssultPointTileIndex(assultPointIndex);
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x002B0CD8 File Offset: 0x002AEED8
		private bool SetActiveDivePoint(int tileIndex, bool bSet)
		{
			if (this.m_listDivePoint.Count == 0)
			{
				return false;
			}
			int divePointIndex = this.GetDivePointIndex(tileIndex);
			if (divePointIndex != -1)
			{
				GameObject gameObject = this.m_listDivePoint[divePointIndex];
				if (bSet && gameObject != null)
				{
					gameObject.transform.localPosition = this.m_listUnitPos[tileIndex];
					NKCUIComButton cNKCUIComButton = gameObject.GetComponent<NKCUIComButton>();
					if (cNKCUIComButton != null)
					{
						cNKCUIComButton.m_DataInt = divePointIndex;
						cNKCUIComButton.PointerClick.RemoveAllListeners();
						cNKCUIComButton.PointerClick.AddListener(delegate()
						{
							this.OnClickDivePoint(cNKCUIComButton.m_DataInt);
						});
					}
				}
				NKCUtil.SetGameobjectActive(gameObject, bSet);
				return true;
			}
			return false;
		}

		// Token: 0x06007FDB RID: 32731 RVA: 0x002B0D9C File Offset: 0x002AEF9C
		private GameObject GetDivePointGO(int tileIndex)
		{
			if (this.GetDivePointIndex(tileIndex) != -1)
			{
				return this.m_listDivePoint[this.GetDivePointIndex(tileIndex)];
			}
			return null;
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x002B0DBC File Offset: 0x002AEFBC
		private bool SetActiveAssultPoint(int tileIndex, bool bSet)
		{
			if (this.m_listAssultPoint.Count == 0)
			{
				return false;
			}
			int assultPointIndex = this.GetAssultPointIndex(tileIndex);
			if (assultPointIndex != -1)
			{
				GameObject gameObject = this.m_listAssultPoint[assultPointIndex];
				if (bSet && gameObject != null)
				{
					gameObject.transform.localPosition = this.m_listUnitPos[tileIndex];
					NKCUIComButton cNKCUIComButton = gameObject.GetComponent<NKCUIComButton>();
					if (cNKCUIComButton != null)
					{
						cNKCUIComButton.m_DataInt = assultPointIndex;
						cNKCUIComButton.PointerClick.RemoveAllListeners();
						cNKCUIComButton.PointerClick.AddListener(delegate()
						{
							this.OnClickAssultPoint(cNKCUIComButton.m_DataInt);
						});
					}
				}
				NKCUtil.SetGameobjectActive(gameObject, bSet);
				return true;
			}
			return false;
		}

		// Token: 0x06007FDD RID: 32733 RVA: 0x002B0E80 File Offset: 0x002AF080
		private GameObject GetAssultPointGO(int tileIndex)
		{
			if (this.GetAssultPointIndex(tileIndex) != -1)
			{
				return this.m_listAssultPoint[this.GetAssultPointIndex(tileIndex)];
			}
			return null;
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x002B0EA0 File Offset: 0x002AF0A0
		private void TurnOnAllAvaiableUserUnitSpawnPoint()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			if (this.GetNKMWarfareGameData() == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < mapTemplet.TileCount; i++)
			{
				NKMWarfareTileTemplet tile = mapTemplet.GetTile(i);
				if (tile != null)
				{
					if (tile.m_TileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
					{
						this.SetActiveDivePoint(i, false);
						this.SetActiveAssultPoint(i, false);
					}
					else if (tile.m_DungeonStrID == null && this.m_unitMgr.GetGameUnitByTileIndex(i) == null)
					{
						bool flag = false;
						bool flag2 = false;
						if (this.SetActiveDivePoint(i, true))
						{
							flag = true;
						}
						if (this.SetActiveAssultPoint(i, true))
						{
							flag2 = true;
						}
						if (flag || flag2)
						{
							if (flag)
							{
								this.m_tileMgr.SetTileLayer0Type(i, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_DIVE_POINT);
							}
							else if (flag2)
							{
								this.m_tileMgr.SetTileLayer0Type(i, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ASSULT_POINT);
							}
							GameObject gameObject = this.GetDivePointGO(i);
							if (gameObject == null)
							{
								gameObject = this.GetAssultPointGO(i);
							}
							this.AnimateSpawnPoint(gameObject, (float)num * 0.1f);
							num++;
						}
					}
				}
			}
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x002B0FB8 File Offset: 0x002AF1B8
		private void AnimateSpawnPoint(GameObject spawnPoint, float fDelay)
		{
			if (spawnPoint == null)
			{
				return;
			}
			spawnPoint.transform.localScale = new Vector3(1f, 0f, 1f);
			spawnPoint.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack, 2.5f).SetDelay(fDelay);
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x002B1028 File Offset: 0x002AF228
		private void TurnOffAllUserUnitSpawnPoint()
		{
			for (int i = 0; i < this.m_listDivePoint.Count; i++)
			{
				GameObject gameObject = this.m_listDivePoint[i];
				if (gameObject != null)
				{
					NKCUtil.SetGameobjectActive(gameObject, false);
				}
			}
			for (int i = 0; i < this.m_listAssultPoint.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_listAssultPoint[i], false);
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			if (this.GetNKMWarfareGameData() == null)
			{
				return;
			}
			for (int i = 0; i < mapTemplet.TileCount; i++)
			{
				NKMWarfareTileTemplet tile = mapTemplet.GetTile(i);
				if (tile != null && tile.m_TileType != NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE && tile.m_DungeonStrID == null)
				{
					bool flag = false;
					bool flag2 = false;
					if (this.GetDivePointIndex(i) != -1)
					{
						flag = true;
					}
					if (this.GetAssultPointIndex(i) != -1)
					{
						flag2 = true;
					}
					if (flag || flag2)
					{
						this.m_tileMgr.SetTileLayer0Type(i, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
					}
				}
			}
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x002B111C File Offset: 0x002AF31C
		private void PreProcessSpawnPoint()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return;
			}
			if (this.m_listDivePoint.Count < mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE))
			{
				int num = mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE) - this.m_listDivePoint.Count;
				for (int i = 0; i < num; i++)
				{
					NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_DIVE_POINT", false, null);
					GameObject instant = nkcassetInstanceData.m_Instant;
					this.m_listAssetInstance.Add(nkcassetInstanceData);
					instant.transform.SetParent(this.m_NUM_WARFARE_DIVE_POINT.transform);
					this.m_listDivePoint.Add(instant);
					NKCUtil.SetGameobjectActive(instant, false);
				}
			}
			if (this.m_listAssultPoint.Count < mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT))
			{
				int num2 = mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT) - this.m_listAssultPoint.Count;
				for (int j = 0; j < num2; j++)
				{
					NKCAssetInstanceData nkcassetInstanceData2 = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_ASSULT_POINT", false, null);
					GameObject instant2 = nkcassetInstanceData2.m_Instant;
					this.m_listAssetInstance.Add(nkcassetInstanceData2);
					instant2.transform.SetParent(this.m_NUM_WARFARE_DIVE_POINT.transform);
					this.m_listAssultPoint.Add(instant2);
					NKCUtil.SetGameobjectActive(instant2, false);
				}
			}
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x002B125C File Offset: 0x002AF45C
		private void UpdateRecoveryCount()
		{
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				this.m_NKCWarfareGameHUD.SetActiveRecovery(false);
				return;
			}
			if (nkmwarfareGameData.isTurnA && !this.IsAutoWarfare())
			{
				int recoverableUnitCount = NKCWarfareManager.GetRecoverableUnitCount(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData);
				this.m_NKCWarfareGameHUD.SetActiveRecovery(recoverableUnitCount > 0);
				this.m_NKCWarfareGameHUD.SetRecoveryCount(recoverableUnitCount);
				return;
			}
			this.m_NKCWarfareGameHUD.SetActiveRecovery(false);
		}

		// Token: 0x06007FE3 RID: 32739 RVA: 0x002B12D4 File Offset: 0x002AF4D4
		public void OnTouchRecoveryBtn()
		{
			if (this.m_bSelectRecovery)
			{
				this.InvalidSelectedUnit();
				return;
			}
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			List<WarfareUnitData> list = nkmwarfareGameData.warfareTeamDataA.warfareUnitDataByUIDMap.Values.ToList<WarfareUnitData>();
			if (NKCWarfareManager.GetRecoverableUnitCount(myUserData.m_ArmyData) <= 0)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_RECOVERY_NO_UNIT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.InvalidSelectedUnit();
			bool flag = false;
			List<int> recoveryPoint = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				WarfareUnitData warfareUnitData = list[i];
				if (warfareUnitData.hp > 0f && !(this.m_unitMgr.GetNKCWarfareGameUnit(warfareUnitData.warfareGameUnitUID) == null))
				{
					NKM_UNIT_STYLE_TYPE shipStyleTypeByGUUID = NKCWarfareManager.GetShipStyleTypeByGUUID(myUserData, nkmwarfareGameData, warfareUnitData.warfareGameUnitUID);
					if (this.SetTilePossibleRecovery(shipStyleTypeByGUUID, (int)warfareUnitData.tileIndex, ref recoveryPoint))
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_RECOVERY_NO_TILE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.m_bSelectRecovery = true;
			this.m_NKCWarfareGameHUD.SetRecoveryFx(true);
			this.SetRecoveryPoint(recoveryPoint);
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x002B13F1 File Offset: 0x002AF5F1
		private void CancelRecovery()
		{
			this.m_NKCWarfareGameHUD.SetRecoveryFx(false);
			this.m_bSelectRecovery = false;
			this.SetRecoveryPoint(null);
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x002B1410 File Offset: 0x002AF610
		private bool SetTilePossibleRecovery(NKM_UNIT_STYLE_TYPE moveType, int pivotTileIndex, ref List<int> listTile)
		{
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return false;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return false;
			}
			int posXByIndex = mapTemplet.GetPosXByIndex(pivotTileIndex);
			int posYByIndex = mapTemplet.GetPosYByIndex(pivotTileIndex);
			MovableTileSet tileSet = MovableTileSet.GetTileSet(moveType);
			bool result = false;
			int num = 0;
			int num2 = 2;
			for (int i = posXByIndex - num2; i <= posXByIndex + num2; i++)
			{
				int num3 = 0;
				for (int j = posYByIndex - num2; j <= posYByIndex + num2; j++)
				{
					if (tileSet[num3++, num])
					{
						int indexByPos = (int)mapTemplet.GetIndexByPos(i, j);
						if (indexByPos >= 0)
						{
							WarfareTileData tileData = nkmwarfareGameData.GetTileData(indexByPos);
							if (tileData.tileType != NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE && tileData.tileType != NKM_WARFARE_MAP_TILE_TYPE.NWMTT_INCR)
							{
								NKMWarfareTileTemplet tile = mapTemplet.GetTile(indexByPos);
								if (tile != null && tile.m_TileWinType != WARFARE_GAME_CONDITION.WFC_TILE_ENTER && !(this.m_unitMgr.GetGameUnitByTileIndex(indexByPos) != null))
								{
									this.m_tileMgr.SetTileLayer0Type(indexByPos, NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_RECOVERY_POINT);
									result = true;
									if (!listTile.Contains(indexByPos))
									{
										listTile.Add(indexByPos);
									}
								}
							}
						}
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x002B1540 File Offset: 0x002AF740
		private void SetRecoveryPoint(List<int> listTile)
		{
			int num = 0;
			if (listTile != null)
			{
				num = listTile.Count;
			}
			if (this.m_listRecoveryPoint.Count < num)
			{
				int num2 = num - this.m_listRecoveryPoint.Count;
				for (int i = 0; i < num2; i++)
				{
					NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_RECOVERY_POINT", false, null);
					GameObject instant = nkcassetInstanceData.m_Instant;
					this.m_listAssetInstance.Add(nkcassetInstanceData);
					instant.transform.SetParent(this.m_NUM_WARFARE_DIVE_POINT.transform);
					instant.transform.localScale = Vector3.one;
					this.m_listRecoveryPoint.Add(instant);
					NKCUtil.SetGameobjectActive(instant, false);
				}
			}
			for (int j = 0; j < this.m_listRecoveryPoint.Count; j++)
			{
				GameObject gameObject = this.m_listRecoveryPoint[j];
				NKCUtil.SetGameobjectActive(gameObject, j < num);
				if (j < num)
				{
					int tileIndex = listTile[j];
					gameObject.transform.localPosition = this.m_listUnitPos[tileIndex];
					NKCUIComButton component = gameObject.GetComponent<NKCUIComButton>();
					if (component != null)
					{
						component.PointerClick.RemoveAllListeners();
						component.PointerClick.AddListener(delegate()
						{
							this.OnTouchRecoveryTile(tileIndex);
						});
					}
				}
			}
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x002B1698 File Offset: 0x002AF898
		private void OnTouchRecoveryTile(int tileIndex)
		{
			List<int> recoverableDeckIndexList = NKCWarfareManager.GetRecoverableDeckIndexList(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData);
			if (recoverableDeckIndexList.Count == 0)
			{
				Debug.LogError("Warfare - ClickRecoveryTile - 복구할 유닛이 없다고 ..??");
				return;
			}
			this.m_LastClickedSpawnPoint = tileIndex;
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_MENU_NAME_WARFARE;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.WarfareRecovery;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnRecoveryDeck);
			options.dCheckSideMenuButton = new NKCUIDeckViewer.DeckViewerOption.CheckDeckButtonConfirm(this.CheckRecoveryDeck);
			options.DeckListButtonStateText = NKCUtilString.GET_STRING_WARFARE_RECOVERABLE;
			recoverableDeckIndexList.Sort();
			options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, recoverableDeckIndexList[0]);
			options.ShowDeckIndexList = recoverableDeckIndexList;
			options.upsideMenuShowResourceList = this.UpsideMenuShowResourceList;
			options.SelectLeaderUnitOnOpen = false;
			options.bEnableDefaultBackground = false;
			options.bUpsideMenuHomeButton = false;
			options.bOpenAlphaAni = true;
			options.bUsableSupporter = false;
			options.CostItemID = 101;
			int rewardMultiply = this.GetNKMWarfareGameData().rewardMultiply;
			options.CostItemCount = NKMCommonConst.WarfareRecoverItemCost;
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_WarfareStrID);
			if (nkmstageTempletV != null)
			{
				options.StageBattleStrID = nkmstageTempletV.m_StageBattleStrID;
			}
			options.dOnHide = delegate()
			{
				this.SetActiveForDeckView(false);
			};
			options.dOnUnhide = delegate()
			{
				this.SetActiveForDeckView(true);
			};
			this.m_bOpenDeckView = true;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x06007FE8 RID: 32744 RVA: 0x002B17F4 File Offset: 0x002AF9F4
		private NKM_ERROR_CODE CheckRecoveryDeck(NKMDeckIndex deckIndex)
		{
			NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID;
			}
			if (deckData.GetState() != NKM_DECK_STATE.DECK_STATE_WARFARE)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_UNIT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06007FE9 RID: 32745 RVA: 0x002B1828 File Offset: 0x002AFA28
		private void OnRecoveryDeck(NKMDeckIndex deckIndex)
		{
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_WARFARE_RECOVERY, NKCUtilString.GET_STRING_WARFARE_RECOVERY_CONFIRM, 101, NKMCommonConst.WarfareRecoverItemCost, delegate()
			{
				this.RecoveryDeck(deckIndex);
			}, null, false);
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x002B1874 File Offset: 0x002AFA74
		private void RecoveryDeck(NKMDeckIndex deckIndex)
		{
			if (this.CheckRecoveryDeck(deckIndex) != NKM_ERROR_CODE.NEC_OK)
			{
				Debug.LogError(string.Format("긴급복구 불가능한 덱 ({0}, {1})", deckIndex.m_eDeckType, deckIndex.m_iIndex));
				return;
			}
			Debug.Log(string.Format("긴급복구 - {0}, tile {1}", deckIndex.m_iIndex, this.m_LastClickedSpawnPoint));
			this.SetPause(true);
			NKCPacketSender.Send_NKMPacket_WARFARE_RECOVER_REQ(deckIndex.m_iIndex, (short)this.m_LastClickedSpawnPoint);
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x002B18EE File Offset: 0x002AFAEE
		public void OnRecv(NKMPacket_WARFARE_RECOVER_ACK res)
		{
			this.InvalidSelectedUnit();
			NKCUIDeckViewer.Instance.Close();
			this.ProcessWarfareGameSyncData(res.warfareSyncData);
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x002B190C File Offset: 0x002AFB0C
		private void CamTrackMovingUnit(Vector3 _orgPos, Vector3 _targetPos)
		{
			if (this.m_bRunningCamTrackMovingUnit)
			{
				this.StopCamTrackMovingUnit();
			}
			this.m_CamTrackMovingUnitCoroutine = base.StartCoroutine(this._CamTrackMovingUnit(_orgPos, _targetPos));
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x002B1930 File Offset: 0x002AFB30
		private IEnumerator _CamTrackMovingUnit(Vector3 _orgPos, Vector3 _targetPos)
		{
			this.m_bRunningCamTrackMovingUnit = true;
			float fDeltaTime = 0f;
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(_targetPos.x, _targetPos.y + -250f, (float)(this.GetFinalCameraZDist() + 50)), 0.9f, TRACKING_DATA_TYPE.TDT_SLOWER);
			fDeltaTime += Time.deltaTime;
			yield return null;
			while (fDeltaTime < 0.9f)
			{
				yield return null;
				if (!this.GetPause())
				{
					fDeltaTime += Time.deltaTime;
				}
			}
			this.SetCamZoomOut(1.3499999f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_bRunningCamTrackMovingUnit = false;
			yield break;
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x002B1946 File Offset: 0x002AFB46
		public void StopCamTrackMovingUnit()
		{
			if (this.m_CamTrackMovingUnitCoroutine != null)
			{
				base.StopCoroutine(this.m_CamTrackMovingUnitCoroutine);
			}
			this.m_CamTrackMovingUnitCoroutine = null;
			this.m_bRunningCamTrackMovingUnit = false;
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x002B196C File Offset: 0x002AFB6C
		private void TryGameLoadReq()
		{
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData == null)
			{
				return;
			}
			this.PlayBattleVoice(nkmwarfareGameData.warfareTeamDataA);
			this.PlayBattleVoice(nkmwarfareGameData.warfareTeamDataB);
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(nkmwarfareGameData.battleMonsterUid);
			if (nkcwarfareGameUnit != null)
			{
				float num = -100f;
				this.StopCamTrackMovingUnit();
				NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(nkcwarfareGameUnit.transform.localPosition.x + num, nkcwarfareGameUnit.transform.localPosition.y - 100f, (float)(this.GetFinalCameraZDist() + 500)), 0.6f, TRACKING_DATA_TYPE.TDT_FASTER);
				this.m_fElapsedTimeToBattle = 0f;
				this.m_bReservedBattle = true;
				NKCCamera.SetFocusBlur(0.6f, 0.5f, 0.5f, 0f);
			}
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x002B1A44 File Offset: 0x002AFC44
		private void PlayBattleVoice(WarfareTeamData teamData)
		{
			if (teamData != null && teamData.warfareUnitDataByUIDMap != null && teamData.warfareUnitDataByUIDMap.ContainsKey(teamData.flagShipWarfareUnitUID))
			{
				WarfareUnitData warfareUnitData = teamData.warfareUnitDataByUIDMap[teamData.flagShipWarfareUnitUID];
				if (warfareUnitData.unitType == WarfareUnitData.Type.User)
				{
					NKCOperatorUtil.PlayVoice(warfareUnitData.deckIndex, VOICE_TYPE.VT_SHIP_MEET, true);
				}
			}
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x002B1A98 File Offset: 0x002AFC98
		public void OnDragByInstance(BaseEventData cBaseEventData)
		{
			if (this.m_bPlayingIntro)
			{
				return;
			}
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			float num = NKCCamera.GetPosNowX(false) - pointerEventData.delta.x * 10f;
			float num2 = NKCCamera.GetPosNowY(false) - pointerEventData.delta.y * 10f;
			num = Mathf.Clamp(num, this.m_rtCamBound.xMin, this.m_rtCamBound.xMax);
			num2 = Mathf.Clamp(num2, this.m_rtCamBound.yMin, this.m_rtCamBound.yMax);
			NKCCamera.TrackingPos(1f, num, num2, -1f);
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x002B1B32 File Offset: 0x002AFD32
		private void SetCamDefaultWhenPlaying(float fTime = 1.6f, TRACKING_DATA_TYPE trackingData = TRACKING_DATA_TYPE.TDT_SLOWER)
		{
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(0f, -308f, (float)this.GetFinalCameraZDist()), fTime, trackingData);
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x002B1B56 File Offset: 0x002AFD56
		private void SetCamZoomOut(float fTime = 1.6f, TRACKING_DATA_TYPE trackingData = TRACKING_DATA_TYPE.TDT_SLOWER)
		{
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(NKCCamera.GetTrackingPos().GetNowValueX(), NKCCamera.GetTrackingPos().GetNowValueY(), (float)this.GetFinalCameraZDist()), fTime, trackingData);
		}

		// Token: 0x06007FF4 RID: 32756 RVA: 0x002B1B84 File Offset: 0x002AFD84
		private int GetFinalCameraZDist()
		{
			float num = (float)Screen.height / NKCUIManager.GetUIFrontCanvasScaler().referenceResolution.y;
			if (num <= 1f)
			{
				num = 1f;
			}
			num -= (num - 1f) * 0.5f;
			return (int)(-798f * num);
		}

		// Token: 0x06007FF5 RID: 32757 RVA: 0x002B1BD0 File Offset: 0x002AFDD0
		private void SetCameraIntro()
		{
			this.m_bPlayingIntro = true;
			NKCCamera.SetPos(this.m_CameraIntroPos.x, this.m_CameraIntroPos.y, this.m_CameraIntroPos.z, true, true);
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(0f, -344f, (float)this.GetFinalCameraZDist()), 1.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
			NKCCamera.GetTrackingRotation().SetNowValue(this.m_CameraIntroRot.x, this.m_CameraIntroRot.y, this.m_CameraIntroRot.z);
			NKCCamera.GetTrackingRotation().SetTracking(-20f, 0f, 0f, 1.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x002B1C7C File Offset: 0x002AFE7C
		private void AddAssistFX(Vector3 start, Vector3 target)
		{
			NKCWarfareGameAssist newInstance = NKCWarfareGameAssist.GetNewInstance(this.m_NUM_WARFARE_LABEL.transform, start, target);
			if (newInstance == null)
			{
				return;
			}
			this.m_listAssist.Add(newInstance);
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x002B1CB4 File Offset: 0x002AFEB4
		private void CloseAssistFX()
		{
			for (int i = 0; i < this.m_listAssist.Count; i++)
			{
				this.m_listAssist[i].Close();
			}
			this.m_listAssist.Clear();
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x002B1CF3 File Offset: 0x002AFEF3
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Warfare, true);
		}

		// Token: 0x06007FF9 RID: 32761 RVA: 0x002B1CFE File Offset: 0x002AFEFE
		private void CheckTutorialAfterBattle()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.WarfareBattle, true);
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x002B1D09 File Offset: 0x002AFF09
		private void CheckTutorialAfterStart()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.WarfareStart, true);
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x002B1D14 File Offset: 0x002AFF14
		private void CheckTutorialAfterSync()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.WarfareSync, true);
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x002B1D20 File Offset: 0x002AFF20
		public GameObject GetTileObject(int tileIndex)
		{
			NKCWarfareGameTile tile = this.m_tileMgr.GetTile(tileIndex);
			if (tile == null)
			{
				return null;
			}
			return tile.GetActiveGameObject();
		}

		// Token: 0x06007FFD RID: 32765 RVA: 0x002B1D4C File Offset: 0x002AFF4C
		public GameObject GetUnitObject(int gameUnitUID)
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(gameUnitUID);
			if (nkcwarfareGameUnit == null)
			{
				return null;
			}
			return nkcwarfareGameUnit.gameObject;
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x002B1D78 File Offset: 0x002AFF78
		public void ProcessTutorialTileTouchEvent(NKCGameEventManager.NKCGameEventTemplet eventTemplet)
		{
			if (eventTemplet == null)
			{
				Log.Warn("[ProcessTutorialTileTouchEvent] eventTemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCWarfareGame.cs", 5526);
				return;
			}
			if (eventTemplet.EventType != NKCGameEventManager.GameEventType.TUTORIAL_CLICK_WARFARE_TILE)
			{
				return;
			}
			string stringValue = eventTemplet.StringValue;
			if (stringValue != null)
			{
				if (stringValue == "CLICK_DIVE_POINT")
				{
					int divePointIndex = this.GetDivePointIndex(eventTemplet.Value);
					this.OnClickDivePoint(divePointIndex);
					return;
				}
				if (!(stringValue == "CLICK_SHIP"))
				{
					if (!(stringValue == "CLICK_MOVE_SHIP"))
					{
						return;
					}
					this.OnClickPossibleArrivalTile(eventTemplet.Value);
				}
				else
				{
					WarfareUnitData unitDataByTileIndex = this.GetNKMWarfareGameData().GetUnitDataByTileIndex(eventTemplet.Value);
					if (unitDataByTileIndex != null)
					{
						this.OnClickUnit(unitDataByTileIndex.warfareGameUnitUID);
						return;
					}
					Log.Warn(string.Format("[ProcessTutorialTileTouchEvent] {0} 타일 위 유닛을 찾을 수 없음", eventTemplet.Value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCWarfareGame.cs", 5548);
					return;
				}
			}
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x002B1E45 File Offset: 0x002B0045
		public void SetAutoForTutorial(bool bAuto)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfare == bAuto)
			{
				return;
			}
			Debug.Log("NKMPacket_WARFARE_GAME_AUTO_REQ - NKCWarfareGame.SetAutoForTutorial");
			this.SetPause(true);
			this.WaitAutoPacket = true;
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_AUTO_REQ(bAuto);
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x002B1E80 File Offset: 0x002B0080
		public int GetTutorialSelectDeck()
		{
			if (NKCGameEventManager.IsEventPlaying())
			{
				WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
				if (nkmwarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING && nkmwarfareGameData.isTurnA)
				{
					NKCWarfareGameUnit nkcwarfareGameUnit = this.m_unitMgr.GetNKCWarfareGameUnit(this.GetSelectedSquadWFGUUID());
					if (nkcwarfareGameUnit != null && nkcwarfareGameUnit.GetNKMWarfareUnitData() != null)
					{
						return (int)nkcwarfareGameUnit.GetNKMWarfareUnitData().deckIndex.m_iIndex;
					}
				}
			}
			return -1;
		}

		// Token: 0x06008001 RID: 32769 RVA: 0x002B1EE4 File Offset: 0x002B00E4
		public void RefreshTutorialSelectDeck(int deckIndex)
		{
			if (deckIndex < 0)
			{
				return;
			}
			WarfareGameData nkmwarfareGameData = this.GetNKMWarfareGameData();
			if (nkmwarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				return;
			}
			if (!nkmwarfareGameData.isTurnA)
			{
				return;
			}
			WarfareUnitData unitDataByNormalDeckIndex = nkmwarfareGameData.GetUnitDataByNormalDeckIndex((byte)deckIndex);
			if (unitDataByNormalDeckIndex != null)
			{
				this.OnClickUnit(unitDataByNormalDeckIndex.warfareGameUnitUID);
			}
		}

		// Token: 0x04006BDC RID: 27612
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_warfare";

		// Token: 0x04006BDD RID: 27613
		private const string UI_ASSET_NAME = "NUM_WARFARE_UI";

		// Token: 0x04006BDE RID: 27614
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04006BDF RID: 27615
		private List<int> RESOURCE_LIST = new List<int>();

		// Token: 0x04006BE0 RID: 27616
		public NKCWarfareGameHUD m_NKCWarfareGameHUD;

		// Token: 0x04006BE1 RID: 27617
		private GameObject m_NUM_WARFARE;

		// Token: 0x04006BE2 RID: 27618
		public GameObject m_NUM_WARFARE_TILE_Panel;

		// Token: 0x04006BE3 RID: 27619
		public GameObject m_NUM_WARFARE_UNIT_LIST;

		// Token: 0x04006BE4 RID: 27620
		public GameObject m_NUM_WARFARE_UNIT_INFO_LIST;

		// Token: 0x04006BE5 RID: 27621
		public GameObject m_NUM_WARFARE_DIVE_POINT;

		// Token: 0x04006BE6 RID: 27622
		public GameObject m_NUM_WARFARE_LABEL;

		// Token: 0x04006BE7 RID: 27623
		public GameObject m_NUM_WARFARE_BATTILE_CONDITION_LIST;

		// Token: 0x04006BE8 RID: 27624
		public GameObject m_NUM_WARFARE_BG_A;

		// Token: 0x04006BE9 RID: 27625
		public Image m_NUM_WARFARE_BG_A_img;

		// Token: 0x04006BEA RID: 27626
		public GameObject m_NUM_WARFARE_BG_B;

		// Token: 0x04006BEB RID: 27627
		public Image m_NUM_WARFARE_BG_B_img;

		// Token: 0x04006BEC RID: 27628
		public GameObject m_NUM_WARFARE_BG_IMG_B;

		// Token: 0x04006BED RID: 27629
		public GameObject m_NUM_WARFARE_BG_B_MOVIE;

		// Token: 0x04006BEE RID: 27630
		public GameObject m_NUM_WARFARE_BG_B_MOVIE_IMG;

		// Token: 0x04006BEF RID: 27631
		public GameObject m_NUM_WARFARE_FX_SHIP_DIVE;

		// Token: 0x04006BF0 RID: 27632
		public GameObject m_NUM_WARFARE_FX_SHIP_DIVE_CANCEL;

		// Token: 0x04006BF1 RID: 27633
		public GameObject m_NUM_WARFARE_FX_UNIT_EXPLOSION;

		// Token: 0x04006BF2 RID: 27634
		public GameObject m_NUM_WARFARE_FX_UNIT_EXPLOSION_BIG;

		// Token: 0x04006BF3 RID: 27635
		public GameObject m_NUM_WARFARE_FX_UNIT_ESCAPE;

		// Token: 0x04006BF4 RID: 27636
		public GridLayoutGroup m_NUM_WARFARE_TILE_Panel_GLG;

		// Token: 0x04006BF5 RID: 27637
		public RectTransform m_NUM_WARFARE_BG_WARBOX_A;

		// Token: 0x04006BF6 RID: 27638
		public RectTransform m_NUM_WARFARE_BG_WARBOX_B;

		// Token: 0x04006BF7 RID: 27639
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04006BF8 RID: 27640
		public float m_fStartAlhpa = 0.1f;

		// Token: 0x04006BF9 RID: 27641
		public float m_fFadeTime = 1f;

		// Token: 0x04006BFA RID: 27642
		[Header("카메라 초기값")]
		public Vector3 m_CameraIntroPos;

		// Token: 0x04006BFB RID: 27643
		public Vector3 m_CameraIntroRot;

		// Token: 0x04006BFC RID: 27644
		private NKCWarfareGameTileMgr m_tileMgr;

		// Token: 0x04006BFD RID: 27645
		private NKCWarfareGameUnitMgr m_unitMgr;

		// Token: 0x04006BFE RID: 27646
		private NKCWarfareGameLabelMgr m_labelMgr;

		// Token: 0x04006BFF RID: 27647
		private NKCWarfareGameBattleCondition m_battleCondition;

		// Token: 0x04006C00 RID: 27648
		private NKCWarfareGameItemMgr m_containerMgr;

		// Token: 0x04006C01 RID: 27649
		private List<Vector3> m_listUnitPos = new List<Vector3>();

		// Token: 0x04006C02 RID: 27650
		private List<GameObject> m_listDivePoint = new List<GameObject>();

		// Token: 0x04006C03 RID: 27651
		private List<GameObject> m_listAssultPoint = new List<GameObject>();

		// Token: 0x04006C04 RID: 27652
		private List<GameObject> m_listRecoveryPoint = new List<GameObject>();

		// Token: 0x04006C05 RID: 27653
		private List<NKCWarfareGameAssist> m_listAssist = new List<NKCWarfareGameAssist>();

		// Token: 0x04006C06 RID: 27654
		private string m_WarfareStrID = "";

		// Token: 0x04006C07 RID: 27655
		private const int DEFAULT_TILE_COUNT = 70;

		// Token: 0x04006C08 RID: 27656
		private int m_LastClickedSpawnPoint = -1;

		// Token: 0x04006C09 RID: 27657
		private NKM_WARFARE_SPAWN_POINT_TYPE m_Last_WARFARE_SPAWN_POINT_TYPE = NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE;

		// Token: 0x04006C0A RID: 27658
		private int m_UserUnitMaxCount = -1;

		// Token: 0x04006C0B RID: 27659
		private int m_LastClickedUnitTileIndex = -1;

		// Token: 0x04006C0C RID: 27660
		private const int DEFAULT_CAMERA_Z_DIST = -798;

		// Token: 0x04006C0D RID: 27661
		private NKMWarfareClearData m_NKMWarfareClearData;

		// Token: 0x04006C0E RID: 27662
		private NKMDeckIndex m_TeamAFlagDeckIndex;

		// Token: 0x04006C0F RID: 27663
		private float m_fElapsedTimeToBattle;

		// Token: 0x04006C10 RID: 27664
		private bool m_bReservedBattle;

		// Token: 0x04006C11 RID: 27665
		private const float ZOOM_IN_TIME_FOR_BATTLE = 0.6f;

		// Token: 0x04006C12 RID: 27666
		public const float MOVE_TIME = 0.9f;

		// Token: 0x04006C13 RID: 27667
		private Coroutine m_CamTrackMovingUnitCoroutine;

		// Token: 0x04006C14 RID: 27668
		private bool m_bRunningCamTrackMovingUnit;

		// Token: 0x04006C15 RID: 27669
		private const float X_OFFSET_FOR_BATTLE = 100f;

		// Token: 0x04006C16 RID: 27670
		private bool m_bReservedShowBattleResult;

		// Token: 0x04006C17 RID: 27671
		private Sequence m_UnitDieSequence;

		// Token: 0x04006C18 RID: 27672
		private NKCWarfareGameUnit m_WarfareGameUnitToDie;

		// Token: 0x04006C19 RID: 27673
		private int m_RefPause;

		// Token: 0x04006C1A RID: 27674
		private bool m_bWaitEnemyTurn;

		// Token: 0x04006C1B RID: 27675
		private float m_fElapsedTimeForEnemyTurn;

		// Token: 0x04006C1C RID: 27676
		private const float ENEMY_TURN_WAIT_TIME = 1f;

		// Token: 0x04006C1D RID: 27677
		private Tweener m_twUnitInfoDie;

		// Token: 0x04006C1E RID: 27678
		private NKMWarfareMapTemplet m_NKMWarfareMapTemplet;

		// Token: 0x04006C1F RID: 27679
		private Rect m_rtCamBound;

		// Token: 0x04006C20 RID: 27680
		private const float CAM_Y_OFFSET_FOR_UNIT = -250f;

		// Token: 0x04006C21 RID: 27681
		private bool m_bReservedCallOnUnitShakeEnd;

		// Token: 0x04006C22 RID: 27682
		private const float UNIT_SHAKE_TIME = 2f;

		// Token: 0x04006C23 RID: 27683
		private const float UNIT_FADE_OUT_TIME = 0.6f;

		// Token: 0x04006C24 RID: 27684
		private float m_fElapsedTimeForShakeEnd;

		// Token: 0x04006C25 RID: 27685
		private bool m_bReservedCallNextOrderREQ;

		// Token: 0x04006C26 RID: 27686
		private int m_LastEnterUnitUID;

		// Token: 0x04006C27 RID: 27687
		private bool m_bOpenDeckView;

		// Token: 0x04006C28 RID: 27688
		private bool m_bWaitingNextOreder;

		// Token: 0x04006C29 RID: 27689
		public bool WaitAutoPacket;

		// Token: 0x04006C2A RID: 27690
		private NKCWarfareGame.DataBeforeBattle m_DataBeforeBattle;

		// Token: 0x04006C2B RID: 27691
		private NKCWarfareGame.RetryData m_RetryData;

		// Token: 0x04006C2C RID: 27692
		private List<NKCAssetInstanceData> m_listAssetInstance = new List<NKCAssetInstanceData>();

		// Token: 0x04006C2D RID: 27693
		private readonly Vector3 BATCH_EFFECT_POS = new Vector3(-500f, 1000f, -1000f);

		// Token: 0x04006C2E RID: 27694
		private const float BATCH_EFFECT_TIME = 1.5f;

		// Token: 0x04006C2F RID: 27695
		private NKCPopupWarfareSelectShip m_NKCPopupWarfareSelectShip;

		// Token: 0x04006C30 RID: 27696
		private bool m_bFirstOpenDeck = true;

		// Token: 0x04006C31 RID: 27697
		private NKCUIDeckViewer.DeckViewerMode m_lastDeckView;

		// Token: 0x04006C32 RID: 27698
		private bool m_bSelectRecovery;

		// Token: 0x04006C33 RID: 27699
		private bool m_bPlayingIntro;

		// Token: 0x0200188B RID: 6283
		public class DataBeforeBattle
		{
			// Token: 0x0600B631 RID: 46641 RVA: 0x00366218 File Offset: 0x00364418
			public DataBeforeBattle(WarfareGameData gameData, WarfareSyncData syncData)
			{
				if (syncData.tiles != null)
				{
					for (int i = 0; i < syncData.tiles.Count; i++)
					{
						WarfareTileData tileData = gameData.GetTileData((int)syncData.tiles[i].index);
						if (tileData != null)
						{
							WarfareTileData warfareTileData = new WarfareTileData();
							warfareTileData.index = tileData.index;
							warfareTileData.tileType = tileData.tileType;
							warfareTileData.battleConditionId = tileData.battleConditionId;
							this.PrevTiles.Add(warfareTileData);
						}
					}
				}
				if (syncData.gameState != null)
				{
					this.PrevContainerCount = (int)gameData.containerCount;
				}
			}

			// Token: 0x0400A930 RID: 43312
			public int PrevContainerCount;

			// Token: 0x0400A931 RID: 43313
			public List<WarfareTileData> PrevTiles = new List<WarfareTileData>();
		}

		// Token: 0x0200188C RID: 6284
		public class RetryData
		{
			// Token: 0x0600B632 RID: 46642 RVA: 0x003662BC File Offset: 0x003644BC
			public RetryData(string warfareStrID, WarfareTeamData teamA)
			{
				this.WarfareStrID = warfareStrID;
				this.UnitList = new List<NKCWarfareGame.RetryData.UnitData>();
				foreach (WarfareUnitData warfareUnitData in teamA.warfareUnitDataByUIDMap.Values)
				{
					if (warfareUnitData.friendCode == 0L)
					{
						NKCWarfareGame.RetryData.UnitData item = new NKCWarfareGame.RetryData.UnitData
						{
							DeckIndex = (int)warfareUnitData.deckIndex.m_iIndex,
							TileIndex = (int)warfareUnitData.tileIndex
						};
						this.UnitList.Add(item);
						if (teamA.flagShipWarfareUnitUID == warfareUnitData.warfareGameUnitUID)
						{
							this.FlagShipDeckIndex = (int)warfareUnitData.deckIndex.m_iIndex;
						}
					}
				}
			}

			// Token: 0x0400A932 RID: 43314
			public string WarfareStrID;

			// Token: 0x0400A933 RID: 43315
			public int FlagShipDeckIndex;

			// Token: 0x0400A934 RID: 43316
			public List<NKCWarfareGame.RetryData.UnitData> UnitList;

			// Token: 0x02001A8F RID: 6799
			public class UnitData
			{
				// Token: 0x0400AE9C RID: 44700
				public int DeckIndex;

				// Token: 0x0400AE9D RID: 44701
				public int TileIndex;
			}
		}
	}
}
