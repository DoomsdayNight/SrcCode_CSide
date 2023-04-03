using System;
using System.Collections.Generic;
using DG.Tweening;
using NKC.UI;
using NKC.UI.Guide;
using NKC.UI.Warfare;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007A3 RID: 1955
	public class NKCDiveGameHUD : MonoBehaviour
	{
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x00170748 File Offset: 0x0016E948
		private NKCPopupWarfareSelectShip NKCPopupWarfareSelectShip
		{
			get
			{
				if (this.m_NKCPopupWarfareSelectShip == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupWarfareSelectShip>("AB_UI_NKM_UI_POPUP_WARFARE_SELECT", "NKM_UI_POPUP_WARFARE_SELECT_SHIP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupWarfareSelectShip = loadedUIData.GetInstance<NKCPopupWarfareSelectShip>();
					this.m_NKCPopupWarfareSelectShip.InitUI();
				}
				return this.m_NKCPopupWarfareSelectShip;
			}
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x00170798 File Offset: 0x0016E998
		public static NKCDiveGameHUD InitUI(NKCDiveGame cNKCDiveGame)
		{
			NKCDiveGameHUD nkcdiveGameHUD = NKCUIManager.OpenUI<NKCDiveGameHUD>("NKM_UI_DIVE_PROCESS_2D");
			nkcdiveGameHUD.m_NKM_UI_DIVE_PAUSE_BUTTON.PointerClick.RemoveAllListeners();
			nkcdiveGameHUD.m_NKM_UI_DIVE_PAUSE_BUTTON.PointerClick.AddListener(new UnityAction(cNKCDiveGame.OnBackButton));
			nkcdiveGameHUD.m_NKM_UI_DIVE_AUTO.OnValueChanged.RemoveAllListeners();
			nkcdiveGameHUD.m_NKM_UI_DIVE_AUTO.OnValueChanged.AddListener(new UnityAction<bool>(cNKCDiveGame.OnChangedAuto));
			nkcdiveGameHUD.m_NKM_UI_DIVE_SQAUD_INFO.OnValueChanged.RemoveAllListeners();
			nkcdiveGameHUD.m_NKM_UI_DIVE_SQAUD_INFO.OnValueChanged.AddListener(new UnityAction<bool>(nkcdiveGameHUD.OnChangedSquadList));
			nkcdiveGameHUD.m_NKM_UI_DIVE_PROCESS_RESEARCH_BUTTON.PointerClick.RemoveAllListeners();
			nkcdiveGameHUD.m_NKM_UI_DIVE_PROCESS_RESEARCH_BUTTON.PointerClick.AddListener(new UnityAction(cNKCDiveGame.OnClickSectorInfoSearch));
			nkcdiveGameHUD.m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON.PointerClick.RemoveAllListeners();
			nkcdiveGameHUD.m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON.PointerClick.AddListener(new UnityAction(cNKCDiveGame.OnClickBattle));
			nkcdiveGameHUD.m_NKM_UI_DIVE_HELP_BUTTON.PointerClick.RemoveAllListeners();
			nkcdiveGameHUD.m_NKM_UI_DIVE_HELP_BUTTON.PointerClick.AddListener(new UnityAction(nkcdiveGameHUD.OnClickHelpBtn));
			NKCUtil.SetGameobjectActive(nkcdiveGameHUD.m_NKM_UI_DIVE_SQAUD_INFO, false);
			NKCUtil.SetGameobjectActive(nkcdiveGameHUD.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SCROLL, true);
			nkcdiveGameHUD.SetDeckViewUnitSlotCount(8);
			NKCUtil.SetGameobjectActive(nkcdiveGameHUD.gameObject, false);
			nkcdiveGameHUD.m_NKCDiveGameHUDArtifact.InitUI(new NKCDiveGameHUDArtifact.dOnFinishScrollToArtifactDummySlot(cNKCDiveGame.OnFinishScrollToArtifactDummySlot));
			NKCUtil.SetButtonClickDelegate(nkcdiveGameHUD.m_csbtnEnemyList, new UnityAction(nkcdiveGameHUD.OnClickEnemyList));
			return nkcdiveGameHUD;
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x00170914 File Offset: 0x0016EB14
		private void Update()
		{
			if (this.m_fPrevUpdateTime < Time.time + 1f)
			{
				this.m_fPrevUpdateTime = Time.time;
				NKMDiveGameData diveGameData = this.GetDiveGameData();
				if (diveGameData != null)
				{
					DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
					TimeSpan timeSpan = new DateTime(diveGameData.Floor.ExpireDate) - serverUTCTime;
					this.m_NKM_UI_DIVE_PAUSE_GAME_TIME_Text.text = NKCUtilString.GetTimeSpanString(timeSpan);
				}
				return;
			}
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x00170983 File Offset: 0x0016EB83
		private void OnClickHelpBtn()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_DIVE_SEARCH", 0);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x00170995 File Offset: 0x0016EB95
		public void PlayIntro()
		{
			if (this.m_NKM_UI_DIVE_INTRO_FX != null)
			{
				this.m_NKM_UI_DIVE_INTRO_FX.Play("NKM_UI_DIVE_INTRO_FX_BASE");
			}
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x001709B8 File Offset: 0x0016EBB8
		private void SetDeckViewUnitSlotCount(int count)
		{
			while (this.m_lstDeckViewUnitSlot.Count < count)
			{
				NKCDeckViewUnitSlot newInstance = NKCDeckViewUnitSlot.GetNewInstance(this.m_NKM_UI_DIVE_SQUAD_UNIT_GRID.transform);
				newInstance.Init(this.m_lstDeckViewUnitSlot.Count, false);
				newInstance.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_lstDeckViewUnitSlot.Add(newInstance);
				NKCUtil.SetGameobjectActive(newInstance, true);
			}
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x00170A2F File Offset: 0x0016EC2F
		public void OpenSquadList()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SCROLL, true);
			this.m_NKM_UI_DIVE_SQAUD_INFO.Select(true, true, false);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x00170A4C File Offset: 0x0016EC4C
		private void OnChangedSquadList(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SCROLL, bSet);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x00170A5A File Offset: 0x0016EC5A
		public NKMDeckIndex GetLastSelectedDeckIndex()
		{
			return this.m_LastSelectedDeckIndex;
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x00170A64 File Offset: 0x0016EC64
		public void SetSelectedSquadSlot(int deckIndexToSelect)
		{
			for (int i = 0; i < this.m_lstNKCDiveGameSquadSlot.Count; i++)
			{
				if (!(this.m_lstNKCDiveGameSquadSlot[i] == null))
				{
					if (this.m_lstNKCDiveGameSquadSlot[i].GetDeckIndex() == deckIndexToSelect)
					{
						this.m_lstNKCDiveGameSquadSlot[i].SetSelected(true);
					}
					else
					{
						this.m_lstNKCDiveGameSquadSlot[i].SetSelected(false);
					}
				}
			}
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x00170AD8 File Offset: 0x0016ECD8
		public void OpenSquadView(int deckIndex)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			this.SetSelectedSquadSlot(deckIndex);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_VIEW, true);
			NKCCamera.EnableBlur(true, 2f, 2);
			NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, deckIndex);
			NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(nkmdeckIndex);
			if (deckData != null)
			{
				this.m_LastSelectedDeckIndex = nkmdeckIndex;
				for (int i = 0; i < this.m_lstDeckViewUnitSlot.Count; i++)
				{
					if (i >= 0 && i < 8)
					{
						this.m_lstDeckViewUnitSlot[i].SetData(myUserData.m_ArmyData.GetUnitFromUID(deckData.m_listDeckUnitUID[i]), false);
						if (i == (int)deckData.m_LeaderIndex)
						{
							this.m_lstDeckViewUnitSlot[i].SetLeader(true, false);
						}
					}
				}
				NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
				NKMUnitTempletBase nkmunitTempletBase = null;
				if (shipFromUID != null)
				{
					nkmunitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
				}
				if (nkmunitTempletBase != null)
				{
					this.m_NKM_UI_DIVE_SQUAD_TITLE_TEXT1.text = string.Format(NKCUtilString.GET_STRING_SQUAD_ONE_PARAM, NKCUtilString.GetDeckNumberString(nkmdeckIndex));
					int num = (int)(nkmdeckIndex.m_iIndex + 1);
					this.m_NKM_UI_DIVE_SQUAD_TITLE_TEXT2.text = string.Format(NKCUtilString.GET_STRING_SQUAD_TWO_PARAM, num, NKCUtilString.GetRankNumber(num, false).ToUpper());
					Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, nkmunitTempletBase);
					Sprite sprite2 = null;
					if (sprite != null)
					{
						sprite2 = sprite;
					}
					if (sprite2 == null)
					{
						NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
						if (assetResourceUnitInvenIconEmpty != null)
						{
							this.m_ANIM_SHIP_IMG.sprite = assetResourceUnitInvenIconEmpty.GetAsset<Sprite>();
						}
						else
						{
							this.m_ANIM_SHIP_IMG.sprite = null;
						}
					}
					else
					{
						this.m_ANIM_SHIP_IMG.sprite = sprite2;
					}
					this.m_UIShipInfo.SetShipData(shipFromUID, nkmunitTempletBase, nkmdeckIndex, false);
				}
				if (!NKCOperatorUtil.IsHide())
				{
					if (deckData.m_OperatorUID != 0L)
					{
						this.m_NKM_UI_OPERATOR_DECK_SLOT.SetData(NKCOperatorUtil.GetOperatorData(deckData.m_OperatorUID), false);
					}
					else
					{
						this.m_NKM_UI_OPERATOR_DECK_SLOT.SetEmpty();
					}
				}
				NKCUtil.SetGameobjectActive(this.m_OPERATOR_INFO, !NKCOperatorUtil.IsHide());
			}
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00170CCE File Offset: 0x0016EECE
		public bool IsOpenSquadView()
		{
			return this.m_NKM_UI_DIVE_PROCESS_SQUAD_VIEW.activeSelf;
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x00170CDB File Offset: 0x0016EEDB
		public void CloseSquadView()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_VIEW, false);
			NKCCamera.EnableBlur(false, 2f, 2);
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x00170CF8 File Offset: 0x0016EEF8
		public void UpdateSectorInfoUI(NKMDiveSlot cNKMDiveSlot, bool bSameCol)
		{
			if (!this.m_NKM_UI_DIVE_PROCESS_INFO.activeSelf)
			{
				return;
			}
			if (NKCDiveManager.IsReimannSectorType(cNKMDiveSlot.SectorType))
			{
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SECTOR_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_SECTOR_REIMANN", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_MAP_REIMANN_B", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_TITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SECTOR_TITLE_BATTLE", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_DESC.text = "";
			}
			else if (NKCDiveManager.IsBossSectorType(cNKMDiveSlot.SectorType))
			{
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SECTOR_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_SECTOR_BOSS", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_BOSS", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_TITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SECTOR_TITLE_CORE", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_DESC.text = "";
			}
			else if (NKCDiveManager.IsPoincareSectorType(cNKMDiveSlot.SectorType))
			{
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SECTOR_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_SECTOR_POINCARE", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_MAP_POINCARE_A", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_TITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SECTOR_TITLE_BATTLE", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_DESC.text = "";
			}
			else if (NKCDiveManager.IsGauntletSectorType(cNKMDiveSlot.SectorType))
			{
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SECTOR_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_SECTOR_GAUNTLET", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_MAP_GAUNTLET_C", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_TITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SECTOR_TITLE_BATTLE", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_DESC.text = "";
			}
			else if (NKCDiveManager.IsEuclidSectorType(cNKMDiveSlot.SectorType))
			{
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SECTOR_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_SECTOR_EUCLID", false);
				NKM_DIVE_EVENT_TYPE eventType = cNKMDiveSlot.EventType;
				if (eventType != NKM_DIVE_EVENT_TYPE.NDET_REPAIR)
				{
					if (eventType == NKM_DIVE_EVENT_TYPE.NDET_ARTIFACT)
					{
						this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_MAP_ARTIFACT", false);
					}
					else
					{
						this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_EUCLID", false);
					}
				}
				else
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_ICON.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_SPRITE", "AB_UI_DIVE_ICON_SECTOR_MAP_EUCLID_REPAIR", false);
				}
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_TITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SECTOR_TITLE_SAFE", false);
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_DESC.text = "";
			}
			if (!NKCDiveManager.IsEuclidSectorType(cNKMDiveSlot.SectorType))
			{
				if (this.GetDiveGameData() != null && this.GetDiveGameData().Floor != null && this.GetDiveGameData().Floor.Templet != null)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_TEXT_DECO, true);
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_TEXT.text = string.Format(NKCUtilString.GET_STRING_DIVE_FLOOR_LEVEL_ONE_PARAM, this.GetDiveGameData().Floor.Templet.StageLevel);
				}
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_DUNGEON", false);
			}
			else
			{
				if (NKCDiveManager.IsItemEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_ITEM_SIGNAL", false);
				}
				else if (NKCDiveManager.IsLostContainerEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_SUPPLY", false);
				}
				else if (NKCDiveManager.IsRandomEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_NO_SIGNAL", false);
				}
				else if (NKCDiveManager.IsRescueSignalEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_HELP_SIGNAL", false);
				}
				else if (NKCDiveManager.IsLostShipEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_LOSTSHIP", false);
				}
				else if (NKCDiveManager.IsSafetyEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_SAFE", false);
				}
				else if (NKCDiveManager.IsRepairKitEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DIVE_EVENT_SUBTITLE_REPAIR_KIT", false);
				}
				else if (NKCDiveManager.IsArtifactEventType(cNKMDiveSlot.EventType))
				{
					this.m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE.text = NKCStringTable.GetString("SI_DP_DIVE_EVENT_TEXT_NDET_BLANK_TITLE", false);
				}
				this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_TEXT.text = "";
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_TEXT_DECO, false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SECTOR_BIGSECTOR_TEXT, NKCDiveManager.IsSectorHardType(cNKMDiveSlot.SectorType));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_RESEARCH_BUTTON, !bSameCol);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON, bSameCol);
			if (bSameCol)
			{
				this.m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON_CG.alpha = 1f;
			}
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x001711A0 File Offset: 0x0016F3A0
		public bool OpenSectorInfo(NKMDiveSlot cNKMDiveSlot, bool bSameCol)
		{
			if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_NONE)
			{
				return false;
			}
			bool flag = false;
			if (!this.m_NKM_UI_DIVE_PROCESS_INFO.activeSelf)
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_INFO, true);
			if (flag)
			{
				this.m_NKM_UI_DIVE_PROCESS_INFO_CG.alpha = 0f;
				this.m_NKM_UI_DIVE_PROCESS_INFO_CG.DOFade(1f, 0.6f);
			}
			else
			{
				this.m_NKM_UI_DIVE_PROCESS_INFO_CG.DOKill(false);
				this.m_NKM_UI_DIVE_PROCESS_INFO_CG.alpha = 1f;
			}
			this.UpdateSectorInfoUI(cNKMDiveSlot, bSameCol);
			this.m_dungeonId = 0;
			if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_DUNGEON || cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS)
			{
				this.m_dungeonId = cNKMDiveSlot.EventValue;
			}
			this.m_isBossSector = NKCDiveManager.IsBossSectorType(cNKMDiveSlot.SectorType);
			NKCUtil.SetGameobjectActive(this.m_csbtnEnemyList, this.m_dungeonId > 0);
			return true;
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0017126F File Offset: 0x0016F46F
		public void SetSectorInfoBottomButtonToBattle()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_RESEARCH_BUTTON, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON, true);
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x00171289 File Offset: 0x0016F489
		public void CloseSectorInfo()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_INFO, false);
			this.m_dungeonId = 0;
			this.m_isBossSector = false;
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x001712A5 File Offset: 0x0016F4A5
		private NKMDiveGameData GetDiveGameData()
		{
			return NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x001712B8 File Offset: 0x0016F4B8
		private void OnClickSquadSlot(NKMDiveSquad cNKMDiveSquad)
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			if (diveGameData.Player.PlayerBase.State != NKMDivePlayerState.BattleReady)
			{
				this.NKCPopupWarfareSelectShip.OpenForMyShipInDive(new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, cNKMDiveSquad.DeckIndex));
				return;
			}
			if (cNKMDiveSquad.CurHp <= 0f)
			{
				return;
			}
			this.OpenSquadView(cNKMDiveSquad.DeckIndex);
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x00171318 File Offset: 0x0016F518
		public void UpdateSquadListUI()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			if (diveGameData.Player == null)
			{
				return;
			}
			if (diveGameData.Player.Squads == null)
			{
				return;
			}
			if (diveGameData.Player.Squads.Count > this.m_lstNKCDiveGameSquadSlot.Count)
			{
				int num = diveGameData.Player.Squads.Count - this.m_lstNKCDiveGameSquadSlot.Count;
				for (int i = 0; i < num; i++)
				{
					NKCDiveGameSquadSlot newInstance = NKCDiveGameSquadSlot.GetNewInstance(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_CONTENT.transform, new NKCDiveGameSquadSlot.OnClickSquadSlot(this.OnClickSquadSlot));
					this.m_lstNKCDiveGameSquadSlot.Add(newInstance);
				}
			}
			List<NKMDiveSquad> list = new List<NKMDiveSquad>(diveGameData.Player.Squads.Values);
			for (int i = 0; i < this.m_lstNKCDiveGameSquadSlot.Count; i++)
			{
				if (i < diveGameData.Player.Squads.Count)
				{
					this.m_lstNKCDiveGameSquadSlot[i].SetUI(list[i]);
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSquadSlot[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSquadSlot[i], false);
				}
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x00171438 File Offset: 0x0016F638
		public void UpdateExploreCountLeftUI()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData != null && diveGameData.Floor.Templet != null)
			{
				this.SetLeftExploreCount(diveGameData.Floor.Templet.RandomSetCount + 1 - diveGameData.Player.PlayerBase.Distance);
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x00171485 File Offset: 0x0016F685
		private void SetSquadListBG(bool bHurdle = false)
		{
			NKCUtil.SetGameobjectActive(this.m_BG_NORMAL, !bHurdle);
			NKCUtil.SetGameobjectActive(this.m_BG_HURDLE, bHurdle);
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x001714A4 File Offset: 0x0016F6A4
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.CloseSectorInfo();
			this.CloseSquadView();
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData != null)
			{
				if (diveGameData.Floor.Templet != null)
				{
					this.SetTitle(diveGameData.Floor.Templet.Get_STAGE_NAME());
					this.SetSubTitle(diveGameData.Floor.Templet.Get_STAGE_NAME_SUB());
					this.SetSquadListBG(diveGameData.Floor.Templet.StageType == NKM_DIVE_STAGE_TYPE.NDST_HARD);
				}
				this.UpdateSquadListUI();
			}
			this.UpdateExploreCountLeftUI();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKMUserOption userOption = myUserData.m_UserOption;
				if (userOption != null)
				{
					this.m_NKM_UI_DIVE_AUTO.Select(userOption.m_bAutoDive, true, false);
				}
			}
			this.m_NKCDiveGameHUDArtifact.ResetUI(diveGameData.Floor.Templet.StageType == NKM_DIVE_STAGE_TYPE.NDST_HARD);
			this.m_NKCDiveGameHUDArtifact.Close(false);
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x00171587 File Offset: 0x0016F787
		public void SetTitle(string str)
		{
			if (this.m_NKM_UI_DIVE_TITLE_TEXT != null)
			{
				this.m_NKM_UI_DIVE_TITLE_TEXT.text = str;
			}
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x001715A3 File Offset: 0x0016F7A3
		public void SetSubTitle(string str)
		{
			if (this.m_NKM_UI_DIVE_SUBTITLE_TEXT_CONTENT != null)
			{
				this.m_NKM_UI_DIVE_SUBTITLE_TEXT_CONTENT.text = str;
			}
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x001715BF File Offset: 0x0016F7BF
		public void SetLeftExploreCount(int leftCount)
		{
			if (this.m_NKM_UI_DIVE_LEFT_COUNT != null)
			{
				this.m_NKM_UI_DIVE_LEFT_COUNT.text = string.Format(NKCUtilString.GET_STRING_DIVE_LEFT_COUNT_ONE_PARAM, leftCount);
			}
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x001715EC File Offset: 0x0016F7EC
		private void OnClickEnemyList()
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_dungeonId);
			if (dungeonTempletBase == null)
			{
				return;
			}
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			NKCPopupEnemyList instance = NKCPopupEnemyList.Instance;
			NKMDungeonTempletBase cNKMDungeonTempletBase = dungeonTempletBase;
			NKMDiveTemplet diveTemplet;
			if (diveGameData == null)
			{
				diveTemplet = null;
			}
			else
			{
				NKMDiveFloor floor = diveGameData.Floor;
				diveTemplet = ((floor != null) ? floor.Templet : null);
			}
			instance.Open(cNKMDungeonTempletBase, diveTemplet, this.m_isBossSector);
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0017163C File Offset: 0x0016F83C
		public void Close()
		{
			this.CloseSquadView();
			this.m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON_CG.DOKill(false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_NKCPopupWarfareSelectShip != null && this.m_NKCPopupWarfareSelectShip.IsOpen)
			{
				this.m_NKCPopupWarfareSelectShip.Close();
			}
			this.m_NKCPopupWarfareSelectShip = null;
		}

		// Token: 0x04003C97 RID: 15511
		[Header("상단바")]
		public NKCUIComButton m_NKM_UI_DIVE_PAUSE_BUTTON;

		// Token: 0x04003C98 RID: 15512
		public Text m_NKM_UI_DIVE_TITLE_TEXT;

		// Token: 0x04003C99 RID: 15513
		public Text m_NKM_UI_DIVE_SUBTITLE_TEXT_CONTENT;

		// Token: 0x04003C9A RID: 15514
		public Text m_NKM_UI_DIVE_LEFT_COUNT;

		// Token: 0x04003C9B RID: 15515
		public NKCUIComToggle m_NKM_UI_DIVE_AUTO;

		// Token: 0x04003C9C RID: 15516
		public Text m_NKM_UI_DIVE_PAUSE_GAME_TIME_Text;

		// Token: 0x04003C9D RID: 15517
		public NKCUIComStateButton m_NKM_UI_DIVE_HELP_BUTTON;

		// Token: 0x04003C9E RID: 15518
		private float m_fPrevUpdateTime;

		// Token: 0x04003C9F RID: 15519
		[Header("좌측 부대정보 리스트")]
		public NKCUIComToggle m_NKM_UI_DIVE_SQAUD_INFO;

		// Token: 0x04003CA0 RID: 15520
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SCROLL;

		// Token: 0x04003CA1 RID: 15521
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_CONTENT;

		// Token: 0x04003CA2 RID: 15522
		public GameObject m_BG_NORMAL;

		// Token: 0x04003CA3 RID: 15523
		public GameObject m_BG_HURDLE;

		// Token: 0x04003CA4 RID: 15524
		private List<NKCDiveGameSquadSlot> m_lstNKCDiveGameSquadSlot = new List<NKCDiveGameSquadSlot>();

		// Token: 0x04003CA5 RID: 15525
		[Header("가운데 부대정보")]
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_VIEW;

		// Token: 0x04003CA6 RID: 15526
		public GameObject m_NKM_UI_DIVE_SQUAD_UNIT_GRID;

		// Token: 0x04003CA7 RID: 15527
		public NKCUIShipInfoSummary m_UIShipInfo;

		// Token: 0x04003CA8 RID: 15528
		public Image m_ANIM_SHIP_IMG;

		// Token: 0x04003CA9 RID: 15529
		public Text m_NKM_UI_DIVE_SQUAD_TITLE_TEXT1;

		// Token: 0x04003CAA RID: 15530
		public Text m_NKM_UI_DIVE_SQUAD_TITLE_TEXT2;

		// Token: 0x04003CAB RID: 15531
		private List<NKCDeckViewUnitSlot> m_lstDeckViewUnitSlot = new List<NKCDeckViewUnitSlot>();

		// Token: 0x04003CAC RID: 15532
		private NKMDeckIndex m_LastSelectedDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, 0);

		// Token: 0x04003CAD RID: 15533
		[Header("우측 섹터 정보")]
		public GameObject m_NKM_UI_DIVE_PROCESS_INFO;

		// Token: 0x04003CAE RID: 15534
		public CanvasGroup m_NKM_UI_DIVE_PROCESS_INFO_CG;

		// Token: 0x04003CAF RID: 15535
		public RawImage m_NKM_UI_DIVE_PROCESS_SECTOR_THUMBNAIL;

		// Token: 0x04003CB0 RID: 15536
		public Image m_NKM_UI_DIVE_PROCESS_SECTOR_ICON;

		// Token: 0x04003CB1 RID: 15537
		public Text m_NKM_UI_DIVE_PROCESS_SECTOR_TITLE;

		// Token: 0x04003CB2 RID: 15538
		public Text m_NKM_UI_DIVE_PROCESS_SECTOR_SUBTITLE;

		// Token: 0x04003CB3 RID: 15539
		public GameObject m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_TEXT_DECO;

		// Token: 0x04003CB4 RID: 15540
		public Text m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_TEXT;

		// Token: 0x04003CB5 RID: 15541
		public Text m_NKM_UI_DIVE_PROCESS_SECTOR_INFO_DESC;

		// Token: 0x04003CB6 RID: 15542
		public GameObject m_NKM_UI_DIVE_PROCESS_SECTOR_BIGSECTOR_TEXT;

		// Token: 0x04003CB7 RID: 15543
		public NKCUIComStateButton m_NKM_UI_DIVE_PROCESS_RESEARCH_BUTTON;

		// Token: 0x04003CB8 RID: 15544
		public NKCUIComStateButton m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON;

		// Token: 0x04003CB9 RID: 15545
		public CanvasGroup m_NKM_UI_DIVE_PROCESS_BATTLE_BUTTON_CG;

		// Token: 0x04003CBA RID: 15546
		[Header("아티팩트")]
		public NKCDiveGameHUDArtifact m_NKCDiveGameHUDArtifact;

		// Token: 0x04003CBB RID: 15547
		[Header("Etc")]
		public Animator m_NKM_UI_DIVE_INTRO_FX;

		// Token: 0x04003CBC RID: 15548
		public NKCUIComStateButton m_csbtnEnemyList;

		// Token: 0x04003CBD RID: 15549
		[Header("오퍼레이터")]
		public GameObject m_OPERATOR_INFO;

		// Token: 0x04003CBE RID: 15550
		public NKCUIOperatorDeckSlot m_NKM_UI_OPERATOR_DECK_SLOT;

		// Token: 0x04003CBF RID: 15551
		private NKCPopupWarfareSelectShip m_NKCPopupWarfareSelectShip;

		// Token: 0x04003CC0 RID: 15552
		private int m_dungeonId;

		// Token: 0x04003CC1 RID: 15553
		private bool m_isBossSector;
	}
}
